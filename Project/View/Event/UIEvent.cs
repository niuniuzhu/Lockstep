using System.Collections.Generic;
using Logic.Controller;
using Logic.Event;
using Logic.Property;
using Protocol.Gen;
using View.Controller;

namespace View.Event
{
	public static class UIEventType
	{
		public const int ENTITY_CREATED = 10000;
		public const int ENTITY_DESTROIED = 10001;
		public const int ENTITY_DIE = 10002;
		public const int PICK_SKILL = 10010;
		public const int DROP_SKILL = 10011;
		public const int SKILL_ATTR_CHANGED = 10030;
		public const int SKILL_USE_FAILED = 10031;
		public const int ATTR_CHANGED = 10032;
		public const int HURT = 10033;
		public const int BATTLE_END = 10100;
	}

	public class UIEvent : BaseEvent
	{
		#region pool support
		private static readonly Stack<UIEvent> POOL = new Stack<UIEvent>();

		private static UIEvent Get()
		{
			return POOL.Count > 0 ? POOL.Pop() : new UIEvent();
		}

		private static void Release( UIEvent element )
		{
			POOL.Push( element );
		}

		public static void EntityCreated( VEntity target )
		{
			UIEvent e = Get();
			e.type = UIEventType.ENTITY_CREATED;
			e.entity = target;
			e.Invoke();
		}

		public static void EntityDestroied( VEntity target )
		{
			UIEvent e = Get();
			e.type = UIEventType.ENTITY_DESTROIED;
			e.entity = target;
			e.Invoke();
		}

		public static void PickSkill( Skill skill )
		{
			UIEvent e = Get();
			e.type = UIEventType.PICK_SKILL;
			e.skill = skill;
			e.Invoke();
		}

		public static void DropSkill()
		{
			UIEvent e = Get();
			e.type = UIEventType.DROP_SKILL;
			e.Invoke();
		}

		public static void Hurt( VBuff buff, VBio caster, VBio target, float damage )
		{
			UIEvent e = Get();
			e.type = UIEventType.HURT;
			e.buff = buff;
			e.entity = caster;
			e.target = target;
			e.f0 = damage;
			e.Invoke();
		}

		public static void AttrChanged( VEntity target, Attr attr, object oldValue, object newValue )
		{
			UIEvent e = Get();
			e.type = UIEventType.ATTR_CHANGED;
			e.entity = target;
			e.attr = attr;
			e.o0 = oldValue;
			e.o1 = newValue;
			e.Invoke();
		}

		public static void SkillUseFailed( VEntity caster, Skill skill, VEntity target )
		{
			UIEvent e = Get();
			e.type = UIEventType.SKILL_USE_FAILED;
			e.entity = caster;
			e.skill = skill;
			e.target = target;
			e.Invoke();
		}

		public static void EntityDie( VEntity target, VEntity killer )
		{
			UIEvent e = Get();
			e.type = UIEventType.ENTITY_DIE;
			e.entity = target;
			e.killer = killer;
			e.Invoke();
		}

		public static void SkillAttrChanged( VEntity target, Skill skill, Attr attr, object oldValue, object newValue )
		{
			UIEvent e = Get();
			e.type = UIEventType.SKILL_ATTR_CHANGED;
			e.entity = target;
			e.skill = skill;
			e.attr = attr;
			e.o0 = oldValue;
			e.o1 = newValue;
			e.Invoke();
		}

		public static void BattleEnd( int team )
		{
			UIEvent e = Get();
			e.type = UIEventType.BATTLE_END;
			e.i0 = team;
			e.Invoke();
		}

		public override void Release()
		{
			this.entity = null;
			this.killer = null;
			this.skill = null;
			Release( this );

		}
		#endregion

		#region all values
		public int i0;
		public int i1;
		public float f0;
		public float f1;
		public bool b0;
		public bool b1;
		public object o0;
		public object o1;
		#endregion

		#region entity created/destroied event
		public VEntity entity;
		public VEntity target;
		public VEntity killer;
		public VBuff buff;
		#endregion

		#region use skill
		public Skill skill;
		#endregion

		public Attr attr;
	}
}