using Protocol.Gen;
using System.Collections.Generic;
using UnityEngine;
using View.Controller;

namespace View.Input
{
	public class AbsInteractionState
	{
		public Interaction owner { get; }

		public AbsInteractionState( Interaction owner )
		{
			this.owner = owner;
		}

		public virtual void OnEnter( params object[] param )
		{

		}

		public virtual void OnExit()
		{

		}

		public virtual void HandlerPointerDown( IInteractive interactive, InputData data )
		{
		}

		public virtual void HandlerPointerUp( IInteractive interactive, InputData data )
		{
		}

		public virtual void HandlerPointerMove( IInteractive interactive, InputData data )
		{
		}

		public virtual void HandlerPointerClick( IInteractive interactive, InputData data )
		{
		}

		protected void MovePlayer( Vector3 targetPoint )
		{
			if ( !VPlayer.instance.CanMove() )
				return;

			FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.Move, targetPoint.x, targetPoint.y, targetPoint.z ) );
		}

		protected bool GetGroundHitPoint( out Vector3 point )
		{
			List<RaycastResult> raycastResults = this.owner.battle.input.raycasts;
			int count = raycastResults.Count;
			for ( int i = 0; i < count; i++ )
			{
				Collider collider = raycastResults[i].collider;
				if ( this.owner.battle.collider == collider )
				{
					point = raycastResults[i].point;
					point.y = 0;
					return true;
				}
			}
			point = Vector3.zero;
			return false;
		}
	}
}