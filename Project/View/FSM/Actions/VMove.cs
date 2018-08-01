using Logic;

namespace View.FSM.Actions
{
	public class VMove : VBioAction
	{
		protected override void OnExit()
		{
			this.owner.graphic.animator.SetClipSpeed( AnimationName.MOVE, 1f );
		}

		protected override void OnUpdate( UpdateContext context )
		{
			if ( this.owner.property.speed < 0.01f )
				this.owner.graphic.animator.CrossFade( AnimationName.IDLE );
			else
			{
				this.owner.graphic.animator.SetClipSpeed( AnimationName.MOVE, this.owner.property.speed / this.owner.maxSpeed );
				this.owner.graphic.animator.CrossFade( AnimationName.MOVE );
			}
		}
	}
}