using Core.Math;
using Logic.Controller;
using Logic.FSM;

namespace Logic.AI.Atomic
{
	public class GoalStructureAttack : Goal
	{
		private Bio _target;

		public override Type type => Type.Attack;

		protected override void Activate()
		{
			this._status = Status.Active;

			Bio bio = ( Bio ) this.owner;
			bio.Attack( bio.commonSkill, this._target, Vec3.zero );
		}

		internal override void Terminate()
		{
			this._target = null;
			base.Terminate();
		}

		internal override Status Process( float dt )
		{
			this.ActivateIfInactive();

			Bio bio = ( Bio ) this.owner;
			this._status = bio.fsm.currState.type == FSMStateType.Idle ? Status.Completed : Status.Active;
			return this._status;
		}

		public void SetTarget( Bio target )
		{
			this._target = target;
		}
	}
}