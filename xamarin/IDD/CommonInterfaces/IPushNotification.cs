using System;
using System.Threading.Tasks;

namespace IDD
{
	public interface IPushNotification
	{
		 void RegisterDevice(int userId,string email);
	}
}
