namespace Server.Biz
{
	public static class BizFactory
	{
		public static readonly UserBiz USER_BIZ;
		public static readonly HallBiz HALL_BIZ;
		public static readonly RoomBiz ROOM_BIZ;
		public static readonly BattleBiz BATTLE_BIZ;

		static BizFactory()
		{
			USER_BIZ = new UserBiz();
			HALL_BIZ = new HallBiz();
			ROOM_BIZ = new RoomBiz();
			BATTLE_BIZ = new BattleBiz();
		}
	}
}