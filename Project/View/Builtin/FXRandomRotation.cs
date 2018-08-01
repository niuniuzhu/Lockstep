using UnityEngine;

namespace View.Builtin
{
	public class FxRandomRotation : MonoBehaviour
	{
		public Vector3 rotation;

		private void Start()
		{
			this.transform.Rotate( new Vector3( Random.Range( -this.rotation.x, this.rotation.x ),
												Random.Range( -this.rotation.y, this.rotation.y ),
												Random.Range( -this.rotation.z, this.rotation.z ) ) );
		}
	}
}