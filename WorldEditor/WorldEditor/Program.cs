using System;
using System.Windows.Forms;
using WorldEditor.DataImpl;
using WorldEditor.Forms;

namespace WorldEditor
{
	public static class Program
	{
		public static MainForm form;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault( false );
			form = new MainForm( new DataLoader() );
			Application.Run( form );
			//Application.Run(new Test());
		}
	}
}
