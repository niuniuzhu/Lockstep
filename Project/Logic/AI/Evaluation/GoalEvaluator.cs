using Logic.AI.Composite;

namespace Logic.AI.Evaluation
{
	public abstract class GoalEvaluator
	{
		public GoalThink brain { get; internal set; }

		public virtual void Reset()
		{
		}

		/// <summary>
		/// 计算期望值
		/// </summary>
		/// <returns></returns>
		public abstract float CalculateDesirability();

		/// <summary>
		/// 向大脑添加目标
		/// </summary>
		public abstract void SetGoal();
	}
}