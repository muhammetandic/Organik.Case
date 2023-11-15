using Organik.Case.Domain.Common;

namespace Organik.Case.Domain.Entities
{
	public class User : BaseEntity
	{
		public required string Username { get; set; }
		public required string Email { get; set; }
		public required ulong Phone { get; set; }
		public required string Password { get; set; }
		public string? Token { get; set; }
		public string? Code { get; set; }
		public DateTimeOffset TokenExpiration { get; set; }
	}
}

