using Newtonsoft.Json;

namespace Organik.Case.Application.Models
{
	public class OrganikSmsSendRequest
	{
		[JsonProperty("message")]
		public string? Message { get; set; }

		[JsonProperty("groups")]
		public IEnumerable<uint>? Groups { get; set; }

		[JsonProperty("recipients")]
		public IEnumerable<string>? Recipients { get; set; }

		[JsonProperty("header")]
		public uint Header { get; set; }

		[JsonProperty("commercial")]
		public string? Commercial { get; set; }

		[JsonProperty("type")]
		public string? Type { get; set; }

		[JsonProperty("otp")]
		public bool? Otp { get; set; }

		[JsonProperty("appeal")]
		public bool? Appeal { get; set; }

		[JsonProperty("validity")]
		public uint? Validity { get; set; }

		[JsonProperty("date")]
		public DateTime? Date { get; set; }

		public OrganikSmsSendRequest(string message, uint header, string[] recepients)
		{
			Message = message;
			Header = header;
			Recipients = recepients;
		}
	}
}

