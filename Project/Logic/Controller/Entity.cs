using Core.Math;
using Logic.AI.Composite;
using Logic.AI.Evaluation;
using Logic.Event;
using Logic.Misc;
using Logic.Model;
using Logic.Property;
using Logic.Steering;
using Utils = Logic.Misc.Utils;

namespace Logic.Controller
{
	public abstract class Entity : GPoolObject, IScriptable
	{
		public string id => this._data.id;
		public EntityFlag flag => this._data.flag;
		public float mass => this._data.mass;
		public bool volumetric => this._data.volumetric;
		public float maxSpeed => this._data.speed;
		public float rotSpeed => this._data.rotSpeed;
		public float trackDistance => this._data.trackDistance;
		public float lifeTime => this._data.lifeTime;
		public Vec3 firingPoint => this._data.firingPoint ?? new Vec3( 0, this.size.y * 0.5f, 0 );
		public Vec3 hitPoint => this._data.hitPoint ?? new Vec3( 0, this.size.y * 0.5f, 0 );
		public Vec3 headPoint => new Vec3( 0, this.size.y * 0.5f, 0 );
		public Vec3 footPoint => new Vec3( 0, this.size.y * 0.1f, 0 );
		public Vec3 size { get; private set; }
		public Vec3 bornPosition { get; private set; }
		public Vec3 bornDirection { get; private set; }
		public EntityProperty property { get; private set; }
		public SteeringBehaviors steering { get; private set; }
		public GoalThink brain { get; private set; }
		public float time { get; protected set; }
		public Battle battle { get; private set; }
		internal bool ignoreSpeedLimits { get; set; }

		internal bool markToDestroy;
		internal bool debugDraw;

		protected EntityData _data;

		protected Script _script;

		internal virtual void Init( Battle battle )
		{
			this.debugDraw = true;
			this.battle = battle;
			this.property = new EntityProperty();
			this.property.OnChanged += this.OnAttrChanged;
			this.steering = new SteeringBehaviors( this );
			this.brain = new GoalThink( this );
		}

		protected override void InternalDispose()
		{
			this.property.OnChanged -= this.OnAttrChanged;
			this.property = null;
			this.steering = null;
			this.brain = null;
			this.battle = null;
		}

		internal void OnAddedToBattle( EntityParam param )
		{
			SyncEventHelper.SpawnEntity( this.GetType().Name, param );
			this._rid = param.rid;
			this._data = ModelFactory.GetEntityData( Utils.GetIDFromRID( this._rid ) );
			this.property.Init( this._data );
			this.property.Equal( Attr.Position, param.position );
			this.property.Equal( Attr.Direction, param.direction );
			this.size = this._data.size * this.property.scale;
			this.bornPosition = param.position;
			this.bornDirection = param.direction;
			AIData[] aiDatas = this._data.aiDatas;
			if ( aiDatas != null )
			{
				int count = aiDatas.Length;
				for ( int i = 0; i < count; i++ )
					this.CreateAIEvaluator( aiDatas[i] );
			}

			this.InternalOnAddedToBattle( param );

			SyncEventHelper.EntityAttrInitialized( this.rid );

			if ( !string.IsNullOrEmpty( this._data.script ) )
			{
				this._script = new Script( this, this.battle.luaEnv, this._data.script );
				this._script.Call( Script.S_ON_ENTITY_ADDED_TO_BATTLE );
			}
		}

		internal void OnRemoveFromBattle()
		{
			this.InternalOnRemoveFromBattle();
			if ( this._script != null )
			{
				this._script.Call( Script.S_ON_ENTITY_REMOVED_FROM_BATTLE );
				this._script.Dispose();
				this._script = null;
			}
			this.RemoveAllAIEvaluator();
			this.markToDestroy = false;
			this._data = null;
			SyncEventHelper.DespawnEntity( this.rid );
		}

		protected virtual void InternalOnAddedToBattle( EntityParam param )
		{
		}

