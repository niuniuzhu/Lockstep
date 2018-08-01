using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Property;

namespace Logic.Steering
{
	public class Parabola : BaseSteering
	{
		private Vec3 _src;
		private Vec3 _targetPoint;
		private Vec3 _midPoint;
		private float _duration;
		private float _timeStamp;

		public bool complete { get; private set; }

		public Parabola( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( float arc, Vec3 targetPoint )
		{
			this._targetPoint = targetPoint;

			Entity self = this._behaviors.owner;

			this._src = self.property.position;
			Vec3 distance = this._targetPoint - this._src;
			float magnitude = distance.Magnitude();

			this._midPoint = distance / 2f + this._src;
			this._midPoint.y = magnitude * arc;
			this._duration = magnitude * MathUtils.PI * arc / ( self.maxSpeed * self.property.moveSpeedFactor );
			this._timeStamp = 0f;

			//用上一个模拟的流逝时间来估算朝向
			Vec3 nextPos = BezierHelper.GetPointAtTime( self.battle.deltaTime / this._duration, this._src, this._midPoint, this._targetPoint );
			self.property.Equal( Attr.Position, Vec3.Normalize( nextPos - self.property.position ) );

			this.complete = false;
		}

		public override void AfterUpdatePosition()
		{
			this._timeStamp += this._behaviors.owner.battle.deltaTime;

			Vec3 lastPos = this._behaviors.owner.property.position;
			this._behaviors.owner.property.Equal( Attr.Position,
												  BezierHelper.GetPointAtTime( this._timeStamp / this._duration, this._src,
																			   this._midPoint, this._targetPoint ) );
			this._behaviors.owner.property.Equal( Attr.Direction,
												  Vec3.Normalize( this._behaviors.owner.property.position - lastPos ) );

			if ( this._timeStamp >= this._duration )
				this.complete = true;
		}
	}
}