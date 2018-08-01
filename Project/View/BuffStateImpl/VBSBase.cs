using System.Collections.Generic;
using Logic;
using Logic.Model;
using Logic.Property;
using UnityEngine;
using View.Controller;

namespace View.BuffStateImpl
{
	public class VBSBase
	{
		public VBio owner { get; private set; }
		public VBuff buff { get; private set; }

		public string id => this._data.id;
		public BuffStateType type => this._data.type;
		public float duration { get; protected set; }
		public BuffStateData.Trigger trigger { get; protected set; }
		public string[] fxs { get; protected set; }
		public Attr[] attrs { get; protected set; }
		public float[] values { get; protected set; }
		public float[] extra { get; protected set; }
		public string[] extra_s { get; protected set; }

		private BuffStateData _data;
		private List<Effect> _fxs;
		protected float _elapsed;

		internal static VBSBase Create( BuffStateType type )
		{
			VBSBase bs;
			switch ( type )
			{
				case BuffStateType.Ragnarok:
					bs = VBuffStatePool.Pop<VBSRagnarok>();
					break;

				case BuffStateType.EssenceTheft:
					bs = VBuffStatePool.Pop<VBSEssenceTheft>();
					break;

				case BuffStateType.Charm:
					bs = VBuffStatePool.Pop<VBSCharm>();
					break;

				default:
					bs = VBuffStatePool.Pop<VBSBase>();
					break;
			}
			return bs;
		}

		internal void Init( string id, VBuff buff, VBio owner )
		{
			this.owner = owner;
			this.buff = buff;
			this._data = ModelFactory.GetBuffStateData( id );
			int index = Mathf.Min( this.buff.property.lvl, this._data.levels.Length - 1 );
			this.duration = this._data.levels[index].duration;
			this.trigger = this._data.levels[index].trigger;
			this.fxs = this._data.levels[index].fxs;
			this.attrs = this._data.levels[index].attrs;
			this.values = this._data.levels[index].values;
			this.extra = this._data.levels[index].extra;
			this.extra_s = this._data.levels[index].extra_s;

			if ( this.fxs != null )
			{
				int count = this.fxs.Length;
				for ( int i = 0; i < count; i++ )
				{
					Effect mfx = this.owner.battle.CreateEffect( this.fxs[i] );
					mfx.SetupTerritory( this.buff.caster, this.owner, this.buff.targetPoint );
					if ( mfx.lifeTime <= 0 )
					{
						if ( this._fxs == null )
							this._fxs = new List<Effect>();
						this._fxs.Add( mfx );
					}
				}
			}

			this._elapsed = 0f;

			this.CreateInternal();
		}

		internal void OnDestroy()
		{
			this.DestroyInternal();
			this.Reset();
			if ( this._fxs != null )
			{
				int count = this._fxs.Count;
				for ( int i = 0; i < count; i++ )
					this._fxs[i].markToDestroy = true;
				this._fxs.Clear();
				this._fxs = null;
			}
			this.owner = null;
			this.buff = null;
			this._data = null;
		}

		public void Update( UpdateContext context )
		{

			this._elapsed += context.deltaTime;

			this.UpdateInternal( context );
		}

		public void HandleTriggered( int triggerIndex )
		{
			int index = this.trigger.fxs.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			string fxId = this.trigger.fxs[index];
			if ( !string.IsNullOrEmpty( fxId ) )
			{
				Effect fx = this.owner.battle.CreateEffect( fxId );
				fx.SetupTerritory( this.buff.caster, this.owner, this.buff.targetPoint );
			}
		}

		public void OnAttrChanged( Attr attr, object oldValue, object newValue )
		{
			this.AttrChangedInternal( attr, oldValue, newValue );
		}

		public void OnDamage( VBuff buff, VBio target, float damage )
		{
			this.DamageInternal( buff, target, damage );
		}

		public void OnHurt( VBuff buff, VBio caster, float damage )
		{
			this.HurtInternal( buff, caster, damage );
		}

		protected virtual void CreateInternal()
		{
		}

		protected virtual void DestroyInternal()
		{
		}

		protected virtual void UpdateInternal( UpdateContext context )
		{
		}

		protected virtual void Reset()
		{
		}

		protected virtual void AttrChangedInternal( Attr attr, object oldValue, object newValue )
		{
		}

		protected virtual void DamageInternal( VBuff vBuff, VBio target, float damage )
		{
		}

		protected virtual void HurtInternal( VBuff vBuff, VBio caster, float damage )
		{
		}
	}
}