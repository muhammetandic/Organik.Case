using Organik.Case.Domain.Entities;

namespace Organik.Case.Application.Interfaces.Utils
{
	public interface IJwtUtilsService
	{
        string GenerateJwtToken(User user);

    }
}

