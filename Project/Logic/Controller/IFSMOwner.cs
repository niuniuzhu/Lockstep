using Logic.FSM;

namespace Logic.Controller
{
	public interface IFSMOwner
	{
		FiniteStateMachine fsm { get; }

		void HandleEntityStateChanged( FSMStateType stateType, bool forceChange, params object[] stateParam );
	}
}