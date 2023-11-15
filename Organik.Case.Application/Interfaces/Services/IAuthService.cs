
using Organik.Case.Application.Dtos;

namespace Organik.Case.Application.Interfaces.Services
{
	public interface IAuthService
	{
        Task<IResponse> RegisterAsync(RegisterRequest request);
        Task<IResponse> CheckCodeAsync(CheckCodeRequest request);
        Task<IResponse> LoginAsync(LoginRequest request);
        Task<IResponse> SendCodeAsync(SendCodeRequest request);
    }
}

