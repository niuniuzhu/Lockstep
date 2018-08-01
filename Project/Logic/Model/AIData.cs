using System.Collections;
using Core.Math;
using Core.Misc;

namespace Logic.Model
{
	public class AIData
	{
		public string type;
		public int i0;
		public int i1;
		public int i2;
		public float f0;
		public float f1;
		public float f2;
		public Vec3 v0;
		public Vec3 v1;
		public Vec3 v2;

		public static AIData[] Parse( ArrayList ais )
		{
			if ( ais != null )
			{
				int count = ais.Count;
				AIData[] aiDatas = new AIData[count];
				for ( int i = 0; i < count; i++ )
				{
					Hashtable ai = ( Hashtable ) ais[i];
					AIData aiData = new AIData();
					aiData.type = ai.GetString( "type" );
					aiData.i0 = ai.GetInt( "i0" );
					aiData.i1 = ai.GetInt( "i`" );
					aiData.i2 = ai.GetInt( "i1" );
					aiData.f0 = ai.GetFloat( "f0" );
					aiData.f1 = ai.GetFloat( "f1" );
					aiData.f2 = ai.GetFloat( "f2" );
					aiData.v0 = ai.GetVec3( "v0" );
					aiData.v1 = ai.GetVec3( "v1" );
					aiData.v2 = ai.GetVec3( "v2" );
					aiDatas[i] = aiData;
				}
				return aiDatas;
			}
			return null;
		}

		public static AIData[] Default( Vec3 marchPoint )
		{
			AIData[] datas = new AIData[2];
			AIData data = new AIData();
			data.type = "march";
			data.v0 = marchPoint;
			datas[0] = data;
			data = new AIData();
			data.type = "attack";
			datas[1] = data;
			return datas;
		}
	}
}