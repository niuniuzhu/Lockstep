using Logic;

namespace View.FSM.Actions
{
	public class VPlayerIdle : VBioAction
	{
		protected override void OnUpdate( UpdateContext context )
		{
			//if ( FrameActionManager.HasFrameAction() ||
			//	 FrameActionManager.HasSendingAction() )
			//	return;

			//VPlayer player = VPlayer.instance;
			//VBio target = player.tracingTarget;
			//Skill skill = player.commonSkill;
			//if ( skill != null &&
			//	 target != null &&
			//	 target.isInBattle &&
			//	 !target.isDead &&
			//	 player.WithinFov( target ) &&

			//	 player.CanUseSkill( skill ) &&
			//	 VEntityUtils.CanAttack( player, target, skill.campType, skill.targetFlag ) )
			//{
			//	if ( player.CanUseSkill( skill ) )
			//	{
			//		UIEvent.FrameAction( new UseSkillFrameAction( player.rid, skill.id,
			//																	player.rid, target.rid, Vector3.zero ) );
			//	}
			//}
		}
	}
}