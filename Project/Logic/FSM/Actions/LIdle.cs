using Core.Math;

namespace Logic.FSM.Actions
{
	public class LIdle : BioAction
	{
		protected override void OnEnter( object[] param )
		{
			this.owner.UpdateVelocity( Vec3.zero );
		}
	}
}