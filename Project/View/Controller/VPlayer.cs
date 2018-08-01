using Logic.FSM;
using View.FSM.Actions;

namespace View.Controller
{
	public sealed class VPlayer : VBio
	{
		public static VPlayer instance { get; private set; }

		public VBio tracingTarget { get; private set; }

		public override void Init( VBattle battle )
		{
			instance = this;
			base.Init( battle );
			FSMState state = this.fsm[FSMStateType.Idle];
			state.CreateAction<VPlayerIdle>();
		}

		protected override void InternalDispose()
		{
			instance = null;
			base.InternalDispose();
		}

		public void SetTracingTarget( VBio target )
		{
			this.tracingTarget?.RedRef( false );
			this.tracingTarget = target;
			this.tracingTarget?.AddRef( false );
		}
	}
}