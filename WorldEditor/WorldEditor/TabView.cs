using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorldEditor
{
	public class TabView
	{
		private readonly TabControl _tab;

		public delegate void IndexChangedEvent();

		public event IndexChangedEvent IndexChanged;

		//private readonly List<IDataMajorNode> _dataList = new List<IDataMajorNode>();

		//public IDataMajorNode selected { get { return this._tab.SelectedIndex >= 0 ? this._dataList[this._tab.SelectedIndex] : null; } }

		public TabView( TabControl tab )
		{
			this._tab = tab;
			this._tab.SelectedIndexChanged += this.OnIndexChanged;
		}

		private void OnIndexChanged( object sender, EventArgs e )
		{
			this.OnIndexChanged();
		}

		private void OnIndexChanged()
		{
			//DataCenter.tabIndex = this._tab.SelectedIndex;
			//DataCenter.majorNode = this.selected;
			if ( this.IndexChanged != null )
				this.IndexChanged.Invoke();
		}

		public void Refresh()
		{
			//this._dataList.Clear();
			//this._tab.TabPages.Clear();

			//int count = DataCenter.datas.Count;
			//if ( count == 0 )
			//	return;

			//int index = this._tab.SelectedIndex;

			//foreach ( KeyValuePair<string, IDataMajorNode> kv in DataCenter.datas )
			//{
			//	this._tab.TabPages.Add( kv.Value.name );
			//	this._dataList.Add( kv.Value );
			//}

			//index = index < 0 ? 0 : ( index >= count ? count - 1 : index );
			//this._tab.SelectedIndex = index;
			//this.OnIndexChanged();
		}
	}
}