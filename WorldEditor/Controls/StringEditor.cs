namespace Controls
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
	}
}
