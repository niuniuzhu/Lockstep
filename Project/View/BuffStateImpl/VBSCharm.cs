using Logic;

namespace View.BuffStateImpl
{
	public class VBSCharm : VBSBase
	{
		protected override void DestroyInternal()
		{
			this.owner.graphic.animator.SetClipSpeed( AnimationName.MOVE, 1f );
			this.owner.graphic.animator.CrossFade( AnimationName.IDLE );
		}

		protected override void UpdateInternal( UpdateContext context )
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