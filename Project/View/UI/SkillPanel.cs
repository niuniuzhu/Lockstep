using FairyUGUI.Event;
using FairyUGUI.UI;
using Logic.Controller;
using Logic.Property;
using Protocol.Gen;
using View.Controller;

namespace View.UI
{
	public class SkillPanel
	{
		private GComponent _root;
		private GComponent[] _skillGrids;
		private GComponent _selectedSkillGrid;

		public SkillPanel( GComponent root )
		{
			this._root = root;
			this._skillGrids = new GComponent[5];
			this._skillGrids[0] = this._root["s0"].asCom;
			this._skillGrids[1] = this._root["s1"].asCom;
			this._skillGrids[2] = this._root["s2"].asCom;
			this._skillGrids[3] = this._root["s3"].asCom;
			this._skillGrids[4] = this._root["s4"].asCom;
		}

		public void Dispose()
		{
			this._selectedSkillGrid = null;
			this._skillGrids = null;
			this._root = null;
		}

		private GComponent GetSkillGrid( string id )
		{
			int count = VPlayer.instance.numSkills;
			for ( int i = 1; i < count; i++ )
			{
				GComponent skillGrid = this._skillGrids[i - 1];
				if ( ( string )skillGrid.data == id )
					return skillGrid;
			}
			return null;
		}

		public void OnEntityCreated( VBio bio )
		{
			Skill[] skills = bio.skills;
			int count = bio.numSkills;
			for ( int i = 1; i < count; i++ )
			{
				Skill skill = skills[i];
				GComponent skillGrid = this._skillGrids[i - 1];
				skillGrid.data = skill.id;

				GLoader loader = skillGrid["n2"].asLoader;
				loader.url = skill.icon;
				loader.onClick.Add( this.OnSkillGridClick );

				GButton uButton = skillGrid["n5"].asButton;
				uButton.onClick.Add( this.OnSkillUpgradeBtnClick );

				loader.grayed = true;
				loader.touchable = false;

				skillGrid["mask"].asImage.fillAmount = 0;

				if ( bio.property.skillPoint > 0 )
					skillGrid.GetController( "c1" ).selectedIndex = 1;
			}
		}

		public void OnEntityAttrChanged( VEntity target, Attr attr, object oldValue, object newValue )
		{
			switch ( attr )
			{
				case Attr.SkillPoint:
					int count = VPlayer.instance.numSkills;
					for ( int i = 1; i < count; i++ )
					{
						GComponent skillGrid = this._skillGrids[i - 1];
						skillGrid.GetController( "c1" ).selectedIndex = ( int )newValue > 0 ? 1 : 0;
					}
					break;
			}
		}

		public void OnSkillAttrChanged( VEntity target, Skill skill, Attr attr, object oldValue, object newValue )
		{
			if ( target != VPlayer.instance )
				return;

			if ( skill == VPlayer.instance.commonSkill )
				return;

			GComponent skillGrid = this.GetSkillGrid( skill.id );
			GLoader loader = skillGrid["n2"].asLoader;

			switch ( attr )
			{
				case Attr.Lvl:
				case Attr.Cooldown:
					loader.grayed = skill.property.lvl < 0;
					loader.touchable = skill.CanUse();
					if ( skill.property.lvl < 0 )
						skillGrid["mask"].asImage.fillAmount = 0;
					else
						skillGrid["mask"].asImage.fillAmount = skill.cd <= 0 ? 0 : skill.property.cooldown / skill.cd;
					break;
			}
		}

		public void OnPickSkill( string skillId )
		{
			GComponent skillGrid = this.GetSkillGrid( skillId );
			if ( skillGrid == null )
				return;

			skillGrid.GetTransition( "t0" ).Play();
			this._selectedSkillGrid = skillGrid;
		}

		public void OnDropSkill()
		{
			if ( this._selectedSkillGrid == null )
				return;

			this._selectedSkillGrid.GetTransition( "t1" ).Play();
			this._selectedSkillGrid = null;
		}

		private void OnSkillGridClick( EventContext context )
		{
			GComponent skillGrid;
			int count = this._skillGrids.Length;
			for ( int i = 0; i < count; i++ )
			{
				skillGrid = this._skillGrids[i];
				skillGrid.GetTransition( "t1" ).Play();
			}

			skillGrid = ( ( GLoader )context.sender ).parent;
			Skill skill = VPlayer.instance.GetSkill( ( string )skillGrid.data );
			if ( this._selectedSkillGrid != skillGrid )
				BattleManager.cBattle.interaction.PickSkill( skill );
			else
				BattleManager.cBattle.interaction.DropSkill();
		}

		private void OnSkillUpgradeBtnClick( EventContext context )
		{
			GComponent skillGrid = ( ( GButton )context.sender ).parent;
			Skill selectedSkill = VPlayer.instance.GetSkill( ( string )skillGrid.data );
			FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.UpgradeSkill, selectedSkill.id ) );
		}
	}
}