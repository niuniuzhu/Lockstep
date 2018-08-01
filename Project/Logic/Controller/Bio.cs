using System.Collections.Generic;
using Core.Math;
using Logic.BuffStateImpl;
using Logic.Event;
using Logic.FSM;
using Logic.FSM.Actions;
using Logic.Misc;
using Logic.Model;
using Logic.Property;

namespace Logic.Controller
{
	public class Bio : Entity
	{
		public string uid { get; private set; }
		public int reliveTime { get; private set; }
		public int reliveGold { get; private set; }
		public int goldBountyAwarded { get; private set; }
		public int expBountyAwarded { get; private set; }
		public int upgradeExpNeeded { get; private set; }
		public int upgradeSkillPointObtained { get; private set; }
		public float fov => this._data.fov;
		public Skill[] skills { get; private set; }
		public Skill commonSkill { get; private set; }
		public Skill usingSkill { get; internal set; }
		public FiniteStateMachine fsm { get; private set; }
		public bool isDead => this.fsm.currState.type == FSMStateType.Dead;
		public SensorySystem sensorySystem { get; private set; }

		private int _numSkills;
		private float _regenTime;
		private readonly List<BSBase> _buffStates = new List<BSBase>();
		private readonly List<BSBase> _buffStatesToDestroy = new List<BSBase>();

		internal override void Init( Battle battle )
		{
			base.Init( battle );

			this.skills = new Skill[6];
			for ( int i = 0; i < this.skills.Length; i++ )
				this.skills[i] = new Skill();

			this.sensorySystem = new SensorySystem( this );
			this.fsm = new FiniteStateMachine( this );
			this.InternalCreateFSMAction();
		}

		protected virtual void InternalCreateFSMAction()
		{
			FSMState state = this.fsm.CreateState( FSMStateType.Idle );
			state.CreateAction<LIdle>();
			state = this.fsm.CreateState( FSMStateType.Dead );
			state.CreateAction<LDead>();
			state = this.fsm.CreateState( FSMStateType.Move );
			state.CreateAction<LMove>();
			state = this.fsm.CreateState( FSMStateType.Track );
			state.CreateAction<LTrack>();
			state = this.fsm.CreateState( FSMStateType.Pursue );
			state.CreateAction<LPursue>();
			state = this.fsm.CreateState( FSMStateType.Attack );
			state.CreateAction<LAttack>();
		}

		protected override void InternalDispose()
		{
			this.sensorySystem.Dispose();
			this.sensorySystem = null;

			this.fsm.Dispose();
			this.fsm = null;

			int count = this.skills.Length;
			for ( int i = 0; i < count; i++ )
				this.skills[i].Dispose();
			this.skills = null;

			base.InternalDispose();
		}

		protected override void InternalOnAddedToBattle( EntityParam param )
		{
			base.InternalOnAddedToBattle( param );

			this.property.Equal( Attr.Team, param.team );
			this.ApplyLevel( 0 );

			this._regenTime = 0f;
			this._numSkills = this._data.skills.Length;
			for ( int i = 0; i < this._numSkills; i++ )
			{
				Skill skill = this.skills[i];
				skill.OnCreated( this._data.skills[i], this.rid );
				skill.OnAttrChanged += this.OnSkillAttrChanged;
				if ( skill.isCommon )
				{
					skill.Upgrade();//普攻默认等级1
					this.commonSkill = skill;
				}
			}

			this.fsm.Start();
			SyncEventHelper.ChangeState( this.rid, FSMStateType.Idle );
			this.ChangeState( FSMStateType.Idle );
			this.ActivePassiveBuffs();
		}

		protected override void InternalOnRemoveFromBattle()
		{
			while ( this._buffStates.Count > 0 )
				this.DestroyBuffStateImmediately( this._buffStates[0] );

			for ( int i = 0; i < this._numSkills; i++ )
			{
				Skill skill = this.skills[i];
				skill.OnAttrChanged -= this.OnSkillAttrChanged;
			}
			this.commonSkill = null;
			this.usingSkill = null;

			this.sensorySystem.Clear();
			this.fsm.Stop();
		}

