using System.Drawing;
using System.Globalization;
using UnityEngine;

namespace WorldEditor.Forms
{
	public partial class VectorEditor : ValueEditor
	{
		private int _type;

		public Vector2 vector2
		{
			get { return new Vector2( ( float )this.xvalue.Value, ( float )this.yvalue.Value ); }
			set
			{
				this._type = 0;
				this.xvalue.Value = ( decimal )value.x;
				this.yvalue.Value = ( decimal )value.y;
				this.UpdateVisual();
				this.UpdateText();
			}
		}

		public Vector3 vector3
		{
			get { return new Vector3( ( float )this.xvalue.Value, ( float )this.yvalue.Value, ( float )this.zvalue.Value ); }
			set
			{
				this._type = 1;
				this.xvalue.Value = ( decimal )value.x;
				this.yvalue.Value = ( decimal )value.y;
				this.zvalue.Value = ( decimal )value.z;
				this.UpdateVisual();
				this.UpdateText();
			}
		}

		public Vector4 vector4
		{
			get { return new Vector4( ( float )this.xvalue.Value, ( float )this.yvalue.Value, ( float )this.zvalue.Value, ( float )this.wvalue.Value ); }
			set
			{
				this._type = 2;
				this.xvalue.Value = ( decimal )value.x;
				this.yvalue.Value = ( decimal )value.y;
				this.zvalue.Value = ( decimal )value.z;
				this.wvalue.Value = ( decimal )value.w;
				this.UpdateVisual();
				this.UpdateText();
			}
		}

		public int decimalPlaces
		{
			get { return this.xvalue.DecimalPlaces; }
			set
			{
				this.xvalue.DecimalPlaces = value;
				this.yvalue.DecimalPlaces = value;
				this.zvalue.DecimalPlaces = value;
				this.wvalue.DecimalPlaces = value;
			}
		}

		public decimal step
		{
			get { return this.xvalue.Increment; }
			set
			{
				this.xvalue.Increment = value;
				this.yvalue.Increment = value;
				this.zvalue.Increment = value;
				this.wvalue.Increment = value;
			}
		}

		public decimal min
		{
			get { return this.xvalue.Minimum; }
			set
			{
				this.xvalue.Minimum = value;
				this.yvalue.Minimum = value;
				this.zvalue.Minimum = value;
				this.wvalue.Minimum = value;
			}
		}

		public decimal max
		{
			get { return this.xvalue.Maximum; }
			set
			{
				this.xvalue.Maximum = value;
				this.yvalue.Maximum = value;
				this.zvalue.Maximum = value;
				this.wvalue.Maximum = value;
			}
		}

		public override object result
		{
			get
			{
				switch ( this._type )
				{
					case 0:
						return this.vector2;
					case 1:
						return this.vector3;
					case 2:
						return this.vector4;
				}
				return null;
			}
		}

		public VectorEditor()
		{
			this.InitializeComponent();
		}

		private void UpdateVisual()
		{
			switch ( this._type )
			{
				case 0:
					this.zlabel.Visible = false;
					this.zvalue.Visible = false;
					this.wlabel.Visible = false;
					this.wvalue.Visible = false;
					break;
				case 1:
					this.zlabel.Visible = true;
					this.zvalue.Visible = true;
					this.wlabel.Visible = false;
					this.wvalue.Visible = false;
					break;
				case 2:
					this.zlabel.Visible = true;
					this.zvalue.Visible = true;
					this.wlabel.Visible = true;
					this.wvalue.Visible = true;
					break;
			}
		}

		private void UpdateText()
		{
			switch ( this._type )
			{
				case 0:
					this.lenLabel.Text = this.vector2.magnitude.ToString( CultureInfo.InvariantCulture );
					break;
				case 1:
					this.lenLabel.Text = this.vector3.magnitude.ToString( CultureInfo.InvariantCulture );
					break;
				case 2:
					this.lenLabel.Text = this.vector4.magnitude.ToString( CultureInfo.InvariantCulture );
					break;
			}
		}

		protected override void UpdateLayout()
		{
			this.valueContainer.Location = new Point( this.nameLabel.Location.X + this.nameLabel.Width + 3, this.valueContainer.Location.Y );
			base.UpdateLayout();
		}

		private void norBtn_Click( object sender, System.EventArgs e )
		{
			switch ( this._type )
			{
				case 0:
					this.vector2 = this.vector2.normalized;
					break;
				case 1:
					this.vector3 = this.vector3.normalized;
					break;
				case 2:
					this.vector4 = this.vector4.normalized;
					break;
			}
		}

		private void ValueChanged( object sender, System.EventArgs e )
		{
			this.UpdateText();
		}
	}
}
