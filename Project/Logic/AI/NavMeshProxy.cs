using Core.Math;
using Logic.Misc;
using org.critterai;
using org.critterai.nav;

namespace Logic.AI
{
	public class NavMeshProxy
	{
		private static readonly NavmeshQueryFilter QUERY_FILTER = new NavmeshQueryFilter();
		private static readonly Vector3 DEFAULT_SEARCH_EXTENTS = new Vector3( 3, 1, 3 );

		private Navmesh _navmesh;
		private NavmeshQuery _query;

		private readonly uint[] _tmpPath1 = new uint[256];
		private readonly uint[] _tmpPath2 = new uint[256];
		private Vector3[] _tmpPoints = new Vector3[32];

		public void Create( Navmesh navmesh )
		{
			this._navmesh = navmesh;//一定要保存起来,Navmesh的析构函数会把非托管内存的指针清掉!!
			NavStatus status = NavmeshQuery.Create( navmesh, 2048, out this._query );
			if ( status != NavStatus.Sucess )
				LLogger.Error( status );
		}

		public void Dispose()
		{
			this._query = null;
			this._navmesh = null;//让gc调用析构函数
		}

		public NavStatus CalculatePath( Vec3 start, Vec3 end, out Vec3[] corners )
		{
			corners = null;

			NavStatus status = this._query.GetNearestPoint( start.ToVector3(), DEFAULT_SEARCH_EXTENTS, QUERY_FILTER,
															out NavmeshPoint startNavmeshPoint );
			if ( status != NavStatus.Sucess )
				return status;

			status = this._query.GetNearestPoint( end.ToVector3(), DEFAULT_SEARCH_EXTENTS, QUERY_FILTER,
												  out NavmeshPoint endNavmeshPoint );
			if ( status != NavStatus.Sucess )
				return status;

			status = this._query.FindPath( startNavmeshPoint, endNavmeshPoint, QUERY_FILTER, this._tmpPath1, out int pathCount );
			if ( status != NavStatus.Sucess )
				return status;

			status = this._query.GetStraightPath( startNavmeshPoint.point, endNavmeshPoint.point, this._tmpPath1, 0, pathCount,
												  this._tmpPoints, null, null, out int straightCount );
			if ( status != NavStatus.Sucess )
				return status;

			corners = VectorHelper.ToVec3Array( ref this._tmpPoints, straightCount );

			return NavStatus.Sucess;
		}

		public NavStatus Raycast( Vec3 start, Vec3 end, out float hitParameter,
								  out Vec3 hitNormal )
		{
			hitParameter = 0;
			hitNormal = Vec3.zero;

			NavStatus status = this._query.GetNearestPoint( start.ToVector3(), DEFAULT_SEARCH_EXTENTS, QUERY_FILTER,
															out NavmeshPoint startNavmeshPoint );
			if ( status != NavStatus.Sucess )
				return status;

			status = this._query.Raycast( startNavmeshPoint, end.ToVector3(), QUERY_FILTER, out hitParameter, out Vector3 v,
										  this._tmpPath2, out _ );
			hitNormal = v.ToVec3();
			if ( status != NavStatus.Sucess )
				return status;

			return NavStatus.Sucess;
		}

		public NavStatus GetNearestPoint( Vec3 searchPoint, out Vec3 vec3 )
		{
			NavStatus status = this._query.GetNearestPoint( searchPoint.ToVector3(), DEFAULT_SEARCH_EXTENTS, QUERY_FILTER,
															out NavmeshPoint startNavmeshPoint );
			if ( status != NavStatus.Sucess )
			{
				vec3 = Vec3.zero;
				return status;
			}
			vec3 = startNavmeshPoint.point.ToVec3();
			return NavStatus.Sucess;
		}
	}
}