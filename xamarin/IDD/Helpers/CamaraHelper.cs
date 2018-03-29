using System;
using System.IO;

namespace IDD
{
    public class CamaraHelper
    {
		public static byte[] GetByteFromMediaFile(Plugin.Media.Abstractions.MediaFile file)
		{
			return File.ReadAllBytes(file.Path);
		}
    }
}
