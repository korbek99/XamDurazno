using System;
namespace IDD
{
	public class Message
	{
		public string id { get; set; }
		public string title { get; set; }
		public string message { get; set; }
		public string created_at { get; set; }
		public string showMoreMessage
		{
			get
			{
				var newMessage = message ?? "";
				return newMessage.Length > 37 ? newMessage.Substring(0,37) + "...": newMessage;
			}
		}
	}

}

