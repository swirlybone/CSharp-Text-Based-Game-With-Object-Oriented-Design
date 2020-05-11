using System;

namespace TextBasedVentures
{
	public interface IItem
	{
		string Name { get; set; }
		float Weight { get; set; }
		float SellValue { get; set; }
		float BuyValue { get; set; }
		void AddDecorator(IItem decorator);
		string LongName();
		float CarryCapacity { get; set; }

		string ToString();

	}
	public class Item : IItem
	{
		public string Name { get; set; }
		private float _weight { get; set; }
		private float _carryCapacity { get; set; }
		public float CarryCapacity
		{
			get
			{
				return _carryCapacity;
			}
			set
			{
				_carryCapacity = 10f;
			}
		}
		public float Weight {
			get
			{
				return _weight + (_decorator == null ? 0 : _decorator.Weight);
			}
			set
			{
				_weight = value;
			}
				}

		private float _sellValue;
		public float SellValue {
			get
			{
				return _sellValue + (_decorator == null ? 0 : _decorator.SellValue);
			}
			set
			{
				_sellValue = value;
			}
				}
		private float _buyValue;
		public float BuyValue {
			get
			{
				return _buyValue + (_decorator == null ? 0 : _decorator.BuyValue);
			}
			set
			{
				_buyValue = value;
			}
				}
		private IItem _decorator;

		public Item() : this("Nameless", 1.0f, 0.1f, 0.5f)
		{
		}
		public Item(string name, float weight, float sellValue, float buyValue)
		{
			Name = name;
			Weight = weight;
			SellValue = sellValue;
			BuyValue = buyValue;
			_decorator = null;
			//CarryCapacity = _carryCapacity;
			//_carryCapacity = 10f;

		}
		public void AddDecorator(IItem decorator)
		{
			if(_decorator == null)
			{
				_decorator = decorator;
			}
			else
			{
				_decorator.AddDecorator(decorator);
			}
			
		}
		public string LongName()
		{
			return Name +(_decorator == null ? "" : ", " + _decorator.LongName());
		}

		override
		public string ToString()
		{
			return Name +  "(" + LongName() + ")" + ", Weight: " + Weight + " Sell Value: " + SellValue + " Buy Value: " + BuyValue;
		}
	}

}
