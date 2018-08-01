using UnityEngine;

namespace View.Builtin
{
	[RequireComponent( typeof( Curve ) )]
	public class AniCurve : MonoBehaviour
	{
		public float maxSpeed;
		public float mass;
		public float duration;

		private AnimationCurve _curve;
		private Vector3 _velocity;
		private float _time;
		private bool _playing;

		void Start()
		{
			this._curve = this.GetComponent<Curve>().curve;
		}

		void Update()
		{
			if ( UnityEngine.Input.GetKeyDown( KeyCode.Z ) )
			{
				this.transform.position = Vector3.zero;
				this._velocity = new Vector3( this.maxSpeed, 0, 0 );
				this._playing = true;
			}

			if ( this._playing )
			{
				this._time += Time.deltaTime;
				float t = this._time / this.duration;
				Vector3 steeringForce = new Vector3( this._curve.Evaluate( t ), 0, 0 );
				Vector3 acceleration = steeringForce / this.mass;
				this._velocity += acceleration * Time.deltaTime;
				float speed = this._velocity.magnitude;
				if ( speed > this.maxSpeed )
				{
					this._velocity *= this.maxSpeed / speed;
				}
				this.transform.position += this._velocity * Time.deltaTime;
				if ( t >= 1f )
				{
					this._playing = false;
					this._time = 0;
				}
			}
		}
	}
}