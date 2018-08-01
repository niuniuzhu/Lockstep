using Game.Loader;
using Logic.Model;
using UnityEngine.SceneManagement;

namespace View
{
	public static class MapLoadHelper
	{
		private static CompleteHandler _completeHandler;
		private static ProgressHandler _progressHandler;
		private static ErrorHandler _errorHandler;
		private static LoadBatch _lb;
		private static SceneLoader _loader;

		public static void Preload( string id, string[] players, string[] neutrals, string[] structures,
			CompleteHandler completeHandler, ProgressHandler progressHandler, ErrorHandler errorHandler )
		{
			_completeHandler = completeHandler;
			_progressHandler = progressHandler;
			_errorHandler = errorHandler;

			_lb = new LoadBatch();

			_lb.Add( new AssetsLoader( "scene/" + ModelFactory.GetBattleData( id ).model + "_navmesh" ) );
			_lb.Add( new AssetsLoader( "model/range_circle", "range_circle" ) );
			_lb.Add( new AssetsLoader( "model/route_point", "route_point" ) );
			_lb.Add( new AssetsLoader( "model/route_point_drag", "route_point_drag" ) );

			int count = players.Length;
			for ( int i = 0; i < count; i++ )
				CollectModels( players[i], _lb );
			count = neutrals.Length;
			for ( int i = 0; i < count; i++ )
				CollectModels( neutrals[i], _lb );
			count = structures.Length;
			for ( int i = 0; i < count; i++ )
				CollectModels( structures[i], _lb );
			_lb.data = id;
			_lb.Start( OnPreloadComplete, OnPreloadProgress, OnPreloadError, OnSingleLoadComplete );
		}

		private static void CollectModels( string id, LoadBatch loader )
		{
			EntityData entityData = ModelFactory.GetEntityData( id );
			if ( !string.IsNullOrEmpty( entityData.model ) )
				loader.Add( new AssetsLoader( "model/" + entityData.model ) );

			int c1 = entityData.skills.Length;
			for ( int i = 0; i < c1; i++ )
			{
				SkillData skillData = ModelFactory.GetSkillData( entityData.skills[i] );

				PreloadBuffs( loader, skillData.passiveBuffs );
				PreloadBuffs( loader, skillData.buffs );

				if ( skillData.levels == null )
					continue;

				int c6 = skillData.levels.Length;
				for ( int j = 0; j < c6; j++ )
				{
					SkillData.Level level = skillData.levels[j];

					string fx = level.atkFx;
					EntityData effectData;
					if ( !string.IsNullOrEmpty( fx ) )
					{
						effectData = ModelFactory.GetEntityData( fx );
						loader.Add( new AssetsLoader( "model/" + effectData.model ) );
					}

					string missile = level.missile;
					if ( !string.IsNullOrEmpty( missile ) )
					{
						EntityData missileData = ModelFactory.GetEntityData( missile );
						if ( !string.IsNullOrEmpty( missileData.model ) )
							loader.Add( new AssetsLoader( "model/" + missileData.model ) );

						if ( !string.IsNullOrEmpty( missileData.hitFx ) )
						{
							effectData = ModelFactory.GetEntityData( missileData.hitFx );
							loader.Add( new AssetsLoader( "model/" + effectData.model ) );
						}
					}
				}
			}
		}

