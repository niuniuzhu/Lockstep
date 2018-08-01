using UnityEngine;

namespace View.Builtin
{
	public class BlobShadow : MonoBehaviour
	{
		public Transform caster;
		public float scale = 0.5f;
		public float offset = .8f;

		private Transform _tr;
		private Vector3 _dir;

		void Start()
		{
			this._tr = this.transform;
			this._dir = this._tr.rotation * Vector3.back;
			this.Update();
		}

		void Update()
		{
			Vector3 pos = this.caster.position;
			pos.y = 0f;
			pos += this.offset * this._dir + this.caster.position.y * this._dir * this.scale;
			this._tr.position = pos;
		}
	}
}