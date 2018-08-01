namespace View.FSM.Actions
{
	public class VIdle : VBioAction
	{
		protected override void OnEnter( object[] param )
		{
			this.owner.graphic.animator.CrossFade( AnimationName.IDLE );
		}
	}
}