using Logic.AI.Atomic;

namespace Logic.AI.Evaluation
{
	public class MarchEvaluator : GoalEvaluator
	{
		public override float CalculateDesirability()
		{
			return 0.001f;//该目标始终是最小权值
		}

		public override void SetGoal()
		{
			GoalMarch goalMarch = this.brain.PopGoal<GoalMarch>();
			this.brain.AddSubgoal( goalMarch );
		}
	}
}