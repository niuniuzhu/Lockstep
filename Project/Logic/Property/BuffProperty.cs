using Logic.Model;

namespace Logic.Property
{
	public partial class BuffProperty : PropertyBase
	{
		private BuffData _data;

		public void Init( BuffData data )
		{
			this._data = data;
			this.SetDefault();
		}

		public void ApplyLevel( int level )
		{
			this.Equal( Attr.Lvl, level );
		}
	}
}