using Server.Misc;

namespace Server.Dao
{
	public class Account
	{
		#region Model

		private string _id = string.Empty;
		private string _account;
		private string _password;

		public string id
		{
			set { this._id = value; }
			get { return this._id; }
		}

		public string account
		{
			set { this._account = value; }
			get { return this._account; }
		}

		public string password
		{
			set { this._password = value; }
			get { return this._password; }
		}

		#endregion Model

		public Account()
		{
			//todo 数据持久化前先把id递增
			this.id = GuidHash.Get();
		}
	}
}