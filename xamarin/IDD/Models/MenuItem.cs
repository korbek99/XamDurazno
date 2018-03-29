using System;
using System.Collections.Generic;

namespace IDD
{
	public class MenuItem
	{
		public IEnumerable<Item> Items { get; set; }

		public string UrlImage { get; set; }

		public bool IsLogin { get; set; }
	}

	public class Item
	{
		public string Title { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }
	}
}
