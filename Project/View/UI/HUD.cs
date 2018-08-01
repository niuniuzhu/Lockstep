using FairyUGUI.UI;
using Logic.Model;
using Logic.Property;
using UnityEngine;
using View.Controller;

namespace View.UI
{
	public class HUD
	{
		private const float OFFSET_HEIGHT = 1.2f;

		private VBio _owner;
		public VBio owner
		{
			get => this._owner;
			set
			{
				this._owner = value;
				float scale = this._owner.property.scale;
				this.root.scale = new Vector2( scale, scale );
				this.root.sortingOrder = VPlayer.instance == this._owner ? 999 : 0;

				this.root.GetController( "c2" ).selectedIndex = ( this._owner.flag & EntityFlag.Hero ) > 0 ? 0 : 1;
				this.root.GetController( "c1" ).selectedIndex = VPlayer.instance.property.team == this._owner.property.team ? 0 : 1;

				this._hpProgressBar = this.root[VPlayer.instance.property.team == this._owner.property.team ? "bar1" : "bar2"].asProgress;
				this._hpProgressBar.maxValue = this.owner.property.mhp;
				this._hpProgressBar.value = this.owner.property.hp;

				this._hpBuffBar = this._hpProgressBar["bbar"].asImage;
				this._hBar = this._hpProgressBar["bar"].asCom;

				this._mpProgressBar = this.root["mpbar"].asProgress;
				this._mpProgressBar.maxValue = this.owner.property.mmana;
				this._mpProgressBar.value = this.owner.property.mana;
			}
		}

		private bool _visible = true;
		public bool visible
		{
			get => this._visible;
			set
			{
				if ( this._visible == value )
					return;
				this._visible = value;
				this.root.visible = this._visible;
			}
		}

		public GComponent root { get; private set; }

		private GProgressBar _hpProgressBar;
		private GProgressBar _mpProgressBar;
		private GImage _hpBuffBar;
		private GComponent _hBar;
		private GTextField _lvl;

		public HUD()
		{
			this.root = UIPackage.CreateObject( "battle", "HUD" ).asCom;
			this._lvl = this.root["lvl"].asTextField;
		}

		public void Dispose()
		{
			this.root.Dispose();
			this.root = null;
			this._lvl = null;
		}

		public void Release()
		{
			this._hpProgressBar = null;
			this._mpProgressBar = null;
			this._hpBuffBar = null;
			this._hBar = null;
			this._owner = null;
		}

		public void Update()
		{
			Vector3 ownerPos = this.owner.position;
			Vector3 position = this.owner.battle.camera.WorldToScreenPoint( new Vector3( ownerPos.x, ownerPos.y + this.owner.size.y + OFFSET_HEIGHT, ownerPos.z ) );
			this.root.position = this.root.parent.ScreenToLocal( position );
			this.UpdateBar();
		}

		private void UpdateBar()
		{
			if ( !Mathf.Approximately( this._hpBuffBar.size.x, this._hBar.size.x ) )
			{
				float x = Mathf.Lerp( this._hpBuffBar.size.x, this._hBar.size.x, this.owner.battle.deltaTime * 0.6f );
				this._hpBuffBar.size = new Vector2( x, this._hpBuffBar.size.y );
			}
		}

		public void OnEntityAttrChanged( Attr attr, object oldValue, object newValue )
		{
			switch ( attr )
			{
				case Attr.Hp:
					this._hpProgressBar.value = ( float )newValue;
					break;
				case Attr.Mhp:
					this._hpProgressBar.maxValue = ( float )newValue;
					break;
				case Attr.Mana:
					this._mpProgressBar.value = ( float )newValue;
					break;
				case Attr.Mmana:
					this._mpProgressBar.maxValue = ( float )newValue;
					break;
				case Attr.Lvl:
					this._lvl.text = string.Empty + ( ( int )newValue + 1 );
					break;
				case Attr.Scale:
					float scale = ( float )newValue;
					this.root.scale = new Vector2( scale, scale );
					break;
				case Attr.Stealth:
					this.visible = ( int )newValue == 0;
					break;
			}
		}
	}
}