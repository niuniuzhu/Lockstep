using Logic.Controller;

namespace Logic.FSM.Actions
{
	public abstract class MissileAction : AbsAction
	{
		protected Missile owner { get; private set; }

		public override FSMState state
		{
			set
			{
				base.state = value;
				this.owner = ( Missile )value.owner;
			}
		}
	}
}