		protected virtual void InternalOnRemoveFromBattle()
		{
		}

		protected void UpdateThink( UpdateContext context )
		{
			if ( this.CanThink() )
				this.brain.Think( context );
		}

		protected void UpdateSteering( UpdateContext context )
		{
			Vec3 oldPos = this.property.position;

			this.steering.Update( context );

			if ( oldPos != this.property.position && ( ( this.flag & EntityFlag.Hero ) > 0 || ( this.flag & EntityFlag.SmallPotato ) > 0 ) )
			{
				//hero or smallp must stand on the navmesh
				Vec3 pos = this.property.position;
				pos = this.battle.SampleNavPosition( pos );
				pos.y = 0;
				this.property.Equal( Attr.Position, pos );
			}
		}

		internal void UpdateVelocity( Vec3 velocity )
		{
			float speed = velocity == Vec3.zero ? 0f : velocity.Magnitude();
			if ( this.ignoreSpeedLimits )
				this.ignoreSpeedLimits = false;
			else
			{
				float max = this.maxSpeed * this.property.moveSpeedFactor;
				if ( speed > max )
				{
					velocity *= max / speed;
					speed = max;
				}
			}
			this.property.Equal( Attr.Velocity, velocity );
			this.property.Equal( Attr.Speed, speed );
		}

		internal virtual void UpdateState( UpdateContext context )
		{
			this.time += context.deltaTime;

			this.UpdateThink( context );
			this.UpdateSteering( context );

			if ( this.debugDraw )
				SyncEventHelper.DebugDraw( SyncEvent.DebugDrawType.WireCube, this.property.position + new Vec3( 0, this.size.y * 0.5f, 0 ), this.size, null, 0, Color4.gray );

			if ( this.lifeTime > 0 &&
				 this.time >= this.lifeTime )
				this.markToDestroy = true;
		}

		internal virtual void UpdateFight( UpdateContext context )
		{
		}

		public void CreateAIEvaluator( AIData aiData )
		{
			this.brain.enable = true;
			switch ( aiData.type )
			{
				case "march":
					this.brain.AddEvaluator( new MarchEvaluator() );
					break;

				case "retreat":
					//todo
					//this.brain.AddEvaluator( new RetreatEvaluator() );
					break;

				case "attack":
					this.brain.AddEvaluator( new AttackEvaluator() );
					break;

				case "structure_attack":
					this.brain.AddEvaluator( new StructureAttackEvaluator() );
					break;
			}
		}

		protected virtual void OnAttrChanged( Attr attr, object oldValue, object value )
		{
			switch ( attr )
			{
				case Attr.Scale:
					this.size = this._data.size * ( float )value;
					break;
			}
			SyncEventHelper.EntityAttrChanged( this.rid, attr, oldValue, value );
		}

		public void RemoveAllAIEvaluator()
		{
			this.brain.RemoveAllEvaluator();
		}

		public void EnableThink()
		{
			this.brain.enable = true;
		}

		public void DisableThink()
		{
			this.brain.enable = false;
		}

		public bool CanThink()
		{
			return true;
		}

		public float DistanceSqrtTo( Entity target )
		{
			return ( this.property.position - target.property.position ).SqrMagnitude();
		}

		public Vec3 PointToWorld( Vec3 point )
		{
			return this.property.position + Quat.FromToRotation( Vec3.right, this.property.direction ) * point;
		}

		public Vec3 PointToLocal( Vec3 point )
		{
			return Quat.Inverse( Quat.FromToRotation( Vec3.right, this.property.direction ) ) * ( point - this.property.position );
		}

		public Vec3 VectorToWorld( Vec3 point )
		{
			return Quat.FromToRotation( Vec3.right, this.property.direction ) * point;
		}

		public Vec3 VectorToLocal( Vec3 point )
		{
			return Quat.Inverse( Quat.FromToRotation( Vec3.right, this.property.direction ) ) * point;
		}
	}
}