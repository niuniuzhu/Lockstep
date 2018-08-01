using Logic.AI.Atomic;
using Logic.Controller;

namespace Logic.AI.Evaluation
{
	public class StructureAttackEvaluator : GoalEvaluator
	{
		private Bio _lastTarget;
		private Bio _target;

		public override float CalculateDesirability()
		{
			Bio self = ( Bio ) this.brain.owner;
			//todo 使用技能的策略?
			if ( !self.CanUseSkill( self.commonSkill ) )
				return 0;

			if ( this._lastTarget != null )
			{
				if ( this._lastTarget.isDead ||
					 !self.WithinSkillRange( this._lastTarget, self.commonSkill ) )
				{
					this._lastTarget = null;
				}
				this._target = this._lastTarget;
			}
			else
			{
				this._target = EntityUtils.GetNearestTarget( self.battle.GetEntities(), self, self.commonSkill.distance, self.commonSkill.campType, self.commonSkill.targetFlag );
				this._lastTarget = this._target;
			}
			return this._target == null ? 0 : 1;
		}

		public override void SetGoal()
		{
			GoalStructureAttack goalStructureAttack = this.brain.PopGoal<GoalStructureAttack>();
			goalStructureAttack.SetTarget( this._target );
			this.brain.AddSubgoal( goalStructureAttack );
			this._target = null;
		}
	}
}