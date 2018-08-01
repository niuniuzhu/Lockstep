namespace Logic.FSM.Actions
{
	public class LDead : BioAction
	{
		private float _time;

		protected override void OnEnter( object[] param )
		{
			this._time = 0f;
		}

		protected override void OnUpdate( UpdateContext context )
		{
			this._time += context.deltaTime;
			if ( this._time >= 3f &&
			     string.IsNullOrEmpty( this.owner.uid ) ) //玩家不会销毁
				this.owner.markToDestroy = true;
		}
	}
}