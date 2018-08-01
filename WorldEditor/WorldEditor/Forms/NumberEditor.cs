using System;
using System.Drawing;
using ValueType = WorldEditor.Attributes.ValueType;

namespace WorldEditor.Forms
{
	public partial class NumberEditor : ValueEditor
	{
		public ValueType valueType;

		public decimal value
		{
			get { return this.input.Value; }
			set { this.input.Value = value; }
		}

		public decimal step
		{
			get { return this.input.Increment; }
			set { this.input.Increment = value; }
		}

		public decimal min
		{
			get { return this.input.Minimum; }
			set { this.input.Minimum = value; }
		}

		public decimal max
		{
			get { return this.input.Maximum; }
			set { this.input.Maximum = value; }
		}

		public int decimalPlaces
		{
			get { return this.input.DecimalPlaces; }
			set { this.input.DecimalPlaces = value; }
		}

		public override object result
		{
			get
			{
				switch ( this.valueType )
				{
					case ValueType.Int:
						return Convert.ToInt32( this.value );
					case ValueType.Float:
						return Convert.ToSingle( this.value );
					case ValueType.Double:
						return Convert.ToDouble( this.value );
					case ValueType.Long:
						return Convert.ToInt64( this.value );
				}
				return this.value;
			}
		}

		public NumberEditor()
		{
			this.InitializeComponent();
		}

		protected override void UpdateLayout()
		{
			this.input.Location = new Point( this.nameLabel.Location.X + this.nameLabel.Width + 5, this.input.Location.Y );
			base.UpdateLayout();
		}
	}
}
