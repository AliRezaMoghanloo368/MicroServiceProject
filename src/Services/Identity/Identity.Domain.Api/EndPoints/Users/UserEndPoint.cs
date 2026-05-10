using Identity.Domain.Application.Services.Authenticate.Implement;
using Identity.Domain.Application.Services.Authenticate.Interfaces;
using Identity.Domain.Core.Common.SeedWork.Interfaces;
using SharedLibrary.Patterns.ResultPattern;

namespace Identity.Domain.Api.EndPoints.Users
{
    public static class UserEndPoint
    {
        public static void MapUser(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (IJwtHandler _jwtHandler, IGenericRepository<Core.AggregateModels.UserItems.UserEntity> _userRepository) =>
            {
                //var user = await _userRepository.GetByIdAsync(loginDto.B);

                //if (user == null)
                //    return Result<string>.ErrorResult("Error", "کاربری با این نام یافت نشد.");

                //if (user.E == true)
                //{
                //    return Result<string>.ErrorResult("Error", "اين كاربر غيرفعال است.");
                //}

                //if (user.G != true)
                //{
                //    if (loginDto.FiscalName != string.Empty)
                //    {
                //        if (loginDto.FiscalId != null)
                //        {

                //            if (!(await _userService.IsAccessToFiscalAsync(user.A, loginDto.FiscalId)))
                //            {
                //                return Result<string>.ErrorResult("Error", "شما امكان دسترسی به اين دفتر را نداريد.");
                //            }
                //        }
                //    }
                //}

                //var token = _jwtHandler.Create(user.A, loginDto.FiscalName);
                //return Result<string>.SuccessResult(token.Token, "");
            });
        }
    }
}
