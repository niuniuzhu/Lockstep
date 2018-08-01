using Logic.Model;

namespace View.Controller
{
	public static class VEntityUtils
	{
		public static bool IsSelf( VEntity a )
		{
			return a == VPlayer.instance;
		}

		public static bool IsAllied( VBio a, VBio b )
		{
			return a.property.team == b.property.team;
		}

		public static bool IsHostile( VBio a, VBio b )
		{
			return a.property.team != b.property.team;
		}

		public static bool IsNeutral( VBio a )
		{
			return a.property.team == 2;
		}

		public static bool IsHero( VEntity a )
		{
			return ( a.flag | EntityFlag.Hero ) > 0;
		}

		public static bool IsSmallPrtatp( VEntity a )
		{
			return ( a.flag | EntityFlag.SmallPotato ) > 0;
		}

		public static bool IsMissile( VEntity a )
		{
			return ( a.flag | EntityFlag.Missile ) > 0;
		}

		public static bool IsStructure( VEntity a )
		{
			return ( a.flag | EntityFlag.Structure ) > 0;
		}

		public static bool IsItem( VEntity a )
		{
			return ( a.flag | EntityFlag.Item ) > 0;
		}

		public static bool IsEffect( VEntity a )
		{
			return ( a.flag | EntityFlag.Effect ) > 0;
		}

		public static bool CanAttack( VBio attacker, VBio target, CampType campType, EntityFlag targetFlag )
		{
			return !target.isDead &&
				   target.property.stealth <= 0 && //不在隐身状态下
				   CheckCampType( attacker, campType, target ) &&
				   CheckTargetFlag( targetFlag, target );
		}

		public static bool CheckCampType( VBio self, CampType campType, VBio target )
		{
			if ( ( campType & CampType.Self ) > 0 &&
				 target == self )
				return true;

			if ( ( campType & CampType.Allied ) > 0 &&
				 target.property.team == self.property.team &&
				 target != self )
				return true;

			if ( ( campType & CampType.Hostile ) > 0 &&
				 target.property.team != self.property.team &&
				 target.property.team != 2 )
				return true;

			if ( ( campType & CampType.Neutral ) > 0 &&
				 target.property.team == 2 )
				return true;

			return false;
		}

		public static bool CheckTargetFlag( EntityFlag targetFlag, VBio target )
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
	}
}