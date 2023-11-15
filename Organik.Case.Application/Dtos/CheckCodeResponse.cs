using Organik.Case.Application.Interfaces;

namespace Organik.Case.Application.Dtos
{
	public class CheckCodeResponse : IResponse
	{
		public string AccessToken { get; set; }
		public CheckCodeResponse(string accessToken)
		{
			AccessToken = accessToken;
		}
	}
}

