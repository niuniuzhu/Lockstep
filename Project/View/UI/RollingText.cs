using System.Collections.Generic;
using DG.Tweening;
using FairyUGUI.UI;
using UnityEngine;

namespace View.UI
{
	public class RollingText
	{
		private int max = 2;
		private float duration = 3f;

		private static readonly Stack<GTextField> POOL = new Stack<GTextField>();

		private readonly GComponent _root;
		private readonly List<GTextField> _tfs = new List<GTextField>();

		public RollingText( GComponent root )
		{
			this._root = root;
		}

		public void Dispose()
		{
			int count = this._tfs.Count;
			while ( count > 0 )
			{
				GTextField tf = this._tfs[0];
				this.Release( tf );
				--count;
			}

			while ( POOL.Count > 0 )
			{
				GTextField tf = POOL.Pop();
				tf.Dispose();
			}
			POOL.Clear();
		}

		public void Create( string text, Color color )
		{
			GTextField tf = POOL.Count > 0 ? POOL.Pop() : new GRichTextField();
			tf.text = text;

			tf.SetAlign( AlignType.Center, VertAlignType.Middle );
			tf.position = Vector2.zero;
			tf.size = this._root.size;
			tf.color = color;
			tf.fontSize = 32;
			tf.alpha = 1f;
			tf.data = Time.time;
			this._root.AddChild( tf );

			this.UpdateVisual();
		}

		private void Release( GTextField tf )
		{
			DOTween.Kill( tf );
			this._root.RemoveChild( tf );
			tf.text = string.Empty;
			POOL.Push( tf );
		}

		private void UpdateVisual()
		{
			float yy = 0;
			int count = this._root.numChildren;
			for ( int i = count - 1; i >= 0; --i )
			{
				GTextField tf = this._root.GetChildAt( i ).asTextField;
				DOTween.Kill( tf );
				tf.TweenMoveY( yy, 0.5f ).SetTarget( tf );
				yy -= tf.size.y;

				if ( count - i > this.max )
				{
					tf.data = float.MaxValue;
					tf.TweenFade( 0, 0.5f ).OnComplete( () => this.Release( tf ) );
				}
			}
		}

		public void Update()
		{
			int count = this._root.numChildren;
			for ( int i = count - 1; i >= 0; --i )
			{
				GTextField tf = this._root.GetChildAt( i ).asTextField;
				float t = ( float ) tf.data;
				if ( Time.time >= t + this.duration )
				{
					tf.data = float.MaxValue;
					tf.TweenFade( 0, 0.5f ).OnComplete( () => this.Release( tf ) );
				}
			}
		}
	}
}