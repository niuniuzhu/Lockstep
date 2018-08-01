using Core.Math;
using Logic.FSM;
using Logic.Misc;

namespace Logic.BuffStateImpl
{
	public class BSStunned : BSBase
	{
		protected override void CreateInternal()
		{
			this.owner.brain.enable = false;
			this.owner.UpdateVelocity( Vec3.zero );
			SyncEventHelper.ChangeState( this.owner.rid, FSMStateType.Idle );
			this.owner.ChangeState( FSMStateType.Idle );
		}

		protected override void DestroyInternal()
		{
			this.owner.brain.Rearbitrate();
			this.owner.brain.enable = true;
		}
	}
}