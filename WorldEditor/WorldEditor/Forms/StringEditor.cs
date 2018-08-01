using System.Drawing;

namespace WorldEditor.Forms
{
	public partial class StringEditor : ValueEditor
	{
		public string value
		{
			get { return this.input.Text; }
			set { this.input.Text = value; }
		}

		public override object result
		{
			get { return this.value; }
		}

		public StringEditor()
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
