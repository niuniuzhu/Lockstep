namespace View.FSM.Actions
{
	public class VDead : VBioAction
	{
		protected override void OnEnter( object[] param )
		{
			this.owner.graphic.animator.CrossFade( AnimationName.DEAD );
		}
	}
}