		internal override void UpdateState( UpdateContext context )
		{
			this.time += context.deltaTime;

			this._regenTime += context.deltaTime;
			if ( this._regenTime >= 5f )
			{
				this._regenTime -= 5f;
				if ( !this.isDead )
				{
					this.property.Add( Attr.Hp, this.property.hpRegen );
					this.property.Add( Attr.Mana, this.property.manaRegen );
				}
			}

			this.UpdateThink( context );
			this.fsm.Update( context );

			for ( int i = 0; i < this._numSkills; i++ )
				this.skills[i].Update( context.deltaTime );

			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].Update( context );

			count = this._buffStatesToDestroy.Count;
			if ( count > 0 )
			{
				for ( int i = 0; i < count; i++ )
				{
					BSBase buffState = this._buffStatesToDestroy[i];
					this.DestroyBuffStateImmediately( buffState );
				}
				this._buffStatesToDestroy.Clear();
			}

			this.UpdateSteering( context );

			this.sensorySystem.Update( context );

			if ( this.debugDraw )
				SyncEventHelper.DebugDraw( SyncEvent.DebugDrawType.WireCube, this.property.position + new Vec3( 0, this.size.y * 0.5f, 0 ), this.size, null, 0, Color4.gray );

			if ( this.lifeTime > 0 &&
				 this.time >= this.lifeTime )
				this.markToDestroy = true;
		}

		internal override void UpdateFight( UpdateContext context )
		{
			if ( !this.isDead &&
				 this.property.hp <= 0f )
			{
				this.Die( this.sensorySystem.killer );
				this.sensorySystem.killer = null;
			}
		}

		private void ActivePassiveBuffs()
		{
			for ( int i = 0; i < this._numSkills; i++ )
			{
				Skill skill = this.skills[i];
				if ( skill.property.lvl < 0 )
					continue;
				if ( skill.passiveBuffs == null )
					continue;
				int count = skill.passiveBuffs.Length;
				for ( int j = 0; j < count; j++ )
					this.battle.CreateBuff( skill.passiveBuffs[j], skill.id, skill.property.lvl, this, skill.rangeType == RangeType.Single ? this : null, this.property.position );
			}
		}

		public Skill GetSkill( string id )
		{
			if ( string.IsNullOrEmpty( id ) )
				return null;

			for ( int i = 0; i < this._numSkills; i++ )
			{
				Skill skill = this.skills[i];
				if ( skill.id == id )
					return skill;
			}
			return null;
		}

		public bool CanCharmed()
		{
			if ( this.property.dashing > 0 ||
				this.property.repelling > 0 ||
				this.property.freezeFactor > 0 ||
				this.property.stunned > 0 ||
				this.isDead )
				return false;

			return true;
		}

		public bool CanMove()
		{
			if ( this.property.dashing > 0 ||
				this.property.repelling > 0 ||
				this.property.freezeFactor > 0 ||
				this.property.stunned > 0 ||
				this.property.charmed > 0 ||
				this.isDead )
				return false;

			if ( this.fsm.currState.type == FSMStateType.Attack )
				return this.usingSkill.canInterrupt && this.battle.time >= this.property.interruptTime;

			return true;
		}

		internal bool CanUseSkill( Skill skill )
		{
			if ( skill.buffs == null )
				return false;

			if ( !skill.CanUse() )
			{
				//LLogger.Log( "skill:{0} cooling time not reached.", skill.id );
				return false;
			}

			if ( this.property.dashing > 0 ||
				this.property.repelling > 0 ||
				this.property.freezeFactor > 0 ||
				this.property.stunned > 0 ||
				this.property.charmed > 0 ||
				this.isDead )
			{
				//LLogger.Log( "skill:{0} can not use on state:{1}.", skill.id , this.fsm.currState.type );
				return false;
			}

			if ( this.fsm.currState.type == FSMStateType.Attack )
			{
				if ( !this.usingSkill.canInterrupt ||
					 this.battle.time < this.property.interruptTime )
					return false;
			}
			return true;
		}

		public bool CanUseSkill( string id )
		{
			return this.CanUseSkill( this.GetSkill( id ) );
		}

		public bool WithinFov( Entity target )
		{
			return this.DistanceSqrtTo( target ) <= this.fov * this.fov;
		}

		public bool WithinSkillRange( Entity target, Skill skill )
		{
			float r = skill.distance;
			return this.DistanceSqrtTo( target ) <= r * r;
		}

