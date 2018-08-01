using System.Collections.Generic;
using Core.Math;
using Logic.Model;

namespace Logic.Controller
{
	public static class EntityUtils
	{
		public static void GetEntitiesInRect( List<Entity> entities, Vec3 central, float length, float width, ref List<Entity> result )
		{
			Rect rect = new Rect
			{
				size = new Vec2( length, width ),
				center = new Vec2( central.x, central.z )
			};
			Vec2 p;
			int count = entities.Count;
			for ( int i = 0; i < count; i++ )
			{
				Entity entity = entities[i];
				//先快速剔除不在矩形内的
				p.x = entity.property.position.x;
				p.y = entity.property.position.z;
				if ( !rect.Contains( p ) )
					continue;
				result.Add( entity );
			}
		}

		public static void GetEntitiesInCircle( List<Entity> entities, Vec3 central, float radius, ref List<Entity> result )
		{
			Rect rect = new Rect
			{
				size = new Vec2( radius * 2, radius * 2 ),
				center = new Vec2( central.x, central.z )
			};
			Vec2 p;
			float squRadius = radius * radius;
			int count = entities.Count;
			for ( int i = 0; i < count; i++ )
			{
				Entity entity = entities[i];
				//先快速剔除不在矩形内的
				p.x = entity.property.position.x;
				p.y = entity.property.position.z;
				if ( !rect.Contains( p ) )
					continue;
				//判断是否在圆内
				float xx = p.x - central.x;
				float zz = p.y - central.z;
				if ( xx * xx + zz * zz > squRadius )
					continue;
				result.Add( entity );
			}
		}

		public static void GetEntitiesInSector( List<Entity> entities, Vec3 central, float radius, ref List<Entity> result )
		{
		}

		public static bool CanAttack( Bio attacker, Bio target, CampType campType, EntityFlag targetFlag )
		{
			return !target.isDead &&
				   target.property.stealth <= 0 && //不在隐身状态下
				   CheckCampType( attacker, campType, target ) &&
				   CheckTargetFlag( targetFlag, target );
		}

		public static bool CheckCampType( Bio self, CampType campType, Bio target )
		{
			if ( ( campType & CampType.Self ) > 0 &&
				 target == self )
				return true;

			if ( ( campType & CampType.Allied ) > 0 &&
				 target.property.team == self.property.team &&
				 target != self )
				return true;

			if ( ( campType & CampType.Hostile ) > 0 &&
				 target.property.team != self.property.team && target.property.team != 2 )
				return true;

			if ( ( campType & CampType.Neutral ) > 0 &&
				 target.property.team == 2 )
				return true;

			return false;
		}

		public static bool CheckTargetFlag( EntityFlag targetFlag, Bio target )
		{
			if ( ( targetFlag & EntityFlag.Hero ) > 0 &&
				 ( target.flag & EntityFlag.Hero ) > 0 )
				return true;

			if ( ( targetFlag & EntityFlag.SmallPotato ) > 0 &&
				 ( target.flag & EntityFlag.SmallPotato ) > 0 )
				return true;

			if ( ( targetFlag & EntityFlag.Structure ) > 0 &&
				 ( target.flag & EntityFlag.Structure ) > 0 )
				return true;

			if ( ( targetFlag & EntityFlag.Missile ) > 0 &&
				 ( target.flag & EntityFlag.Missile ) > 0 )
				return true;

			if ( ( targetFlag & EntityFlag.Effect ) > 0 &&
				 ( target.flag & EntityFlag.Effect ) > 0 )
				return true;

			if ( ( targetFlag & EntityFlag.Item ) > 0 &&
				 ( target.flag & EntityFlag.Item ) > 0 )
				return true;

			return false;
		}

		public static void FilterLimit( ref List<Entity> targets, ref List<Entity> results, int targetNum )
		{
			if ( targetNum == 0 )
				return;

			if ( targets.Count <= targetNum )
			{
				results.AddRange( targets );
				return;
			}
			for ( int i = 0; i < targetNum; i++ )
				results.Add( targets[i] );
		}

		public static void FilterTarget( Bio self, CampType campType, EntityFlag targetFlag, ref List<Entity> targets, ref List<Entity> results )
		{
			int count = targets.Count;
			for ( int i = 0; i < count; i++ )
			{
				Bio target = targets[i] as Bio;
				if ( target == null || target.isDead )
					continue;

				//作为buff选择范围来进行筛选,隐身等状态应该被选中,因此这里不能使用CanAttack方法
				if ( CheckCampType( self, campType, target ) &&
				   CheckTargetFlag( targetFlag, target ) )
					results.Add( target );
			}
		}

		private static List<Entity> _temp = new List<Entity>();

		public static Bio GetNearestTarget( List<Entity> entities, Bio self, float radius, CampType campType, EntityFlag targetFlag )
		{
			GetEntitiesInCircle( entities, self.property.position, radius, ref _temp );

			float minDistance = float.MaxValue;
			Bio nearest = null;
			int count = _temp.Count;
			for ( int i = 0; i < count; i++ )
			{
				Bio target = _temp[i] as Bio;
				if ( target == null || !CanAttack( self, target, campType, targetFlag ) )
					continue;

				float d = ( self.property.position - target.property.position ).SqrMagnitude();
				if ( d < minDistance )
				{
					minDistance = d;
					nearest = target;
				}
			}

			_temp.Clear();
			return nearest;
		}
	}
}