using System.Windows.Forms;

namespace Controls
{
    public partial class ValueEditor: UserControl
	{
		public virtual object result { get { return null; } }

        public ValueEditor()
        {
	        this.InitializeComponent();
        }
    }
}