		public void ChangeState( FSMStateType type, bool force = false, params object[] param )
		{
			this.fsm.ChangeState( type, force, param );
		}

		internal void CreateBuffState( string id, Buff buff )
		{
			BuffStateData buffStateData = ModelFactory.GetBuffStateData( id );

			//有免疫限制技能的属性,并且将要产生的state是限制类型,则不会生效
			if ( this.property.immuneDisables > 0 &&
				 buffStateData.beneficialType == BeneficialType.Debuff )
				return;

			bool create = true;
			if ( buffStateData.overlapType == BuffStateOverlapType.Replace ||
				 buffStateData.overlapType == BuffStateOverlapType.Unique )
			{
				BSBase bs = this.GetBuffState( id );
				if ( bs != null )
				{
					switch ( buffStateData.overlapType )
					{
						case BuffStateOverlapType.Replace:
							this.DestroyBuffStateImmediately( bs );
							break;

						case BuffStateOverlapType.Unique:
							bs.Unite();
							create = false;
							break;
					}
				}
			}
			if ( create )
			{
				SyncEventHelper.BuffStateAdded( this.rid, id, buff.rid );
				BSBase buffState = BSBase.Create( buffStateData.type );
				buffState.Init( id, this, buff );
				this._buffStates.Add( buffState );
			}
		}

		internal bool DestroyBuffState( BSBase bs )
		{
			if ( this._buffStatesToDestroy.Contains( bs ) )
			{
				LLogger.Warning( "BuffState {0} already in destroy list", bs.id );
				return false;
			}
			this._buffStatesToDestroy.Add( bs );
			return true;
		}

		private void DestroyBuffStateImmediately( BSBase buffState )
		{
			SyncEventHelper.BuffStateRemoved( this.rid, buffState.id );
			buffState.OnDestroy();
			this._buffStates.Remove( buffState );
			BuffStatePool.Push( buffState );
		}

		public void DestroyAllDisableBuffStates()
		{
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
			{
				BSBase buffState = this._buffStates[i];
				if ( buffState.beneficialType == BeneficialType.Debuff )
				{
					this.DestroyBuffStateImmediately( buffState );
					--i;
					--count;
				}
			}
		}

		public BSBase GetBuffState( string id )
		{
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
			{
				BSBase buffState = this._buffStates[i];
				if ( buffState.id == id )
					return buffState;
			}
			return null;
		}

		internal void OnEnterBuff( Buff buff )
		{
		}

		internal void OnExitBuff( Buff buff )
		{
			//跟随buff失效的state需要去掉
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
			{
				BSBase buffState = this._buffStates[i];
				if ( !MathUtils.Approximately( buffState.duration, -1f ) ||
					 buffState.buff != buff )
					continue;
				this.DestroyBuffStateImmediately( buffState );
				--i;
				--count;
			}
		}

