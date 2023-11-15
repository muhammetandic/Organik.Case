using Organik.Case.Application.Interfaces;

namespace Organik.Case.Application.Dtos
{
	public class SendCodeResponse : IResponse
	{
		public string Token { get; set; }
		public SendCodeResponse(string token)
		{
			Token = token;
		}
	}
}

