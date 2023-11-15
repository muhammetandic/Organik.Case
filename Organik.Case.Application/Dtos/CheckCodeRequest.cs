namespace Organik.Case.Application.Dtos
{
	public class CheckCodeRequest
	{
		public required uint UserId { get; set; }
		public required string Token { get; set; }
		public required string Code { get; set; }
	}
}

