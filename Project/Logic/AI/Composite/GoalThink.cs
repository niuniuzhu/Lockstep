using System.Collections.Generic;
using Logic.AI.Evaluation;
using Logic.Controller;

namespace Logic.AI.Composite
{
	public class GoalThink : GoalComposite
	{
		public override Type type => Type.Brain;

		private readonly List<GoalEvaluator> _evaluators = new List<GoalEvaluator>();

		public bool enable;

		public GoalThink( Entity owner )
		{
			this.owner = owner;
		}

		public void AddEvaluator( GoalEvaluator evaluator )
		{
			evaluator.brain = this;
			this._evaluators.Add( evaluator );
		}

		public GoalEvaluator GetEvaluator( System.Type type )
		{
			int count = this._evaluators.Count;
			for ( int i = 0; i < count; i++ )
			{
				GoalEvaluator evaluator = this._evaluators[i];
				if ( evaluator.GetType() == type )
					return evaluator;
			}
			return null;
		}

		public GoalEvaluator GetEvaluator<T>() where T : GoalEvaluator
		{
			int count = this._evaluators.Count;
			for ( int i = 0; i < count; i++ )
			{
				GoalEvaluator evaluator = this._evaluators[i];
				if ( evaluator is T )
					return evaluator;
			}
			return null;
		}

		public void RemoveAllEvaluator()
		{
			while ( this._evaluators.Count > 0 )
			{
				this._evaluators[0].brain = null;
				this._evaluators.RemoveAt( 0 );
			}
		}

		private void Arbitrate()
		{
			int count = this._evaluators.Count;
			if ( count == 0 )
				return;

			float best = 0;
			GoalEvaluator mostDesirable = null;

			for ( int i = 0; i < count; i++ )
			{
				GoalEvaluator evaluator = this._evaluators[i];
				float desirability = evaluator.CalculateDesirability();
				if ( desirability > best )
				{
					best = desirability;
					mostDesirable = evaluator;
				}
			}

			mostDesirable?.SetGoal();
		}

		internal override Status Process( float dt )
		{
			Status subGoalStatus = this.ProcessSubgoals( dt );

			if ( subGoalStatus == Status.Completed || subGoalStatus == Status.Failed )
				this._status = Status.Inactive;

			//所有返回非Active状态都会重新仲裁
			return this._status;
		}

		protected override void Activate()
		{
			this.Arbitrate();

			this._status = Status.Active;
		}

		public void Rearbitrate()
		{
			this.previous = Type.Undefined;
			this.RemoveAllSubgoals();
			int count = this._evaluators.Count;
			for ( int i = 0; i < count; i++ )
			{
				GoalEvaluator evaluator = this._evaluators[i];
				evaluator.Reset();
			}
			this._status = Status.Inactive;
		}

		internal void Think( UpdateContext context )
		{
			if ( !this.enable )
				return;

			this.ActivateIfInactive();
			this.Process( context.deltaTime );
		}
	}
}