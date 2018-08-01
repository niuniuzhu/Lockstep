using System.Collections.Generic;
using Core.Misc;
using Logic;
using Logic.Controller;
using Logic.Event;
using Logic.Model;
using UnityEngine;
using View.Controller;
using View.Graphics;
using View.Input;
using View.Manager;
using View.Misc;
using Logger = Core.Misc.Logger;
using Utils = Logic.Misc.Utils;
using VectorHelper = View.Misc.VectorHelper;

namespace View
{
	public class VBattle : Interactive
	{
		public int frame { get; private set; }
		public float deltaTime { get; private set; }
		public float time { get; private set; }
		public BattleData data { get; private set; }
		public GodCamera camera { get; }
		public PointerInput input { get; }
		public Interaction interaction { get; }
		public GraphicManager graphicManager { get; private set; }

		private UpdateContext _context = new UpdateContext();
		private VEntityManager _entityManager;
		private VBuffManager _buffManager;
		private DebugDrawer _debugDrawer;

		public VBattle( BattleParams param )
		{
			this.collider = GameObject.Find( "Collider" ).GetComponent<BoxCollider>();
			this._rid = param.id;
			this.data = ModelFactory.GetBattleData( Utils.GetIDFromRID( this._rid ) );

			Camera mainCamera = GameObject.Find( "Camera" ).GetComponent<Camera>();
			this.camera = new GodCamera( mainCamera, this.data.camera );
			this.input = new PointerInput( mainCamera );
			this.input.RegisterInteractive( this );
			this.interaction = new Interaction( this );

			this._debugDrawer = new DebugDrawer();
			this.graphicManager = new GraphicManager();
			this._entityManager = new VEntityManager( this );
			this._buffManager = new VBuffManager( this );

			EventCenter.AddListener( SyncEventType.BATTLE_DESTROIED, this.HandleBattleDestroied );
			EventCenter.AddListener( SyncEventType.SPAWN_ENTITY, this.HandleSpawnEntity );
			EventCenter.AddListener( SyncEventType.DESPAWN_ENTITY, this.HandleDespawnEntity );
			EventCenter.AddListener( SyncEventType.ENTITY_ATTR_CHANGED, this.HandleEntityAttrChanged );
			EventCenter.AddListener( SyncEventType.SKILL_ATTR_CHANGED, this.HandleSkillAttrChanged );
			EventCenter.AddListener( SyncEventType.BUFF_ATTR_CHANGED, this.HandleBuffAttrChanged );
			EventCenter.AddListener( SyncEventType.ENTITY_ATTR_INITIALIZED, this.HandleEntityAttrInitialized );
			EventCenter.AddListener( SyncEventType.BUFF_ATTR_INITIALIZED, this.HandleBuffAttrInitialized );
			EventCenter.AddListener( SyncEventType.ENTITY_STATE_CHANGED, this.HandleEntityStateChanged );
			EventCenter.AddListener( SyncEventType.SPAWN_BUFF, this.HandleSpawnBuff );
			EventCenter.AddListener( SyncEventType.DESPAWN_BUFF, this.HandleDespawnBuff );
			EventCenter.AddListener( SyncEventType.ENTER_BUFF, this.HandleEnterBuff );
			EventCenter.AddListener( SyncEventType.EXIT_BUFF, this.HandleExitBuff );
			EventCenter.AddListener( SyncEventType.BUFF_TRIGGERED, this.HandleBuffTriggered );
			EventCenter.AddListener( SyncEventType.TRIGGER_TARGET, this.HandleTriggerTarget );
			EventCenter.AddListener( SyncEventType.BUFF_STATE_ADDED, this.HandleBuffStateAdded );
			EventCenter.AddListener( SyncEventType.BUFF_STATE_REMOVED, this.HandleBuffStateRemoved );
			EventCenter.AddListener( SyncEventType.BUFF_STATE_TRIGGERED, this.HandleBuffStateTriggered );
			EventCenter.AddListener( SyncEventType.ENTITY_RELIVE, this.HandleRelive );
			EventCenter.AddListener( SyncEventType.DAMAGE, this.HandleDamage );
			EventCenter.AddListener( SyncEventType.ENTITY_DIE, this.HandleEntityDie );
			EventCenter.AddListener( SyncEventType.MISSILE_COMPLETE, this.HandleMissileComplete );
			EventCenter.AddListener( SyncEventType.DEBUG_DRAW, this.HandleDebugDraw );
		}

