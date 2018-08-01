using Core.Net;
using Protocol;
using Server.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Cache
{
	public class AccountCache
	{
		readonly Dictionary<string, Account> _accMap = new Dictionary<string, Account>();

		readonly Dictionary<IUserToken, string> _onlineAccMap = new Dictionary<IUserToken, string>();

		public PResult Create( string account, string password )
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

		public bool IsOnline( IUserToken token )
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

		public PResult Online( IUserToken token, string account )
		{
			//判断此账号当前是否在线
			if ( this.IsOnline( token ) )
				return PResult.ACCOUNT_ONLINE;

			this._onlineAccMap.Add( token, account );
			return PResult.SUCCESS;
		}

		public void Offline( IUserToken token )
		{
			//如果当前连接有登陆 进行移除
			if ( this._onlineAccMap.ContainsKey( token ) ) this._onlineAccMap.Remove( token );
		}
	}
}