using Organik.Case.Application.Interfaces;
using Organik.Case.Domain.Entities;

namespace Organik.Case.Infrastructure.Data.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext context, ICurrentUserService userService): base(context, userService)
		{
		}
	}
}

