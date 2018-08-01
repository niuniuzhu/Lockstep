using System.Collections.Generic;
using Logic.AI.Atomic;
using Logic.Controller;

namespace Logic.AI.Evaluation
{
	public class RetreatEvaluator : GoalEvaluator
	{
		private readonly List<Bio> _threatenings = new List<Bio>();

		public override float CalculateDesirability()
		{
			float minDistance = this.StatisticsDamage();
			minDistance = minDistance <= 0 ? 0.01f : minDistance;//这个距离只能在fov和0之间，避免下面除0，最小值设置为0.1
			return 25f / minDistance;
		}

		private float StatisticsDamage()
		{
			//this._threatenings.Clear();

			//List<Entity> entities1 = ListPool<Entity>.Get();
			//List<Entity> entities2 = ListPool<Entity>.Get();

			//Bio self = ( Bio ) this.brain.owner;
			//self.battle.GetEntitiesInCircle( self.position, self.fov, ref entities1 );
			//self.battle.FilterTarget( self,
			//					SkillData.TargetFlag.Hostile,
			//					ref entities1, ref entities2 );

			float minDistance = float.MaxValue;
			//int count = entities2.Count;
			//for ( int i = 0; i < count; i++ )
			//{
			//	Bio bio = entities2[i] as Bio;
			//	if ( bio == null )
			//		continue;
			//	int c2 = bio.skills.Length;
			//	for ( int j = 0; j < c2; j++ )
			//	{
			//		int statisticsSkillDamage = bio.skills[j].StatisticsSkillDamage();
			//		if ( statisticsSkillDamage >= self.hp )
			//		{
			//			minDistance = MathUtils.Min( minDistance, ( self.position - bio.position ).sqrMagnitude );
			//			this._threatenings.Add( bio );
			//		}
			//	}
			//}

			//entities1.Clear();
			//entities2.Clear();
			//ListPool<Entity>.Release( entities1 );
			//ListPool<Entity>.Release( entities2 );

			return minDistance;
		}

		public override void SetGoal()
		{
			GoalRetreat goalMarch = this.brain.PopGoal<GoalRetreat>();
			goalMarch.SetThreatnings( this._threatenings );
			this.brain.AddSubgoal( goalMarch );
			this._threatenings.Clear();
		}
	}
}