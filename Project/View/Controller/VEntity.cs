using Core.Math;
using Logic;
using Logic.Controller;
using Logic.Model;
using Logic.Property;
using UnityEngine;
using View.Event;
using View.Graphics;
using View.Misc;
using Utils = Logic.Misc.Utils;

namespace View.Controller
{
	public abstract class VEntity : Interactive
	{
		public VBattle battle { get; private set; }
		public string id => this._data.id;
		public string name => this._data.name;
		public EntityFlag flag => this._data.flag;
		public bool noShadow => this._data.noShadow;
		public float fov => this._data.fov;
		public float maxSpeed => this._data.speed;
		public bool destructImmediately => this._data.destructImmediately;
		public float lifeTime => this._data.lifeTime;

		protected EntityData _data;

		private Vector3 _position;
		public Vector3 position
		{
			get => this._position;
			set
			{
				if ( this._position == value )
					return;
				this._position = value;
				if ( this.graphic != null )
					this.graphic.position = this._position;
			}
		}

		private Vector3 _direction;
		public Vector3 direction
		{
			get => this._direction;
			set
			{
				if ( this._direction == value )
					return;
				this._direction = value;
				if ( this.graphic != null )
					this.graphic.rotation = Quaternion.FromToRotation( Vector3.right, this._direction );
			}
		}
		public Vector3 size { get; private set; }
		public Graphic graphic { get; private set; }
		public EntityProperty property { get; private set; }
		public bool isLiving => !this.destructImmediately && this.graphic.isLiving;

		internal bool markToDestroy;

		protected Vector3 _logicPos;
		protected Vector3 _logicDir;

		public virtual void Init( VBattle battle )
		{
			this.battle = battle;
			this.property = new EntityProperty();
		}

		protected override void InternalDispose()
		{
			if ( this.graphic != null )
			{
				this.battle.graphicManager.Release( this.graphic );
				this.graphic = null;
			}
			this.battle = null;

			base.InternalDispose();
		}

		internal void OnAddedToBattle( EntityParam param )
		{
			this._rid = param.rid;
			this._data = ModelFactory.GetEntityData( Utils.GetIDFromRID( this._rid ) );
			this.property.Init( this._data );
			this.size = this._data.size.ToVector3() * this.property.scale;
			this.graphic = this.battle.graphicManager.Get( this, this._data.model );
			this.graphic.name = this.rid;
			this.graphic.initScale = new Vector3( this._data.scale, this._data.scale, this._data.scale );
			this.graphic.shadowVisible = !this.noShadow;
			this.UpdateGraphicSpeed();
			this.InternalOnAddedToBattle( param );
			UIEvent.EntityCreated( this );
		}

		internal void OnRemoveFromBattle()
		{
			UIEvent.EntityDestroied( this );
			this.InternalOnRemoveFromBattle();
			this.battle.graphicManager.Release( this.graphic );
			this.markToDestroy = false;
			this.graphic = null;
			this._data = null;
		}

		protected virtual void InternalOnAddedToBattle( EntityParam param )
		{
		}

		protected virtual void InternalOnRemoveFromBattle()
		{
		}

		public void HandleAttrInitialized()
		{
			this.position = this._logicPos;
			this.direction = this._logicDir;
		}

		internal virtual void HandleAttrChanged( Attr attr, object oldValue, object newValue )
		{
			this.property.Equal( attr, newValue );
			switch ( attr )
			{
				case Attr.Position:
					this._logicPos = ( ( Vec3 )newValue ).ToVector3();
					break;

				case Attr.Direction:
					this._logicDir = ( ( Vec3 )newValue ).ToVector3();
					break;

				case Attr.Scale:
					float s = ( float )newValue;
					this.graphic.scale = new Vector3( s, s, s );
					this.size = this._data.size.ToVector3() * s;
					break;

				case Attr.MoveSpeedFactor:
				case Attr.FreezeFactor:
					this.UpdateGraphicSpeed();
					break;
			}
		}

		public virtual void UpdateState( UpdateContext context )
		{
			this.position = Vector3.Lerp( this.position, this._logicPos, context.deltaTime * 12f );
			this.direction = Vector3.Slerp( this.direction, this._logicDir, context.deltaTime * 10f );
		}

		public float DistanceSqrtTo( VEntity target )
		{
			return ( this.position - target.position ).sqrMagnitude;
		}

		public bool WithinFov( VEntity target )
		{
			return this.DistanceSqrtTo( target ) <= this.fov * this.fov;
		}

		private void UpdateGraphicSpeed()
		{
			if ( this.graphic != null )
				this.graphic.animator.speed = this.property.freezeFactor > 0 ? 0 : this.property.moveSpeedFactor;
		}
	}
}