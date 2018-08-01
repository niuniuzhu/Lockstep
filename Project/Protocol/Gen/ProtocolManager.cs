// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
using Core.Net;
using Core.Net.Protocol;
using System;
using System.Collections.Generic;

namespace Protocol.Gen
{
	public static class ProtocolManager
	{
		private static readonly Dictionary<ushort, Type> DTO_MAP = new Dictionary<ushort, Type>
		{
			{ 0, typeof( _DTO_byte ) },
{ 1, typeof( _DTO_bool ) },
{ 2, typeof( _DTO_short ) },
{ 3, typeof( _DTO_ushort ) },
{ 4, typeof( _DTO_int ) },
{ 5, typeof( _DTO_uint ) },
{ 6, typeof( _DTO_float ) },
{ 7, typeof( _DTO_long ) },
{ 8, typeof( _DTO_ulong ) },
{ 9, typeof( _DTO_double ) },
{ 10, typeof( _DTO_string ) },
{ 11, typeof( _DTO_reply ) },
{ 12, typeof( _DTO_sync_time ) },
{ 13, typeof( _DTO_account ) },
{ 14, typeof( _DTO_charactor ) },
{ 15, typeof( _DTO_request_room_list ) },
{ 16, typeof( _DTO_room_list ) },
{ 17, typeof( _DTO_room_info ) },
{ 18, typeof( _DTO_room_info_detail ) },
{ 19, typeof( _DTO_player_info ) },
{ 20, typeof( _DTO_begin_fight ) },
{ 21, typeof( _DTO_frame_info ) },
{ 22, typeof( _DTO_action_info ) },
{ 23, typeof( _DTO_enter_battle ) },
		};

		private static readonly Dictionary<int, Type> PACKET_MAP = new Dictionary<int, Type>
		{
			{ EncodeID( 100, 0 ), typeof( _PACKET_GENERIC_QCMD_SYNC_TIME ) },
{ EncodeID( 100, 32000 ), typeof( _PACKET_GENERIC_ACMD_REPLY ) },
{ EncodeID( 100, 32001 ), typeof( _PACKET_GENERIC_ACMD_SYNC_TIME ) },
{ EncodeID( 101, 0 ), typeof( _PACKET_USER_QCMD_LOGIN ) },
{ EncodeID( 101, 1 ), typeof( _PACKET_USER_QCMD_REG ) },
{ EncodeID( 101, 2 ), typeof( _PACKET_USER_QCMD_CREATE_USER ) },
{ EncodeID( 101, 3 ), typeof( _PACKET_USER_QCMD_USER_INFOS ) },
{ EncodeID( 101, 4 ), typeof( _PACKET_USER_QCMD_USER_ONLINE ) },
{ EncodeID( 101, 32001 ), typeof( _PACKET_USER_ACMD_USER_INFOS ) },
{ EncodeID( 101, 32002 ), typeof( _PACKET_USER_ACMD_USER_ONLINE ) },
{ EncodeID( 102, 0 ), typeof( _PACKET_HALL_QCMD_ROOM_LIST ) },
{ EncodeID( 102, 1 ), typeof( _PACKET_HALL_QCMD_CREATE_ROOM ) },
{ EncodeID( 102, 2 ), typeof( _PACKET_HALL_QCMD_JOIN_ROOM ) },
{ EncodeID( 102, 32000 ), typeof( _PACKET_HALL_ACMD_ROOM_LIST ) },
{ EncodeID( 102, 32001 ), typeof( _PACKET_HALL_ACMD_BRO_ROOM_CREATED ) },
{ EncodeID( 102, 32002 ), typeof( _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED ) },
{ EncodeID( 102, 32003 ), typeof( _PACKET_HALL_ACMD_JOIN_ROOM ) },
{ EncodeID( 103, 0 ), typeof( _PACKET_ROOM_QCMD_LEAVE_ROOM ) },
{ EncodeID( 103, 1 ), typeof( _PACKET_ROOM_QCMD_ROOM_INFO ) },
{ EncodeID( 103, 2 ), typeof( _PACKET_ROOM_QCMD_CHANGE_MAP ) },
{ EncodeID( 103, 3 ), typeof( _PACKET_ROOM_QCMD_CHANGE_HERO ) },
{ EncodeID( 103, 4 ), typeof( _PACKET_ROOM_QCMD_CHANGE_MODEL ) },
{ EncodeID( 103, 5 ), typeof( _PACKET_ROOM_QCMD_CHANGE_SKIN ) },
{ EncodeID( 103, 6 ), typeof( _PACKET_ROOM_QCMD_BEGIN_FIGHT ) },
{ EncodeID( 103, 7 ), typeof( _PACKET_ROOM_QCMD_CHANGE_TEAM ) },
{ EncodeID( 103, 8 ), typeof( _PACKET_ROOM_QCMD_ADD_FIGHT_READY ) },
{ EncodeID( 103, 9 ), typeof( _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY ) },
{ EncodeID( 103, 10 ), typeof( _PACKET_ROOM_QCMD_MAP_READY ) },
{ EncodeID( 103, 32000 ), typeof( _PACKET_ROOM_ACMD_LEAVE_ROOM ) },
{ EncodeID( 103, 32001 ), typeof( _PACKET_ROOM_ACMD_ROOM_INFO ) },
{ EncodeID( 103, 32002 ), typeof( _PACKET_ROOM_ACMD_BEGIN_FIGHT ) },
{ EncodeID( 104, 0 ), typeof( _PACKET_BATTLE_QCMD_BATTLE_CREATED ) },
{ EncodeID( 104, 1 ), typeof( _PACKET_BATTLE_QCMD_ACTION ) },
{ EncodeID( 104, 2 ), typeof( _PACKET_BATTLE_QCMD_LEAVE_BATTLE ) },
{ EncodeID( 104, 3 ), typeof( _PACKET_BATTLE_QCMD_END_BATTLE ) },
{ EncodeID( 104, 32000 ), typeof( _PACKET_BATTLE_ACMD_ENTER_BATTLE ) },
{ EncodeID( 104, 32001 ), typeof( _PACKET_BATTLE_ACMD_FRAME ) },
{ EncodeID( 104, 32002 ), typeof( _PACKET_BATTLE_ACMD_BATTLE_START ) },
{ EncodeID( 104, 32003 ), typeof( _PACKET_BATTLE_ACMD_BATTLE_END ) },
		};
		
