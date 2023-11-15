using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Organik.Case.Application.Dtos;
using Organik.Case.Application.Interfaces.Services;
using Organik.Case.Application.Validations;

namespace Organik.Case.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<SendCodeRequest> _sendCodeValidator;
        private readonly IValidator<CheckCodeRequest> _checkCodeValidator;

        public AuthController(IAuthService service, IValidator<RegisterRequest> registerValidator, IValidator<LoginRequest> loginValidator, IValidator<SendCodeRequest> sendCodeValidator, IValidator<CheckCodeRequest> checkCodeValidator)
        {
            _service = service;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _sendCodeValidator = sendCodeValidator;
            _checkCodeValidator = checkCodeValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var validationResult = await _registerValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _service.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _service.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeRequest request)
        {
            var validationResult = await _sendCodeValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _service.SendCodeAsync(request);
            return Ok(response);
        }

        [HttpPost("check-code")]
        public async Task<IActionResult> CheckCode([FromBody] CheckCodeRequest request)
        {
            var validationResult = await _checkCodeValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var response = await _service.CheckCodeAsync(request);
            return Ok(response);
        }
    }
}