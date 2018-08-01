using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.FSM;
using Logic.Misc;
using Logic.Model;
using Logic.Property;

namespace Logic.BuffImpl
{
	public class BIBase
	{
		private readonly List<Bio> _tempOldTargets = new List<Bio>();
		private List<Entity> _temp1 = new List<Entity>();
		private List<Entity> _temp2 = new List<Entity>();
		private readonly List<Bio> _tempEnter = new List<Bio>();
		private readonly List<Bio> _tempExit = new List<Bio>();
		private List<Bio> _tempInOut = new List<Bio>();
		private List<Bio> _tempTargets = new List<Bio>();
		protected readonly Dictionary<Bio, int> _triggerCountMap = new Dictionary<Bio, int>();
		protected int _totalTriggerCount;
		private int _triggerIndex;
		private float _time;
		private float _triggerDt;
		protected Buff _buff;

		internal static BIBase Create( string id )
		{
			BIBase bi;
			switch ( id )
			{
				case "b13":
					bi = BuffImplPool.Pop<BIRecklessSwing>();
					break;

				case "b29":
					bi = BuffImplPool.Pop<BIUndertow>();
					break;

				case "b33":
					bi = BuffImplPool.Pop<BIOrbOfDeception>();
					break;

				case "b34":
					bi = BuffImplPool.Pop<BIFoxFire>();
					break;

				case "b36":
					bi = BuffImplPool.Pop<BISpiritRush>();
					break;

				default:
					bi = BuffImplPool.Pop<BIBase>();
					break;
			}
			return bi;
		}

		public void Init( Buff buff )
		{
			this._buff = buff;
			this._triggerIndex = 0;
			this._time = 0f;
			this._triggerDt = 0f;
			this.CreateInternal();
		}

		internal void OnDestroy()
		{
			this.HandleTargetInOut( null, this._tempInOut );
			this.DestroyInternal();
			this.Reset();
			this._tempInOut.Clear();
			this._tempTargets.Clear();
			this._triggerCountMap.Clear();
			this._totalTriggerCount = 0;
			this._buff = null;
		}

		private void SelectTargets( Bio mainTarget, bool detectInOut, ref List<Bio> result )
		{
			if ( detectInOut )
			{
				this._tempOldTargets.AddRange( result );
				result.Clear();
			}

			switch ( this._buff.rangeType )
			{
				case RangeType.Single:
					//检查指定的目标是否符合条件
					//在隐身等状态下也能选中,因此不能使用CanAttack方法
					if ( !mainTarget.isDead &&
						 EntityUtils.CheckCampType( this._buff.caster, this._buff.campType, mainTarget ) &&
						 EntityUtils.CheckTargetFlag( this._buff.targetFlag, mainTarget ) )
						result.Add( mainTarget );
					break;

				case RangeType.Circle:
					{
						bool targetAdded = false;
						if ( mainTarget != null &&
							 EntityUtils.CanAttack( this._buff.caster, mainTarget, this._buff.campType, this._buff.targetFlag ) )
						{
							targetAdded = true;
							result.Add( mainTarget ); //如果指定的目标符合条件则优先添加到目标列表
						}

						int maxTargetNum = mainTarget != null
											   ? this._buff.maxTriggerTargets - 1
											   : this._buff.maxTriggerTargets;
						if ( maxTargetNum > 0 )
						{
							EntityUtils.GetEntitiesInCircle( this._buff.battle.GetEntities(), this._buff.property.position,
															 this._buff.radius, ref this._temp1 );
							if ( targetAdded )
								this._temp1.Remove( mainTarget ); //之前已经添加目标了
							EntityUtils.FilterTarget( this._buff.caster, this._buff.campType, this._buff.targetFlag, ref this._temp1,
													  ref this._temp2 );
							this._temp1.Clear();
							EntityUtils.FilterLimit( ref this._temp2, ref this._temp1, maxTargetNum );

							int count = this._temp1.Count;
							for ( int i = 0; i < count; i++ )
								result.Add( ( Bio )this._temp1[i] );

							this._temp1.Clear();
							this._temp2.Clear();
						}
					}
					break;

				case RangeType.Sector:
					//todo
					break;
			}

			if ( detectInOut )
			{
				int tc = result.Count;
				for ( int i = 0; i < tc; i++ )
				{
					Bio mTarget = result[i];
					if ( !this._tempOldTargets.Contains( mTarget ) )
						this._tempEnter.Add( mTarget );
				}
				tc = this._tempOldTargets.Count;
				for ( int i = 0; i < tc; i++ )
				{
					Bio mTarget = this._tempOldTargets[i];
					if ( !result.Contains( mTarget ) )
						this._tempExit.Add( mTarget );
				}

				this.HandleTargetInOut( this._tempEnter, this._tempExit );

				this._tempEnter.Clear();
				this._tempExit.Clear();
				this._tempOldTargets.Clear();
			}
		}