		internal virtual void OnDamage( Buff buff, Bio target, float damage )
		{
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].OnDamage( buff, target, damage );
		}

		internal virtual void OnHurt( Buff buff, Bio attacker, float damage )
		{
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].OnHurt( buff, attacker, damage );
		}

		protected override void OnAttrChanged( Attr attr, object oldValue, object value )
		{
			base.OnAttrChanged( attr, oldValue, value );

			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].OnAttrChanged( attr, oldValue, value );
		}

		private void OnSkillAttrChanged( Skill skill, Attr attr, object oldValue, object value )
		{
			SyncEventHelper.SkillAttrChanged( this.rid, skill.id, attr, oldValue, value );
		}

		private void Die( Bio killer )
		{
			this._script?.Call( Script.S_ON_ENTITY_DIE );

			killer?.OnKillTarget( this );

			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].OnDie( killer );

			while ( this._buffStates.Count > 0 )
				this.DestroyBuffStateImmediately( this._buffStates[0] );

			this.UpdateVelocity( Vec3.zero );

			this.sensorySystem.Clear();

			this.property.Add( Attr.Gold, -this.goldBountyAwarded );

			SyncEventHelper.ChangeState( this.rid, FSMStateType.Dead );
			this.ChangeState( FSMStateType.Dead );

			this.brain.Rearbitrate();
			this.brain.enable = false;

			SyncEventHelper.EntityDie( this.rid, killer == null ? string.Empty : killer.rid );
		}

		private void OnKillTarget( Bio target )
		{
			int count = this._buffStates.Count;
			for ( int i = 0; i < count; i++ )
				this._buffStates[i].OnKill( target );

			this.property.Add( Attr.Gold, target.goldBountyAwarded );
			this.property.Add( Attr.Exp, target.expBountyAwarded );

			while ( this.property.exp >= this.upgradeExpNeeded )
			{
				this.Upgrade();
				//todo 重新计算属性
			}
		}

		public void UseSkill( Skill skill, Bio target, Vec3 targetPoint )
		{
			if ( !this.CanUseSkill( skill ) )
			{
				LLogger.Log( "skill:{0} can not use.", skill.id );
				return;
			}

			if ( skill.castType == CastType.Target &&
				 ( target == null || target.isDead ) )
				return;

			switch ( skill.castType )
			{
				case CastType.Target:
				case CastType.Point:
					this.Pursue( skill, target, skill.castType == CastType.Immediately ? this.property.position : targetPoint );
					break;

				case CastType.Immediately:
				case CastType.Dash:
					this.Attack( skill, target, skill.castType == CastType.Immediately ? this.property.position : targetPoint );
					break;
			}
		}

		public void Move( Vec3 targetPoint )
		{
			SyncEventHelper.ChangeState( this.rid, FSMStateType.Move, true );
			this.ChangeState( FSMStateType.Move, true, targetPoint );
		}

		public void Track( Bio target )
		{
			SyncEventHelper.ChangeState( this.rid, FSMStateType.Track, true );
			this.ChangeState( FSMStateType.Track, true, target );
		}

		public void Pursue( Skill skill, Bio target, Vec3 targetPoint )
		{
			SyncEventHelper.ChangeState( this.rid, FSMStateType.Pursue, true );
			this.ChangeState( FSMStateType.Pursue, true, skill, target, targetPoint );
		}

		public void Attack( Skill skill, Bio target, Vec3 targetPoint )
		{
			SyncEventHelper.ChangeState( this.rid, FSMStateType.Attack, true, skill.id, target == null ? string.Empty : target.rid, targetPoint );
			this.ChangeState( FSMStateType.Attack, true, skill, target, targetPoint );
		}

		public void Relive()
		{
			SyncEventHelper.Relive( this.rid, this.bornPosition, this.bornDirection );

			this.ChangeState( FSMStateType.Idle );
			this.property.Equal( Attr.Hp, this.property.mhp );
			this.property.Equal( Attr.Mana, this.property.mmana );
			this.property.Equal( Attr.Position, this.bornPosition );
			this.property.Equal( Attr.Direction, this.bornDirection );
			this.ActivePassiveBuffs();
			this.brain.enable = true;
			//todo 重新计算属性
		}

		private void Upgrade()
		{
			if ( this.property.lvl >= this._data.levels.Length - 1 )
				return;
			this.ApplyLevel( this.property.lvl + 1 );
		}

		private void ApplyLevel( int level )
		{
			this.property.ApplyLevel( level );
			EntityData.Level lvlDef = this._data.levels[level];
			this.reliveTime = lvlDef.reliveTime;
			this.reliveGold = lvlDef.reliveGold;
			this.goldBountyAwarded = lvlDef.goldBountyAwarded;
			this.expBountyAwarded = lvlDef.expBountyAwarded;
			this.upgradeExpNeeded = lvlDef.upgradeExpNeeded;
			this.upgradeSkillPointObtained = lvlDef.upgradeSkillPointObtained;
		}

		internal void UpgradeSkill( Skill skill )
		{
			if ( this.property.skillPoint < 0 )//todo 考虑不同等级技能花费不同的技能点
				return;

			this.property.Add( Attr.SkillPoint, -1 );
			skill.Upgrade();

			if ( skill.passiveBuffs != null )
			{
				int count = skill.passiveBuffs.Length;
				for ( int j = 0; j < count; j++ )
				{
					string buffId = skill.passiveBuffs[j];
					Buff buff = this.battle.GetBuff( this.rid, buffId );
					if ( buff != null )
						buff.markToDestroy = true;
					this.battle.CreateBuff( buffId, skill.id, skill.property.lvl, this, skill.rangeType == RangeType.Single ? this : null, this.property.position );
				}
			}
		}
	}
}