using Core.Math;
using Logic.Controller;
using Logic.Event;
using Logic.Misc;

namespace Logic.Steering
{
	public class FollowPath : BaseSteering
	{
		public class Path
		{
			public int currentIndex { get; private set; }
			public Vec3 currentWaypoint => this.corners[this.currentIndex];
			public Vec3 nextWaypoint => this.corners[this.currentIndex + 1];
			public bool lastWaypoint { get; private set; }
			public Vec3[] corners { get; }
			public bool vaild => this.corners != null && this.corners.Length > 1;

			public Path( Vec3[] corners )
			{
				this.corners = corners;
			}

			public bool HasNext()
			{
				return this.corners != null && this.currentIndex < this.corners.Length - 1;
			}

			public bool Next()
			{
				if ( this.corners == null )
					return false;

				if ( this.currentIndex < this.corners.Length - 1 )
				{
					++this.currentIndex;
					return true;
				}
				this.lastWaypoint = true;
				return false;
			}
		}

		public Path path { get; private set; }

		public bool complete { get; private set; }

		public FollowPath( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public void Set( Vec3[] corners )
		{
			this.path = new Path( corners );

			if ( !this.path.vaild )
			{
				LLogger.Warning( "Invalid path" );
				return;
			}
			this.complete = false;
			this.path.Next();
		}

		public void MaxVelocity()
		{
			if ( !this.path.vaild )
				return;

			Entity self = this._behaviors.owner;
			self.UpdateVelocity( Vec3.Normalize( this.path.currentWaypoint - self.property.position ) *
								 ( self.maxSpeed * self.property.moveSpeedFactor ) );
		}

		public override Vec3 Steer()
		{
			if ( !this.path.vaild || this.complete )
				return Vec3.zero;

			this.DebugDrawPath();

			Entity self = this._behaviors.owner;
			Vec3 dir = Vec3.Normalize( this.path.currentWaypoint - this._behaviors.owner.property.position );
			Vec3 desiredVelocity = dir * ( self.maxSpeed * self.property.moveSpeedFactor );
			return desiredVelocity - self.property.velocity;
		}

		public override void AfterUpdatePosition()
		{
			Entity self = this._behaviors.owner;
			if ( SteeringTools.CheckPointCrossTargetPoint( self.property.position, this.path.currentWaypoint ) )
			{
				if ( !this.path.Next() )
				{
					self.UpdateVelocity( Vec3.zero );
					this.complete = true;
					return;
				}
				self.UpdateVelocity( Vec3.Normalize( this.path.currentWaypoint - self.property.position ) *
									 ( self.maxSpeed * self.property.moveSpeedFactor ) );
			}
		}

		private void DebugDrawPath()
		{
			SyncEventHelper.DebugDraw( SyncEvent.DebugDrawType.Path, Vec3.zero, Vec3.zero, this.path.corners, 0, Color4.white );
		}
	}
}