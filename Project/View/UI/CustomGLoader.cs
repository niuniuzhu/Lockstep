using System.Collections;
using FairyUGUI.Core;
using FairyUGUI.UI;
using Game.Loader;
using UnityEngine;
using Logger = Core.Misc.Logger;

namespace View.UI
{
	public class CustomGLoader : GLoader
	{
		public delegate void LoadCompleteCallback( Texture2D texture );
		public delegate void LoadErrorCallback( string error );

		protected override void LoadExternal()
		{
			this.LoadIcon( this.url, this.OnLoadSuccess, this.OnLoadFail );
		}

		protected override void FreeExternal( NSprite sprite )
		{
		}

		void OnLoadSuccess( Texture2D texture2D )
		{
			this.OnExternalLoadSuccess( texture2D );
		}

		void OnLoadFail( string error )
		{
			Debug.Log( "load " + this.url + " failed: " + error );
			this.OnExternalLoadFailed();
		}

		private void LoadIcon( string url,
			LoadCompleteCallback onSuccess,
			LoadErrorCallback onFail )
		{
			AssetsLoader loader = new AssetsLoader( "icon/" + url, url, new ArrayList { onSuccess, onFail } );
			loader.Load( this.OnLoadComplete, null, this.OnLoadError );
		}

		private void OnLoadError( object sender, string msg, object data )
		{
			Logger.Error( msg );
		}

		private void OnLoadComplete( object sender, AssetsProxy assetsproxy, object data )
		{
			AssetsLoader loader = ( AssetsLoader )sender;

			ArrayList al = ( ArrayList )loader.data;
			LoadCompleteCallback onSuccess = ( LoadCompleteCallback )al[0];

			Texture2D texture = assetsproxy.LoadAsset<Texture2D>( loader.assetName );

			onSuccess?.Invoke( texture );
		}
	}
}
