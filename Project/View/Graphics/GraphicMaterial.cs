using System.Collections.Generic;
using Game.Loader;
using UnityEngine;

namespace View.Graphics
{
	public class GraphicMaterial
	{
		private Renderer[] _renderers;

		private Dictionary<Renderer, Material> _sharedMaterials = new Dictionary<Renderer, Material>();

		private Material _material;
		public Material material
		{
			get { return this._material; }
			set { this.SetMaterial( value ); }
		}

		private Material _transMat;

		internal void Dispose()
		{
			this._renderers = null;
			this._sharedMaterials.Clear();
			this._sharedMaterials = null;
			if ( this._material != null )
			{
				Object.Destroy( this._material );
				this._material = null;
			}
			if ( this._transMat != null )
			{
				Object.Destroy( this._transMat );
				this._transMat = null;
			}
		}

		private void SetMaterial( Material material, bool destroyLastMaterial = true )
		{
			if ( this._material == material )
				return;

			if ( this._material != null && destroyLastMaterial )
				Object.Destroy( this._material );

			this._material = material;

			int count = this._renderers.Length;
			for ( int i = 0; i < count; i++ )
			{
				Renderer renderer = this._renderers[i];
				this._material.mainTexture = renderer.sharedMaterial.mainTexture;
				this._material.color = renderer.sharedMaterial.color;
				renderer.material = this._material;
			}
		}

		public void SetDefaultMaterial( bool destroyLastMaterial = true )
		{
			if ( this._renderers == null )
				return;

			if ( this._material != null && destroyLastMaterial )
				Object.Destroy( this._material );
			this._material = null;

			int count = this._renderers.Length;
			for ( int i = 0; i < count; i++ )
			{
				Renderer renderer = this._renderers[i];
				renderer.material = this._sharedMaterials[renderer];
			}
		}

		public void Translucent( float alpha )
		{
			if ( this._transMat == null )
				this._transMat = Object.Instantiate( AssetsManager.LoadAsset<Material>( "material", "translucent" ) );
			this.material = this._transMat;
			Color color = this._material.color;
			color.a = alpha;
			this._material.color = color;
		}

		internal void Setup( Renderer[] renderers )
		{
			this._renderers = renderers;
			int count = this._renderers.Length;
			for ( int i = 0; i < count; i++ )
			{
				Renderer renderer = this._renderers[i];
				this._sharedMaterials[renderer] = renderer.sharedMaterial;
			}
		}

		internal void OnSpawn()
		{
			int count = this._renderers.Length;
			for ( int i = 0; i < count; i++ )
			{
				Renderer renderer = this._renderers[i];
				if ( this._material != null )
					renderer.material = this._material;
			}
		}

		internal void OnDespawn()
		{
			this.SetDefaultMaterial();
		}
	}
}