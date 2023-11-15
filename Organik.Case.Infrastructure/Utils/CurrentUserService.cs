using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Organik.Case.Application.Interfaces;

namespace Organik.Case.Infrastructure.Utils
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

        public uint UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return 0;
                var isParsed = uint.TryParse(userId, out uint value);
                if (isParsed) return value;
                return 0;
            }
        }
    }
}

