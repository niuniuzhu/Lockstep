using Logic.Property;

namespace Logic.BuffStateImpl
{
	//参数1-每损失,2-获得
	public class BSBerserkerRage : BSBase
	{
		private float _precent = 1f;

		protected override void Reset()
		{
			base.Reset();
			this._precent = 1f;
		}

		protected override void DestroyInternal()
		{
			this.owner.property.Equal( Attr.AttackSpeedFactor, 1f );
		}

		protected override void AttrChangedInternal( Attr attr, object oldValue, object newValue )
		{
			if ( attr != Attr.Hp )
				return;

			if ( ( float ) oldValue <= ( float ) newValue )
				return;

			float value = this.owner.property.attackSpeedFactor / this._precent;
			this._precent = 1f + ( this.owner.property.mhp - this.owner.property.hp ) / this.owner.property.mhp * ( this.extra[1] / this.extra[0] );
			value *= this._precent;
			this.owner.property.Equal( Attr.AttackSpeedFactor, value );
		}
	}
}