		protected override void InternalDispose()
		{
			EventCenter.RemoveListener( SyncEventType.BATTLE_DESTROIED, this.HandleBattleDestroied );
			EventCenter.RemoveListener( SyncEventType.SPAWN_ENTITY, this.HandleSpawnEntity );
			EventCenter.RemoveListener( SyncEventType.DESPAWN_ENTITY, this.HandleDespawnEntity );
			EventCenter.RemoveListener( SyncEventType.ENTITY_ATTR_CHANGED, this.HandleEntityAttrChanged );
			EventCenter.RemoveListener( SyncEventType.SKILL_ATTR_CHANGED, this.HandleSkillAttrChanged );
			EventCenter.RemoveListener( SyncEventType.BUFF_ATTR_CHANGED, this.HandleBuffAttrChanged );
			EventCenter.RemoveListener( SyncEventType.ENTITY_ATTR_INITIALIZED, this.HandleEntityAttrInitialized );
			EventCenter.RemoveListener( SyncEventType.BUFF_ATTR_INITIALIZED, this.HandleBuffAttrInitialized );
			EventCenter.RemoveListener( SyncEventType.ENTITY_STATE_CHANGED, this.HandleEntityStateChanged );
			EventCenter.RemoveListener( SyncEventType.SPAWN_BUFF, this.HandleSpawnBuff );
			EventCenter.RemoveListener( SyncEventType.DESPAWN_BUFF, this.HandleDespawnBuff );
			EventCenter.RemoveListener( SyncEventType.ENTER_BUFF, this.HandleEnterBuff );
			EventCenter.RemoveListener( SyncEventType.EXIT_BUFF, this.HandleExitBuff );
			EventCenter.RemoveListener( SyncEventType.BUFF_TRIGGERED, this.HandleBuffTriggered );
			EventCenter.RemoveListener( SyncEventType.TRIGGER_TARGET, this.HandleTriggerTarget );
			EventCenter.RemoveListener( SyncEventType.BUFF_STATE_ADDED, this.HandleBuffStateAdded );
			EventCenter.RemoveListener( SyncEventType.BUFF_STATE_REMOVED, this.HandleBuffStateRemoved );
			EventCenter.RemoveListener( SyncEventType.BUFF_STATE_TRIGGERED, this.HandleBuffStateTriggered );
			EventCenter.RemoveListener( SyncEventType.ENTITY_RELIVE, this.HandleRelive );
			EventCenter.RemoveListener( SyncEventType.DAMAGE, this.HandleDamage );
			EventCenter.RemoveListener( SyncEventType.ENTITY_DIE, this.HandleEntityDie );
			EventCenter.RemoveListener( SyncEventType.MISSILE_COMPLETE, this.HandleMissileComplete );
			EventCenter.RemoveListener( SyncEventType.DEBUG_DRAW, this.HandleDebugDraw );

			this._debugDrawer = null;
			this._entityManager.Dispose();
			this._buffManager.Dispose();
			this.interaction.Dispose();
			this.input.UnregisterInteractive( this );
			this.input.Dispose();
			this.graphicManager.Dispose();
			this.graphicManager = null;
			this._entityManager = null;
			this._buffManager = null;
			this._context = null;
			this.data = null;

			base.InternalDispose();
		}

		internal List<VEntity> GetEntities()
		{
			return this._entityManager.entities;
		}

		public VEntity GetEntity( string rid )
		{
			return this._entityManager.GetEntity( rid );
		}

		public VBio GetBio( string rid )
		{
			return this._entityManager.GetBio( rid );
		}

		public VMissile GetMissile( string rid )
		{
			return this._entityManager.GetMissile( rid );
		}

		public Effect GetEffect( string rid )
		{
			return this._entityManager.GetEffect( rid );
		}

		internal Effect CreateEffect( string id )
		{
			EntityParam param = new EntityParam();
			param.rid = id + "@" + GuidHash.GetString();
			return this._entityManager.CreateEffect( param );
		}

		public VBuff CreateBuff( string buffId, string skillId, int lvl, string casterId, string targetId, Vector3 targetPoint )
		{
			VBio caster = this.GetBio( casterId );
			VBio target = this.GetBio( targetId );
			return this._buffManager.CreateBuff( buffId, skillId, lvl, caster, target, targetPoint );
		}

		public VBuff GetBuff( string rid )
		{
			if ( string.IsNullOrEmpty( rid ) )
				return null;
			VBuff buff = this._buffManager.Get( rid );
			if ( buff == null )
				Logger.Error( $"Buff:{rid} not exist" );
			return buff;
		}

		public void Update( float deltaTime )
		{
			++this.frame;
			this.deltaTime = deltaTime;
			this.time += this.deltaTime;

			this._context.deltaTime = this.deltaTime;
			this._context.time = this.time;
			this._context.frame = this.frame;

			this._buffManager.Update( this._context );
			this._entityManager.Update( this._context );
		}

		public void LateUpdate()
		{
			this.camera?.Update( this._context );
			this.input.Process();
		}

