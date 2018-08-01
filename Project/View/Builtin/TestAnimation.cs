using UnityEngine;

namespace View.Builtin
{
	public class TestAnimation : MonoBehaviour
	{
		private Animation _animation;
		private string[] _names;
		private int _index;

		void Start()
		{
			this._animation = this.GetComponent<Animation>();
			this._names = new string[this._animation.GetClipCount()];
			int i = 0;
			foreach ( AnimationState state in this._animation )
			{
				this._names[i] = state.name;
				++i;
			}
		}

		void Play()
		{
			this._animation.Play( this._names[this._index] );
		}

		void Update()
		{
			if ( UnityEngine.Input.GetKeyDown( KeyCode.UpArrow ) )
			{
				++this._index;
				this._index = this._index >= this._names.Length ? 0 : this._index;
				this.Play();
			}
			if ( UnityEngine.Input.GetKeyDown( KeyCode.DownArrow ) )
			{
				--this._index;
				this._index = this._index < 0 ? this._names.Length - 1 : this._index;
				this.Play();
			}
		}

		void OnGUI()
		{
			GUILayout.Label( this._names[this._index] );
		}
	}
}