using Core.Math;
using Logic.BuffImpl;
using Logic.Misc;
using Logic.Model;
using Logic.Property;
using Utils = Logic.Misc.Utils;

namespace Logic.Controller
{
	public class Buff : GPoolObject
	{
		public float radius { get; private set; }
		public string areaFx { get; private set; }
		public float[] extra { get; private set; }
		public string[] extra_s { get; private set; }
		public float duration { get; private set; }
		public float speed { get; private set; }
		public int maxTriggerTargets { get; private set; }
		public int perTargetTriggerCount { get; private set; }
		public int maxTriggerCount { get; private set; }
		public BuffData.Trigger trigger { get; private set; }
		public DeadType deadType { get; private set; }

		public string id => this._data.id;
		public SpawnPoint spawnPoint => this._data.spawnPoint;
		public Orbit orbit => this._data.orbit;
		public bool canInterrupt => this._data.canInterrupt;
		public string[] enterStates => this._data.enterStates;
		public string[] triggerStates => this._data.triggerStates;

		public Battle battle { get; private set; }
		public BuffProperty property { get; private set; }

		public SkillData skillData { get; private set; }
		public Bio caster { get; private set; }
		public Bio target { get; private set; }
		public Vec3 targetPoint { get; private set; }
		public CampType campType { get; private set; }
		public EntityFlag targetFlag { get; private set; }
		public RangeType rangeType { get; private set; }

		internal bool markToDestroy;

		private BuffData _data;
		private BIBase _implement;

		internal virtual void Init( Battle battle )
		{
			this.battle = battle;
			this.property = new BuffProperty();
			this.property.OnChanged += this.OnAttrChanged;
		}

		protected override void InternalDispose()
		{
			this.property.OnChanged -= this.OnAttrChanged;
			this.property = null;
			this.battle = null;
			this._data = null;
		}

		internal void OnAddedToBattle( string rid, string skillId, int lvl, Bio caster, Bio target, Vec3 targetPoint )
		{
			SyncEventHelper.SpawnBuff( rid, skillId, lvl, caster.rid, target == null ? string.Empty : target.rid, targetPoint );
			this._rid = rid;
			this._data = ModelFactory.GetBuffData( Utils.GetIDFromRID( this._rid ) );
			this.skillData = ModelFactory.GetSkillData( skillId );
			this.campType = this._data.campType == 0 ? this.skillData.campType : this._data.campType;
			this.targetFlag = this._data.targetFlag == 0 ? this.skillData.targetFlag : this._data.targetFlag;
			this.rangeType = this._data.rangeType == 0 ? this.skillData.rangeType : this._data.rangeType;
			this.caster = caster;
			this.target = target;
			if ( this.target == null &&
				 ( this.campType & CampType.Self ) > 0 )
				this.target = caster;
			this.caster.AddRef();
			this.target?.AddRef();
			this.targetPoint = targetPoint;
			this.deadType = this._data.deadType;

			if ( this.target == null )
			{
				if ( this.deadType == DeadType.WithMainTarget )
					LLogger.Error( "Dead_type of the buff that has no target can not set to DeadType.WithMainTarget" );

				if ( this.skillData.rangeType == RangeType.Single )
					LLogger.Error( "Range_type of the buff that has no target can not set to RangeType.Single" );

				if ( this.orbit == Orbit.FollowTarget )
					LLogger.Error( "Orbit_type of the buff that has no target can not set to Orbit.FollowTarget" );
			}
			else
			{
				this.deadType = DeadType.WithMainTarget;
			}

			this.property.Init( this._data );
			this.ApplyLevel( lvl );
			targetPoint = this.target?.property.position ?? this.targetPoint;
			switch ( this.spawnPoint )
			{
				case SpawnPoint.Target:
					this.property.Equal( Attr.Position, targetPoint );
					break;

				case SpawnPoint.Caster:
					this.property.Equal( Attr.Position, this.caster.property.position );
					break;
			}
			this.property.Equal( Attr.Direction, targetPoint == this.caster.property.position
														 ? this.caster.property.direction
														 : Vec3.Normalize( targetPoint - this.caster.property.position ) );

			SyncEventHelper.BuffAttrInitialized( this.rid );

			this._implement = BIBase.Create( this.id );
			this._implement.Init( this );
		}

		internal void OnRemoveFromBattle()
		{
			this._implement.OnDestroy();
			BuffImplPool.Push( this._implement );
			this._implement = null;
			this.target?.RedRef();
			this.caster.RedRef();
			this.target = null;
			this.caster = null;
			this.skillData = null;
			this.markToDestroy = false;
			SyncEventHelper.DespawnBuff( this.rid );
		}

		private void ApplyLevel( int level )
		{
			this.property.ApplyLevel( level );
			BuffData.Level lvlDef = this._data.levels[level];
			this.radius = lvlDef.radius;
			this.areaFx = lvlDef.areaFx;
			this.extra = lvlDef.extra;
			this.extra_s = lvlDef.extra_s;
			this.duration = lvlDef.duration;
			this.speed = lvlDef.speed;
			this.maxTriggerTargets = lvlDef.maxTriggerTargets;
			this.perTargetTriggerCount = lvlDef.perTargetTriggerCount;
			this.maxTriggerCount = lvlDef.maxTriggerCount;
			this.trigger = lvlDef.trigger;
		}

		private void OnAttrChanged( Attr attr, object oldvalue, object newvalue )
		{
			SyncEventHelper.BuffAttrChanged( this.rid, attr, oldvalue, newvalue );
		}

		internal void UpdateState( UpdateContext context )
		{
			this._implement.Update( context );
		}

		//public void OnAttrChanged( Attr attr, object oldValue, object value )
		//{
		//	SyncEventHelper.BuffAttrChanged( this.rid, attr, oldValue, value );
		//}
	}
}