using Logic.Controller;
using Protocol.Gen;
using UnityEngine;
using View.Controller;
using View.Event;

namespace View.Input
{
	public class CommonInteraction : AbsInteractionState
	{
		private Effect _decal;

		public CommonInteraction( Interaction owner )
			: base( owner )
		{
		}

		public override void OnExit()
		{
			if ( this._decal != null )
			{
				this._decal.markToDestroy = true;
				this._decal = null;
			}
		}

		public override void HandlerPointerDown( IInteractive interactive, InputData data )
		{
			if ( interactive is VBattle )
			{
				if ( this._decal != null )
					this._decal.markToDestroy = true;
				this._decal = this.owner.battle.CreateEffect( "e153" );
				Vector3 position = data.currentRaycast.point;
				position.y = 0.02f;
				this._decal.position = position;
			}
		}

		public override void HandlerPointerUp( IInteractive interactive, InputData data )
		{
			VPlayer player = VPlayer.instance;
			player.SetTracingTarget( null );

			if ( interactive is VBattle )
			{
				Vector3 point;
				if ( this.GetGroundHitPoint( out point ) )
				{
					if ( this._decal != null )
					{
						this._decal.markToDestroy = true;
						this._decal = null;
					}

					Effect e = this.owner.battle.CreateEffect( "e152" );
					point.y = 0.02f;
					e.position = point;

					this.MovePlayer( point );
				}
			}
			else
			{
				if ( interactive is VBio bio && !bio.isDead )
				{
					if ( VEntityUtils.IsAllied( player, bio ) &&
						 bio != player &&
						 player.CanMove() )
					{
						FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.Track, bio.rid ) );
					}
					else
					{
						Vector3 position = data.currentRaycast.point;
						position.y = 0f;
						Skill skill = player.commonSkill;
						if ( player.CanUseSkill( skill ) &&
							 VEntityUtils.CanAttack( player, bio, skill.campType, skill.targetFlag ) )
						{
							player.SetTracingTarget( bio );
							//普攻
							FrameActionManager.SetFrameAction( new _DTO_action_info( player.rid, ( byte )FrameActionType.UseSkill, skill.id,
																		  player.rid, bio.rid, position.x, position.y, position.z ) );
						}
						else
							UIEvent.SkillUseFailed( player, skill, bio );
					}
				}
				else
				{
					if ( this._decal != null )
					{
						this._decal.markToDestroy = true;
						this._decal = null;
					}

					Effect e = this.owner.battle.CreateEffect( "e152" );
					Vector3 point = data.currentRaycast.point;
					point.y = 0.02f;
					e.position = point;

					this.MovePlayer( point );
				}
			}
		}

		public override void HandlerPointerMove( IInteractive interactive, InputData data )
		{
			if ( this._decal == null )
				return;

			Vector3 point;
			if ( this.GetGroundHitPoint( out point ) )
			{
				VPlayer.instance.SetTracingTarget( null );
				this.MovePlayer( point );

				point.y = 0.02f;
				this._decal.position = point;
			}
		}
	}
}