		public static Type GetDTOType( ushort dtoId )
		{
			DTO_MAP.TryGetValue( dtoId, out Type type );
			return type;
		}
		
		public static Type GetPacketType( byte module, ushort command )
		{
			PACKET_MAP.TryGetValue( EncodeID( module, command ), out Type type );
			return type;
		}

		public static int EncodeID( byte moduleId, ushort cmd )
		{
			return ( moduleId << 16 ) | cmd;
		}

		public static _DTO_byte DTO_byte(  )
		{
			return new _DTO_byte(  );
		}
public static _DTO_byte DTO_byte( byte value )
		{
			return new _DTO_byte( value );
		}
public static _DTO_bool DTO_bool(  )
		{
			return new _DTO_bool(  );
		}
public static _DTO_bool DTO_bool( bool value )
		{
			return new _DTO_bool( value );
		}
public static _DTO_short DTO_short(  )
		{
			return new _DTO_short(  );
		}
public static _DTO_short DTO_short( short value )
		{
			return new _DTO_short( value );
		}
public static _DTO_ushort DTO_ushort(  )
		{
			return new _DTO_ushort(  );
		}
public static _DTO_ushort DTO_ushort( ushort value )
		{
			return new _DTO_ushort( value );
		}
public static _DTO_int DTO_int(  )
		{
			return new _DTO_int(  );
		}
public static _DTO_int DTO_int( int value )
		{
			return new _DTO_int( value );
		}
public static _DTO_uint DTO_uint(  )
		{
			return new _DTO_uint(  );
		}
public static _DTO_uint DTO_uint( uint value )
		{
			return new _DTO_uint( value );
		}
public static _DTO_float DTO_float(  )
		{
			return new _DTO_float(  );
		}
public static _DTO_float DTO_float( float value )
		{
			return new _DTO_float( value );
		}
public static _DTO_long DTO_long(  )
		{
			return new _DTO_long(  );
		}
public static _DTO_long DTO_long( long value )
		{
			return new _DTO_long( value );
		}
public static _DTO_ulong DTO_ulong(  )
		{
			return new _DTO_ulong(  );
		}
public static _DTO_ulong DTO_ulong( ulong value )
		{
			return new _DTO_ulong( value );
		}
public static _DTO_double DTO_double(  )
		{
			return new _DTO_double(  );
		}
public static _DTO_double DTO_double( double value )
		{
			return new _DTO_double( value );
		}
public static _DTO_string DTO_string(  )
		{
			return new _DTO_string(  );
		}
public static _DTO_string DTO_string( string value )
		{
			return new _DTO_string( value );
		}
public static _DTO_reply DTO_reply(  )
		{
			return new _DTO_reply(  );
		}
public static _DTO_reply DTO_reply( ushort result,ushort src_cmd,byte src_module )
		{
			return new _DTO_reply( result,src_cmd,src_module );
		}
public static _DTO_sync_time DTO_sync_time(  )
		{
			return new _DTO_sync_time(  );
		}
public static _DTO_sync_time DTO_sync_time( long clientTime,long serverTime )
		{
			return new _DTO_sync_time( clientTime,serverTime );
		}
public static _DTO_account DTO_account(  )
		{
			return new _DTO_account(  );
		}
public static _DTO_account DTO_account( string account,string password )
		{
			return new _DTO_account( account,password );
		}
public static _DTO_charactor DTO_charactor(  )
		{
			return new _DTO_charactor(  );
		}
public static _DTO_charactor DTO_charactor( string name,string uid )
		{
			return new _DTO_charactor( name,uid );
		}
public static _DTO_request_room_list DTO_request_room_list(  )
		{
			return new _DTO_request_room_list(  );
		}
public static _DTO_request_room_list DTO_request_room_list( byte count,byte from )
		{
			return new _DTO_request_room_list( count,from );
		}
public static _DTO_room_list DTO_room_list(  )
		{
			return new _DTO_room_list(  );
		}
public static _DTO_room_list DTO_room_list( _DTO_room_info[] rs )
		{
			return new _DTO_room_list( rs );
		}
public static _DTO_room_info DTO_room_info(  )
		{
			return new _DTO_room_info(  );
		}
public static _DTO_room_info DTO_room_info( int ct,string map,string name,int roomId )
		{
			return new _DTO_room_info( ct,map,name,roomId );
		}
public static _DTO_room_info_detail DTO_room_info_detail(  )
		{
			return new _DTO_room_info_detail(  );
		}
public static _DTO_room_info_detail DTO_room_info_detail( string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			return new _DTO_room_info_detail( host,map,name,players,roomId );
		}
public static _DTO_player_info DTO_player_info(  )
		{
			return new _DTO_player_info(  );
		}
public static _DTO_player_info DTO_player_info( string cid,string name,bool ready,byte skin,byte team,string uid )
		{
			return new _DTO_player_info( cid,name,ready,skin,team,uid );
		}
public static _DTO_begin_fight DTO_begin_fight(  )
		{
			return new _DTO_begin_fight(  );
		}
public static _DTO_begin_fight DTO_begin_fight( string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			return new _DTO_begin_fight( host,map,name,players,roomId );
		}
public static _DTO_frame_info DTO_frame_info(  )
		{
			return new _DTO_frame_info(  );
		}
public static _DTO_frame_info DTO_frame_info( _DTO_action_info[] actions,int frameId )
		{
			return new _DTO_frame_info( actions,frameId );
		}
public static _DTO_action_info DTO_action_info(  )
		{
			return new _DTO_action_info(  );
		}
public static _DTO_action_info DTO_action_info( string sender,byte type,float x,float y,float z,string target,string sid,string src )
		{
			return new _DTO_action_info( sender,type,x,y,z,target,sid,src );
		}
public static _DTO_action_info DTO_action_info( string sender,byte type,float x,float y,float z )
		{
			return new _DTO_action_info( sender,type,x,y,z );
		}
public static _DTO_action_info DTO_action_info( string sender,byte type,string target )
		{
			return new _DTO_action_info( sender,type,target );
		}
public static _DTO_action_info DTO_action_info( string sender,byte type,string sid,string src,string target,float x,float y,float z )
		{
			return new _DTO_action_info( sender,type,sid,src,target,x,y,z );
		}
public static _DTO_action_info DTO_action_info( string sender,byte type )
		{
			return new _DTO_action_info( sender,type );
		}
public static _DTO_enter_battle DTO_enter_battle(  )
		{
			return new _DTO_enter_battle(  );
		}
public static _DTO_enter_battle DTO_enter_battle( int frameRate,int framesPerKeyFrame,string mapId,_DTO_player_info[] players,int rndSeed,string uid )
		{
			return new _DTO_enter_battle( frameRate,framesPerKeyFrame,mapId,players,rndSeed,uid );
		}

