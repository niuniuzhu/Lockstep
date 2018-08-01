using System;
using ValueType = WorldEditor.Attributes.ValueType;

namespace WorldEditor
{
	public abstract class BasePropertyInfo
	{
		public string key;
		public object value;
	}

	public class PropertyInfo : BasePropertyInfo
	{
		public ValueType valueType;
		public decimal min = -decimal.MaxValue;
		public decimal max = decimal.MaxValue;
		public decimal step = 0.1m;
		public int decimalPlaces = 1;
		public string desc;
		public Type metaType;
		public DataNode parent;

		public virtual void ReadValue( ref Core.Misc.Map data )
		{
			this.value = data[this.key];
		}

		public virtual void SaveValue( ref Core.Misc.Map data )
		{
			data[this.key] = this.value;
		}
	}

	public class ListPropertyInfo : BasePropertyInfo
	{

	}

	public class MapPropertyInfo : BasePropertyInfo
	{

	}
}