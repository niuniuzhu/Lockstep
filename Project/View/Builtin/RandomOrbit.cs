using UnityEngine;

namespace View.Builtin
{
	public class RandomOrbit : MonoBehaviour
	{
		public Transform @ref;
		public float speed;
		public float speed2;
		public float interval;
		private Vector3 _dir;
		private float _time = float.MaxValue;

		void Start()
		{
		}

		void Update()
		{
			this._time += Time.deltaTime;
			if ( this._time >= this.interval )
			{
				this._time = 0f;
				this._dir = Random.onUnitSphere;
			}
			this.@ref.transform.RotateAround( this.@ref.transform.position, this._dir, this.speed2 * Time.deltaTime );
			this.transform.RotateAround( this.@ref.position, this.@ref.forward, this.speed * Time.deltaTime );
			//this.transform.LookAt( this.@ref.position );
		}
	}
}