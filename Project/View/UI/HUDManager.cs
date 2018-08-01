using System.Collections.Generic;
using FairyUGUI.UI;
using Logic.Property;
using View.Controller;

namespace View.UI
{
	public class HUDManager
	{
		private static readonly Stack<HUD> HUD_POOL = new Stack<HUD>();

		private static HUD PopHUD()
		{
			if ( HUD_POOL.Count > 0 )
				return HUD_POOL.Pop();
			return new HUD();
		}

		private static void PushHUD( HUD hud )
		{
			HUD_POOL.Push( hud );
		}

		private readonly Dictionary<string, HUD> _idToHud = new Dictionary<string, HUD>();

		private readonly Queue<VBio> _delayQueue = new Queue<VBio>();

		public GComponent root { get; private set; }

		public HUDManager()
		{
			this.root = new GComponent();
			this.root.displayObject.name = "Hud";
			this.root.touchable = false;
			this.root.size = GRoot.inst.size;
			this.root.AddRelation( this.root.parent, RelationType.Size );
		}

		public void Dispose()
		{
			PopText.Destroy();
			this._delayQueue.Clear();
			foreach ( KeyValuePair<string, HUD> kv in this._idToHud )
				kv.Value.Dispose();
			this._idToHud.Clear();
			foreach ( HUD hud in HUD_POOL )
				hud.Dispose();
			HUD_POOL.Clear();
			this.root.Dispose();
			this.root = null;
		}

		public void OnEntityCreated( VBio bio )
		{
			//先处理玩家自己，其他实体先放入队列
			if ( VPlayer.instance == null )
				this._delayQueue.Enqueue( bio );
			else
			{
				this.InternalCreate( bio );
				while ( this._delayQueue.Count > 0 )
				{
					bio = this._delayQueue.Dequeue();
					this.InternalCreate( bio );
				}
			}
		}

		private void InternalCreate( VBio bio )
		{
			if ( !bio.canInteractive )
				return;
			HUD hud = PopHUD();
			hud.owner = bio;
			hud.visible = bio.graphic.visible;
			this._idToHud[bio.rid] = hud;
			this.root.AddChild( hud.root );
			hud.Update();
		}

		public void OnEntityDestroied( VBio bio )
		{
			HUD hud;
			if ( !this._idToHud.TryGetValue( bio.rid, out hud ) )
				return;
			this._idToHud.Remove( bio.rid );
			this.root.RemoveChild( hud.root );
			hud.Release();
			PushHUD( hud );
		}

		public void OnEntityHurt( VBio target, float damage )
		{
			PopText popText = PopText.Create();
			popText.PopHurt( this.root, target, damage, false );
		}

		public void OnEntityAttrChanged( VEntity target, Attr attr, object oldValue, object newValue )
		{
			HUD hud;
			if ( !this._idToHud.TryGetValue( target.rid, out hud ) )
				return;

			hud.OnEntityAttrChanged( attr, oldValue, newValue );
		}

		public void Update()
		{
			foreach ( KeyValuePair<string, HUD> kv in this._idToHud )
				kv.Value.Update();
		}
	}
}