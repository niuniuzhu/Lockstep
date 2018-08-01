using Logic;
using Logic.Model;
using UnityEngine;
using View.Builtin;
using View.Misc;

namespace View.Controller
{
	public class GodCamera
	{
		private readonly Transform _cameraTr;

		private readonly Camera _camera;

		private Vector3 _offset;
		public Vector3 offset
		{
			get { return this._offset; }
			set
			{
				if ( this._offset == value )
					return;
				this._offset = value;
				this.UpdateVisualImmediately();
			}
		}

		private VEntity _target;
		public VEntity target
		{
			get { return this._target; }
			set
			{
				if ( this._target == value )
					return;
				this._target = value;
				this.UpdateVisualImmediately();
			}
		}

		private Vector3 _velocity;
		private readonly float _smoothTime;

		public GodCamera( Camera camera, BattleData.Camera settings )
		{
			camera.enabled = true;
			this._cameraTr = camera.transform;
			this._camera = camera;

			this._offset = settings.offset.ToVector3();
			this._camera.fieldOfView = settings.fov;
			this._smoothTime = settings.smoothTime;
		}

		public Vector3 WorldToScreenPoint( Vector3 worldPoint )
		{
			return this._camera.WorldToScreenPoint( worldPoint );
		}

		public void UpdateVisualImmediately()
		{
			Vector3 pos = this.target.position;
			pos.y += 5f;
			this._cameraTr.position = pos;
			this._cameraTr.LookAt( this.target.position );
		}

		private void UpdateVisual( float dt )
		{
			if ( this._target == null )
				return;
			Vector3 destPos = this.target.position + this.offset;
			this._cameraTr.position = Vector3.SmoothDamp( this._cameraTr.position, destPos, ref this._velocity, this._smoothTime, Mathf.Infinity, dt );
			this._cameraTr.LookAt( this._cameraTr.position - this.offset );
		}

		public void Update( UpdateContext context )
		{
			this.UpdateVisual( context.deltaTime );
		}

		public void BSCOn()
		{
			BSCImageEffect bsc = this._camera.gameObject.AddComponent<BSCImageEffect>();
			bsc.material = Object.Instantiate( Resources.Load<Material>( "Materials/BSCImageEffect" ) );
			bsc.FadeIn( null );
		}

		public void BSCOff()
		{
			BSCImageEffect bsc = this._camera.gameObject.GetComponent<BSCImageEffect>();

			bsc?.FadeOut( () =>
			{
				Object.Destroy( bsc.material );
				Object.Destroy( bsc );
			} );
		}
	}
}