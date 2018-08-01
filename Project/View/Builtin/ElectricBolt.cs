using System.Collections.Generic;
using UnityEngine;

namespace View.Builtin
{
	[RequireComponent( typeof( LineRenderer ) )]
	public class ElectricBolt : MonoBehaviour
	{
		public enum LightningBoltAnimationMode
		{
			None,
			Random,
			Loop,
			PingPong,
			Flow
		}

		public float chaosFactor = 0.15f;

		public float chaosDamping = 0.5f;

		[Range( 0, 120 )]
		public float frequency = 20;

		[Range( 0, 8 )]
		public int numberOfNodes = 6;

		public Vector3 startPoint;
		public Vector3 endPoint;

		public LightningBoltAnimationMode animationMode = LightningBoltAnimationMode.PingPong;

		[Range( 1, 64 )]
		public int rows = 1;

		[Range( 1, 64 )]
		public int columns = 1;

		[Range( 0, 64 )]
		public float flowSpeed = 1;

		private LineRenderer _lineRenderer;
		private ParticleSystem _ps;
		private ParticleSystem _ps2;
		private readonly List<KeyValuePair<Vector3, Vector3>> _segments = new List<KeyValuePair<Vector3, Vector3>>();
		private int _startIndex;
		private Vector2[] _offsets;
		private int _animationOffsetIndex;
		private int _animationPingPongDirection = 1;
		private float _t;
		private bool _stoped;

		private void OnEnable()
		{
			this._lineRenderer = this.GetComponent<LineRenderer>();
			this._lineRenderer.positionCount = 0;
			this._ps = this.transform.GetChild( 0 ).GetComponent<ParticleSystem>();
			this._ps2 = this.transform.GetChild( 1 ).GetComponent<ParticleSystem>();
			this._t = 0;
			this._stoped = false;

			this.UpdateFromMaterialChange();
		}

		public void Stop()
		{
			this._stoped = true;
		}

		public void InternalUpdate( float dt )
		{
			if ( this.frequency <= 0 || this._stoped )
				return;

			if ( this._t < 1 / this.frequency )
			{
				this._t += dt;
				return;
			}
			this._t = 0;

			this._startIndex = 0;
			this.GenerateLightningBolt( this.startPoint, this.endPoint, this.numberOfNodes, 0f );
			this.UpdateLineRenderer();
			this._ps.transform.position = this.endPoint;
			this._ps2.transform.position = this.startPoint + ( this.endPoint - this.startPoint ) * Random.Range( 0.1f, 0.9f );
		}

		private void GenerateLightningBolt( Vector3 start, Vector3 end, int generation, float offsetAmount )
		{
			if ( generation < 0 || generation > 8 )
				return;

			this._segments.Add( new KeyValuePair<Vector3, Vector3>( start, end ) );

			if ( generation == 0 )
				return;

			if ( offsetAmount <= 0.0f )
				offsetAmount = ( end - start ).magnitude * this.chaosFactor;

			while ( generation-- > 0 )
			{
				int previousStartIndex = this._startIndex;
				this._startIndex = this._segments.Count;
				for ( int i = previousStartIndex; i < this._startIndex; i++ )
				{
					start = this._segments[i].Key;
					end = this._segments[i].Value;
					//Vector2 insideUnitCircle = Random.insideUnitCircle * offsetAmount;
					Vector3 midPoint = ( start + end ) * 0.5f + Random.insideUnitSphere * offsetAmount;
					//midPoint.x += insideUnitCircle.x;
					//midPoint.y += insideUnitCircle.y;
					//midPoint.x = Mathf.Clamp( midPoint.x, start.x, end.x );
					//midPoint.y = Mathf.Clamp( midPoint.y, start.y, end.y );
					this._segments.Add( new KeyValuePair<Vector3, Vector3>( start, midPoint ) );
					this._segments.Add( new KeyValuePair<Vector3, Vector3>( midPoint, end ) );
				}
				offsetAmount *= this.chaosDamping;
			}
		}
		private void UpdateLineRenderer()
		{
			int segmentCount = ( this._segments.Count - this._startIndex ) + 1;
			this._lineRenderer.positionCount = segmentCount;

			if ( segmentCount < 1 )
				return;

			int index = 0;
			this._lineRenderer.SetPosition( index++, this._segments[this._startIndex].Key );

			for ( int i = this._startIndex; i < this._segments.Count; i++ )
				this._lineRenderer.SetPosition( index++, this._segments[i].Value );

			this._segments.Clear();

			this.SelectOffsetFromAnimationMode();
		}

		private void SelectOffsetFromAnimationMode()
		{
			int index;
			switch ( this.animationMode )
			{
				case LightningBoltAnimationMode.None:
					this._lineRenderer.material.mainTextureOffset = this._offsets[0];
					return;

				case LightningBoltAnimationMode.PingPong:
					index = this._animationOffsetIndex;
					this._animationOffsetIndex += this._animationPingPongDirection;
					if ( this._animationOffsetIndex >= this._offsets.Length )
					{
						this._animationOffsetIndex = this._offsets.Length - 2;
						this._animationPingPongDirection = -1;
					}
					else if ( this._animationOffsetIndex < 0 )
					{
						this._animationOffsetIndex = 1;
						this._animationPingPongDirection = 1;
					}
					this._lineRenderer.material.mainTextureOffset = this._offsets[index];
					break;

				case LightningBoltAnimationMode.Loop:
					index = this._animationOffsetIndex++;
					if ( this._animationOffsetIndex >= this._offsets.Length )
						this._animationOffsetIndex = 0;
					this._lineRenderer.material.mainTextureOffset = this._offsets[index];
					break;

				case LightningBoltAnimationMode.Random:
					index = Random.Range( 0, this._offsets.Length );
					this._lineRenderer.material.mainTextureOffset = this._offsets[index];
					break;

				case LightningBoltAnimationMode.Flow:
					Vector2 offset = this._lineRenderer.material.mainTextureOffset;
					this._lineRenderer.material.mainTextureOffset = new Vector2( offset.x + Time.deltaTime * this.flowSpeed, offset.y );
					break;
			}
		}

		private void UpdateFromMaterialChange()
		{
			Vector2 size = new Vector2( 1.0f / this.columns, 1.0f / this.rows );
			this._lineRenderer.material.mainTextureScale = size;
			this._offsets = new Vector2[this.rows * this.columns];
			for ( int y = 0; y < this.rows; y++ )
			{
				for ( int x = 0; x < this.columns; x++ )
				{
					this._offsets[x + ( y * this.columns )] = new Vector2( ( float )x / this.columns, ( float )y / this.rows );
				}
			}
		}
	}
}