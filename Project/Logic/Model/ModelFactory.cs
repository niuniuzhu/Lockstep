using System.Collections.Generic;

namespace Logic.Model
{
	public static class ModelFactory
	{
		private static readonly Dictionary<string, BattleData> BATTLE_DATAS = new Dictionary<string, BattleData>();
		private static readonly Dictionary<string, EntityData> ENTITY_DATAS = new Dictionary<string, EntityData>();
		private static readonly Dictionary<string, SkillData> SKILL_DATAS = new Dictionary<string, SkillData>();
		private static readonly Dictionary<string, BuffData> BUFF_DATAS = new Dictionary<string, BuffData>();
		private static readonly Dictionary<string, BuffStateData> BUFF_STATE_DATAS = new Dictionary<string, BuffStateData>();

		public static BattleData GetBattleData( string id )
		{
			BattleData data;
			if ( BATTLE_DATAS.TryGetValue( id, out data ) )
				return data;

			data = new BattleData();
			data.LoadFromDef( id );
			BATTLE_DATAS[data.id] = data;
			return data;
		}

		public static EntityData GetEntityData( string id )
		{
			EntityData data;
			if ( ENTITY_DATAS.TryGetValue( id, out data ) )
				return data;

			data = new EntityData();
			data.LoadFromDef( id );
			ENTITY_DATAS[data.id] = data;
			return data;
		}

		public static SkillData GetSkillData( string id )
		{
			SkillData data;
			if ( SKILL_DATAS.TryGetValue( id, out data ) )
				return data;

			data = new SkillData();
			data.LoadFromDef( id );
			SKILL_DATAS[data.id] = data;
			return data;
		}

		public static BuffData GetBuffData( string id )
		{
			BuffData data;
			if ( BUFF_DATAS.TryGetValue( id, out data ) )
				return data;

			data = new BuffData();
			data.LoadFromDef( id );
			BUFF_DATAS[data.id] = data;
			return data;
		}

		public static BuffStateData GetBuffStateData( string id )
		{
			BuffStateData data;
			if ( BUFF_STATE_DATAS.TryGetValue( id, out data ) )
				return data;

			data = new BuffStateData();
			data.LoadFromDef( id );
			BUFF_STATE_DATAS[data.id] = data;
			return data;
		}
	}
}