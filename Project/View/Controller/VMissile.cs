using Logic.Controller;
using UnityEngine;

namespace View.Controller
{
	public class VMissile : VEntity
	{
		protected override void InternalOnAddedToBattle( EntityParam param )
		{
			base.InternalOnAddedToBattle( param );

			this.graphic.animator.Play( this.graphic.id + "_play" );
		}

		public void HandleComplete( Vector3 position, Vector3 direction )
		{
			this.position = this._logicPos = position;
			this.direction = this._logicDir;

			if ( !string.IsNullOrEmpty( this._data.hitFx ) )
			{
				Effect fx = this.battle.CreateEffect( this._data.hitFx );
				fx.position = this.position;
				fx.direction = this.direction;
			}
		}
	}
}