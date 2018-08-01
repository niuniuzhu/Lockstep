using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.Event;
using Logic.Misc;

namespace Logic.Steering
{
	public class ObstacleAvoidance : BaseSteering
	{
		private const float MIN_DETECTION_BOX_LENGTH = .8f;

		private List<Entity> _temp = new List<Entity>();

		public ObstacleAvoidance( SteeringBehaviors behaviors ) : base( behaviors )
		{
		}

		public override Vec3 Steer()
		{
			Entity self = this._behaviors.owner;

			float detectRadius = MIN_DETECTION_BOX_LENGTH * ( 1 + self.property.speed / self.maxSpeed );

			EntityUtils.GetEntitiesInCircle( self.battle.GetEntities(), self.property.position, detectRadius, ref this._temp );

			this.DebugDrawDetectRadius( detectRadius );

			Bio closestIntersectingObstacle = null;
			float distToClosestIp = float.MaxValue;
			Vec3 localPosOfClosestObstacle = Vec3.zero;

			int count = this._temp.Count;
			for ( int i = 0; i < count; i++ )
			{
				Bio bio = this._temp[i] as Bio;
				if ( bio == null ||
					 bio == self ||
					 bio.isDead ||
					 !bio.volumetric ||
					 bio.property.dashing > 0 ||
					 bio.property.ignoreVolumetric > 0 )
					continue;

				Vec3 localPos = self.PointToLocal( bio.property.position );
				if ( localPos.x >= 0 )
				{
					float expandedRadius = bio.size.z + self.size.z;
					if ( MathUtils.Abs( localPos.z ) < expandedRadius )
					{
						float cX = localPos.x;
						float cZ = localPos.z;

						float sqrtPart = MathUtils.Sqrt( expandedRadius * expandedRadius - cZ * cZ );
						float ip = cX - sqrtPart;
						if ( ip <= 0.0f )
							ip = cX + sqrtPart;

						if ( ip < distToClosestIp )
						{
							distToClosestIp = ip;
							closestIntersectingObstacle = bio;
							localPosOfClosestObstacle = localPos;
						}
					}
				}
			}
			this._temp.Clear();

			if ( closestIntersectingObstacle != null )
			{
				Vec3 steeringForce = Vec3.zero;

				const float minLocalXDistance = .3f;//限定最小的x轴距离,值越小,下面得到的x轴因子越大,侧向力就越大
				float multiplier = 1.5f / MathUtils.Min( minLocalXDistance, localPosOfClosestObstacle.x );//侧向力和障碍物的x距离成反比,越近x轴的因子越大,侧向力越大
				steeringForce.z = -closestIntersectingObstacle.size.z * multiplier / localPosOfClosestObstacle.z;//侧向力和障碍物的半径成正比,z轴距离成反比

				const float brakingWeight = .2f;//制动力因子,值越大,速度减少越快
				steeringForce.x = ( closestIntersectingObstacle.size.x -
									localPosOfClosestObstacle.x ) *
								  brakingWeight;

				steeringForce = self.VectorToWorld( steeringForce );

				this.DebugDrawForce( steeringForce * 3 );

				return steeringForce;
			}
			return Vec3.zero;
		}

		private void DebugDrawDetectRadius( float detectRadius )
		{
			SyncEventHelper.DebugDraw( SyncEvent.DebugDrawType.WireSphere, this._behaviors.owner.property.position, Vec3.zero, null, detectRadius, Color4.blue );
		}

		private void DebugDrawForce( Vec3 force )
		{
			SyncEventHelper.DebugDraw( SyncEvent.DebugDrawType.Ray, this._behaviors.owner.property.position, force, null, 0f, Color4.yellow );
		}
	}
}