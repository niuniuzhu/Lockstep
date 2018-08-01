using UnityEngine;

namespace View.Builtin
{
	public class NaughtyMove : MonoBehaviour
	{
		public float verticleLength;
		public float duration;
		public AnimationCurve dampint;

		private Vector3 _verticleDest;
		private Vector3 _orgiPos;
		private int _verticleMoveCount;
		private float _time;

		void Start()
		{
			this.BeginVerticleMove();
		}

		void BeginVerticleMove()
		{
			this._orgiPos = this._verticleDest = this.transform.position;
			if ( this._verticleMoveCount == 0 )
				this._verticleDest.y += this.verticleLength;
			else
				this._verticleDest.y -= this.verticleLength;
		}

		void Update()
		{
			this._time += Time.deltaTime;
			if ( this._time >= this.duration )
			{
				this._verticleMoveCount = 1 - this._verticleMoveCount;
				this._time = 0f;
				this.BeginVerticleMove();
			}
			this.transform.position = Vector3.Lerp( this._orgiPos, this._verticleDest,
													this.dampint.Evaluate( this._time / this.duration ) );
		}
	}
}