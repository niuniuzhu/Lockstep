using System.Collections;
using Core.Math;
using Core.Misc;
using Logic.FSM;
using Logic.FSM.Actions;
using Logic.Model;

namespace Logic.Controller
{
	public class FoxFire : Bio
	{
		public float detectRange { get; private set; }
		public float angleSpeed { get; private set; }
		public float angleSpeed2 { get; private set; }
		public float dashDelay { get; private set; }
		public float dashDelay2 { get; private set; }

		private Bio _target;
		private Bio _caster;

		protected override void InternalCreateFSMAction()
		{
			FSMState state = this.fsm.CreateState( FSMStateType.Idle );
			state.CreateAction<LFoxFireIdle>();
			state = this.fsm.CreateState( FSMStateType.Move );
			state.CreateAction<LFoxFireMove>();
		}

		protected override void InternalOnAddedToBattle( EntityParam param )
		{
			base.InternalOnAddedToBattle( param );

			Hashtable def = Defs.GetEntity( this.id );
			this.detectRange = def.GetFloat( "detect_range" );
			this.angleSpeed = def.GetFloat( "angle_speed" );
			this.angleSpeed2 = def.GetFloat( "angle_speed2" );
			this.dashDelay = def.GetFloat( "dash_delay" );
			this.dashDelay2 = def.GetFloat( "dash_delay2" );
		}

		protected override void InternalOnRemoveFromBattle()
		{
			this._target?.RedRef();
			this._target = null;

			this._caster.RedRef();
			this._caster = null;

			base.InternalOnRemoveFromBattle();
		}

		internal void Setup( int lvl, Bio caster, float distance )
		{
			this.skills[0].ApplyLevel( lvl );

			LFoxFireIdle idleAction = this.fsm[FSMStateType.Idle].GetAction<LFoxFireIdle>();
			idleAction.caster = caster;
			idleAction.distance = distance;
			idleAction.angleSpeed = Quat.Euler( 0f, this.battle.random.NextFloat( this.angleSpeed, this.angleSpeed2 ), 0f );

			this._caster = caster;
			this._caster.AddRef();
		}

		internal void Emmit( Bio target )
		{
			this._target = target;
			this._target.AddRef();
			this.fsm.ChangeState( FSMStateType.Move, false, target, ( LFoxFireMove.MoveCompleteHander )this.OnMoveComplete );
		}

		private void OnMoveComplete()
		{
			this.ChangeState( FSMStateType.Idle );

			if ( this._target != null && !this._target.isDead )
			{
				Skill skill = this.skills[0];
				int count = skill.buffs.Length;
				for ( int i = 0; i < count; i++ )
					this.battle.CreateBuff( skill.buffs[i], skill.id, skill.property.lvl, this._caster, this._target, Vec3.zero );
			}
			this.markToDestroy = true;
		}
	}
}