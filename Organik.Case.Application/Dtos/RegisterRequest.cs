namespace Organik.Case.Application.Dtos
{
	public class RegisterRequest
	{
		public required string Username { get; set; }
		public required string Email { get; set; }
		public required ulong Phone { get; set; }
		public required string Password { get; set; }
	}
}

