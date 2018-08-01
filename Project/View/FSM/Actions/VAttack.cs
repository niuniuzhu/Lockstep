using Logic;
using Logic.Controller;
using UnityEngine;
using View.Controller;

namespace View.FSM.Actions
{
	public class VAttack : VBioAction
	{
		private Skill _skill;
		private VBio _target;
		private float _time;
		private float _fxTime;

		protected override void OnEnter( object[] param )
		{
			this._skill = this.owner.GetSkill( ( string )param[0] );
			this._target = this.owner.battle.GetBio( ( string )param[1] );

			this.owner.usingSkill = this._skill;
			string action = this._skill.action;
			if ( !string.IsNullOrEmpty( action ) )
			{
				this.owner.graphic.animator.SetClipSpeed( action, this._skill.actionLength / this._skill.atkTime );
				this.owner.graphic.animator.CrossFade( action );
			}

			this._time = 0f;
			this._fxTime = this._skill.atkFxTime / this.owner.property.attackSpeedFactor;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			base.OnUpdate( context );
			this._time += context.deltaTime;

			if ( this._fxTime >= 0f && this._time >= this._fxTime )
			{
				this._fxTime = -1f;
				this.SpawnFx();
			}
		}

		protected override void OnExit()
		{
			this.owner.usingSkill = null;
			if ( !string.IsNullOrEmpty( this._skill.action ) )
				this.owner.graphic.animator.SetClipSpeed( this._skill.action, 1f );
			this._skill = null;
			this._target = null;
		}

		private void SpawnFx()
		{
			if ( !string.IsNullOrEmpty( this._skill.atkFx ) )
			{
				Effect effect = this.owner.battle.CreateEffect( this._skill.atkFx );
				effect.SetupTerritory( this.owner, this._target, Vector3.zero );
			}
		}
	}
}