		public static _PACKET_GENERIC_QCMD_SYNC_TIME PACKET_GENERIC_QCMD_SYNC_TIME( _DTO_long dto )
		{
			return new _PACKET_GENERIC_QCMD_SYNC_TIME( dto );
		}
public static _PACKET_GENERIC_ACMD_REPLY PACKET_GENERIC_ACMD_REPLY( _DTO_reply dto )
		{
			return new _PACKET_GENERIC_ACMD_REPLY( dto );
		}
public static _PACKET_GENERIC_ACMD_SYNC_TIME PACKET_GENERIC_ACMD_SYNC_TIME( _DTO_sync_time dto )
		{
			return new _PACKET_GENERIC_ACMD_SYNC_TIME( dto );
		}
public static _PACKET_USER_QCMD_LOGIN PACKET_USER_QCMD_LOGIN( _DTO_account dto )
		{
			return new _PACKET_USER_QCMD_LOGIN( dto );
		}
public static _PACKET_USER_QCMD_REG PACKET_USER_QCMD_REG( _DTO_account dto )
		{
			return new _PACKET_USER_QCMD_REG( dto );
		}
public static _PACKET_USER_QCMD_CREATE_USER PACKET_USER_QCMD_CREATE_USER( _DTO_string dto )
		{
			return new _PACKET_USER_QCMD_CREATE_USER( dto );
		}
public static _PACKET_USER_ACMD_USER_INFOS PACKET_USER_ACMD_USER_INFOS( _DTO_charactor dto )
		{
			return new _PACKET_USER_ACMD_USER_INFOS( dto );
		}
public static _PACKET_USER_ACMD_USER_ONLINE PACKET_USER_ACMD_USER_ONLINE( _DTO_charactor dto )
		{
			return new _PACKET_USER_ACMD_USER_ONLINE( dto );
		}
public static _PACKET_HALL_QCMD_ROOM_LIST PACKET_HALL_QCMD_ROOM_LIST( _DTO_request_room_list dto )
		{
			return new _PACKET_HALL_QCMD_ROOM_LIST( dto );
		}
public static _PACKET_HALL_QCMD_CREATE_ROOM PACKET_HALL_QCMD_CREATE_ROOM( _DTO_string dto )
		{
			return new _PACKET_HALL_QCMD_CREATE_ROOM( dto );
		}
public static _PACKET_HALL_QCMD_JOIN_ROOM PACKET_HALL_QCMD_JOIN_ROOM( _DTO_int dto )
		{
			return new _PACKET_HALL_QCMD_JOIN_ROOM( dto );
		}
public static _PACKET_HALL_ACMD_ROOM_LIST PACKET_HALL_ACMD_ROOM_LIST( _DTO_room_list dto )
		{
			return new _PACKET_HALL_ACMD_ROOM_LIST( dto );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_CREATED PACKET_HALL_ACMD_BRO_ROOM_CREATED( _DTO_room_info dto )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_CREATED( dto );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( _DTO_int dto )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( dto );
		}
public static _PACKET_HALL_ACMD_JOIN_ROOM PACKET_HALL_ACMD_JOIN_ROOM( _DTO_int dto )
		{
			return new _PACKET_HALL_ACMD_JOIN_ROOM( dto );
		}
public static _PACKET_ROOM_QCMD_ROOM_INFO PACKET_ROOM_QCMD_ROOM_INFO( _DTO_int dto )
		{
			return new _PACKET_ROOM_QCMD_ROOM_INFO( dto );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MAP PACKET_ROOM_QCMD_CHANGE_MAP( _DTO_string dto )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MAP( dto );
		}
public static _PACKET_ROOM_QCMD_CHANGE_HERO PACKET_ROOM_QCMD_CHANGE_HERO( _DTO_string dto )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_HERO( dto );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MODEL PACKET_ROOM_QCMD_CHANGE_MODEL( _DTO_byte dto )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MODEL( dto );
		}
public static _PACKET_ROOM_QCMD_CHANGE_SKIN PACKET_ROOM_QCMD_CHANGE_SKIN( _DTO_byte dto )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_SKIN( dto );
		}
public static _PACKET_ROOM_QCMD_CHANGE_TEAM PACKET_ROOM_QCMD_CHANGE_TEAM( _DTO_byte dto )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_TEAM( dto );
		}
