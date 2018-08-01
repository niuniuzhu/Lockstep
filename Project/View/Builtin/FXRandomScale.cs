using UnityEngine;

namespace View.Builtin
{
	public class FXRandomScale : MonoBehaviour
	{
		public float scaleMin = 0;
		public float scaleMax = 1;

		void Start()
		{
			this.transform.localScale *= Random.Range( this.scaleMin, this.scaleMax );
		}
	}
}