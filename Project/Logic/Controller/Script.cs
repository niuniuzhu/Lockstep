using Logic.Model;
using XLua;

namespace Logic.Controller
{
	public class Script
	{
		public const string S_ON_BATTLE_INITIALIZED = "OnBattleInitialized";
		public const string S_ON_BATTLE_ENDED = "OnBattleEnded";
		public const string S_ON_BATTLE_DESTROIED = "OnBattleDestroied";

		public const string S_ON_ENTITY_ADDED_TO_BATTLE = "OnEntityAddedToBattle";
		public const string S_ON_ENTITY_REMOVED_FROM_BATTLE = "OnEntityRemovedFromBattle";
		public const string S_ON_ENTITY_DIE = "OnEntityDie";

		private LuaTable _scriptEnv;

		public Script( IScriptable owner, LuaEnv luaEnv, string scriptId )
		{
			string ownerId = owner.rid.Replace( '@', '_' );
			this._scriptEnv = luaEnv.NewTable();
			this._scriptEnv.Set( "owner", owner );
			luaEnv.Global.Set( ownerId, this._scriptEnv );
			scriptId = Defs.GetScript( scriptId );
			if ( !string.IsNullOrEmpty( scriptId ) )
			{
				scriptId = scriptId.Replace( "[T]", ownerId );
				luaEnv.DoString( scriptId );
			}
		}

		public void Dispose()
		{
			this._scriptEnv.Dispose();
			this._scriptEnv = null;
		}

		public void Call( string function, params object[] param )
		{
			this._scriptEnv.Get( function, out LuaFunction func );
			if ( func != null )
			{
				func.Call( this._scriptEnv, param );
				func.Dispose();
			}
		}
	}
}