using Organik.Case.Application.Interfaces;

namespace Organik.Case.Application.Dtos
{
	public class RegisterResponse : IResponse
	{
		public uint Id { get; set; }
		public RegisterResponse(uint id)
		{
			Id = id;
		}
	}
}

