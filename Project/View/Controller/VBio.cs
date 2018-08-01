using System.Collections.Generic;
using Logic;
using Logic.Controller;
using Logic.FSM;
using Logic.Model;
using Logic.Property;
using UnityEngine;
using View.BuffStateImpl;
using View.Event;
using View.FSM.Actions;
using Logger = Core.Misc.Logger;

namespace View.Controller
{
	public class VBio : VEntity, IFSMOwner
	{
		public int reliveTime { get; private set; }
		public int reliveGold { get; private set; }
		public int goldBountyAwarded { get; private set; }
		public int expBountyAwarded { get; private set; }
		public int upgradeExpNeeded { get; private set; }
		public int upgradeSkillPointObtained { get; private set; }

		public Skill[] skills { get; private set; }
		public bool isDead => this.fsm.currState.type == FSMStateType.Dead;
		public bool canInteractive => ( this.flag & ( EntityFlag.Hero | EntityFlag.SmallPotato | EntityFlag.Structure ) ) > 0;
		public Skill usingSkill { get; internal set; }
		public FiniteStateMachine fsm { get; private set; }

		public Skill commonSkill { get; private set; }

		public int numSkills { get; private set; }

		private readonly Dictionary<string, VBSBase> _buffStates = new Dictionary<string, VBSBase>();

		public override void Init( VBattle battle )
		{
			base.Init( battle );

			this.skills = new Skill[6];
			for ( int i = 0; i < this.skills.Length; i++ )
				this.skills[i] = new Skill();

			this.fsm = new FiniteStateMachine( this );
			FSMState state = this.fsm.CreateState( FSMStateType.Idle );
			state.CreateAction<VIdle>();
			state = this.fsm.CreateState( FSMStateType.Dead );
			state.CreateAction<VDead>();
			state = this.fsm.CreateState( FSMStateType.Move );
			state.CreateAction<VMove>();
			state = this.fsm.CreateState( FSMStateType.Track );
			state.CreateAction<VMove>();
			state = this.fsm.CreateState( FSMStateType.Pursue );
			state.CreateAction<VMove>();
			state = this.fsm.CreateState( FSMStateType.Attack );
			state.CreateAction<VAttack>();
		}

		protected override void InternalDispose()
		{
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

			if ( this._data.skills != null )
			{
				this.numSkills = this._data.skills.Length;
				for ( int i = 0; i < this.numSkills; i++ )
				{
					Skill skill = this.skills[i];
					skill.OnCreated( this._data.skills[i], this._rid );
					if ( skill.isCommon )
					{
						this.commonSkill = skill;
						skill.Upgrade();//普攻默认等级1
					}
				}
			}
			this.ApplyLevel( 0 );
			if ( this.canInteractive )
			{
				this.collider = this.graphic.CreateCollider();
				this.battle.input.RegisterInteractive( this );
			}
			this.fsm.Start();
		}

		protected override void InternalOnRemoveFromBattle()
		{
			this.graphic.DestroyCollider();

			this.fsm.Stop();

			this.battle.input.UnregisterInteractive( this );

			this.usingSkill = null;
			this.numSkills = 0;

			base.InternalOnRemoveFromBattle();
		}

		public override void UpdateState( UpdateContext context )
		{
			base.UpdateState( context );

			this.fsm.Update( context );

			foreach ( KeyValuePair<string, VBSBase> kv in this._buffStates )
				kv.Value.Update( context );
		}

		public Skill GetSkill( string id )
		{
			Skill skill = null;
			if ( !string.IsNullOrEmpty( id ) )
			{
				for ( int i = 0; i < this.numSkills; i++ )
				{
					skill = this.skills[i];
					if ( skill.id == id )
						break;
				}
			}
			if ( skill == null )
				Logger.Error( $"Skill:{id} not exist" );
			return skill;
		}

		public bool CanUseSkill( Skill skill )
		{
			if ( skill.buffs == null )
				return false;

			if ( !skill.CanUse() )
				return false;

			if ( this.property.dashing > 0 ||
				 this.property.repelling > 0 ||
				 this.property.freezeFactor > 0 ||
				 this.property.stunned > 0 ||
				 this.property.charmed > 0 ||
				 this.isDead )
				return false;

			if ( this.fsm.currState.type == FSMStateType.Attack )
			{
				if ( !this.usingSkill.canInterrupt ||
					 this.battle.time < this.property.interruptTime )
					return false;
			}

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
			{
				if ( !this.usingSkill.canInterrupt ||
					 this.battle.time < this.property.interruptTime )
					return false;
			}
			return true;
		}

		private void ApplyLevel( int level )
		{
			EntityData.Level lvlDef = this._data.levels[level];
			this.reliveTime = lvlDef.reliveTime;
			this.reliveGold = lvlDef.reliveGold;
			this.goldBountyAwarded = lvlDef.goldBountyAwarded;
			this.expBountyAwarded = lvlDef.expBountyAwarded;
			this.upgradeExpNeeded = lvlDef.upgradeExpNeeded;
			this.upgradeSkillPointObtained = lvlDef.upgradeSkillPointObtained;
		}

