using Logic.Controller;

namespace Logic.FSM.Actions
{
	public abstract class BioAction : AbsAction
	{
		protected Bio owner { get; private set; }

		public override FSMState state
		{
			set
			{
				base.state = value;
				this.owner = ( Bio )value.owner;
			}
		}
	}
}