		public void OnDrawGizmos()
		{
			this._debugDrawer.Update();
		}

		private void HandleBattleDestroied( BaseEvent e )
		{
		}

		private void HandleSpawnEntity( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			EntityParam param = e.entityParam;
			switch ( e.entityType )
			{
				case "Bio":
					VEntity entity = CUser.id == param.uid
										 ? this._entityManager.CreatePlayer( param )
										 : this._entityManager.CreateBio( param );

					if ( entity == VPlayer.instance )
					{
						//hero.fsm.enableDebug = true;
						this.camera.target = entity;
					}
					break;

				case "Missile":
					this._entityManager.CreateMissile( param );
					break;

				case "FoxFire":
					this._entityManager.CreateBio<VFoxFire>( param );
					break;
			}
		}

		private void HandleDespawnEntity( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetEntity( e.targetId ).markToDestroy = true;
		}

		private void HandleEntityAttrChanged( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetEntity( e.targetId ).HandleAttrChanged( e.attr, e.attrOldValue, e.attrNewValue );
		}

		private void HandleSkillAttrChanged( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetBio( e.targetId ).HandleSkillAttrChanged( e.skillId, e.attr, e.attrOldValue, e.attrNewValue );
		}

		private void HandleBuffAttrChanged( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetBuff( e.buffId ).HandleAttrChanged( e.attr, e.attrOldValue, e.attrNewValue );
		}

		private void HandleEntityAttrInitialized( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetEntity( e.targetId ).HandleAttrInitialized();
		}

		private void HandleBuffAttrInitialized( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetBuff( e.buffId ).HandleAttrInitialized();
		}

		private void HandleEntityStateChanged( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			IFSMOwner entity = this.GetBio( e.targetId );
			entity.HandleEntityStateChanged( e.stateType, e.forceChange, e.stateParam );
		}

		private void HandleSpawnBuff( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.CreateBuff( e.buffId, e.skillId, e.lvl, e.casterId, e.targetId, e.position.ToVector3() );
		}

		private void HandleDespawnBuff( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this.GetBuff( e.buffId ).markToDestroy = true;
		}

		private void HandleEnterBuff( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			VBuff buff = this.GetBuff( e.buffId );
			VBio target = this.GetBio( e.targetId );
			target.HandleEnterBuff( buff );
		}

		private void HandleExitBuff( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			VBuff buff = this.GetBuff( e.buffId );
			VBio target = this.GetBio( e.targetId );
			target.HandleExitBuff( buff );
		}

		private void HandleBuffTriggered( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			VBuff buff = this.GetBuff( e.buffId );
			buff.HandleTriggered( e.triggerIndex );
		}

		private void HandleTriggerTarget( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			VBuff buff = this.GetBuff( e.buffId );
			VBio target = this.GetBio( e.targetId );
			target.HandleTrigger( buff, e.triggerIndex );
		}

		private void HandleBuffStateAdded( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio bio = this.GetBio( e.targetId );
			VBuff buff = this.GetBuff( e.buffId );
			bio.HandleBuffStateAdded( e.buffStateId, buff );
		}

		private void HandleBuffStateRemoved( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio bio = this.GetBio( e.targetId );
			bio.HandleBuffStateRemoved( e.buffStateId );
		}

		private void HandleBuffStateTriggered( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio bio = this.GetBio( e.targetId );
			bio.HandleBuffStateTriggered( e.buffStateId, e.triggerIndex );
		}

		private void HandleRelive( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio bio = this.GetBio( e.targetId );
			bio.HandleRelive( e.position.ToVector3(), e.direction.ToVector3() );
		}

		private void HandleDamage( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio caster = this.GetBio( e.casterId );
			VBio target = this.GetBio( e.targetId );
			VBuff buff = this.GetBuff( e.buffId );
			caster.HandleDamage( buff, target, e.f0 );
			target.HandleHurt( buff, caster, e.f0 );
		}

		private void HandleEntityDie( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VBio bio = this.GetBio( e.targetId );
			VBio killer = this.GetBio( e.casterId );
			bio.HandleDie( killer );
		}

		private void HandleMissileComplete( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;

			VMissile missile = this.GetMissile( e.missileId );
			missile.HandleComplete( e.position.ToVector3(), e.direction.ToVector3() );
		}

		private void HandleDebugDraw( BaseEvent baseEvent )
		{
			SyncEvent e = ( SyncEvent )baseEvent;
			this._debugDrawer.HandleDebugDraw( e.debugDrawType, e.dv1.ToVector3(), e.dv2.ToVector3(), VectorHelper.ToVector3Array( e.dvs ), e.df, e.dc.ToColor() );
		}
	}
}
