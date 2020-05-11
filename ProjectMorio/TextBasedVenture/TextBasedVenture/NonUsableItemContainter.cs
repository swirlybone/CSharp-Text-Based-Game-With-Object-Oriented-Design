using System;
using System.Collections.Generic;

namespace TextBasedVentures
{
	/*
	public interface IUItemContainer : IUItem
	{
		void put(IUItem uitem);
		IUItem remove(string uitemName);
		string contents();
	}
	public class UItemContainer : IUItemContainer
	{
		private Dictionary<string, IUItem> uitems;
		public string Name { get; set; }

		public UItemContainer()
		{
			uitems = new Dictionary<string, IUItem>();
		}
		public void put(IUItem item)
		{
			uitems[item.Name] = item;
		}
		public IUItem remove(string uitemName)
		{
			IUItem uitem = null;
			uitems.Remove(uitemName, out uitem);
			return uitem;
		}

		public void AddDecorator(IUItem decorator2)
		{

		}

		public string contents()
		{
			string uitemNames = "Items: \n";
			Dictionary<string, IUItem>.KeyCollection keys = uitems.Keys;
			foreach (string uitemName in keys)
			{
				uitemNames += " " + uitems[uitemName].ToString() + "\n";
			}
			return uitemNames;
		}
		public string LongName()
		{
			return "To be implemented";
		}
		override
		public string ToString()
		{
			return Name;
		}
	}
	*/
}