public static _PACKET_ROOM_ACMD_ROOM_INFO PACKET_ROOM_ACMD_ROOM_INFO( _DTO_room_info_detail dto )
		{
			return new _PACKET_ROOM_ACMD_ROOM_INFO( dto );
		}
public static _PACKET_ROOM_ACMD_BEGIN_FIGHT PACKET_ROOM_ACMD_BEGIN_FIGHT( _DTO_begin_fight dto )
		{
			return new _PACKET_ROOM_ACMD_BEGIN_FIGHT( dto );
		}
public static _PACKET_BATTLE_QCMD_ACTION PACKET_BATTLE_QCMD_ACTION( _DTO_frame_info dto )
		{
			return new _PACKET_BATTLE_QCMD_ACTION( dto );
		}
public static _PACKET_BATTLE_QCMD_END_BATTLE PACKET_BATTLE_QCMD_END_BATTLE( _DTO_byte dto )
		{
			return new _PACKET_BATTLE_QCMD_END_BATTLE( dto );
		}
public static _PACKET_BATTLE_ACMD_ENTER_BATTLE PACKET_BATTLE_ACMD_ENTER_BATTLE( _DTO_enter_battle dto )
		{
			return new _PACKET_BATTLE_ACMD_ENTER_BATTLE( dto );
		}
public static _PACKET_BATTLE_ACMD_FRAME PACKET_BATTLE_ACMD_FRAME( _DTO_frame_info dto )
		{
			return new _PACKET_BATTLE_ACMD_FRAME( dto );
		}
public static _PACKET_BATTLE_ACMD_BATTLE_END PACKET_BATTLE_ACMD_BATTLE_END( _DTO_byte dto )
		{
			return new _PACKET_BATTLE_ACMD_BATTLE_END( dto );
		}
		public static _PACKET_GENERIC_QCMD_SYNC_TIME PACKET_GENERIC_QCMD_SYNC_TIME(  )
		{
			return new _PACKET_GENERIC_QCMD_SYNC_TIME(  );
		}
public static _PACKET_GENERIC_QCMD_SYNC_TIME PACKET_GENERIC_QCMD_SYNC_TIME( long value )
		{
			return new _PACKET_GENERIC_QCMD_SYNC_TIME( value );
		}
public static _PACKET_GENERIC_ACMD_REPLY PACKET_GENERIC_ACMD_REPLY(  )
		{
			return new _PACKET_GENERIC_ACMD_REPLY(  );
		}
public static _PACKET_GENERIC_ACMD_REPLY PACKET_GENERIC_ACMD_REPLY( ushort result,ushort src_cmd,byte src_module )
		{
			return new _PACKET_GENERIC_ACMD_REPLY( result,src_cmd,src_module );
		}
public static _PACKET_GENERIC_ACMD_SYNC_TIME PACKET_GENERIC_ACMD_SYNC_TIME(  )
		{
			return new _PACKET_GENERIC_ACMD_SYNC_TIME(  );
		}
public static _PACKET_GENERIC_ACMD_SYNC_TIME PACKET_GENERIC_ACMD_SYNC_TIME( long clientTime,long serverTime )
		{
			return new _PACKET_GENERIC_ACMD_SYNC_TIME( clientTime,serverTime );
		}
public static _PACKET_USER_QCMD_LOGIN PACKET_USER_QCMD_LOGIN(  )
		{
			return new _PACKET_USER_QCMD_LOGIN(  );
		}
public static _PACKET_USER_QCMD_LOGIN PACKET_USER_QCMD_LOGIN( string account,string password )
		{
			return new _PACKET_USER_QCMD_LOGIN( account,password );
		}
public static _PACKET_USER_QCMD_REG PACKET_USER_QCMD_REG(  )
		{
			return new _PACKET_USER_QCMD_REG(  );
		}
public static _PACKET_USER_QCMD_REG PACKET_USER_QCMD_REG( string account,string password )
		{
			return new _PACKET_USER_QCMD_REG( account,password );
		}
public static _PACKET_USER_QCMD_CREATE_USER PACKET_USER_QCMD_CREATE_USER(  )
		{
			return new _PACKET_USER_QCMD_CREATE_USER(  );
		}
public static _PACKET_USER_QCMD_CREATE_USER PACKET_USER_QCMD_CREATE_USER( string value )
		{
			return new _PACKET_USER_QCMD_CREATE_USER( value );
		}
public static _PACKET_USER_QCMD_USER_INFOS PACKET_USER_QCMD_USER_INFOS(  )
		{
			return new _PACKET_USER_QCMD_USER_INFOS(  );
		}
public static _PACKET_USER_QCMD_USER_ONLINE PACKET_USER_QCMD_USER_ONLINE(  )
		{
			return new _PACKET_USER_QCMD_USER_ONLINE(  );
		}
public static _PACKET_USER_ACMD_USER_INFOS PACKET_USER_ACMD_USER_INFOS(  )
		{
			return new _PACKET_USER_ACMD_USER_INFOS(  );
		}
public static _PACKET_USER_ACMD_USER_INFOS PACKET_USER_ACMD_USER_INFOS( string name,string uid )
		{
			return new _PACKET_USER_ACMD_USER_INFOS( name,uid );
		}
public static _PACKET_USER_ACMD_USER_ONLINE PACKET_USER_ACMD_USER_ONLINE(  )
		{
			return new _PACKET_USER_ACMD_USER_ONLINE(  );
		}
public static _PACKET_USER_ACMD_USER_ONLINE PACKET_USER_ACMD_USER_ONLINE( string name,string uid )
		{
			return new _PACKET_USER_ACMD_USER_ONLINE( name,uid );
		}
public static _PACKET_HALL_QCMD_ROOM_LIST PACKET_HALL_QCMD_ROOM_LIST(  )
		{
			return new _PACKET_HALL_QCMD_ROOM_LIST(  );
		}
