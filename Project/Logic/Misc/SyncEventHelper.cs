using Core.Math;
using Logic.Controller;
using Logic.Event;
using Logic.FSM;
using Logic.Property;

namespace Logic.Misc
{
	public static class SyncEventHelper
	{
		public static void DestroyBattle()
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BATTLE_DESTROIED;
			e.BeginInvoke();
		}

		public static void SpawnEntity( string type, EntityParam param )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.SPAWN_ENTITY;
			e.entityType = type;
			e.entityParam = param;
			e.BeginInvoke();
		}

		public static void DespawnEntity( string entityId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.DESPAWN_ENTITY;
			e.targetId = entityId;
			e.BeginInvoke();
		}

		public static void EntityAttrInitialized( string entityId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTITY_ATTR_INITIALIZED;
			e.targetId = entityId;
			e.BeginInvoke();
		}

		public static void BuffAttrInitialized( string buffId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_ATTR_INITIALIZED;
			e.buffId = buffId;
			e.BeginInvoke();
		}

		public static void EntityAttrChanged( string targetId, Attr attr, object oldValue, object newValue )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTITY_ATTR_CHANGED;
			e.targetId = targetId;
			e.attr = attr;
			e.attrOldValue = oldValue;
			e.attrNewValue = newValue;
			e.BeginInvoke();
		}

		public static void SkillAttrChanged( string targetId, string skillId, Attr attr, object oldValue, object newValue )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.SKILL_ATTR_CHANGED;
			e.targetId = targetId;
			e.skillId = skillId;
			e.attr = attr;
			e.attrOldValue = oldValue;
			e.attrNewValue = newValue;
			e.BeginInvoke();
		}

		public static void BuffAttrChanged( string buffId, Attr attr, object oldValue, object newValue )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_ATTR_CHANGED;
			e.buffId = buffId;
			e.attr = attr;
			e.attrOldValue = oldValue;
			e.attrNewValue = newValue;
			e.BeginInvoke();
		}

		public static void SpawnBuff( string buffId, string skillId, int lvl, string casterId, string targetId, Vec3 targetPoint )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.SPAWN_BUFF;
			e.buffId = buffId;
			e.lvl = lvl;
			e.skillId = skillId;
			e.casterId = casterId;
			e.targetId = targetId;
			e.position = targetPoint;
			e.BeginInvoke();
		}

		public static void DespawnBuff( string buffId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.DESPAWN_BUFF;
			e.buffId = buffId;
			e.BeginInvoke();
		}

		public static void EnterBuff( string buffId, string targetId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTER_BUFF;
			e.buffId = buffId;
			e.targetId = targetId;
			e.BeginInvoke();
		}

		public static void ExitBuff( string buffId, string targetId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.EXIT_BUFF;
			e.buffId = buffId;
			e.targetId = targetId;
			e.BeginInvoke();
		}

		public static void BuffTriggered( string buffId, int triggerIndex )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_TRIGGERED;
			e.buffId = buffId;
			e.triggerIndex = triggerIndex;
			e.BeginInvoke();
		}

		public static void TriggerTarget( string buffId, string targetId, int triggerIndex )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.TRIGGER_TARGET;
			e.buffId = buffId;
			e.targetId = targetId;
			e.triggerIndex = triggerIndex;
			e.BeginInvoke();
		}

		public static void ChangeState( string targetId, FSMStateType type, bool force = false, params object[] param )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTITY_STATE_CHANGED;
			e.targetId = targetId;
			e.stateType = type;
			e.forceChange = force;
			e.stateParam = param;
			e.BeginInvoke();
		}

		public static void EntityDie( string targetId, string killerId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTITY_DIE;
			e.targetId = targetId;
			e.casterId = killerId;
			e.BeginInvoke();
		}

		public static void Damage( string buffId, float damage, string casterId, string targetId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.DAMAGE;
			e.buffId = buffId;
			e.targetId = targetId;
			e.casterId = casterId;
			e.f0 = damage;
			e.BeginInvoke();
		}

		public static void Relive( string targetId, Vec3 position, Vec3 direction )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.ENTITY_RELIVE;
			e.targetId = targetId;
			e.position = position;
			e.direction = direction;
			e.BeginInvoke();
		}

		public static void BuffStateAdded( string targetId, string id, string buffId )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_STATE_ADDED;
			e.targetId = targetId;
			e.buffStateId = id;
			e.buffId = buffId;
			e.BeginInvoke();
		}

		public static void BuffStateRemoved( string targetId, string id )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_STATE_REMOVED;
			e.targetId = targetId;
			e.buffStateId = id;
			e.BeginInvoke();
		}

		public static void BuffStateTriggered( string targetId, string id, int triggerIndex )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.BUFF_STATE_TRIGGERED;
			e.targetId = targetId;
			e.buffStateId = id;
			e.triggerIndex = triggerIndex;
			e.BeginInvoke();
		}

		public static void HandleMissileComplete( string missileId, Vec3 position, Vec3 direction )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.MISSILE_COMPLETE;
			e.missileId = missileId;
			e.position = position;
			e.direction = direction;
			e.BeginInvoke();
		}

		public static void DebugDraw( SyncEvent.DebugDrawType type, Vec3 v0, Vec3 v1, Vec3[] dvs, float f, Color4 color )
		{
			SyncEvent e = SyncEvent.Get();
			e.type = SyncEventType.DEBUG_DRAW;
			e.debugDrawType = type;
			e.dv1 = v0;
			e.dv2 = v1;
			e.dvs = dvs;
			e.df = f;
			e.dc = color;
			e.BeginInvoke();
		}
	}
}