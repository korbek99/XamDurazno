using System;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace IDD
{
    public class GeneralSetting
    {

		static ISettings AppSettings => CrossSettings.Current;

		const string WatchFile = "watch";

		#region Setting Constants

		public static string UserSettings
		{
			get => AppSettings.GetValueOrDefault(nameof(UserSettings), string.Empty, WatchFile);
			set => AppSettings.AddOrUpdateValue(nameof(UserSettings), value, WatchFile);
		}
		public static string SettingsDefault
		{
			get => AppSettings.GetValueOrDefault(nameof(SettingsDefault), string.Empty, WatchFile);
			set => AppSettings.AddOrUpdateValue(nameof(SettingsDefault), value, WatchFile);
		}
		public static string BlueSettings
		{
			get => AppSettings.GetValueOrDefault(nameof(BlueSettings), string.Empty, WatchFile);
			set => AppSettings.AddOrUpdateValue(nameof(BlueSettings), value, WatchFile);
		}

		public static string FirstTime
		{
			get => AppSettings.GetValueOrDefault(nameof(FirstTime), string.Empty, WatchFile);
			set => AppSettings.AddOrUpdateValue(nameof(FirstTime), value, WatchFile);
		}

		#endregion

		public static bool GetUser()
		{
			try
			{
                var usr = JsonConvert.DeserializeObject<UserFB>(UserSettings);
				if (usr != null)
				{
					App.userFB = usr;
					return true;
				}
			}
			catch (Exception e)
			{
				return false;
			}
			return false;
		}
    }
}
