using System;

namespace TextBasedVentures
{
	/*
	public interface IUItem
	{
		string Name { get; set; }
		void AddDecorator(IUItem decorator2);
		string LongName();

		string ToString();

	}
	public class UItem : IUItem
	{
		public string Name { get; set; }



		private IUItem _decorator2;

		public UItem() : this("Nameless")
		{
		}
		public UItem(string name)
		{
			Name = name;

			_decorator2 = null;

		}
		public void AddDecorator(IUItem decorator2)
		{
			if (_decorator2 == null)
			{
				_decorator2 = decorator2;
			}
			else
			{
				_decorator2.AddDecorator(decorator2);
			}

		}
		public string LongName()
		{
			return Name + (_decorator2 == null ? "" : ", " + _decorator2.LongName());
		}

		override
		public string ToString()
		{
			return Name;
		}
	}
	*/
}

