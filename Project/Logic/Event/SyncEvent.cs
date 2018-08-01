using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.FSM;
using Logic.Property;

namespace Logic.Event
{
	public static class SyncEventType
	{
		public const int BATTLE_DESTROIED = 0;
		public const int SPAWN_ENTITY = 1;
		public const int DESPAWN_ENTITY = 2;
		public const int ENTITY_STATE_CHANGED = 3;
		public const int ENTITY_ATTR_CHANGED = 4;
		public const int SKILL_ATTR_CHANGED = 5;
		public const int BUFF_ATTR_CHANGED = 6;
		public const int ENTITY_ATTR_INITIALIZED = 7;
		public const int BUFF_ATTR_INITIALIZED = 8;
		public const int SPAWN_BUFF = 11;
		public const int DESPAWN_BUFF = 12;
		public const int ENTER_BUFF = 13;
		public const int EXIT_BUFF = 14;
		public const int BUFF_TRIGGERED = 15;
		public const int TRIGGER_TARGET = 16;
		public const int ENTITY_DIE = 20;
		public const int ENTITY_RELIVE = 21;
		public const int DAMAGE = 43;
		public const int BUFF_STATE_ADDED = 50;
		public const int BUFF_STATE_REMOVED = 51;
		public const int BUFF_STATE_TRIGGERED = 52;
		public const int MISSILE_COMPLETE = 60;

		public const int SET_FRAME_ACTION = 98;
		public const int DEBUG_DRAW = 99;
	}

	public class SyncEvent : BaseEvent
	{
		#region pool support
		private static readonly Stack<SyncEvent> POOL = new Stack<SyncEvent>();

		private static readonly object LOCK_OBJ = new object();

		public static SyncEvent Get()
		{
			lock ( LOCK_OBJ )
			{
				if ( POOL.Count > 0 )
					return POOL.Pop();
			}
			return new SyncEvent();
		}

		private static void Release( SyncEvent element )
		{
			lock ( LOCK_OBJ )
			{
				POOL.Push( element );
			}
		}
		#endregion

		public override void Release()
		{
			Release( this );
		}

		public static void HandleFrameAction()
		{
			SyncEvent e = Get();
			e.type = SyncEventType.SET_FRAME_ACTION;
			e.BeginInvoke();
		}

		#region global
		public string entityType;
		public EntityParam entityParam;
		public string casterId;
		public string targetId;
		public string missileId;
		public string skillId;
		public string buffId;
		public int lvl;
		public int i0;
		public float f0;
		#endregion

		#region entity position/rotation sync
		public Vec3 position;
		public Vec3 direction;
		#endregion

		#region fsm state
		public FSMStateType stateType;
		public bool forceChange;
		public object[] stateParam;
		#endregion

		#region buff
		public string buffStateId;
		public int triggerIndex;
		#endregion

		#region attr
		public Attr attr;
		public object attrOldValue;
		public object attrNewValue;
		#endregion
		
		#region debug
		public enum DebugDrawType
		{
			Ray,
			Line,
			Cube,
			Sphere,
			WireCube,
			WireSphere,
			Path
		}
		public DebugDrawType debugDrawType;
		public Vec3 dv1;
		public Vec3 dv2;
		public Vec3[] dvs;
		public Color4 dc;
		public float df;
		#endregion
	}
}