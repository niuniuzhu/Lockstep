using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WorldEditor
{
	public class PathView
	{
		private readonly Panel _container;
		private readonly List<IDataNode> _nodes = new List<IDataNode>();
		private readonly Dictionary<LinkLabel, IDataNode> _linkToDataNode = new Dictionary<LinkLabel, IDataNode>();

		public PathView( Panel container )
		{
			this._container = container;
		}

		public void Refresh()
		{
			//this._nodes.Clear();
			//this._linkToDataNode.Clear();
			//this._container.Controls.Clear();

			//IDataNode node = DataCenter.currNode;
			//while ( node != null )
			//{
			//	this._nodes.Add( node );
			//	node = node.parent;
			//}
			//int count = this._nodes.Count;
			//int xx = 0;
			//for ( int i = count - 1; i >= 0; i-- )
			//{
			//	node = this._nodes[i];

			//	Label label;
			//	if ( node.GetType() == typeof( DataNode ) )
			//	{
			//		LinkLabel link = new LinkLabel();
			//		link.LinkColor = Color.DodgerBlue;
			//		link.Click += this.OnLinkClicked;
			//		this._linkToDataNode[link] = node;
			//		label = link;
			//	}
			//	else
			//		label = new Label();
			//	string name = string.IsNullOrEmpty( node.name ) ? "空" : node.name;
			//	if ( !string.IsNullOrEmpty( node.id ) )
			//		name += string.Format( "({0})", node.id );
			//	label.Text = name;
			//	label.Margin = new Padding( 0, 0, 0, 0 );
			//	label.Padding = new Padding( 0, 0, 0, 0 );
			//	label.AutoSize = true;
			//	this._container.Controls.Add( label );
			//	label.Location = new Point( xx, ( this._container.Height - label.Height ) / 2 );
			//	xx += label.Width + 3;

			//	if ( i > 0 )
			//	{
			//		label = new Label();
			//		label.Text = ">>";
			//		label.Padding = new Padding( 0, 0, 0, 0 );
			//		label.Margin = new Padding( 0, 0, 0, 0 );
			//		label.AutoSize = true;
			//		this._container.Controls.Add( label );
			//		label.Location = new Point( xx, ( this._container.Height - label.Height ) / 2 );
			//		xx += label.Width + 3;
			//	}
			//}
		}

		private void OnLinkClicked( object sender, EventArgs e )
		{
			LinkLabel link = ( LinkLabel )sender;
			IDataNode node = this._linkToDataNode[link];
		}
	}
}