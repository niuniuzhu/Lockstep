using System;
using System.Windows.Forms;
using JWC;
using WorldEditor.Properties;

namespace WorldEditor.Forms
{
	public partial class MainForm : Form
	{
		private const string MRU_REG_KEY = "SOFTWARE\\Smitear\\WorldEditor";

		private readonly IDataLoader _dataLoader;
		private readonly MruStripMenu _mruMenu;
		private readonly TabView _tabView;
		private readonly PathView _pathView;
		private readonly ItemList _itemList;
		private readonly ItemView _itemView;
		private string _currFile;

		public MainForm( IDataLoader dataLoader )
		{
			if ( dataLoader == null )
				throw new ArgumentNullException( "Invalid data loader." );

			this._dataLoader = dataLoader;

			this.InitializeComponent();

			this._tabView = new TabView( this.tab );
			this._pathView = new PathView( this.pathContainer );
			this._itemList = new ItemList( this.list );
			this._itemView = new ItemView( this.listView );

			this._tabView.IndexChanged += this.OnCategoryChanged;
			this._itemList.ItemSelected += this.OnItemSelected;
			this._itemView.ItemSelected += this.OnProperySelected;

			this._mruMenu = new MruStripMenu( this.menuRecentFile, this.OnMruFile, MRU_REG_KEY + "\\RecentFiles", false );
			this._mruMenu.LoadFromRegistry();
			this.CheckRecentFiles();

			if ( this._mruMenu.GetFiles().Length > 0 )
				this.OnMruFile( 0, this._mruMenu.GetFileAt( 0 ) );
		}

		private void CheckRecentFiles()
		{
			this.menuRemoveRecentFiles.Enabled = this._mruMenu.GetFiles().Length > 0;
		}

		private void menuRemoveRecentFiles_Click( object sender, EventArgs e )
		{
			this._mruMenu.RemoveAll();
			this._mruMenu.SaveToRegistry();
			this.CheckRecentFiles();
		}

		private void OnMruFile( int number, string filename )
		{
			this.LoadFromFile( filename );
		}

		private void openToolStripMenuItem_Click( object sender, EventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = @"数据文件 (*.txt)|*.txt";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;
			if ( openFileDialog.ShowDialog() == DialogResult.OK )
				this.LoadFromFile( openFileDialog.FileName );
		}

		private void LoadFromFile( string filename )
		{
			string error;
			DataCenter.CreateDataRoot( this._dataLoader.Load( filename, out error ) );
			if ( DataCenter.root != null )
			{
				this._currFile = filename;
				this._mruMenu.AddFile( filename );
				this._mruMenu.SaveToRegistry();
				this.CheckRecentFiles();
				this.RefreshView();
			}
			else
			{
				MessageBox.Show( error
				, Resources.project_name
				, MessageBoxButtons.OK
				, MessageBoxIcon.Error );
				this._mruMenu.RemoveFile( filename );
			}
		}

		private void ExitToolStripMenuItem_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void saveAsToolStripMenuItem_Click( object sender, EventArgs e )
		{
			this.ShowSaveFileDialog();
		}

		private void saveToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if ( string.IsNullOrEmpty( this._currFile ) )
				this.ShowSaveFileDialog();
			else
				this.SaveToFile( this._currFile );
		}

		private void ShowSaveFileDialog()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = @"数据文件 (*.txt)|*.txt";

			if ( saveFileDialog.ShowDialog() == DialogResult.OK )
				this.SaveToFile( saveFileDialog.FileName );
		}

		private void SaveToFile( string filename )
		{
			string error;
			bool result = this._dataLoader.Save( DataCenter.SaveFromRoot(), filename, out error );
			if ( result )
			{
				this._currFile = filename;
				this._mruMenu.AddFile( filename );
			}
		}

		private void RefreshView()
		{
			this._tabView.Refresh();
			this._itemList.Refresh();
			this._itemView.Refresh();
			this._pathView.Refresh();
		}

		private void OnCategoryChanged()
		{
			this._itemList.Refresh();
			this._itemView.Refresh();
			this._pathView.Refresh();
		}

		private void OnItemSelected()
		{
			this._itemView.Refresh();
			this._pathView.Refresh();
		}

		private void OnProperySelected()
		{
			this._pathView.Refresh();
		}
	}
}
