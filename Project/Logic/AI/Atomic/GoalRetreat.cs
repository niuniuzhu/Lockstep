using System.Collections.Generic;
using Core.Math;
using Logic.Controller;
using Logic.FSM;

namespace Logic.AI.Atomic
{
	public class GoalRetreat : Goal
	{
		public override Type type => Type.Retreat;

		private Vec3 _targetPoint;
		private readonly List<Bio> _threatenings = new List<Bio>();

		public void SetThreatnings( List<Bio> threatenings )
		{
			this._threatenings.AddRange( threatenings );
		}

		protected override void Activate()
		{
			this._status = Status.Active;

			Vec3 dir = Vec3.zero;
			int count = this._threatenings.Count;
			for ( int i = 0; i < count; i++ )
			{
				Bio threatening = this._threatenings[i];
				dir += this.owner.property.position - threatening.property.position;
			}
			dir.Normalize();

			Vec3 homeDir = Vec3.Normalize( this.owner.bornPosition - this.owner.property.position );

			dir = Vec3.Normalize( dir * 1.4f + homeDir );
			this._targetPoint = this.owner.property.position + dir * 5f;

			Bio bio = ( Bio )this.owner;
			bio.Move( this._targetPoint );
		}

		internal override void Terminate()
		{
			this._threatenings.Clear();
			base.Terminate();
		}

		internal override Status Process( float dt )
		{
			this.ActivateIfInactive();

			Bio bio = ( Bio )this.owner;
			this._status = bio.fsm.currState.type == FSMStateType.Idle ? Status.Completed : Status.Active;
			return this._status;
		}
	}
}