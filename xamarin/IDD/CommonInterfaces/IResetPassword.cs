using System;
namespace IDD
{
	public interface IResetPassword
	{
		ResetPasswordInfo GetDataFromUriCall();

		void CleanDataFromUriCall();
	}
}
