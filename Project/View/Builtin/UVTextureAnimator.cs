using System.Collections;
using UnityEngine;

namespace View.Builtin
{
	internal class UVTextureAnimator : MonoBehaviour
	{
		private int _count, _allCount;
		private float _deltaFps;
		private int _index;
		private bool _isCorutineStarted;

		private bool _isInizialised;
		private bool _isVisible;
		private Renderer _renderer;

		public Material[] animatedMaterialsNotInstance = null;
		public int columns = 4;
		public float fps = 20;
		public bool isBump = false;
		public bool isHeight = false;
		public bool isLoop = true;
		public bool isRandomOffsetForInctance = false;
		public bool isReverse = false;
		public int offsetMat;
		public int rows = 4;
		public Vector2 selfTiling = new Vector2();

		#region Non-public methods

		private void Awake()
		{
			this._renderer = this.GetComponent<Renderer>();
			this.InitDefaultVariables();
			this._isInizialised = true;
		}

		private void InitDefaultVariables()
		{
			this._allCount = 0;
			this._deltaFps = 1f / this.fps;
			this._count = this.rows * this.columns;
			this._index = this.columns - 1;
			Vector2 offset = new Vector2( ( float ) this._index / this.columns - this._index / this.columns,
										  1 - this._index / this.columns / ( float ) this.rows );
			this.offsetMat = !this.isRandomOffsetForInctance
								 ? this.offsetMat - this.offsetMat / this._count * this._count
								 : Random.Range( 0, this._count );
			Vector2 size = this.selfTiling == Vector2.zero ? new Vector2( 1f / this.columns, 1f / this.rows ) : this.selfTiling;
			if ( this.animatedMaterialsNotInstance.Length > 0 )
			{
				foreach ( Material mat in this.animatedMaterialsNotInstance )
				{
					mat.SetTextureScale( "_MainTex", size );
					mat.SetTextureOffset( "_MainTex", Vector2.zero );
					if ( this.isBump )
					{
						mat.SetTextureScale( "_BumpMap", size );
						mat.SetTextureOffset( "_BumpMap", Vector2.zero );
					}
					if ( this.isHeight )
					{
						mat.SetTextureScale( "_HeightMap", size );
						mat.SetTextureOffset( "_HeightMap", Vector2.zero );
					}
				}
			}
			else
			{
				this._renderer.material.SetTextureScale( "_MainTex", size );
				this._renderer.material.SetTextureOffset( "_MainTex", offset );
				if ( this.isBump )
				{
					this._renderer.material.SetTextureScale( "_BumpMap", size );
					this._renderer.material.SetTextureOffset( "_BumpMap", offset );
				}
				if ( this.isBump )
				{
					this._renderer.material.SetTextureScale( "_HeightMap", size );
					this._renderer.material.SetTextureOffset( "_HeightMap", offset );
				}
			}
		}

		#region CorutineCode

		private void OnEnable()
		{
			if ( this._isInizialised )
				this.InitDefaultVariables();
			this._isVisible = true;
			if ( !this._isCorutineStarted )
				this.StartCoroutine( this.UpdateCorutine() );
		}

		private void OnDisable()
		{
			this._isCorutineStarted = false;
			this._isVisible = false;
			this.StopAllCoroutines();
		}

		private void OnBecameVisible()
		{
			this._isVisible = true;
			if ( !this._isCorutineStarted )
				this.StartCoroutine( this.UpdateCorutine() );
		}

		private void OnBecameInvisible()
		{
			this._isVisible = false;
		}

		private IEnumerator UpdateCorutine()
		{
			this._isCorutineStarted = true;
			while ( this._isVisible &&
					( this.isLoop || this._allCount != this._count ) )
			{
				this.UpdateCorutineFrame();
				if ( !this.isLoop &&
					 this._allCount == this._count )
					break;
				yield return new WaitForSeconds( this._deltaFps );
			}
			this._isCorutineStarted = false;
		}

		#endregion CorutineCode

		private void UpdateCorutineFrame()
		{
			++this._allCount;
			if ( this.isReverse )
				--this._index;
			else
				++this._index;
			if ( this._index >= this._count )
				this._index = 0;

			if ( this.animatedMaterialsNotInstance.Length > 0 )
			{
				for ( int i = 0; i < this.animatedMaterialsNotInstance.Length; i++ )
				{
					int idx = i * this.offsetMat + this._index;
					idx = idx - idx / this._count * this._count;
					Vector2 offset = new Vector2( ( float ) idx / this.columns - idx / this.columns,
												  1 - idx / this.columns / ( float ) this.rows );
					this.animatedMaterialsNotInstance[i].SetTextureOffset( "_MainTex", offset );
					if ( this.isBump )
						this.animatedMaterialsNotInstance[i].SetTextureOffset( "_BumpMap", offset );
					if ( this.isHeight )
						this.animatedMaterialsNotInstance[i].SetTextureOffset( "_HeightMap", offset );
				}
			}
			else
			{
				Vector2 offset;
				if ( this.isRandomOffsetForInctance )
				{
					int idx = this._index + this.offsetMat;
					offset = new Vector2( ( float ) idx / this.columns - idx / this.columns,
										  1 - idx / this.columns / ( float ) this.rows );
				}
				else
				{
					offset = new Vector2( ( float ) this._index / this.columns - this._index / this.columns,
										  1 - this._index / this.columns / ( float ) this.rows );
				}
				this._renderer.material.SetTextureOffset( "_MainTex", offset );
				if ( this.isBump )
					this._renderer.material.SetTextureOffset( "_BumpMap", offset );
				if ( this.isHeight )
					this._renderer.material.SetTextureOffset( "_HeightMap", offset );
			}
		}

		#endregion
	}
}