		public void ChangeState( FSMStateType type, bool force = false, params object[] param )
		{
			this.fsm.ChangeState( type, force, param );
		}

		private void UpdateStealth()
		{
			if ( this.property.stealth > 0 )
			{
				if ( VEntityUtils.IsAllied( this, VPlayer.instance ) )
					this.graphic.material.Translucent( 0.4f );
				else
					this.graphic.visible = false;
			}
			else
			{
				if ( VEntityUtils.IsAllied( this, VPlayer.instance ) )
					this.graphic.material.SetDefaultMaterial( false );
				else
					this.graphic.visible = true;
			}
		}

		public void HandleEntityStateChanged( FSMStateType stateType, bool forceChange, params object[] stateParam )
		{
			this.ChangeState( stateType, forceChange, stateParam );
		}

		public void HandleRelive( Vector3 position, Vector3 direction )
		{
			this.battle.camera.BSCOff();

			Effect fx = this.battle.CreateEffect( "e164" );
			fx.SetupTerritory( this, null, Vector3.zero );

			this.ChangeState( FSMStateType.Idle );
		}

		internal override void HandleAttrChanged( Attr attr, object oldValue, object newValue )
		{
			base.HandleAttrChanged( attr, oldValue, newValue );
			switch ( attr )
			{
				case Attr.Lvl:
					if ( ( int )newValue > ( int )oldValue )
					{
						Effect fx = this.battle.CreateEffect( "e164" );
						fx.SetupTerritory( this, null, Vector3.zero );
						this.ApplyLevel( this.property.lvl );
					}
					break;

				case Attr.Stealth:
					this.UpdateStealth();
					break;

				case Attr.Stunned:
					if ( ( int )newValue > 0 )
						this.ChangeState( FSMStateType.Idle );
					break;
			}

			foreach ( KeyValuePair<string, VBSBase> kv in this._buffStates )
				kv.Value.OnAttrChanged( attr, oldValue, newValue );

			UIEvent.AttrChanged( this, attr, oldValue, newValue );
		}

		internal void HandleSkillAttrChanged( string skillId, Attr attr, object oldValue, object newValue )
		{
			Skill skill = this.GetSkill( skillId );
			skill.property.Equal( attr, newValue );
			switch ( attr )
			{
				case Attr.Lvl:
					skill.ApplyLevel( skill.property.lvl );
					break;
			}

			UIEvent.SkillAttrChanged( this, skill, attr, oldValue, newValue );
		}

		public void HandleDamage( VBuff buff, VBio target, float damage )
		{
			foreach ( KeyValuePair<string, VBSBase> kv in this._buffStates )
				kv.Value.OnDamage( buff, target, damage );
		}

		public void HandleHurt( VBuff buff, VBio caster, float damage )
		{
			foreach ( KeyValuePair<string, VBSBase> kv in this._buffStates )
				kv.Value.OnHurt( buff, caster, damage );
			UIEvent.Hurt( buff, caster, this, damage );
		}

		public void HandleDie( VBio killer )
		{
			if ( this is VPlayer )
			{
				( ( VPlayer )this ).SetTracingTarget( null );
				this.battle.camera.BSCOn();
			}
			else if ( VPlayer.instance.tracingTarget == this )
				VPlayer.instance.SetTracingTarget( null );
			UIEvent.EntityDie( this, killer );
		}

		public void HandleEnterBuff( VBuff buff )
		{
		}

		public void HandleExitBuff( VBuff buff )
		{
		}

		public void HandleTrigger( VBuff buff, int triggerIndex )
		{
			string[] fxss = buff.trigger?.fxs;
			if ( fxss == null )
				return;

			int index = fxss.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			string fxId = fxss[index];
			if ( !string.IsNullOrEmpty( fxId ) )
			{
				Effect fx = this.battle.CreateEffect( fxId );
				fx.SetupTerritory( buff.caster, this, Vector3.zero );
			}
		}

		public void HandleBuffStateAdded( string id, VBuff buff )
		{
			BuffStateData buffStateData = ModelFactory.GetBuffStateData( id );
			VBSBase buffState = VBSBase.Create( buffStateData.type );
			buffState.Init( id, buff, this );
			this._buffStates[id] = buffState;
		}

		public void HandleBuffStateRemoved( string id )
		{
			if ( !this._buffStates.TryGetValue( id, out VBSBase buffState ) )
				return;
			buffState.OnDestroy();
			this._buffStates.Remove( id );
		}

		public void HandleBuffStateTriggered( string id, int triggerIndex )
		{
			if ( !this._buffStates.TryGetValue( id, out VBSBase buffState ) )
				return;
			buffState.HandleTriggered( triggerIndex );
		}
	}
}