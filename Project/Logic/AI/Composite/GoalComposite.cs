using System.Collections.Generic;
using Logic.AI.Atomic;
using Logic.Controller;

namespace Logic.AI.Composite
{
	public abstract class GoalComposite : Goal
	{
		private readonly Stack<Goal> _subGoals = new Stack<Goal>();
		private readonly GPool _evaluatorPool = new GPool();

		public Type previous { get; protected set; }

		public Goal present
		{
			get
			{
				if ( this._subGoals.Count > 0 )
					return this._subGoals.Peek();
				return null;
			}
		}

		public bool NotPresent( Goal goal )
		{
			if ( this._subGoals.Count > 0 )
				return this._subGoals.Peek() != goal;
			return true;
		}

		protected Status ProcessSubgoals( float dt )
		{
			if ( this._subGoals.Count <= 0 )
				return Status.Completed;
			//if any subgoals remain, process the one at the front of the list
			//grab the status of the front-most subgoal
			Goal goal = this._subGoals.Peek();
			Status statusOfSubGoals = goal.Process( dt );

			//we have to test for the special case where the front-most subgoal
			//reports 'completed' *and* the subgoal list contains additional goals.When
			//this is the case, to ensure the parent keeps processing its subgoal list
			//we must return the 'active' status.
			if ( statusOfSubGoals == Status.Completed )
			{
				this.previous = goal.type;
				this._subGoals.Pop();
				goal.Terminate();
				this._evaluatorPool.Push( goal );

				if ( this._subGoals.Count > 0 )
					return Status.Active;
			}
			else if ( statusOfSubGoals == Status.Failed )
			{
				this.previous = goal.type;
				while ( this._subGoals.Count > 0 )
				{
					this._subGoals.Pop();
					goal.Terminate();
					this._evaluatorPool.Push( goal );
				}
				return Status.Failed;
			}

			//返回结果将会影响大脑判定接下来是需要继续执行目标还是重新仲裁
			return statusOfSubGoals;
		}

		public void AddSubgoal( Goal goal )
		{
			this._subGoals.Push( goal );
		}

		protected void RemoveAllSubgoals()
		{
			foreach ( Goal subGoal in this._subGoals )
			{
				subGoal.Terminate();
				this._evaluatorPool.Push( subGoal );
			}
			this._subGoals.Clear();
		}

		public T PopGoal<T>() where T : Goal, new()
		{
			this._evaluatorPool.Pop( out T goal );
			goal.owner = this.owner;
			return goal;
		}

		public void PushGoal( Goal goal )
		{
			this._evaluatorPool.Push( goal );
		}
	}
}