public static _PACKET_HALL_QCMD_ROOM_LIST PACKET_HALL_QCMD_ROOM_LIST( byte count,byte from )
		{
			return new _PACKET_HALL_QCMD_ROOM_LIST( count,from );
		}
public static _PACKET_HALL_QCMD_CREATE_ROOM PACKET_HALL_QCMD_CREATE_ROOM(  )
		{
			return new _PACKET_HALL_QCMD_CREATE_ROOM(  );
		}
public static _PACKET_HALL_QCMD_CREATE_ROOM PACKET_HALL_QCMD_CREATE_ROOM( string value )
		{
			return new _PACKET_HALL_QCMD_CREATE_ROOM( value );
		}
public static _PACKET_HALL_QCMD_JOIN_ROOM PACKET_HALL_QCMD_JOIN_ROOM(  )
		{
			return new _PACKET_HALL_QCMD_JOIN_ROOM(  );
		}
public static _PACKET_HALL_QCMD_JOIN_ROOM PACKET_HALL_QCMD_JOIN_ROOM( int value )
		{
			return new _PACKET_HALL_QCMD_JOIN_ROOM( value );
		}
public static _PACKET_HALL_ACMD_ROOM_LIST PACKET_HALL_ACMD_ROOM_LIST(  )
		{
			return new _PACKET_HALL_ACMD_ROOM_LIST(  );
		}
public static _PACKET_HALL_ACMD_ROOM_LIST PACKET_HALL_ACMD_ROOM_LIST( _DTO_room_info[] rs )
		{
			return new _PACKET_HALL_ACMD_ROOM_LIST( rs );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_CREATED PACKET_HALL_ACMD_BRO_ROOM_CREATED(  )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_CREATED(  );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_CREATED PACKET_HALL_ACMD_BRO_ROOM_CREATED( int ct,string map,string name,int roomId )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_CREATED( ct,map,name,roomId );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED PACKET_HALL_ACMD_BRO_ROOM_DESTROIED(  )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED(  );
		}
public static _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( int value )
		{
			return new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( value );
		}
public static _PACKET_HALL_ACMD_JOIN_ROOM PACKET_HALL_ACMD_JOIN_ROOM(  )
		{
			return new _PACKET_HALL_ACMD_JOIN_ROOM(  );
		}
public static _PACKET_HALL_ACMD_JOIN_ROOM PACKET_HALL_ACMD_JOIN_ROOM( int value )
		{
			return new _PACKET_HALL_ACMD_JOIN_ROOM( value );
		}
public static _PACKET_ROOM_QCMD_LEAVE_ROOM PACKET_ROOM_QCMD_LEAVE_ROOM(  )
		{
			return new _PACKET_ROOM_QCMD_LEAVE_ROOM(  );
		}
public static _PACKET_ROOM_QCMD_ROOM_INFO PACKET_ROOM_QCMD_ROOM_INFO(  )
		{
			return new _PACKET_ROOM_QCMD_ROOM_INFO(  );
		}
