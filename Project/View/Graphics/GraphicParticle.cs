using UnityEngine;

namespace View.Graphics
{
	public class GraphicParticle
	{
		private ParticleSystem[] _particleSystems;

		private bool _isLiving;
		public bool isLiving
		{
			get
			{
				if ( !this._isLiving )
					return false;

				this._isLiving = false;
				int count = this._particleSystems.Length;
				for ( int i = 0; i < count; i++ )
				{
					if ( !this._particleSystems[i].isPlaying )
						continue;
					this._isLiving = true;
					break;
				}
				return this._isLiving;
			}
		}

		private bool _playing;

		internal void Dispose()
		{
			this._particleSystems = null;
		}

		internal void Setup( ParticleSystem[] particleSystems )
		{
			this._particleSystems = particleSystems;
		}

		internal void OnSpawn()
		{
			this._isLiving = true;

			if ( this._playing )
				this.Play();
		}

		internal void OnDespawn()
		{
		}

		public void Play()
		{
			this._playing = true;

			if ( this._particleSystems == null )
				return;

			int count = this._particleSystems.Length;
			for ( int i = 0; i < count; i++ )
				this._particleSystems[i].Play( true );
		}

		public void Stop()
		{
			this._playing = false;

			if ( this._particleSystems == null )
				return;

			int count = this._particleSystems.Length;
			for ( int i = 0; i < count; i++ )
				this._particleSystems[i].Stop( true );
		}
	}
}