		protected virtual void DoUpdateOrbit( float dt )
		{
			switch ( this._buff.orbit )
			{
				case Orbit.FollowTarget:
					this._buff.property.Equal( Attr.Position, this._buff.target.property.position );
					this._buff.property.Equal( Attr.Direction, this._buff.target.property.direction );
					break;

				case Orbit.FollowCaster:
					this._buff.property.Equal( Attr.Position, this._buff.caster.property.position );
					this._buff.property.Equal( Attr.Direction, this._buff.caster.property.direction );
					break;

				case Orbit.Direction:
					Vec3 position = this._buff.property.position;
					position += this._buff.property.direction * this._buff.speed * dt;
					this._buff.property.Equal( Attr.Position, position );
					break;
			}
		}

		protected virtual void DoTriggerOrbit( int triggerIndex, float dt )
		{
			switch ( this._buff.orbit )
			{
				case Orbit.CustomPosition:
					BuffData.Trigger trigger = this._buff.trigger;
					float distance = trigger.distances[triggerIndex];
					Vec3 position = this._buff.property.position;
					position += this._buff.property.direction * distance;
					this._buff.property.Equal( Attr.Position, position );
					break;
			}
		}

		internal void Update( UpdateContext context )
		{
			float dt = context.deltaTime;

			this.DoUpdateOrbit( dt );

			bool selected = false;
			if ( this._buff.enterStates != null )
			{
				selected = true;
				this.SelectTargets( this._buff.target, true, ref this._tempInOut );
			}

			BuffData.Trigger trigger = this._buff.trigger;
			if ( trigger != null && ( trigger.times == null || this._triggerIndex <= trigger.times.Length - 1 ) )
			{
				this._triggerDt += dt;
				float interval = trigger.times?[this._triggerIndex] ?? MathUtils.Max( dt, trigger.interval );

				if ( this._triggerDt >= interval )
				{
					SyncEventHelper.BuffTriggered( this._buff.rid, this._triggerIndex );

					this.DoTriggerOrbit( this._triggerIndex, this._triggerDt );

					if ( !selected )
						this.SelectTargets( this._buff.target, false, ref this._tempTargets );
					else
						this._tempTargets.AddRange( this._tempInOut );

					int count = this._tempTargets.Count;
					for ( int i = 0; i < count; i++ )
						this.OnTargetTrigger( this._tempTargets[i], this._triggerIndex );

					this.CreateSummons( this._triggerIndex );

					this._tempTargets.Clear();

					++this._triggerIndex;

					this._triggerDt -= interval;
				}
			}

			this._time += dt;

			this.UpdateInternal( context );

			if ( this._buff.canInterrupt &&
				 this._buff.caster.fsm.currState.type != FSMStateType.Attack )
			{
				this._buff.markToDestroy = true;
				return;
			}

			if ( this._buff.deadType == DeadType.WithCaster &&
				 this._buff.caster.isDead )
			{
				this._buff.markToDestroy = true;
				return;
			}

			if ( this._buff.deadType == DeadType.WithMainTarget &&
				 this._buff.target.isDead )
			{
				this._buff.markToDestroy = true;
				return;
			}

			if ( this._buff.deadType == DeadType.WithTriggerTarget &&
				 this._totalTriggerCount >= this._buff.maxTriggerCount )
			{
				this._buff.markToDestroy = true;
				return;
			}

			if ( this._buff.duration >= 0 &&
				 this._time >= this._buff.duration )
				this._buff.markToDestroy = true;
		}

		protected virtual void UpdateInternal( UpdateContext context )
		{
		}

		protected virtual void CreateSummons( int triggerIndex )
		{
			BuffData.Summon[][] summonss = this._buff.trigger.summons;
			if ( summonss == null )
				return;

			int index = summonss.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			BuffData.Summon[] summons = summonss[index];

			Battle battle = this._buff.battle;
			int count = summons.Length;
			for ( int i = 0; i < count; i++ )
			{
				BuffData.Summon summon = summons[i];
				if ( string.IsNullOrEmpty( summon.id ) )
					continue;

				for ( int j = 0; j < summon.count; j++ )
				{
					Vec3 position;
					Vec3 direction;
					Vec3 buffPos = this._buff.property.position;
					switch ( summon.fill )
					{
						default:
							Vec2 insideUnitCircle = battle.random.insideUnitCircle * this._buff.radius;
							position = new Vec3( buffPos.x + insideUnitCircle.x,
													buffPos.y,
													buffPos.z + insideUnitCircle.y );
							break;

						case BuffData.Summon.Fill.BaseShell:
							Vec3 onUnitSphere = battle.random.onUnitSphere * this._buff.radius;
							position = new Vec3( buffPos.x + onUnitSphere.x,
													buffPos.y,
													buffPos.z + onUnitSphere.z );
							break;
					}
					switch ( summon.direction )
					{
						default:
							Vec2 insideUnitCircle = battle.random.insideUnitCircle;
							direction = Vec3.Normalize( new Vec3( insideUnitCircle.x, 0, insideUnitCircle.y ) );
							break;

						case BuffData.Summon.Direction.FollowCaster:
							direction = this._buff.caster.property.direction;
							break;
					}
					battle.CreateBio( summon.id, position, direction, this._buff.caster.property.team );
				}
			}
		}