public static _PACKET_ROOM_QCMD_ROOM_INFO PACKET_ROOM_QCMD_ROOM_INFO( int value )
		{
			return new _PACKET_ROOM_QCMD_ROOM_INFO( value );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MAP PACKET_ROOM_QCMD_CHANGE_MAP(  )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MAP(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MAP PACKET_ROOM_QCMD_CHANGE_MAP( string value )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MAP( value );
		}
public static _PACKET_ROOM_QCMD_CHANGE_HERO PACKET_ROOM_QCMD_CHANGE_HERO(  )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_HERO(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_HERO PACKET_ROOM_QCMD_CHANGE_HERO( string value )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_HERO( value );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MODEL PACKET_ROOM_QCMD_CHANGE_MODEL(  )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MODEL(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_MODEL PACKET_ROOM_QCMD_CHANGE_MODEL( byte value )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_MODEL( value );
		}
public static _PACKET_ROOM_QCMD_CHANGE_SKIN PACKET_ROOM_QCMD_CHANGE_SKIN(  )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_SKIN(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_SKIN PACKET_ROOM_QCMD_CHANGE_SKIN( byte value )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_SKIN( value );
		}
public static _PACKET_ROOM_QCMD_BEGIN_FIGHT PACKET_ROOM_QCMD_BEGIN_FIGHT(  )
		{
			return new _PACKET_ROOM_QCMD_BEGIN_FIGHT(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_TEAM PACKET_ROOM_QCMD_CHANGE_TEAM(  )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_TEAM(  );
		}
public static _PACKET_ROOM_QCMD_CHANGE_TEAM PACKET_ROOM_QCMD_CHANGE_TEAM( byte value )
		{
			return new _PACKET_ROOM_QCMD_CHANGE_TEAM( value );
		}
public static _PACKET_ROOM_QCMD_ADD_FIGHT_READY PACKET_ROOM_QCMD_ADD_FIGHT_READY(  )
		{
			return new _PACKET_ROOM_QCMD_ADD_FIGHT_READY(  );
		}
public static _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY PACKET_ROOM_QCMD_REMOVE_FIGHT_READY(  )
		{
			return new _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY(  );
		}
public static _PACKET_ROOM_QCMD_MAP_READY PACKET_ROOM_QCMD_MAP_READY(  )
		{
			return new _PACKET_ROOM_QCMD_MAP_READY(  );
		}
public static _PACKET_ROOM_ACMD_LEAVE_ROOM PACKET_ROOM_ACMD_LEAVE_ROOM(  )
		{
			return new _PACKET_ROOM_ACMD_LEAVE_ROOM(  );
		}
public static _PACKET_ROOM_ACMD_ROOM_INFO PACKET_ROOM_ACMD_ROOM_INFO(  )
		{
			return new _PACKET_ROOM_ACMD_ROOM_INFO(  );
		}
public static _PACKET_ROOM_ACMD_ROOM_INFO PACKET_ROOM_ACMD_ROOM_INFO( string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			return new _PACKET_ROOM_ACMD_ROOM_INFO( host,map,name,players,roomId );
		}
public static _PACKET_ROOM_ACMD_BEGIN_FIGHT PACKET_ROOM_ACMD_BEGIN_FIGHT(  )
		{
			return new _PACKET_ROOM_ACMD_BEGIN_FIGHT(  );
		}
public static _PACKET_ROOM_ACMD_BEGIN_FIGHT PACKET_ROOM_ACMD_BEGIN_FIGHT( string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			return new _PACKET_ROOM_ACMD_BEGIN_FIGHT( host,map,name,players,roomId );
		}
public static _PACKET_BATTLE_QCMD_BATTLE_CREATED PACKET_BATTLE_QCMD_BATTLE_CREATED(  )
		{
			return new _PACKET_BATTLE_QCMD_BATTLE_CREATED(  );
		}
public static _PACKET_BATTLE_QCMD_ACTION PACKET_BATTLE_QCMD_ACTION(  )
		{
			return new _PACKET_BATTLE_QCMD_ACTION(  );
		}
public static _PACKET_BATTLE_QCMD_ACTION PACKET_BATTLE_QCMD_ACTION( _DTO_action_info[] actions,int frameId )
		{
			return new _PACKET_BATTLE_QCMD_ACTION( actions,frameId );
		}
public static _PACKET_BATTLE_QCMD_LEAVE_BATTLE PACKET_BATTLE_QCMD_LEAVE_BATTLE(  )
		{
			return new _PACKET_BATTLE_QCMD_LEAVE_BATTLE(  );
		}
public static _PACKET_BATTLE_QCMD_END_BATTLE PACKET_BATTLE_QCMD_END_BATTLE(  )
		{
			return new _PACKET_BATTLE_QCMD_END_BATTLE(  );
		}
public static _PACKET_BATTLE_QCMD_END_BATTLE PACKET_BATTLE_QCMD_END_BATTLE( byte value )
		{
			return new _PACKET_BATTLE_QCMD_END_BATTLE( value );
		}
public static _PACKET_BATTLE_ACMD_ENTER_BATTLE PACKET_BATTLE_ACMD_ENTER_BATTLE(  )
		{
			return new _PACKET_BATTLE_ACMD_ENTER_BATTLE(  );
		}
public static _PACKET_BATTLE_ACMD_ENTER_BATTLE PACKET_BATTLE_ACMD_ENTER_BATTLE( int frameRate,int framesPerKeyFrame,string mapId,_DTO_player_info[] players,int rndSeed,string uid )
		{
			return new _PACKET_BATTLE_ACMD_ENTER_BATTLE( frameRate,framesPerKeyFrame,mapId,players,rndSeed,uid );
		}
public static _PACKET_BATTLE_ACMD_FRAME PACKET_BATTLE_ACMD_FRAME(  )
		{
			return new _PACKET_BATTLE_ACMD_FRAME(  );
		}
public static _PACKET_BATTLE_ACMD_FRAME PACKET_BATTLE_ACMD_FRAME( _DTO_action_info[] actions,int frameId )
		{
			return new _PACKET_BATTLE_ACMD_FRAME( actions,frameId );
		}
public static _PACKET_BATTLE_ACMD_BATTLE_START PACKET_BATTLE_ACMD_BATTLE_START(  )
		{
			return new _PACKET_BATTLE_ACMD_BATTLE_START(  );
		}
public static _PACKET_BATTLE_ACMD_BATTLE_END PACKET_BATTLE_ACMD_BATTLE_END(  )
		{
			return new _PACKET_BATTLE_ACMD_BATTLE_END(  );
		}
public static _PACKET_BATTLE_ACMD_BATTLE_END PACKET_BATTLE_ACMD_BATTLE_END( byte value )
		{
			return new _PACKET_BATTLE_ACMD_BATTLE_END( value );
		}
		public static void CALL_GENERIC_QCMD_SYNC_TIME( this INetTransmitter transmitter, _DTO_long dto )
		{
			transmitter.Send( new _PACKET_GENERIC_QCMD_SYNC_TIME( dto ) );
		}
public static void CALL_GENERIC_ACMD_REPLY( this INetTransmitter transmitter, _DTO_reply dto )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_REPLY( dto ) );
		}
public static void CALL_GENERIC_ACMD_SYNC_TIME( this INetTransmitter transmitter, _DTO_sync_time dto )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_SYNC_TIME( dto ) );
		}
public static void CALL_USER_QCMD_LOGIN( this INetTransmitter transmitter, _DTO_account dto )
		{
			transmitter.Send( new _PACKET_USER_QCMD_LOGIN( dto ) );
		}
public static void CALL_USER_QCMD_REG( this INetTransmitter transmitter, _DTO_account dto )
		{
			transmitter.Send( new _PACKET_USER_QCMD_REG( dto ) );
		}
public static void CALL_USER_QCMD_CREATE_USER( this INetTransmitter transmitter, _DTO_string dto )
		{
			transmitter.Send( new _PACKET_USER_QCMD_CREATE_USER( dto ) );
		}
public static void CALL_USER_ACMD_USER_INFOS( this INetTransmitter transmitter, _DTO_charactor dto )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_INFOS( dto ) );
		}
public static void CALL_USER_ACMD_USER_ONLINE( this INetTransmitter transmitter, _DTO_charactor dto )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_ONLINE( dto ) );
		}
public static void CALL_HALL_QCMD_ROOM_LIST( this INetTransmitter transmitter, _DTO_request_room_list dto )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_ROOM_LIST( dto ) );
		}
public static void CALL_HALL_QCMD_CREATE_ROOM( this INetTransmitter transmitter, _DTO_string dto )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_CREATE_ROOM( dto ) );
		}
public static void CALL_HALL_QCMD_JOIN_ROOM( this INetTransmitter transmitter, _DTO_int dto )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_JOIN_ROOM( dto ) );
		}
