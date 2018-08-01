using Logic.Model;

namespace Logic.Property
{
	public partial class SkillProperty : PropertyBase
	{
		private SkillData _data;

		public void Init( SkillData data )
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