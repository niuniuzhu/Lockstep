using Logic.Controller;

namespace Logic.BuffStateImpl
{
	//参数1-指定攻击次数后此状态失效
	public class BSFlurry : BSBase
	{
		private int _count;

		protected override void Reset()
		{
			base.Reset();
			this._count = 0;
		}

		protected override void DamageInternal( Buff buff, Bio target, float damage )
		{
			if ( ++this._count == ( int ) this.extra[0] )
				this.owner.DestroyBuffState( this );
		}
	}
}