public static void CALL_HALL_ACMD_ROOM_LIST( this INetTransmitter transmitter, _DTO_room_list dto )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_ROOM_LIST( dto ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_CREATED( this INetTransmitter transmitter, _DTO_room_info dto )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_CREATED( dto ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_DESTROIED( this INetTransmitter transmitter, _DTO_int dto )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( dto ) );
		}
public static void CALL_HALL_ACMD_JOIN_ROOM( this INetTransmitter transmitter, _DTO_int dto )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_JOIN_ROOM( dto ) );
		}
public static void CALL_ROOM_QCMD_ROOM_INFO( this INetTransmitter transmitter, _DTO_int dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_ROOM_INFO( dto ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MAP( this INetTransmitter transmitter, _DTO_string dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MAP( dto ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_HERO( this INetTransmitter transmitter, _DTO_string dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_HERO( dto ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MODEL( this INetTransmitter transmitter, _DTO_byte dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MODEL( dto ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_SKIN( this INetTransmitter transmitter, _DTO_byte dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_SKIN( dto ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_TEAM( this INetTransmitter transmitter, _DTO_byte dto )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_TEAM( dto ) );
		}
public static void CALL_ROOM_ACMD_ROOM_INFO( this INetTransmitter transmitter, _DTO_room_info_detail dto )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_ROOM_INFO( dto ) );
		}
public static void CALL_ROOM_ACMD_BEGIN_FIGHT( this INetTransmitter transmitter, _DTO_begin_fight dto )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_BEGIN_FIGHT( dto ) );
		}
public static void CALL_BATTLE_QCMD_ACTION( this INetTransmitter transmitter, _DTO_frame_info dto )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_ACTION( dto ) );
		}
public static void CALL_BATTLE_QCMD_END_BATTLE( this INetTransmitter transmitter, _DTO_byte dto )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_END_BATTLE( dto ) );
		}
public static void CALL_BATTLE_ACMD_ENTER_BATTLE( this INetTransmitter transmitter, _DTO_enter_battle dto )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_ENTER_BATTLE( dto ) );
		}
public static void CALL_BATTLE_ACMD_FRAME( this INetTransmitter transmitter, _DTO_frame_info dto )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_FRAME( dto ) );
		}
public static void CALL_BATTLE_ACMD_BATTLE_END( this INetTransmitter transmitter, _DTO_byte dto )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_BATTLE_END( dto ) );
		}
		public static void CALL_GENERIC_QCMD_SYNC_TIME( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_GENERIC_QCMD_SYNC_TIME(  ) );
		}
public static void CALL_GENERIC_QCMD_SYNC_TIME( this INetTransmitter transmitter, long value )
		{
			transmitter.Send( new _PACKET_GENERIC_QCMD_SYNC_TIME( value ) );
		}
public static void CALL_GENERIC_ACMD_REPLY( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_REPLY(  ) );
		}
public static void CALL_GENERIC_ACMD_REPLY( this INetTransmitter transmitter, ushort result,ushort src_cmd,byte src_module )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_REPLY( result,src_cmd,src_module ) );
		}
public static void CALL_GENERIC_ACMD_SYNC_TIME( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_SYNC_TIME(  ) );
		}
public static void CALL_GENERIC_ACMD_SYNC_TIME( this INetTransmitter transmitter, long clientTime,long serverTime )
		{
			transmitter.Send( new _PACKET_GENERIC_ACMD_SYNC_TIME( clientTime,serverTime ) );
		}
public static void CALL_USER_QCMD_LOGIN( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_QCMD_LOGIN(  ) );
		}
public static void CALL_USER_QCMD_LOGIN( this INetTransmitter transmitter, string account,string password )
		{
			transmitter.Send( new _PACKET_USER_QCMD_LOGIN( account,password ) );
		}
public static void CALL_USER_QCMD_REG( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_QCMD_REG(  ) );
		}
public static void CALL_USER_QCMD_REG( this INetTransmitter transmitter, string account,string password )
		{
			transmitter.Send( new _PACKET_USER_QCMD_REG( account,password ) );
		}
public static void CALL_USER_QCMD_CREATE_USER( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_QCMD_CREATE_USER(  ) );
		}
public static void CALL_USER_QCMD_CREATE_USER( this INetTransmitter transmitter, string value )
		{
			transmitter.Send( new _PACKET_USER_QCMD_CREATE_USER( value ) );
		}
public static void CALL_USER_QCMD_USER_INFOS( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_QCMD_USER_INFOS(  ) );
		}
public static void CALL_USER_QCMD_USER_ONLINE( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_QCMD_USER_ONLINE(  ) );
		}
public static void CALL_USER_ACMD_USER_INFOS( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_INFOS(  ) );
		}
public static void CALL_USER_ACMD_USER_INFOS( this INetTransmitter transmitter, string name,string uid )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_INFOS( name,uid ) );
		}
public static void CALL_USER_ACMD_USER_ONLINE( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_ONLINE(  ) );
		}
public static void CALL_USER_ACMD_USER_ONLINE( this INetTransmitter transmitter, string name,string uid )
		{
			transmitter.Send( new _PACKET_USER_ACMD_USER_ONLINE( name,uid ) );
		}
public static void CALL_HALL_QCMD_ROOM_LIST( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_ROOM_LIST(  ) );
		}
public static void CALL_HALL_QCMD_ROOM_LIST( this INetTransmitter transmitter, byte count,byte from )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_ROOM_LIST( count,from ) );
		}
public static void CALL_HALL_QCMD_CREATE_ROOM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_CREATE_ROOM(  ) );
		}
public static void CALL_HALL_QCMD_CREATE_ROOM( this INetTransmitter transmitter, string value )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_CREATE_ROOM( value ) );
		}
public static void CALL_HALL_QCMD_JOIN_ROOM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_JOIN_ROOM(  ) );
		}
