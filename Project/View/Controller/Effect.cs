using Logic;
using Logic.Controller;
using Logic.Model;
using UnityEngine;

namespace View.Controller
{
	public class Effect : VEntity
	{
		public bool shadowVisible => this._data.shadowVisible;
		public EffectPositionType positionType => this._data.positionType;
		public EffectRotationType rotationType => this._data.rotationType;
		public Spare spare => this._data.spare;
		public float time { get; private set; }
		public VEntity followedTarget { get; private set; }
		public VBuff followedBuff { get; private set; }

		protected override void InternalOnAddedToBattle( EntityParam param )
		{
			base.InternalOnAddedToBattle( param );

			this.graphic.shadowVisible = this.shadowVisible;
			this.graphic.animator.Play( this.graphic.id + "_play" );
			this.graphic.particle.Play();
			this.time = 0f;
		}

		protected override void InternalOnRemoveFromBattle()
		{
			this.followedBuff?.RedRef();
			this.followedBuff = null;
			this.followedTarget?.RedRef();
			this.followedTarget = null;

			base.InternalOnRemoveFromBattle();
		}

		private void SetupTerritory( Vector3 position, Vector3 direction )
		{
			this.position = position;
			switch ( this.rotationType )
			{
				case EffectRotationType.Absolute:
					break;

				case EffectRotationType.Follow:
					this.direction = direction;
					break;
			}
		}

		public void SetupTerritory( VEntity caster, VEntity target, Vector3 targetPoint )
		{
			switch ( this.positionType )
			{
				case EffectPositionType.DockedToCaster:
					this.SetDocked( caster );
					break;

				case EffectPositionType.DockedToTarget:
					if ( target != null )
						this.SetDocked( target );
					else
						this.SetupTerritory( targetPoint, caster.direction );
					break;

				case EffectPositionType.FollowCaster:
					this.followedTarget = caster;
					this.followedTarget.AddRef();
					this.SetDocked( caster );
					break;

				case EffectPositionType.FollowTarget:
					this.followedTarget = target;
					this.followedTarget.AddRef();
					if ( target != null )
						this.SetDocked( target );
					else
						this.SetupTerritory( targetPoint, caster.direction );
					break;
			}
			this.graphic.electric.target = target;
		}

		private void SetDocked( VEntity docked )
		{
			this.SetupTerritory( this.GetDockedPosition( docked, this.spare ), docked.direction );
		}

		public Vector3 GetDockedPosition( VEntity docked, Spare part )
		{
			Vector3 p;
			switch ( part )
			{
				case Spare.Overhead:
					p = docked.graphic.spare.overheadPos;
					break;
				case Spare.HitPoint:
					p = docked.graphic.spare.hitPointPos;
					break;
				case Spare.LHand:
					p = docked.graphic.spare.lHandPos;
					break;
				case Spare.RHand:
					p = docked.graphic.spare.rHandPos;
					break;
				case Spare.LFoot:
					p = docked.graphic.spare.lFootPos;
					break;
				case Spare.RFoot:
					p = docked.graphic.spare.rFootPos;
					break;
				case Spare.HeadNub:
					p = docked.graphic.spare.headNubPos;
					break;
				case Spare.Weapon0:
					p = docked.graphic.spare.weapon0Pos;
					break;
				case Spare.Weapon1:
					p = docked.graphic.spare.weapon1Pos;
					break;
				default:
					p = docked.graphic.spare.footholdPos;
					break;
			}
			return p;
		}

		public override void UpdateState( UpdateContext context )
		{
			this.time += context.deltaTime;

			switch ( this.positionType )
			{
				case EffectPositionType.FollowCaster:
				case EffectPositionType.FollowTarget:
					this.SetDocked( this.followedTarget );
					break;
			}
			this.graphic.UpdateElectric( context.deltaTime );

			if ( this.lifeTime > 0 &&
				 this.time >= this.lifeTime &&
				 !this.graphic.expired )
				this.markToDestroy = true;
		}
	}
}