using Server.Misc;

namespace Server.Dao
{
	public class User
	{
		#region Model

		private string _id = string.Empty;
		private string _name;
		private string _accountID;

		public string id
		{
			set => this._id = value;
			get => this._id;
		}

		public string name
		{
			set => this._name = value;
			get => this._name;
		}

		public string accountID
		{
			set => this._accountID = value;
			get => this._accountID;
		}

		#endregion Model

		public User()
		{
			//todo 数据持久化前先把id递增
			this.id = GuidHash.Get();
		}
	}
}