		private void HandleTargetInOut( List<Bio> targetsEnter, List<Bio> targetsExit )
		{
			if ( targetsEnter != null )
			{
				int c1 = targetsEnter.Count;
				for ( int i = 0; i < c1; i++ )
				{
					Bio target = targetsEnter[i];
					this.OnTargetEnter( target );
					LLogger.Info( "enter" );
					target.AddRef();
				}
			}
			if ( targetsExit != null )
			{
				int count = targetsExit.Count;
				for ( int i = 0; i < count; i++ )
				{
					Bio target = targetsExit[i];
					LLogger.Info( "exit" );
					this.OnTargetExit( target );
					target.RedRef();
				}
			}
		}

		private void OnTargetEnter( Bio target )
		{
			SyncEventHelper.EnterBuff( this._buff.rid, target.rid );

			target.OnEnterBuff( this._buff );

			if ( this._buff.enterStates != null )
				this.CreateStates( this._buff.enterStates, target );
		}

		private void OnTargetExit( Bio target )
		{
			SyncEventHelper.ExitBuff( this._buff.rid, target.rid );

			target.OnExitBuff( this._buff );
		}

		protected virtual void OnTargetTrigger( Bio target, int triggerIndex )
		{
			if ( !this._triggerCountMap.ContainsKey( target ) )
				this._triggerCountMap[target] = 0;

			if ( this._triggerCountMap[target] >= this._buff.perTargetTriggerCount )
				return;

			SyncEventHelper.TriggerTarget( this._buff.rid, target.rid, triggerIndex );

			this._triggerCountMap[target]++;
			++this._totalTriggerCount;

			this.CalcDamage( this._buff.caster, target, triggerIndex );

			if ( this._buff.triggerStates != null )
				this.CreateStates( this._buff.triggerStates, target );
		}

		private void CalcDamage( Bio caster, Bio target, int triggerIndex )
		{
			BuffData.Trigger trigger = this._buff.trigger;
			if ( trigger.damaged == null )
				return;

			int index = trigger.damaged.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			if ( !trigger.damaged[index] )
				return;

			float damage = this.CalcDamageInternal( caster, target, index );
			target.property.Add( Attr.Hp, -damage );

			if ( target.property.hp <= 0 )
				target.sensorySystem.killer = caster;

			SyncEventHelper.Damage( this._buff.rid, damage, caster.rid, target.rid );
			this.OnDamage( damage, caster, target, triggerIndex );
			caster.OnDamage( this._buff, target, damage );
			target.OnHurt( this._buff, caster, damage );
		}

		protected virtual float CalcDamageInternal( Bio caster, Bio target, int index )
		{
			BuffData.Trigger trigger = this._buff.trigger;
			EntityProperty casterProperty = caster.property;
			EntityProperty targetProperty = target.property;
			float cad = casterProperty.ad;
			float cap = casterProperty.ap;
			float std = trigger.td?[index] ?? 0f;
			float sad = trigger.ad?[index] ?? 0f;
			float sap = trigger.ap?[index] ?? 0f;
			float tpadp = trigger.tpadp?[index] ?? 0f;
			float tpapp = trigger.tpapp?[index] ?? 0f;
			float padp = trigger.padp?[index] ?? 0f;
			float papp = trigger.papp?[index] ?? 0f;

			float armorResist = MathUtils.Max( 0, targetProperty.armor * ( 1 - casterProperty.armorPen ) - casterProperty.armorPenFlat );
			float tad = sad + cad * padp;
			float pdamage = tad * ( 100 / ( 100 + armorResist ) );

			float magicResist = MathUtils.Max( 0, targetProperty.magicResist * ( 1 - casterProperty.magicPen ) - casterProperty.magicPenFlat );
			float tap = sap + cap * papp;
			float mdamage = tap * ( 100 / ( 100 + magicResist ) );

			float trueDamage = std + cad * tpadp + cap * tpapp;

			return pdamage + mdamage + trueDamage;
		}

		protected virtual void OnDamage( float damage, Bio caster, Bio target, int triggerIndex )
		{
		}

		private void CreateStates( string[] ids, Bio target )
		{
			int count = ids.Length;
			for ( int i = 0; i < count; i++ )
				target.CreateBuffState( ids[i], this._buff );
		}

		protected virtual void Reset()
		{
		}

		protected virtual void CreateInternal()
		{
		}

		protected virtual void DestroyInternal()
		{
		}
	}
}