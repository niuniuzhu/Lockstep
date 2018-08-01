using Core.Math;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	public class BSOrbOfDeception : BSBase
	{
		private float _moveSpeedFactor;

		protected override void CreateInternal()
		{
			this._moveSpeedFactor = this.owner.property.moveSpeedFactor;
		}

		protected override void Reset()
		{
			this._moveSpeedFactor = 0f;
		}

		protected override void UpdateInternal( UpdateContext context )
		{
			float value = this._moveSpeedFactor * MathUtils.Lerp( this.values[0], 1f, this._elapsed / this.duration );
			this.owner.property.Equal( Attr.MoveSpeedFactor, value );
			this._deltaf[Attr.MoveSpeedFactor] = value - this._moveSpeedFactor;
		}
	}
}