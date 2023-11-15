namespace Organik.Case.Application.Interfaces.ExternalServices
{
	public interface ISmsService
	{
        Task SendSms(string code, string phone);
    }
}

