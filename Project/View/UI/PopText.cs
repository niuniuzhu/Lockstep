using System.Collections.Generic;
using DG.Tweening;
using FairyUGUI.UI;
using UnityEngine;
using View.Controller;

namespace View.UI
{
	public class PopText : GComponent
	{
		private const float OFFSET_HEIGHT = 1.5f;

		private static readonly Stack<PopText> POP_TEXT_POOL = new Stack<PopText>();
		private static readonly List<PopText> POP_TEXT = new List<PopText>();

		public static PopText Create()
		{
			PopText pt = POP_TEXT_POOL.Count > 0 ? POP_TEXT_POOL.Pop() : new PopText();
			POP_TEXT.Add( pt );
			return pt;
		}

		public static void Return( PopText pt )
		{
			POP_TEXT.Remove( pt );
			POP_TEXT_POOL.Push( pt );
		}

		public static void Destroy()
		{
			while ( POP_TEXT.Count > 0 )
			{
				PopText popText = POP_TEXT[0];
				popText.Cancel();
			}
			POP_TEXT_POOL.Clear();
		}

		private static string _sPlayerHurtFont;

		private static readonly Vector2 JITTER_FACTOR = new Vector2( 60, 40 );

		private readonly GTextField _numberText;
		private readonly Pos _tweenPos;
		private Vector3 _pointInWorld;
		private float _offsetHeight;

		public PopText()
		{
			this.touchable = false;

			if ( _sPlayerHurtFont == null )
				_sPlayerHurtFont = UIPackage.GetItemURL( "z战斗", "伤害数字" );

			this._tweenPos = new Pos();
			this._numberText = new GTextField();
			this._numberText.autoSize = AutoSizeType.Both;
			this.AddChild( this._numberText );
		}

		private void Pop( GComponent parent, VEntity target, string font, string text )
		{
			parent.AddChild( this );

			this._numberText.fontName = font;
			this._numberText.text = text;
			this._numberText.position = new Vector2( -this._numberText.size.x * 0.5f, 0 );

			this._pointInWorld = target.position;
			this._offsetHeight = target.size.y;
			this.UpdatePosition();

			this._tweenPos.x = 0;
			this._tweenPos.y = 0;
			float toX = 0;
			float toY = JITTER_FACTOR.y;

			this.alpha = 1;
			DOTween.To( () => new Vector2( this._tweenPos.x, this._tweenPos.y ), val =>
			{
				this._tweenPos.x = val.x;
				this._tweenPos.y = val.y;
			}, new Vector2( toX, toY ), 1.6f ).OnUpdate( this.UpdatePosition ).SetTarget( this ).SetUpdate( false );

			this.TweenFade( 0, 0.8f ).SetDelay( 0.8f )
				.OnUpdate( this.UpdatePosition ).SetUpdate( false )
				.OnComplete( this.OnCompleted );
		}

		public void PopHurt( GComponent parent, VEntity target, float damage, bool crit )
		{
			string font = _sPlayerHurtFont;
			string str = string.Empty + ( int ) damage;

			this.Pop( parent, target, font, str );
		}

		private void OnCompleted()
		{
			this.parent.RemoveChild( this );
			Return( this );
		}

		private void Cancel()
		{
			DOTween.Kill( this );
			this.parent.RemoveChild( this );
			Return( this );
		}

		void UpdatePosition()
		{
			Vector3 pos =
				BattleManager.cBattle.camera.WorldToScreenPoint( new Vector3( this._pointInWorld.x,
																				this._pointInWorld.y + this._offsetHeight +
																				OFFSET_HEIGHT, this._pointInWorld.z ) );
			this.position = this.parent.ScreenToLocal( pos ) - new Vector3( this._tweenPos.x, this._tweenPos.y, 0 );
		}

		private class Pos
		{
			public float x;
			public float y;
		}
	}
}