using Logic.Misc;
using Logic.Model;
using Logic.Property;

namespace Logic.Controller
{
	public class Skill : GPoolObject
	{
		public delegate void AttrChangedHandler( Skill sender, Attr attr, object oldValue, object newValue );

		public event AttrChangedHandler OnAttrChanged;

		public float atkTime { get; private set; }
		public float sufTime { get; private set; }
		public float firingTime { get; private set; }
		public string action { get; private set; }
		public float actionLength { get; private set; }
		public float dashStartSpeed { get; private set; }
		public AnimationCurve dashSpeedCurve { get; private set; }
		public float distance { get; private set; }
		public float cd { get; private set; }
		public float manaCost { get; private set; }
		public string atkFx { get; private set; }
		public float atkFxTime { get; private set; }
		public string atkSound { get; private set; }
		public string missile { get; private set; }

		public string id => this._data.id;
		public string name => this._data.name;
		public string desc => this._data.desc;
		public bool isCommon => this._data.isCommon;
		public CastType castType => this._data.castType;
		public CampType campType => this._data.campType;
		public RangeType rangeType => this._data.rangeType;
		public EntityFlag targetFlag => this._data.targetFlag;
		public bool canInterrupt => this._data.canInterrupt;
		public bool ignoreObstacles => this._data.ignoreObstacles;
		public string icon => this._data.icon;
		public string[] passiveBuffs => this._data.passiveBuffs;
		public string[] buffs => this._data.buffs;

		public SkillProperty property { get; private set; }

		private SkillData _data;

		public Skill()
		{
			this.property = new SkillProperty();
			this.property.OnChanged += this._OnAttrChanged;
		}

		protected override void InternalDispose()
		{
			this.property.OnChanged -= this._OnAttrChanged;
			this.property = null;
			this._data = null;
		}

		public void OnCreated( string id, string ownerRid )
		{
			this._data = ModelFactory.GetSkillData( id );
			this._rid = $"{ownerRid}#{id}";
			this.property.Init( this._data );
		}

		public bool CanUse()
		{
			return this.property.lvl >= 0 && ( this.property.allowUseWhenCooldown > 0 || this.IsCooldownComplete() );
		}

		public bool IsCooldownComplete()
		{
			return this.property.cooldown <= 0.05f;
		}

		public void Upgrade()
		{
			if ( this.property.lvl >= this._data.levels.Length - 1 )
				return;
			this.ApplyLevel( this.property.lvl + 1 );
		}

		public void ApplyLevel( int level )
		{
			this.property.ApplyLevel( level );
			SkillData.Level lvlDef = this._data.levels[level];
			this.atkTime = lvlDef.atkTime;
			this.sufTime = lvlDef.sufTime;
			this.firingTime = lvlDef.firingTime;
			this.action = lvlDef.action;
			this.actionLength = lvlDef.actionLength;
			this.dashStartSpeed = lvlDef.dashStartSpeed;
			this.dashSpeedCurve = lvlDef.dashSpeedCurve;
			this.distance = lvlDef.distance;
			this.manaCost = lvlDef.manaCost;
			this.cd = lvlDef.cooldown;
			this.atkFx = lvlDef.atkFx;
			this.atkFxTime = lvlDef.atkFxTime;
			this.atkSound = lvlDef.atkSound;
			this.missile = lvlDef.missile;
		}

		private void _OnAttrChanged( Attr attr, object oldValue, object newValue )
		{
			this.OnAttrChanged?.Invoke( this, attr, oldValue, newValue );
		}

		internal void Update( float dt )
		{
			this.property.Add( Attr.Cooldown, -dt );
		}

		//internal int StatisticsSkillDamage()
		//{
		//	if ( this.isPassive || this.lvl < 0 )
		//		return 0;

		//	int damage = 0;
		//	BuffData buffData = ModelFactory.GetBuffData( this.buff );

		//	BuffData.InOut enter = buffData.enter[this.lvl];
		//	if ( enter != null )
		//		StatisticsAffectsDamage( enter.states, ref damage );

		//	BuffData.InOut exit = buffData.exit[this.lvl];
		//	if ( exit != null )
		//		StatisticsAffectsDamage( exit.states, ref damage );

		//	BuffData.Trigger trigger = buffData.trigger[this.lvl];
		//	if ( trigger != null )
		//	{
		//		BuffData.CustomTrigger[] customTriggers = trigger.customTriggers;
		//		if ( customTriggers != null )
		//		{
		//			int c1 = customTriggers.Length;
		//			for ( int i = 0; i < c1; i++ )
		//				StatisticsAffectsDamage( customTriggers[i].states, ref damage );
		//		}
		//		if ( trigger.states != null )
		//			StatisticsAffectsDamage( trigger.states, ref damage );
		//	}
		//	return damage;
		//}

		//private static void StatisticsAffectsDamage( BuffData.State[] states, ref int damage )
		//{
		//	int count = states.Length;
		//	for ( int i = 0; i < count; i++ )
		//	{
		//		BuffData.State state = states[i];
		//		int c2 = state.attrs.Length;
		//		for ( int j = 0; j < c2; j++ )
		//		{
		//			Attr attr = state.attrs[j];
		//			float value = state.values[i];
		//			if ( attr != Attr.Hp )
		//				continue;

		//			if ( value < 0 )
		//				damage -= ( int ) value;
		//		}
		//	}
		//}
	}
}