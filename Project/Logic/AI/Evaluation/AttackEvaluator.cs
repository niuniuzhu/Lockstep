using Logic.AI.Atomic;
using Logic.Controller;

namespace Logic.AI.Evaluation
{
	public class AttackEvaluator : GoalEvaluator
	{
		private Bio _target;
		private Bio _lastTarget;

		public override void Reset()
		{
			this._target = this._lastTarget = null;
		}

		public override float CalculateDesirability()
		{
			Bio self = ( Bio ) this.brain.owner;
			//todo 使用技能的策略?
			Skill skill = self.commonSkill;
			if ( !self.CanUseSkill( skill ) )
				return 0;

			if ( this._lastTarget != null )
			{
				if ( this._lastTarget.isDead || !self.WithinSkillRange( this._lastTarget, skill ) )
					this._lastTarget = null;
				this._target = this._lastTarget;
			}
			else
			{
				this._target = EntityUtils.GetNearestTarget( self.battle.GetEntities(), self, self.fov, skill.campType, skill.targetFlag );
				this._lastTarget = this._target;
			}
			return this._target == null ? 0 : 1;
		}

		public override void SetGoal()
		{
			GoalAttack goalAttack = this.brain.PopGoal<GoalAttack>();
			goalAttack.SetTarget( this._target );
			this.brain.AddSubgoal( goalAttack );
			this._target = null;
		}
	}
}