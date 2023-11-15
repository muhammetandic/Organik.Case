namespace Organik.Case.Application.Helpers
{
	public class AppSettings
	{
		public string Secret { get; set; }
		public uint AccessTokenLifeInMinutes { get; set; }
		public string OrganikApiKey { get; set; }
		public uint HeaderId { get; set; }
	}

    public class MailSettings
    {
        public string? FromName { get; set; }
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

