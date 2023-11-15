using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Hangfire;
using Organik.Case.Application.Dtos;
using Organik.Case.Application.Dtos.Error;
using Organik.Case.Application.Interfaces;
using Organik.Case.Application.Interfaces.ExternalServices;
using Organik.Case.Application.Interfaces.Services;
using Organik.Case.Application.Interfaces.Utils;
using Organik.Case.Domain.Entities;
using Organik.Case.Domain.Enums;

namespace Organik.Case.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IJwtUtilsService _jwtUtilsService;

        public AuthService(IMapper mapper, IUserRepository repository, IJwtUtilsService jwtUtilsService)
        {
            _mapper = mapper;
            _repository = repository;
            _jwtUtilsService = jwtUtilsService;
        }

        public async Task<IResponse> RegisterAsync(RegisterRequest request)
        {
            var user = await _repository.GetOneAsync(x => x.Username == request.Username);
            if (user != null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.DuplicateUsername);
            user = await _repository.GetOneAsync(x => x.Email == request.Email);
            if (user != null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.DuplicateEmail);
            user = await _repository.GetOneAsync(x => x.Phone == request.Phone);
            if (user != null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.DuplicatePhone);

            var newUser = _mapper.Map<User>(request);
            newUser.Password = GeneratePasswordHash(request.Password);
            newUser = await _repository.InsertAsync(newUser);

            return _mapper.Map<RegisterResponse>(newUser);
        }

        public async Task<IResponse> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetOneAsync(x => x.Username == request.Username && x.Password == GeneratePasswordHash(request.Password));
            if (user == null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.UsernameOrPasswordIncorrect);

            user.Token = GenerateControlToken();
            await _repository.UpdateAsync(user);
            return new LoginResponse(user.Id, user.Username, user.Token);
        }

        public async Task<IResponse> SendCodeAsync(SendCodeRequest request)
        {
            var user = await _repository.GetOneAsync(x=>x.Id == request.UserId && x.Token == request.Token);
            if (user == null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.UserNotFound);
            user.Code = GenerateControlCode();
            user.Token = GenerateControlToken();
            user.TokenExpiration = DateTimeOffset.Now.AddMinutes(5);

            await _repository.UpdateAsync(user);
            if (request.SendMethod == SendTypes.Sms)
                BackgroundJob.Enqueue<ISmsService>((x) => x.SendSms(user.Code, user.Phone.ToString()));
            if (request.SendMethod == SendTypes.Email)
                BackgroundJob.Enqueue<IEmailService>(x => x.SendMail(user.Email, user.Username, user.Code));

            return new SendCodeResponse(user.Token);
        }

        public async Task<IResponse> CheckCodeAsync(CheckCodeRequest request)
        {
            var user = await _repository.GetByIdAsync(request.UserId);
            if (user == null)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.UserNotFound);
            if (user.Token != request.Token || user.TokenExpiration < DateTimeOffset.Now)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.TokenInvalid);
            if (user.Code != request.Code)
                return new ErrorResponse((int)ErrorCodes.BadRequest, ErrorMessages.CodeInvalid);

            var accessToken = _jwtUtilsService.GenerateJwtToken(user);
            return new CheckCodeResponse(accessToken);
        }

        private string GenerateControlCode()
        {
            var chars = "0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray()
            );
            return result;
        }

        private string GenerateControlToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GeneratePasswordHash(string password)
        {
            using MD5 md5 = MD5.Create();
            var hashed = string.Join(string.Empty, md5.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(b => b.ToString("x2")));
            return hashed;
        }
    }
}