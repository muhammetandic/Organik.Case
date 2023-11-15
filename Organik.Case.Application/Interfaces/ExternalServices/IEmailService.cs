namespace Organik.Case.Application.Interfaces.ExternalServices
{
	public interface IEmailService
	{
        void SendMail(string to, string name, string code);
    }
}

