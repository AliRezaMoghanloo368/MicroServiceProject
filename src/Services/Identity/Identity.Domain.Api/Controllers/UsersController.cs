using Identity.Domain.Application.Dtos.User;
using Identity.Domain.Application.Dtos.User.FluentValidations;
using Identity.Domain.Application.Services.Authenticate.Interfaces;
using Identity.Domain.Core.AggregateModels.UserItems;
using Identity.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Encryptor;
using SharedLibrary.Patterns.ResultPattern;

namespace Identity.Domain.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class UsersController : GenericController
    {
        private readonly IUserRepository _repository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IEncryptor _encryptor;
        public UsersController(IUserRepository repository, IJwtHandler jwtHandler, IEncryptor encryptor)
        {
            _repository = repository;
            _jwtHandler = jwtHandler;
            _encryptor = encryptor;
        }

        [HttpPost("name/{username}")]
        public async Task<Result<User>> GetUserName([FromRoute] string userName)
        {
            var user = await _repository.GetUserByNameAsync(userName);
            return Result<User>.SuccessResult(user);
        }

        [HttpPost("id/{userId}")]
        public async Task<Result<User>> GetUserById([FromRoute] string userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            return Result<User>.SuccessResult(user);
        }

        [HttpPost("login")]
        public async Task<Result<string>> Login(UserDto dto)
        {
            var user = await _repository.GetByIdAsync(dto.UserName);
            if (user == null)
                return Result<string>.ErrorResult("Error", "کاربری با این نام یافت نشد.");

            bool isCorrect = user.ValidatePassword(dto.Password, _encryptor);
            if (!isCorrect)
            {
                return Result<string>.ErrorResult("Error", "نام کاربری و رمز عبور اشتباه است.");
            }
            //var userPassword = dto.Password.HashPassword(user.Salt, _encryptor);
            //if (!user.Password.Equals(userPassword))
            //{
            //    return Result<string>.ErrorResult("Error", "نام کاربری و رمز عبور اشتباه است.");
            //}

            var token = _jwtHandler.Create(user);
            return Result<string>.SuccessResult(token.Token);
        }

        [HttpPost("register")]
        public async Task<Result<CreateUserDto>> Register(CreateUserDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var valid = new CreateUserDtoValidation(_repository);
                var userIsValid = await valid.ValidateAsync(dto);
                if (!userIsValid.IsValid)
                {
                    return Result<CreateUserDto>.ErrorResult(userIsValid.Errors.Select(x => x.ErrorMessage).ToList());
                }

                var fileInfo = Core.AggregateModels.UserItems.User.CreateUserInfo(dto.UserInfo.FullName,
                    dto.UserInfo.PhoneNumber, dto.UserInfo.Email);

                var user = Core.AggregateModels.UserItems.User.CreateUser(dto.UserName,
                    dto.Password, fileInfo);

                await _repository.AddAsync(user, cancellationToken);

                return Result<CreateUserDto>.SuccessResult(dto, "عملیات با موفقیت انجام شد!");

            }
            catch (Exception e)
            {
                return Result<CreateUserDto>.ErrorResult(e.Message);
            }
        }
    }
}
