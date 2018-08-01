using Core.Math;
using Logic.FSM;
using Logic.FSM.Actions;
using Logic.Misc;
using Logic.Model;

namespace Logic.Controller
{
	public class Missile : Entity
	{
		public delegate void HitHandler( Skill skill, Bio caster, Bio target, Vec3 targetPoint );

		public FlightType flightType => this._data.flightType;
		public float arc => this._data.arc;
		public float duration => this._data.duration;
		public string hitFx => this._data.hitFx;

		public FiniteStateMachine fsm { get; private set; }

		private string _skillId;
		private int _skillLvl;

		internal Bio caster;
		internal Bio target;
		internal Vec3 targetPoint;

		internal override void Init( Battle battle )
		{
			base.Init( battle );

			this.fsm = new FiniteStateMachine( this );
			this.fsm.CreateState( FSMStateType.Idle );
			FSMState state = this.fsm.CreateState( FSMStateType.Move );
			state.CreateAction<LMissileMove>();
		}

		protected override void InternalDispose()
		{
			this.fsm.Dispose();
			this.fsm = null;
			base.InternalDispose();
		}

		protected override void InternalOnAddedToBattle( EntityParam param )
		{
			base.InternalOnAddedToBattle( param );

			this.fsm.Start();
			this.ChangeState( FSMStateType.Idle );
		}

		protected override void InternalOnRemoveFromBattle()
		{
			this.fsm.Stop();

			this._skillId = string.Empty;
			this._skillLvl = 0;

			this.caster.RedRef();
			this.caster = null;
			this.target?.RedRef();
			this.target = null;
		}

		internal override void UpdateState( UpdateContext context )
		{
			this.fsm.Update( context );
			base.UpdateState( context );
		}

		public void ChangeState( FSMStateType type, bool force = false, params object[] param )
		{
			this.fsm.ChangeState( type, force, param );
		}

		internal void Emmit( string skillId, int lvl, Bio caster, Bio target, Vec3 targetPoint )
		{
			this._skillId = skillId;
			this._skillLvl = lvl;
			this.caster = caster;
			this.target = target;
			this.targetPoint = targetPoint;

			this.caster.AddRef();
			this.target?.AddRef();

			this.ChangeState( FSMStateType.Move, false, ( LMissileMove.MoveCompleteHander )this.OnMoveComplete );
		}

		private void OnMoveComplete()
		{
			SyncEventHelper.HandleMissileComplete( this.rid, this.property.position, this.property.direction );

			this.ChangeState( FSMStateType.Idle );

			this.markToDestroy = true;

			if ( this.target != null &&
				 this.target.isDead )
				return;

			SkillData skillData = ModelFactory.GetSkillData( this._skillId );
			int count = skillData.buffs.Length;
			for ( int i = 0; i < count; i++ )
				this.battle.CreateBuff( skillData.buffs[i], skillData.id, this._skillLvl, this.caster, this.target, this.targetPoint );
		}
	}
}