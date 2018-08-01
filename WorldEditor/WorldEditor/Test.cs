using System.Windows.Forms;
using GlacialComponents.Controls;

namespace WorldEditor
{
	public partial class Test : Form
	{
		public Test()
		{
			this.InitializeComponent();

			this.listview.Columns.Add( "Name", 100 );
			this.listview.Columns.Add( "Value", 300 );

			GLItem item = this.listview.Items.Add( "No.1" );
			GLSubItem subItem = item.SubItems.Add( "v0" );
			subItem.Control = new Controls.StringEditor();

			item = this.listview.Items.Add( "No.2" );
			subItem = item.SubItems.Add( "v1" );
			subItem.Control = new Controls.ValueEditor();
		}
	}
}
