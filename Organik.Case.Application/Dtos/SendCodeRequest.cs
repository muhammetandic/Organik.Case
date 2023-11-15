namespace Organik.Case.Application.Dtos
{
	public class SendCodeRequest
	{
		public required string SendMethod { get; set; }
		public uint UserId { get; set; }
		public required string Token { get; set; }
	}
}

