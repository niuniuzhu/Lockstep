using System;

namespace Logic.Model
{
	[Flags]
	public enum EntityFlag
	{
		Hero = 1 << 0,
		SmallPotato = 1 << 1,
		Structure = 1 << 2,
		Missile = 1 << 3,
		Effect = 1 << 4,
		Item = 1 << 5
	}

	[Flags]
	public enum CampType : uint
	{
		Hostile = 1 << 0,
		Allied = 1 << 1,
		Neutral = 1 << 2,
		Self = 1 << 3,
		Any = 0xFFFFFFFF
	}

	public enum CastType
	{
		Target,
		Point,
		Immediately,
		Dash
	}

	public enum RangeType
	{
		Single,
		Circle,
		Sector
	}

	public enum FlightType
	{
		Target,
		Point,
		Parabola,
		Directional
	}

	public enum EffectPositionType
	{
		DockedToCaster = 0,
		DockedToTarget = 1,
		FollowCaster = 2,
		FollowTarget = 3,
		DockedToBuff = 4,
		FollowBuff = 5
	}

	public enum EffectRotationType
	{
		Absolute,
		Follow
	}

	public enum Spare
	{
		Foothold = 0,
		Overhead = 1,
		HitPoint = 2,
		LHand = 3,
		RHand = 4,
		LFoot = 5,
		RFoot = 6,
		HeadNub = 7,
		Weapon0 = 8,
		Weapon1 = 9
	}

	public enum SpawnPoint
	{
		Target,
		Caster
	}

	public enum Orbit
	{
		Static = 0,
		FollowTarget = 1,
		CustomPosition = 2,
		FollowCaster = 3,
		Direction = 4
	}

	public enum DeadType
	{
		Timeup,
		WithCaster,
		WithMainTarget,
		WithTriggerTarget
	}

	public enum BeneficialType
	{
		Debuff,
		Buff
	}

	public enum BuffStateOverlapType
	{
		Replace,
		Overlap,
		Unique
	}

	public enum BuffStateType
	{
		Undefine = 0,
		Stunned = 1,
		Freezed = 2,
		Poisoned = 3,
		Impenetrable = 4,
		SpellImmunity = 5,
		Repelled = 6,
		Flurry = 7,
		BerserkerRage = 8,
		ViciousStrikes = 9,
		Undertow = 10,
		Ragnarok = 11,
		EssenceTheft = 12,
		OrbOfDeception = 13,
		Charm = 14,
		SpiritRush = 15
	}
}