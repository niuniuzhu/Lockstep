using System;

namespace WorldEditor.Attributes
{
	public enum ValueType
	{
		String,
		Boolean,
		Int,
		Float,
		Long,
		Double,
		Vector2,
		Vector3,
		Vector4,
		Map,
		List
	}

	[AttributeUsage( AttributeTargets.Property | AttributeTargets.Field )]
	public class DataPropertyAttribute : Attribute
	{
		public string name;
		public string key;
		public ValueType valueType;
		public decimal min = -decimal.MaxValue;
		public decimal max = decimal.MaxValue;
		public decimal step = 0.1m;
		public int decimalPlaces = 1;
		public string desc;
		public Type metaType;
	}
}