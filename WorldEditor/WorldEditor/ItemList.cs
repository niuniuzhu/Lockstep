using System.Collections.Generic;
using System.Windows.Forms;

namespace WorldEditor
{
	public class ItemList
	{
		public delegate void ItemSelectedEvent();
		public event ItemSelectedEvent ItemSelected;

		private readonly TreeView _list;
		private readonly Dictionary<TreeNode, IDataNode> _nodeToData = new Dictionary<TreeNode, IDataNode>();

		public IDataNode selected { get { return this._list.SelectedNode == null ? null : this._nodeToData[this._list.SelectedNode]; } }

		public ItemList( TreeView list )
		{
			this._list = list;
			this._list.AfterSelect += this.OnSelected;
		}

		private void OnSelected( object sender, TreeViewEventArgs e )
		{
			this.OnSelected();
		}

		private void OnSelected()
		{
			//DataCenter.minorNode = this.selected;
			if ( this.ItemSelected != null )
				this.ItemSelected.Invoke();
		}

		public void Refresh()
		{
			//int index = this._list.SelectedNode == null ? 0 : this._list.SelectedNode.Index;

			//this._nodeToData.Clear();
			//this._list.Nodes.Clear();

			//if ( DataCenter.majorNode != null &&
			//	DataCenter.majorNode.datas != null &&
			//	DataCenter.majorNode.datas.Count != 0 )
			//{
			//	foreach ( KeyValuePair<string, IDataNode> kv in DataCenter.majorNode.datas )
			//	{
			//		TreeNode node = new TreeNode( string.Format( "{0}({1})", kv.Value.name, kv.Key ) );
			//		this._list.Nodes.Add( node );
			//		this._nodeToData[node] = kv.Value;
			//	}
			//	index = index >= this._list.Nodes.Count ? this._list.Nodes.Count - 1 : index;
			//	this._list.SelectedNode = this._list.Nodes[index];
			//}
			//this.OnSelected();
		}
	}
}