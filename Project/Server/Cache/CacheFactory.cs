namespace Server.Cache
{
	public static class CacheFactory
	{
		public static readonly UserCache USER_CACHE;
		public static readonly HallCache HALL_CACHE;
		public static readonly RoomCache ROOM_CACHE;

		static CacheFactory()
		{
			USER_CACHE = new UserCache();
			HALL_CACHE = new HallCache();
			ROOM_CACHE = new RoomCache();
		}
	}
}