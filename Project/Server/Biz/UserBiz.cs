using Core.Net;
using Protocol;
using Server.Cache;
using Server.Dao;

namespace Server.Biz
{
	public class UserBiz
	{
		public PResult Reg( IUserToken token, string account, string password )
		{
			return CacheFactory.USER_CACHE.Reg( account, password );
		}

		public PResult Login( IUserToken token, string account, string password )
		{
			//账号密码为空 输入不合法
			if ( string.IsNullOrEmpty( account ) )
				return PResult.ACCOUNT_INVAILD;

			if ( string.IsNullOrEmpty( password ) )
				return PResult.PASSWORD_INVALID;

			//判断账号是否存在  不存在则无法登陆
			if ( !CacheFactory.USER_CACHE.HasAccount( account ) )
				return PResult.ACCOUNT_NOT_FOUND;

			//判断账号密码是否匹配
			if ( !CacheFactory.USER_CACHE.Match( account, password ) )
				return PResult.ACCOUNT_PASSWORD_NOT_MATCH;

			//验证都通过 说明可以登录  调用上线并返回成功
			return CacheFactory.USER_CACHE.Login( token, account );
		}

		public void Logout( IUserToken token )
		{
			if ( !this.IsLogin( token ) )
				return;
			CacheFactory.USER_CACHE.Logout( token );
		}

		public string GetAccountId( IUserToken token )
		{
			return CacheFactory.USER_CACHE.GetId( token );
		}

		public bool IsLogin( IUserToken token )
		{
			return CacheFactory.USER_CACHE.IsLogin( token );
		}

		public PResult Create( IUserToken token, string name, out User user )
		{
			string accountId = BizFactory.USER_BIZ.GetAccountId( token );
			return CacheFactory.USER_CACHE.Create( token, name, accountId, out user );
		}

		public PResult GetByAccount( IUserToken token, out User user )
		{
			string accountId = BizFactory.USER_BIZ.GetAccountId( token );
			user = CacheFactory.USER_CACHE.GetByAccountId( accountId );
			if ( user == null )
				return PResult.USER_NOT_FOUND;

			return PResult.SUCCESS;
		}

		public User GetUser( string id )
		{
			return CacheFactory.USER_CACHE.Get( id );
		}

		public bool IsOnline( IUserToken token )
		{
			return CacheFactory.USER_CACHE.IsOnline( token );
		}

		public PResult Online( IUserToken token )
		{
			PResult result = CacheFactory.USER_CACHE.Online( token, BizFactory.USER_BIZ.GetAccountId( token ) );
			if ( result == PResult.SUCCESS )
				BizFactory.HALL_BIZ.Enter( this.GetUser( token ).id );
			return result;
		}

		public void Offline( IUserToken token )
		{
			if ( !this.IsOnline( token ) )
				return;
			CacheFactory.USER_CACHE.Offline( token );
		}

		public IUserToken GetToken( string id )
		{
			return CacheFactory.USER_CACHE.GetToken( id );
		}

		public User GetUser( IUserToken token )
		{
			return CacheFactory.USER_CACHE.Get( token );
		}
	}
}