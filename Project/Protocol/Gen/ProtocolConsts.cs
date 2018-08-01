namespace Protocol.Gen
{
	public static class Module
	{
		public const byte GENERIC = 100;
public const byte USER = 101;
public const byte HALL = 102;
public const byte ROOM = 103;
public const byte BATTLE = 104;
	}

	public static class Command
	{
		public const ushort QCMD_SYNC_TIME = 0;
public const ushort ACMD_REPLY = 32000;
public const ushort ACMD_SYNC_TIME = 32001;
public const ushort QCMD_LOGIN = 0;
public const ushort QCMD_REG = 1;
public const ushort QCMD_CREATE_USER = 2;
public const ushort QCMD_USER_INFOS = 3;
public const ushort QCMD_USER_ONLINE = 4;
public const ushort ACMD_USER_INFOS = 32001;
public const ushort ACMD_USER_ONLINE = 32002;
public const ushort QCMD_ROOM_LIST = 0;
public const ushort QCMD_CREATE_ROOM = 1;
public const ushort QCMD_JOIN_ROOM = 2;
public const ushort ACMD_ROOM_LIST = 32000;
public const ushort ACMD_BRO_ROOM_CREATED = 32001;
public const ushort ACMD_BRO_ROOM_DESTROIED = 32002;
public const ushort ACMD_JOIN_ROOM = 32003;
public const ushort QCMD_LEAVE_ROOM = 0;
public const ushort QCMD_ROOM_INFO = 1;
public const ushort QCMD_CHANGE_MAP = 2;
public const ushort QCMD_CHANGE_HERO = 3;
public const ushort QCMD_CHANGE_MODEL = 4;
public const ushort QCMD_CHANGE_SKIN = 5;
public const ushort QCMD_BEGIN_FIGHT = 6;
public const ushort QCMD_CHANGE_TEAM = 7;
public const ushort QCMD_ADD_FIGHT_READY = 8;
public const ushort QCMD_REMOVE_FIGHT_READY = 9;
public const ushort QCMD_MAP_READY = 10;
public const ushort ACMD_LEAVE_ROOM = 32000;
public const ushort ACMD_ROOM_INFO = 32001;
public const ushort ACMD_BEGIN_FIGHT = 32002;
public const ushort QCMD_BATTLE_CREATED = 0;
public const ushort QCMD_ACTION = 1;
public const ushort QCMD_LEAVE_BATTLE = 2;
public const ushort QCMD_END_BATTLE = 3;
public const ushort ACMD_ENTER_BATTLE = 32000;
public const ushort ACMD_FRAME = 32001;
public const ushort ACMD_BATTLE_START = 32002;
public const ushort ACMD_BATTLE_END = 32003;
	}
}