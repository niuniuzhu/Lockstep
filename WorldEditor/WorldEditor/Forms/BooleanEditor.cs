
using System.Drawing;

namespace WorldEditor.Forms
{
	public partial class BooleanEditor : ValueEditor
	{
		public bool value
		{
			get { return this.checkBox.Checked; }
			set { this.checkBox.Checked = value; }
		}

		public override object result
		{
			get { return this.value; }
		}

		public BooleanEditor()
		{
			this.InitializeComponent();
		}

		protected override void UpdateLayout()
		{
			this.checkBox.Location = new Point( this.nameLabel.Location.X + this.nameLabel.Width + 5, this.checkBox.Location.Y );
			base.UpdateLayout();
		}
	}
}
