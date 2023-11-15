using Organik.Case.Application.Interfaces;

namespace Organik.Case.Application.Dtos
{
	public class LoginResponse : IResponse
	{
		public uint Id { get; set; }
		public string Username { get; set; }
		public string Token { get; set; }
		public LoginResponse(uint id, string username, string token)
		{
			Id = id;
			Username = username;
			Token = token;
		}
	}
}

