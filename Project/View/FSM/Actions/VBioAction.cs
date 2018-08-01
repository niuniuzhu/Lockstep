using Logic.FSM;
using Logic.FSM.Actions;
using View.Controller;

namespace View.FSM.Actions
{
	public abstract class VBioAction : AbsAction
	{
		public VBio owner { get; private set; }

		public override FSMState state
		{
			set
			{
				base.state = value;
				this.owner = ( VBio )base.state.owner;
			}
		}
	}
}