public static void CALL_HALL_QCMD_JOIN_ROOM( this INetTransmitter transmitter, int value )
		{
			transmitter.Send( new _PACKET_HALL_QCMD_JOIN_ROOM( value ) );
		}
public static void CALL_HALL_ACMD_ROOM_LIST( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_ROOM_LIST(  ) );
		}
public static void CALL_HALL_ACMD_ROOM_LIST( this INetTransmitter transmitter, _DTO_room_info[] rs )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_ROOM_LIST( rs ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_CREATED( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_CREATED(  ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_CREATED( this INetTransmitter transmitter, int ct,string map,string name,int roomId )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_CREATED( ct,map,name,roomId ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_DESTROIED( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED(  ) );
		}
public static void CALL_HALL_ACMD_BRO_ROOM_DESTROIED( this INetTransmitter transmitter, int value )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_BRO_ROOM_DESTROIED( value ) );
		}
public static void CALL_HALL_ACMD_JOIN_ROOM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_JOIN_ROOM(  ) );
		}
public static void CALL_HALL_ACMD_JOIN_ROOM( this INetTransmitter transmitter, int value )
		{
			transmitter.Send( new _PACKET_HALL_ACMD_JOIN_ROOM( value ) );
		}
public static void CALL_ROOM_QCMD_LEAVE_ROOM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_LEAVE_ROOM(  ) );
		}
public static void CALL_ROOM_QCMD_ROOM_INFO( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_ROOM_INFO(  ) );
		}
public static void CALL_ROOM_QCMD_ROOM_INFO( this INetTransmitter transmitter, int value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_ROOM_INFO( value ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MAP( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MAP(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MAP( this INetTransmitter transmitter, string value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MAP( value ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_HERO( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_HERO(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_HERO( this INetTransmitter transmitter, string value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_HERO( value ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MODEL( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MODEL(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_MODEL( this INetTransmitter transmitter, byte value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_MODEL( value ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_SKIN( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_SKIN(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_SKIN( this INetTransmitter transmitter, byte value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_SKIN( value ) );
		}
public static void CALL_ROOM_QCMD_BEGIN_FIGHT( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_BEGIN_FIGHT(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_TEAM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_TEAM(  ) );
		}
public static void CALL_ROOM_QCMD_CHANGE_TEAM( this INetTransmitter transmitter, byte value )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_CHANGE_TEAM( value ) );
		}
public static void CALL_ROOM_QCMD_ADD_FIGHT_READY( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_ADD_FIGHT_READY(  ) );
		}
public static void CALL_ROOM_QCMD_REMOVE_FIGHT_READY( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_REMOVE_FIGHT_READY(  ) );
		}
public static void CALL_ROOM_QCMD_MAP_READY( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_QCMD_MAP_READY(  ) );
		}
public static void CALL_ROOM_ACMD_LEAVE_ROOM( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_LEAVE_ROOM(  ) );
		}
public static void CALL_ROOM_ACMD_ROOM_INFO( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_ROOM_INFO(  ) );
		}
public static void CALL_ROOM_ACMD_ROOM_INFO( this INetTransmitter transmitter, string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_ROOM_INFO( host,map,name,players,roomId ) );
		}
public static void CALL_ROOM_ACMD_BEGIN_FIGHT( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_BEGIN_FIGHT(  ) );
		}
public static void CALL_ROOM_ACMD_BEGIN_FIGHT( this INetTransmitter transmitter, string host,string map,string name,_DTO_player_info[] players,int roomId )
		{
			transmitter.Send( new _PACKET_ROOM_ACMD_BEGIN_FIGHT( host,map,name,players,roomId ) );
		}
public static void CALL_BATTLE_QCMD_BATTLE_CREATED( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_BATTLE_CREATED(  ) );
		}
public static void CALL_BATTLE_QCMD_ACTION( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_ACTION(  ) );
		}
public static void CALL_BATTLE_QCMD_ACTION( this INetTransmitter transmitter, _DTO_action_info[] actions,int frameId )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_ACTION( actions,frameId ) );
		}
public static void CALL_BATTLE_QCMD_LEAVE_BATTLE( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_LEAVE_BATTLE(  ) );
		}
public static void CALL_BATTLE_QCMD_END_BATTLE( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_END_BATTLE(  ) );
		}
public static void CALL_BATTLE_QCMD_END_BATTLE( this INetTransmitter transmitter, byte value )
		{
			transmitter.Send( new _PACKET_BATTLE_QCMD_END_BATTLE( value ) );
		}
public static void CALL_BATTLE_ACMD_ENTER_BATTLE( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_ENTER_BATTLE(  ) );
		}
public static void CALL_BATTLE_ACMD_ENTER_BATTLE( this INetTransmitter transmitter, int frameRate,int framesPerKeyFrame,string mapId,_DTO_player_info[] players,int rndSeed,string uid )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_ENTER_BATTLE( frameRate,framesPerKeyFrame,mapId,players,rndSeed,uid ) );
		}
public static void CALL_BATTLE_ACMD_FRAME( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_FRAME(  ) );
		}
public static void CALL_BATTLE_ACMD_FRAME( this INetTransmitter transmitter, _DTO_action_info[] actions,int frameId )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_FRAME( actions,frameId ) );
		}
public static void CALL_BATTLE_ACMD_BATTLE_START( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_BATTLE_START(  ) );
		}
public static void CALL_BATTLE_ACMD_BATTLE_END( this INetTransmitter transmitter )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_BATTLE_END(  ) );
		}
public static void CALL_BATTLE_ACMD_BATTLE_END( this INetTransmitter transmitter, byte value )
		{
			transmitter.Send( new _PACKET_BATTLE_ACMD_BATTLE_END( value ) );
		}
	}
}
// ReSharper restore InconsistentNaming
// ReSharper restore UnusedMember.Global