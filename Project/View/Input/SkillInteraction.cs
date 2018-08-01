using Logic.Controller;
using Logic.Model;
using Protocol.Gen;
using UnityEngine;
using View.Controller;
using View.Event;

namespace View.Input
{
	public class SkillInteraction : AbsInteractionState
	{
		private Effect _decal;

		private VEntity _lastTarget;
		private Skill _skill;

		public SkillInteraction( Interaction owner )
			: base( owner )
		{
		}

		public override void OnEnter( params object[] param )
		{
			this._skill = ( Skill )param[0];

			UIEvent.PickSkill( this._skill );

			VPlayer player = VPlayer.instance;
			if ( this._skill.castType != CastType.Immediately )
			{
				Vector3 position = player.direction * this._skill.distance + player.position;
				position.y = 0.02f;
				this._decal = this.owner.battle.CreateEffect( "e151" );
				this._decal.position = position;
			}
			else
			{
				player.SetTracingTarget( null );
				if ( player.CanUseSkill( this._skill ) )
				{
					FrameActionManager.SetFrameAction( new _DTO_action_info( player.rid, ( byte )FrameActionType.UseSkill,
																			 this._skill.id,
																			 player.rid,
																			 this._skill.rangeType == RangeType.Single
																				 ? player.rid
																				 : string.Empty, player.position.x, player.position.y,
																			 player.position.z ) );
				}
				this.owner.DropSkill();
			}
		}

		public override void OnExit()
		{
			if ( this._decal != null )
			{
				this._decal.markToDestroy = true;
				this._decal = null;
			}
			this._skill = null;
			if ( this._lastTarget != null )
			{
				this._lastTarget.graphic.material.SetDefaultMaterial( false );
				this._lastTarget = null;
			}
			UIEvent.DropSkill();
		}

		public override void HandlerPointerUp( IInteractive interactive, InputData data )
		{
			bool hitGround = this.GetGroundHitPoint( out Vector3 point );

			if ( this._skill.castType == CastType.Point ||
				 this._skill.castType == CastType.Dash )
			{
				if ( !hitGround )
					return;

				VPlayer.instance.SetTracingTarget( null );
				if ( VPlayer.instance.CanUseSkill( this._skill ) )
				{
					FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.UseSkill,
																			 this._skill.id,
																			 VPlayer.instance.rid, null, point.x, point.y, point.z ) );
					this.owner.DropSkill();
				}
			}
			else if ( this._skill.castType == CastType.Target )
			{
				if ( interactive is VBio entity &&
					 !entity.isDead &&
					 VPlayer.instance.CanUseSkill( this._skill ) &&
					 VEntityUtils.CanAttack( VPlayer.instance, entity, this._skill.campType, this._skill.targetFlag ) )
				{
					VPlayer.instance.SetTracingTarget( entity );
					if ( VPlayer.instance.CanUseSkill( this._skill ) )
					{
						FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.UseSkill,
																				 this._skill.id,
																				 VPlayer.instance.rid, entity.rid, 0, 0, 0 ) );
						this.owner.DropSkill();
					}
				}
				else
				{
					//当点击了地面但技能的施放类型不是目标点，则移动角色
					if ( hitGround )
					{
						VPlayer.instance.SetTracingTarget( null );
						this.MovePlayer( point );

						Effect e = this.owner.battle.CreateEffect( "e152" );
						point.y = 0.02f;
						e.position = point;
					}
				}
			}
		}

		public override void HandlerPointerMove( IInteractive interactive, InputData data )
		{
			if ( this._decal == null )
				return;

			if ( !this.GetGroundHitPoint( out Vector3 point ) )
				return;

			//if ( this._skill.castType == CastType.Target )
			//{
			//	VBio entity = interactive as VBio;
			//	if ( entity != null &&
			//		 !entity.isDead )
			//	{
			//		point = entity.position;
			//		if ( entity != this._lastTarget )
			//		{
			//			entity.graphic?.material.RimSin();
			//			this._lastTarget?.graphic?.material.SetDefaultMaterial( false );
			//			this._lastTarget = entity;
			//		}
			//	}
			//	else if ( this._lastTarget != null )
			//	{
			//		this._lastTarget?.graphic?.material.SetDefaultMaterial( false );
			//		this._lastTarget = null;
			//	}
			//}

			this._decal.position = new Vector3( point.x, 0.02f, point.z );
		}
	}
}