using System;
using Core.Net;
using Protocol;
using Server.Dao;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Server.Cache
{
	public class UserCache
	{
		readonly Dictionary<string, Account> _accMap = new Dictionary<string, Account>();
		readonly Dictionary<IUserToken, string> _onlineAccMap = new Dictionary<IUserToken, string>();
		readonly ConcurrentDictionary<string, string> _accToUid = new ConcurrentDictionary<string, string>();
		readonly ConcurrentDictionary<string, User> _idToModel = new ConcurrentDictionary<string, User>();
		readonly ConcurrentDictionary<string, IUserToken> _idToToken = new ConcurrentDictionary<string, IUserToken>();
		readonly ConcurrentDictionary<IUserToken, string> _tokenToId = new ConcurrentDictionary<IUserToken, string>();

		public PResult Reg( string account, string password )
		{
			if ( this.HasAccount( account ) )
				return PResult.ACCOUNT_EXIST;

			//创建账号实体并进行绑定
			Account model = new Account();
			model.account = account;
			model.password = Convert.ToBase64String( Encoding.UTF8.GetBytes( password ) );
			//todo 创建数据库表
			//model.Add();
			this._accMap.Add( account, model );
			return PResult.SUCCESS;
		}

		public bool HasAccount( string account )
		{
			return this._accMap.ContainsKey( account );
		}

		public bool Match( string account, string password )
		{
			//判断账号是否存在 不存在就谈不上匹配了
			if ( !this.HasAccount( account ) ) return false;
			//获取账号的信息 判断密码是否匹配并返回
			return this._accMap[account].password.Equals( Convert.ToBase64String( Encoding.UTF8.GetBytes( password ) ) );
		}

		public bool IsLogin( IUserToken token )
		{
			return this._onlineAccMap.ContainsKey( token );
		}

		public string GetId( IUserToken token )
		{
			//判断在线字典中是否有此连接的记录  没有说明此连接没有登陆 无法获取到账号id
			if ( !this._onlineAccMap.ContainsKey( token ) )
				return string.Empty;
			//返回绑定账号的id
			return this._accMap[this._onlineAccMap[token]].id;
		}

		public PResult Login( IUserToken token, string account )
		{
			//判断此账号当前是否在线
			if ( this.IsLogin( token ) )
				return PResult.ACCOUNT_ONLINE;

			this._onlineAccMap.Add( token, account );
			return PResult.SUCCESS;
		}

		public void Logout( IUserToken token )
		{
			//如果当前连接有登陆 进行移除
			if ( this._onlineAccMap.ContainsKey( token ) ) this._onlineAccMap.Remove( token );
		}

		public PResult Create( IUserToken token, string name, string accountId, out User user )
		{
			user = null;
			//判断当前帐号是否已经拥有角色
			if ( this._accToUid.ContainsKey( accountId ) )
				return PResult.USER_EXIST;

			user = new User
			{
				name = name,
				accountID = accountId
			};
			this._accToUid.TryAdd( accountId, user.id );
			this._idToModel.TryAdd( user.id, user );

			return PResult.SUCCESS;
		}

		public PResult Online( IUserToken token, string accountId )
		{
			if ( this.IsOnline( token ) )
				return PResult.USER_ONLINE;

			User user = this.GetByAccountId( accountId );

			this._idToToken.TryAdd( user.id, token );
			this._tokenToId.TryAdd( token, user.id );

			return PResult.SUCCESS;
		}

		public PResult Offline( IUserToken token )
		{
			if ( !this.IsOnline( token ) )
				return PResult.USER_OFFLINE;

			this._idToToken.TryRemove( this._tokenToId[token], out _ );
			this._tokenToId.TryRemove( token, out _ );

			return PResult.SUCCESS;
		}

		public User GetByAccountId( string accId )
		{
			if ( !this._accToUid.TryGetValue( accId, out string uid ) )
				return null;

			if ( !this._idToModel.TryGetValue( uid, out User model ) )
				return null;

			return model;
		}

		public bool IsOnline( IUserToken token )
		{
			return this._tokenToId.ContainsKey( token );
		}

		public User Get( IUserToken token )
		{
			if ( !this.IsOnline( token ) )
				return null;

			if ( !this._tokenToId.TryGetValue( token, out string id ) )
				return null;

			if ( !this._idToModel.TryGetValue( id, out User model ) )
				return null;

			return model;
		}

		public User Get( string id )
		{
			this._idToModel.TryGetValue( id, out User user );
			return user;
		}

		public IUserToken GetToken( string id )
		{
			this._idToToken.TryGetValue( id, out IUserToken token );
			return token;
		}
	}
}