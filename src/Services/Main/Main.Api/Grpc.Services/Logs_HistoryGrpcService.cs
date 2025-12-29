using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Logs.Grpc.Protos;

namespace Main.Api.Grpc.Services
{
    public class Logs_HistoryGrpcService
    {
        #region Constructor
        private readonly HistoryService.HistoryServiceClient _service;
        private readonly IMapper _mapper;
        public Logs_HistoryGrpcService(HistoryService.HistoryServiceClient service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #endregion

        #region Get Histories
        public async Task<GetHistoriesResponse> GetHistories(string userName, string? section, string? recordId)
        {
            var request =
                new GetHistoriesRequest { UserName = userName, Section = section, RecordId = recordId };

            return await _service.GetHistoriesAsync(request);
        }
        #endregion

        #region Create History
        public async Task CreateHistoryAsync(string section, string recordId)
        {
            try
            {
                var request = new CreateHistoryRequest
                {
                    UserId = "1",
                    UserName = "test",
                    HostName = "DESKTOP-MOGHANLOO",
                    Section = section,
                    RecordId = recordId,
                    CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow),
                    Action = "new",
                    Description = ""
                };

                _service.CreateHistory(request);
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }



            //    try
            //    {
            //        // 1️⃣ ایجاد channel به سرور
            //        using var channel = GrpcChannel.ForAddress("http://localhost:5024");

            //        // 2️⃣ ساخت client با استفاده از channel
            //        var client = new HistoryService.HistoryServiceClient(channel);

            //        // 3️⃣ ارسال درخواست
            //        var request = new CreateHistoryRequest
            //        {
            //            Section = "Student",
            //            RecordId = "123"
            //        };
            //        var response = client.CreateHistory(request);
            //    }
            //    catch (RpcException ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
        }
        #endregion
    }
}
