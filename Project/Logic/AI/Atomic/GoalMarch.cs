using Logic.Controller;
using Logic.FSM;

namespace Logic.AI.Atomic
{
	public class GoalMarch : Goal
	{
		public override Type type => Type.GoalMarch;

		protected override void Activate()
		{
			this._status = Status.Active;

			Bio bio = ( Bio ) this.owner;
			if ( bio.fsm.currState.type == FSMStateType.Idle )
			{
				if ( bio.property.team == 0 )
					bio.Move( bio.battle.basePoint2 );
				else if ( bio.property.team == 1 )
					bio.Move( bio.battle.basePoint1 );
			}
		}

		internal override Status Process( float dt )
		{
			this.ActivateIfInactive();

			this._status = Status.Completed;
			return this._status;
		}
	}
}