using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorldEditor.Forms
{
	public partial class ValueEditor : Form
	{
		public string name
		{
			get { return this.nameLabel.Text; }
			set
			{
				this.nameLabel.Text = value;
				this.UpdateLayout();
			}
		}

		public string desc
		{
			get { return this.descLable.Text; }
			set
			{
				this.descLable.Text = value;
				this.UpdateLayout();
			}
		}

		public virtual object result { get { return null; } }

		public ValueEditor()
		{
			this.InitializeComponent();
		}

		private void confirm_Click( object sender, EventArgs e )
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancel_Click( object sender, EventArgs e )
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		protected virtual void UpdateLayout()
		{
			if ( string.IsNullOrEmpty( this.desc ) )
				this.descLable.Visible = false;
			else
			{
				this.descLable.Visible = true;
				this.descLable.MaximumSize = new Size( this.textContainer.Width, 0 );
			}

			if ( string.IsNullOrEmpty( this.desc ) )
				this.btnContainer.Location = new Point( ( this.textContainer.Width - this.btnContainer.Width ) / 2 + this.textContainer.Location.X, this.textContainer.Location.Y + this.textContainer.Height + 8 );
			else
			{
				this.descLable.Location = new Point( this.descLable.Location.X, this.textContainer.Location.Y + this.textContainer.Height + 8 );
				this.btnContainer.Location = new Point( ( this.textContainer.Width - this.btnContainer.Width ) / 2 + this.textContainer.Location.X, this.descLable.Location.Y + this.descLable.Height + 8 );
			}
		}
	}
}