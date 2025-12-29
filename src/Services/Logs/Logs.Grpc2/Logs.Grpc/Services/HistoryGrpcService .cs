using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Logs.Core.Contracts.Persistence;
using Logs.Domain.Models;
using Logs.Grpc.Protos;

namespace Logs.Grpc.Services
{
    public class HistoryGrpcService : HistoryService.HistoryServiceBase
    {
        private readonly ILogger<HistoryGrpcService> _logger;
        private readonly IHistoryRepository _repo;
        private readonly IMapper _mapper;
        public HistoryGrpcService(IHistoryRepository repo, ILogger<HistoryGrpcService> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<GetHistoriesResponse> GetHistories(GetHistoriesRequest request, ServerCallContext context)
        {
            var list = await _repo.GetHistoriesAsync(request.UserName, request.Section, request.RecordId);
            var response = new GetHistoriesResponse();

            if (list is null)
            {
                _logger.LogError($"History from grpc is null");
            }
            else
            {
                response.Histories.AddRange(
                    _mapper.Map<IEnumerable<HistoryModel>>(list)
                );
            }

            return response;
        }

        public override async Task<HistoryModel> GetHistory(GetHistoryRequest request, ServerCallContext context)
        {
            var entity = await _repo.GetHistoryAsync(request.Id);

            if (entity is null)
            {
                _logger.LogError($"History from grpc is not found");
                throw new RpcException(new Status(StatusCode.NotFound, "History not found"));
            }
            return _mapper.Map<HistoryModel>(entity);
        }

        public override async Task<HistoryModel> CreateHistory(CreateHistoryRequest request, ServerCallContext context)
        {
            try
            {
                var entity = _mapper.Map<History>(request);
                var result = await _repo.CreateHistoryAsync(entity);
                return _mapper.Map<HistoryModel>(result);
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex, "Error in CreateHistory");
                throw new Exception(ex.Message);
            }

        }
    }
}
