namespace Organik.Case.Domain.Enums
{
	public struct ErrorMessages
	{
		public const string DuplicateUsername = "Bu kullanıcı adı sistemde kayıtlı";
		public const string DuplicateEmail = "Bu eposta sistemde kayıtlı";
		public const string DuplicatePhone = "Bu telefon sistemde kayıtlı";
		public const string UserNotFound = "Kullanıcı bulunamadı";
		public const string TokenInvalid = "Gönderilen mesajın geçerlilik süresi doldu";
		public const string CodeInvalid = "Kod hatalı veya geçersiz";
		public const string UsernameOrPasswordIncorrect = "Kullanıcı adı veya şifre hatalı";
	}
}

