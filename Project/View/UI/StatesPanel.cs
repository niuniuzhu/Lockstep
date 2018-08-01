using FairyUGUI.UI;
using Logic.Property;
using View.Controller;

namespace View.UI
{
	public class StatesPanel
	{
		private GComponent _root;

		public StatesPanel( GComponent root )
		{
			this._root = root;
		}

		public void Dispose()
		{
			this._root = null;
		}

		public void OnEntityCreated( VBio bio )
		{
			int count = this._root.numChildren;
			for ( int i = 0; i < count; i++ )
			{
				GObject child = this._root.GetChildAt( i );
				if ( !child.name.StartsWith( "a_" ) )
					continue;
				int n = int.Parse( child.name.Substring( 2 ) );
				Attr attr = ( Attr ) n;
				GTextField tf = child.asTextField;
				tf.text = string.Empty + VPlayer.instance.property[attr];
			}
		}

		public void OnEntityAttrChanged( VEntity target, Attr attr, object oldValue, object newValue )
		{
			GObject gObject = this._root["a_" + ( int ) attr];
			if ( gObject == null )
				return;

			GTextField tf = gObject.asTextField;
			tf.text = string.Empty + newValue;
		}
	}
}