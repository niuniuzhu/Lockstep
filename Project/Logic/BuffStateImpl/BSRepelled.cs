using Core.Math;
using Logic.Steering;

namespace Logic.BuffStateImpl
{
	//参数1-速度,2-距离
	public class BSRepelled : BSBase
	{
		protected override void CreateInternal()
		{
			this.owner.brain.enable = false;
			Vec3 dir = Vec3.Normalize( this.owner.property.position - this.buff.property.position );
			Vec3 targetPoint = this.owner.property.position + dir * this.extra[1];
			this.owner.steering.replled.Set( targetPoint, this.extra[0] );
			this.owner.steering.On( SteeringBehaviors.BehaviorType.Repelled );
		}

		protected override void DestroyInternal()
		{
			this.owner.steering.Off( SteeringBehaviors.BehaviorType.Repelled );
			this.owner.UpdateVelocity( Vec3.zero );
			this.owner.brain.Rearbitrate();
			this.owner.brain.enable = true;
		}

		protected override void UpdateInternal( UpdateContext context )
		{
			if ( !this.owner.steering.replled.complete )
				return;

			this.owner.DestroyBuffState( this );
		}
	}
}