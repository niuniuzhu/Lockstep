using Core.Math;
using Logic;
using Logic.Controller;
using Logic.Model;
using Logic.Property;
using UnityEngine;
using View.Misc;
using Utils = Logic.Misc.Utils;

namespace View.Controller
{
	public class VBuff : GPoolObject
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

		public VBattle battle { get; private set; }
		public BuffProperty property { get; private set; }
		public Vector3 position { get; private set; }
		public Vector3 direction { get; private set; }
		public bool isInBattle => this.battle != null;

		public SkillData skillData { get; private set; }
		public VBio caster { get; private set; }
		public VBio target { get; private set; }
		public Vector3 targetPoint { get; private set; }

		internal bool markToDestroy;

		private BuffData _data;
		private Effect _areaFx;
		private Vector3 _logicPos;
		private Vector3 _logicDir;

		public virtual void Init( VBattle battle )
		{
			this.battle = battle;
			this.property = new BuffProperty();
		}

		internal void OnAddedToBattle( string rid, string skillId, int lvl, VBio caster, VBio target, Vector3 targetPoint )
		{
			this._rid = rid;
			this._data = ModelFactory.GetBuffData( Utils.GetIDFromRID( this._rid ) );
			this.skillData = ModelFactory.GetSkillData( skillId );
			this.caster = caster;
			this.target = target;
			this.targetPoint = targetPoint;
			this.property.Init( this._data );
			this.ApplyLevel( lvl );
		}

		protected override void InternalDispose()
		{
			this.property = null;
			this.battle = null;
			this._data = null;
		}

		public void OnRemoveFromBattle()
		{
			if ( this._areaFx != null )
			{
				this._areaFx.markToDestroy = true;
				this._areaFx = null;
			}
			this.caster = null;
			this.target = null;
			this.markToDestroy = false;
		}

		private void ApplyLevel( int level )
		{
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

		public void HandleAttrInitialized()
		{
			this._logicPos = this.position = this.property.position.ToVector3();
			this._logicDir = this.direction = this.property.direction.ToVector3();
			if ( !string.IsNullOrEmpty( this.areaFx ) )
			{
				this._areaFx = this.battle.CreateEffect( this.areaFx );
				this._areaFx.position = this.position;
				this._areaFx.direction = this.direction;
				if ( this._data.autoScaleAreaFx )
				{
					float r = this.radius * 2;
					this._areaFx.graphic.scale = new Vector3( r, r, r );
				}
			}
		}

		public void HandleAttrChanged( Attr attr, object oldValue, object value )
		{
			this.property.Equal( attr, value );
			switch ( attr )
			{
				case Attr.Position:
					this._logicPos = ( ( Vec3 )value ).ToVector3();
					break;

				case Attr.Direction:
					this._logicDir = ( ( Vec3 )value ).ToVector3();
					break;
			}
		}

		internal void UpdateState( UpdateContext context )
		{
			this.position = Vector3.Lerp( this.position, this._logicPos, context.deltaTime * 12f );
			this.direction = Vector3.Slerp( this.direction, this._logicDir, context.deltaTime * 10f );

			if ( this._areaFx != null && this._areaFx.positionType == EffectPositionType.FollowBuff )
				this._areaFx.position = this.position;

			if ( this._areaFx != null && this._areaFx.rotationType == EffectRotationType.Follow )
				this._areaFx.direction = this.direction;
		}

		public void HandleTriggered( int triggerIndex )
		{
			BuffData.Trigger trigger = this.trigger;
			if ( trigger.tfxs == null )
				return;
			int index = trigger.tfxs.Length - 1;
			index = triggerIndex <= index ? triggerIndex : index;

			if ( !string.IsNullOrEmpty( trigger.tfxs[index] ) )
			{
				Effect fx = this.battle.CreateEffect( trigger.tfxs[index] );
				fx.position = this.position;
				fx.direction = this.direction;
			}
		}
	}
}