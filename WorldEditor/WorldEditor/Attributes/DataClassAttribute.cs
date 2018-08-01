using System;

namespace WorldEditor.Attributes
{
	[AttributeUsage( AttributeTargets.Class )]
	public class DataClassAttribute : Attribute
	{
		public string name;
	}
}