using System.Text;
using UnityEngine;

namespace View.Builtin
{
	public class Curve : MonoBehaviour
	{
		public AnimationCurve curve;

		public Vector4[] inputs;

		public void UpdateVisual()
		{
			if ( this.inputs == null )
				return;

			this.curve = new AnimationCurve();
			int count = this.inputs.Length;
			for ( int i = 0; i < count; i++ )
			{
				Vector4 v = this.inputs[i];
				this.curve.AddKey( new Keyframe( v[0], v[1], v[2], v[3] ) );
			}
		}

		public string LogInfo()
		{
			StringBuilder sb = new StringBuilder();
			Keyframe[] keyframes = this.curve.keys;
			for ( int i = 0; i < keyframes.Length; i++ )
			{
				Keyframe keyframe = keyframes[i];
				sb.Append( $"{keyframe.time},{keyframe.value},{keyframe.inTangent},{keyframe.outTangent}," );
			}
			string str = sb.ToString();
			str = str.Substring( 0, str.Length - 1 );
			return str;
		}
	}
}