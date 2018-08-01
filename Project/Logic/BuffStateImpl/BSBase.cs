using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.Misc;
using Logic.Model;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	public class BSBase
	{
		public Bio owner { get; private set; }
		public Buff buff { get; private set; }//小心使用,如果state和buff没关系,可能buff已经被销毁了
		public SkillData skillData { get; private set; }

		public string id => this._data.id;
		public BuffStateType type => this._data.type;
		public BeneficialType beneficialType => this._data.beneficialType;
		public float duration { get; protected set; }
		public BuffStateData.Trigger trigger { get; protected set; }
		public Attr[] attrs { get; protected set; }
		public float[] values { get; protected set; }
		public float[] extra { get; protected set; }

		private bool _enable;
		public bool enable
		{
			get { return this._enable; }
			set
			{
				if ( this._enable == value )
					return;
				this._enable = value;
				if ( this._enable )
				{
					this.ApplyAttrs( this.attrs, this.values, false );
					this.OnEnable();
				}
				else
				{
					this.OnDisable();
					this.EliminateAttrs();
				}
			}
		}

		private BuffStateData _data;
		protected readonly Dictionary<Attr, int> _deltai = new Dictionary<Attr, int>();
		protected readonly Dictionary<Attr, float> _deltaf = new Dictionary<Attr, float>();
		protected float _elapsed;
		private double _triggerDt;
		private int _triggerIndex;

		internal static BSBase Create( BuffStateType type )
		{
			BSBase bs;
			switch ( type )
			{
				case BuffStateType.Ragnarok:
					bs = BuffStatePool.Pop<BSRagnarok>();
					break;
				case BuffStateType.Stunned:
					bs = BuffStatePool.Pop<BSStunned>();
					break;

				case BuffStateType.Freezed:
					bs = BuffStatePool.Pop<BSFreezed>();
					break;

				case BuffStateType.Repelled:
					bs = BuffStatePool.Pop<BSRepelled>();
					break;

				case BuffStateType.Flurry:
					bs = BuffStatePool.Pop<BSFlurry>();
					break;

				case BuffStateType.BerserkerRage:
					bs = BuffStatePool.Pop<BSBerserkerRage>();
					break;

				case BuffStateType.ViciousStrikes:
					bs = BuffStatePool.Pop<BSViciousStrikes>();
					break;

				case BuffStateType.Undertow:
					bs = BuffStatePool.Pop<BSUndertow>();
					break;

				case BuffStateType.EssenceTheft:
					bs = BuffStatePool.Pop<BSEssenceTheft>();
					break;

				case BuffStateType.OrbOfDeception:
					bs = BuffStatePool.Pop<BSOrbOfDeception>();
					break;

				case BuffStateType.Charm:
					bs = BuffStatePool.Pop<BSCharm>();
					break;

				case BuffStateType.SpiritRush:
					bs = BuffStatePool.Pop<BSSpiritRush>();
					break;

				default:
					bs = BuffStatePool.Pop<BSBase>();
					break;
			}
			return bs;
		}

		internal void Init( string id, Bio owner, Buff buff )
		{
			this.owner = owner;
			this.owner.AddRef();
			this.buff = buff;
			this.buff.AddRef();
			this.skillData = this.buff.skillData;

			this._data = ModelFactory.GetBuffStateData( id );
			int index = MathUtils.Min( this.buff.property.lvl, this._data.levels.Length - 1 );
			this.duration = this._data.levels[index].duration;
			this.trigger = this._data.levels[index].trigger;
			this.attrs = this._data.levels[index].attrs;
			this.values = this._data.levels[index].values;
			this.extra = this._data.levels[index].extra;

			this._elapsed = 0f;
			this._triggerIndex = 0;
			this._enable = true;

			this.CreateInternal();
			this.ApplyAttrs( this.attrs, this.values, false );
		}

		internal void OnDestroy()
		{
			this.EliminateAttrs();
			this.DestroyInternal();
			this.Reset();
			this.skillData = null;
			this.buff.RedRef();
			this.buff = null;
			this.owner.RedRef();
			this.owner = null;
			this._data = null;
		}

		public void Unite()
		{
			this.UniteInternal();
		}

		protected virtual void Reset()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		internal void Update( UpdateContext context )
		{
			if ( !this._enable )
				return;

			this._elapsed += context.deltaTime;

			this.UpdateInternal( context );

			if ( this.trigger != null )
			{
				this._triggerDt += context.deltaTime;
				float interval = this.trigger.interval;
				interval = interval < context.deltaTime ? context.deltaTime : interval;
				if ( this._triggerDt >= interval )
				{
					SyncEventHelper.BuffStateTriggered( this.owner.rid, this.id, this._triggerIndex );

					this.TriggerAttr( this._triggerIndex );

					++this._triggerIndex;
					this._triggerDt -= interval;
				}
			}

			if ( this.duration >= 0f && this._elapsed >= this.duration )
			{
				this.TimeupInternal();
				this.owner.DestroyBuffState( this );
			}
		}

		private void TriggerAttr( int triggerIndex )
		{
			int index = this.trigger.attrs.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;
			this.ApplyAttrs( this.trigger.attrs[index], this.trigger.values[index], true );
		}

		protected virtual void ApplyAttrs( Attr[] attrs, float[] values, bool oneOff )
		{
			if ( attrs == null )
				return;

			EntityProperty property = this.owner.property;
			int count = attrs.Length;
			for ( int i = 0; i < count; i++ )
			{
				Attr attr = attrs[i];
				float value = values[i];
				switch ( attr )
				{
					case Attr.Hp:
					case Attr.Mana:
					case Attr.Ap:
					case Attr.Ad:
					case Attr.Armor:
					case Attr.ArmorPen:
					case Attr.ArmorPenFlat:
					case Attr.MagicResist:
					case Attr.MagicPen:
					case Attr.MagicPenFlat:
						{
							float oldValue, newValue;
							property.Add( attr, value, out oldValue, out newValue );
							if ( !oneOff )
								this._deltaf.Add( attr, newValue - oldValue );
						}
						break;

					case Attr.Repelling:
					case Attr.Stunned:
					case Attr.IgnoreVolumetric:
					case Attr.FreezeFactor:
					case Attr.Stealth:
					case Attr.ImmuneDisables:
					case Attr.Charmed:
						{
							int oldValue, newValue;
							property.Add( attr, ( int ) value, out oldValue, out newValue );
							if ( !oneOff )
								this._deltai.Add( attr, newValue - oldValue );
						}
						break;

					case Attr.MoveSpeedFactor:
						{
							float oldValue, newValue;
							property.Mul( attr, value, out oldValue, out newValue );
							if ( !oneOff )
								this._deltaf.Add( attr, newValue - oldValue );
						}
						break;
				}
			}
		}

		protected virtual void EliminateAttrs()
		{
			foreach ( KeyValuePair<Attr, int> kv in this._deltai )
				this.owner.property.Add( kv.Key, -kv.Value );
			foreach ( KeyValuePair<Attr, float> kv in this._deltaf )
				this.owner.property.Add( kv.Key, -kv.Value );
			this._deltai.Clear();
			this._deltaf.Clear();
		}

		protected virtual void CreateInternal()
		{
		}

		protected virtual void DestroyInternal()
		{
		}

		protected virtual void UniteInternal()
		{
		}

		protected virtual void UpdateInternal( UpdateContext context )
		{
		}

		protected virtual void TimeupInternal()
		{
		}

		internal void OnDamage( Buff buff, Bio target, float damage )
		{
			this.DamageInternal( buff, target, damage );
		}

		internal void OnHurt( Buff buff, Bio caster, float damage )
		{
			this.HurtInternal( buff, caster, damage );
		}

		internal void OnAttrChanged( Attr attr, object oldValue, object newValue )
		{
			this.AttrChangedInternal( attr, oldValue, newValue );
		}

		internal void OnKill( Bio target )
		{
			this.KillInternal( target );
		}

		internal void OnDie( Bio killer )
		{
			this.DieInternal( killer );
		}

		protected virtual void DamageInternal( Buff buff, Bio target, float damage )
		{
		}

		protected virtual void HurtInternal( Buff buff, Bio caster, float damage )
		{
		}

		protected virtual void AttrChangedInternal( Attr attr, object oldValue, object newValue )
		{
		}

		protected virtual void KillInternal( Bio target )
		{
		}

		protected virtual void DieInternal( Bio killer )
		{
		}
	}
}