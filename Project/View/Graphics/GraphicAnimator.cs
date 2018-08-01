using System.Collections.Generic;
using UnityEngine;

namespace View.Graphics
{
	public class GraphicAnimator
	{
		private Animation[] _animations;

		private float _speed = 1f;
		public float speed
		{
			get { return this._speed; }
			set
			{
				if ( Mathf.Approximately( this._speed, value ) )
					return;
				this._speed = value;
				this.UpdateSpeed();
			}
		}

		public string playingAction { get; private set; }

		private bool _isLiving;
		public bool isLiving
		{
			get
			{
				if ( !this._isLiving )
					return false;

				this._isLiving = false;
				int count = this._animations.Length;
				for ( int i = 0; i < count; i++ )
				{
					Animation animation = this._animations[i];
					if ( animation.isPlaying && animation[this.playingAction].normalizedTime < 1f )
					{
						this._isLiving = true;
						break;
					}
				}
				return this._isLiving;
			}
		}

		private readonly Dictionary<string, float> _speedToClips = new Dictionary<string, float>();

		internal void Dispose()
		{
			this._speedToClips.Clear();
			this._animations = null;
		}

		public bool HasAction( string name )
		{
			if ( this._animations == null || this._animations.Length == 0 )
				return false;

			AnimationClip clip = this._animations[0].GetClip( name );
			return clip != null;
		}

		public void CrossFade( string action, float fadeLength = 0.1f, bool recover = false )
		{
			if ( !recover && this.playingAction == action )
				return;

			this.playingAction = action;

			if ( this._animations == null )
				return;

			int count = this._animations.Length;
			if ( count == 0 )
				return;

			for ( int i = 0; i < count; i++ )
			{
				Animation animation = this._animations[i];
				if ( animation.GetClip( action ) == null )
					continue;
				animation.CrossFade( action, fadeLength );
			}
		}

		public void Play( string action, bool recover = false )
		{
			if ( !recover && this.playingAction == action )
				return;

			this.playingAction = action;

			if ( this._animations == null )
				return;

			int count = this._animations.Length;
			if ( count == 0 )
				return;

			for ( int i = 0; i < count; i++ )
			{
				Animation animation = this._animations[i];
				animation.Play( action );
			}
		}

		public void Stop()
		{
			this.playingAction = string.Empty;

			if ( this._animations == null )
				return;

			int count = this._animations.Length;
			if ( count == 0 )
				return;

			for ( int i = 0; i < count; i++ )
			{
				Animation animation = this._animations[i];
				animation.Stop();
			}
		}

		private void UpdateSpeed()
		{
			if ( this._animations == null || this._animations.Length == 0 )
				return;

			Animation animation = this._animations[0];
			foreach ( AnimationState state in animation )
			{
				float clipSpeed = this._speedToClips[state.name];
				state.speed = this.speed * clipSpeed;
			}
		}

		public void SetClipSpeed( string name, float speed )
		{
			float clipSpeed;
			if ( !this._speedToClips.TryGetValue( name, out clipSpeed ) )
				return;

			if ( Mathf.Approximately( clipSpeed, speed ) )
				return;

			AnimationState state = this._animations[0][name];
			state.speed = this.speed * speed;
			this._speedToClips[state.name] = speed;
		}

		internal void Setup( Animation[] animators )
		{
			this._animations = animators;

			if ( this._animations != null &&
				 this._animations.Length > 0 )
			{
				Animation animation = this._animations[0];
				foreach ( AnimationState state in animation )
					this._speedToClips[state.name] = state.speed;
			}
		}

		internal void OnSpawn()
		{
			this._isLiving = true;

			this.UpdateSpeed();

			if ( !string.IsNullOrEmpty( this.playingAction ) )
				this.Play( this.playingAction, true );
		}

		internal void OnDespawn()
		{
			if ( this._animations != null &&
				 this._animations.Length > 0 )
			{
				Animation animation = this._animations[0];
				foreach ( AnimationState state in animation )
					state.speed = 1f;
			}
			this._speed = 1f;
		}
	}
}