		private static void PreloadBuffs( LoadBatch loader, string[] buffs )
		{
			if ( buffs == null )
				return;

			int c2 = buffs.Length;
			for ( int i = 0; i < c2; i++ )
			{
				BuffData buffData = ModelFactory.GetBuffData( buffs[i] );
				//enter state
				if ( buffData.enterStates != null )
				{
					int c3 = buffData.enterStates.Length;
					for ( int j = 0; j < c3; j++ )
					{
						string stateId = buffData.enterStates[j];
						BuffStateData buffStateData = ModelFactory.GetBuffStateData( stateId );
						int c4 = buffStateData.levels.Length;
						for ( int k = 0; k < c4; k++ )
						{
							BuffStateData.Level level = buffStateData.levels[k];
							if ( level.fxs == null )
								continue;

							int c6 = level.fxs.Length;
							for ( int l = 0; l < c6; l++ )
							{
								EntityData effectData = ModelFactory.GetEntityData( level.fxs[l] );
								loader.Add( new AssetsLoader( "model/" + effectData.model ) );
							}

							if ( level.trigger != null &&
								 level.trigger.fxs != null )
							{
								int c5 = level.trigger.fxs.Length;
								for ( int l = 0; l < c5; l++ )
								{
									string fxId = level.trigger.fxs[l];
									if ( string.IsNullOrEmpty( fxId ) )
										continue;
									EntityData effectData = ModelFactory.GetEntityData( fxId );
									loader.Add( new AssetsLoader( "model/" + effectData.model ) );
								}
							}
						}
					}
				}

				//trigger state
				if ( buffData.triggerStates != null )
				{
					int c3 = buffData.triggerStates.Length;
					for ( int j = 0; j < c3; j++ )
					{
						string stateId = buffData.triggerStates[j];
						BuffStateData buffStateData = ModelFactory.GetBuffStateData( stateId );
						int c4 = buffStateData.levels.Length;
						for ( int k = 0; k < c4; k++ )
						{
							BuffStateData.Level level = buffStateData.levels[k];
							if ( level.fxs == null )
								continue;

							int c6 = level.fxs.Length;
							for ( int l = 0; l < c6; l++ )
							{
								EntityData effectData = ModelFactory.GetEntityData( level.fxs[l] );
								loader.Add( new AssetsLoader( "model/" + effectData.model ) );
							}

							if ( level.trigger != null &&
								 level.trigger.fxs != null )
							{
								int c5 = level.trigger.fxs.Length;
								for ( int l = 0; l < c5; l++ )
								{
									string fxId = level.trigger.fxs[l];
									if ( string.IsNullOrEmpty( fxId ) )
										continue;
									EntityData effectData = ModelFactory.GetEntityData( fxId );
									loader.Add( new AssetsLoader( "model/" + effectData.model ) );
								}
							}
						}
					}
				}

				if ( buffData.levels != null )
				{
					int c3 = buffData.levels.Length;
					for ( int k = 0; k < c3; k++ )
					{
						BuffData.Level buffLevel = buffData.levels[k];

						if ( !string.IsNullOrEmpty( buffLevel.fx ) )
						{
							EntityData effectData = ModelFactory.GetEntityData( buffLevel.fx );
							loader.Add( new AssetsLoader( "model/" + effectData.model ) );
						}

						if ( !string.IsNullOrEmpty( buffLevel.areaFx ) )
						{
							EntityData effectData = ModelFactory.GetEntityData( buffLevel.areaFx );
							loader.Add( new AssetsLoader( "model/" + effectData.model ) );
						}

						//trigger
						BuffData.Trigger trigger = buffLevel.trigger;
						if ( trigger != null )
						{
							if ( trigger.fxs != null )
							{
								int c4 = trigger.fxs.Length;
								for ( int l = 0; l < c4; l++ )
								{
									if ( !string.IsNullOrEmpty( trigger.fxs[l] ) )
									{
										EntityData effectData = ModelFactory.GetEntityData( trigger.fxs[l] );
										loader.Add( new AssetsLoader( "model/" + effectData.model ) );
									}
								}
							}

							if ( trigger.tfxs != null )
							{
								int c4 = trigger.tfxs.Length;
								for ( int l = 0; l < c4; l++ )
								{
									if ( !string.IsNullOrEmpty( trigger.tfxs[l] ) )
									{
										EntityData effectData = ModelFactory.GetEntityData( trigger.tfxs[l] );
										loader.Add( new AssetsLoader( "model/" + effectData.model ) );
									}
								}
							}
						}
					}
				}
			}
		}

		private static void OnPreloadComplete( object sender, object data )
		{
			string id = ( string )_lb.data;
			_lb = null;
			LoadLevel( id );
		}

		private static void OnPreloadProgress( object sender, float progress, IBatchLoader loader )
		{
			_progressHandler?.Invoke( null, progress * 0.5f );
		}

		private static void OnPreloadError( object sender, string msg, object data )
		{
			_errorHandler?.Invoke( null, msg, data );
			_completeHandler = null;
			_progressHandler = null;
			_errorHandler = null;
		}

		private static void OnSingleLoadComplete( object sender, AssetsProxy assetsproxy, IBatchLoader loader, object data )
		{
			//CLogger.Log( $"batch:{( ( AssetsLoader )loader ).assetBundleName}" );
		}

		private static void LoadLevel( string id )
		{
			string model = ModelFactory.GetBattleData( id ).model;
			_loader = new SceneLoader( "scene/" + model, model, LoadSceneMode.Single );
			_loader.Load( OnLoadComplete, OnLoadProgress, OnLoadError );
		}

		private static void OnLoadProgress( object sender, float progress )
		{
			_progressHandler?.Invoke( null, progress * 0.5f + 0.5f );
		}

		private static void OnLoadComplete( object sender, AssetsProxy assetsproxy, object data )
		{
			_completeHandler?.Invoke( null );
			_completeHandler = null;
			_progressHandler = null;
			_errorHandler = null;
		}

		private static void OnLoadError( object sender, string msg, object data )
		{
			_errorHandler?.Invoke( null, msg, data );
			_completeHandler = null;
			_progressHandler = null;
			_errorHandler = null;
		}

		public static void BeginSceneActivation( CompleteHandler sceneActivedCallback, object param = null )
		{
			_loader.BeginSceneActivation( sceneActivedCallback, param );
			_loader = null;
		}

		public static void Cancel()
		{
			if ( _lb != null )
			{
				_lb.Cancel();
				_lb = null;
			}

			if ( _loader != null )
			{
				_loader.Cancel();
				_loader = null;
			}
			_completeHandler = null;
			_progressHandler = null;
			_errorHandler = null;
		}
	}
}