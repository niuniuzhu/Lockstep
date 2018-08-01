namespace WorldEditor
{
	public abstract class DataNode
	{
		public string key { get; private set; }

		public DataNode parent { get; internal set; }

		public DataNode[] children { get; private set; }

		public BasePropertyInfo[] propertyInfos { get; protected set; }

		public DataNode( string key )
		{
			this.key = key;
		}

		public virtual void FromJson( Core.Misc.Map data )
		{
		}

		public virtual Core.Misc.Map ToJson()
		{
			Core.Misc.Map map = new Core.Misc.Map();
			foreach ( DataNode child in this.children )
				map[child.key] = child.ToJson();
			return map;
		}
	}
}