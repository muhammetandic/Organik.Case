using Organik.Case.Application.Interfaces;

namespace Organik.Case.Application.Dtos.Error
{
	public class ErrorResponse : IResponse
	{
		public int  ErrorCode { get; set; }
		public List<string> Errors { get; set; }

		public ErrorResponse(int errorCode, string error)
		{
			Errors = new List<string>();
			ErrorCode = errorCode;
			Errors.Add(error);
		}
		public ErrorResponse(int errorCode, List<string> errors)
		{
			ErrorCode = errorCode;
			Errors = errors;
		}
	}
}

