using Core.Net.Protocol;
using FairyUGUI.Event;
using FairyUGUI.UI;
using Game.Loader;
using Game.Task;
using Logic;
using Logic.Controller;
using Logic.Event;
using Logic.Property;
using Protocol;
using Protocol.Gen;
using UnityEngine;
using UnityEngine.SceneManagement;
using View.Controller;
using View.Event;
using View.Net;
using View.UI.Wins;
using PResultUtils = View.Misc.PResultUtils;

namespace View.UI
{
	public class UIBattle : IUIModule
	{
		private GComponent _root;
		private HUDManager _hudManager;
		private SkillPanel _skillPanel;
		private StatesPanel _statesPanel;
		private RollingText _rollingText;

		private bool _showNavMesh;
		public bool showNavMesh
		{
			get { return this._showNavMesh; }
			set
			{
				if ( this._showNavMesh == value )
					return;
				this._showNavMesh = value;
#if UNITY_EDITOR
				if ( this._showNavMesh )
					UnityEditor.SceneView.onSceneGUIDelegate += this.OnSceneGUI;
				else
					UnityEditor.SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
				UnityEditor.SceneView.RepaintAll();
#endif
			}
		}

		public UIBattle()
		{
			UIPackage.AddPackage( "UI/battle" );
		}

		public void Dispose()
		{
		}

		public void Enter( object param )
		{
			this._hudManager = new HUDManager();
			GRoot.inst.AddChild( this._hudManager.root );

			this._root = UIPackage.CreateObject( "battle", "Main" ).asCom;
			this._root.displayObject.name = "battle";
			this._root.eventGraphicOnly = true;
			GRoot.inst.AddChild( this._root );

			this._skillPanel = new SkillPanel( this._root["skillPanel"].asCom );
			this._statesPanel = new StatesPanel( this._root["statsPanel"].asCom );
			this._rollingText = new RollingText( this._root["tips"].asCom );

			GTextInput lagInput = this._root["lag"].asTextInput;
			lagInput.onChanged.Add( this.OnLagInputChanged );

			GTextInput jitterInput = this._root["jitter"].asTextInput;
			jitterInput.onChanged.Add( this.OnJitterInputChanged );

			GButton exitBtn = this._root["exitBtn"].asButton;
			exitBtn.onClick.Add( this.OnQuitBtnClick );

			EventCenter.AddListener( UIEventType.ENTITY_CREATED, this.HandleEntityCreated );
			EventCenter.AddListener( UIEventType.ENTITY_DESTROIED, this.HandleEntityDestroied );
			EventCenter.AddListener( UIEventType.PICK_SKILL, this.HandlePickSkill );
			EventCenter.AddListener( UIEventType.DROP_SKILL, this.HandleDropSkill );
			EventCenter.AddListener( UIEventType.SKILL_ATTR_CHANGED, this.HandleSkillAttrChanged );
			EventCenter.AddListener( UIEventType.ATTR_CHANGED, this.HandleEntityAttrChanged );
			EventCenter.AddListener( UIEventType.SKILL_USE_FAILED, this.HandleSkillUseFailed );
			EventCenter.AddListener( UIEventType.ENTITY_DIE, this.HandleEntityDie );
			EventCenter.AddListener( UIEventType.BATTLE_END, this.HandleBattleEnd );
			EventCenter.AddListener( UIEventType.HURT, this.HandleEntityHurt );

			NetModule.instance.AddQCMDListener( Module.BATTLE, Command.QCMD_LEAVE_BATTLE, this.HandleLeaveBattle );

			BattleManager.Init( ( BattleParams )param );
		}

		public void Leave()
		{
			this.showNavMesh = false;
			TaskManager.instance.UnregisterTimer( this.OnReliveTimer );

			EventCenter.RemoveListener( UIEventType.ENTITY_CREATED, this.HandleEntityCreated );
			EventCenter.RemoveListener( UIEventType.ENTITY_DESTROIED, this.HandleEntityDestroied );
			EventCenter.RemoveListener( UIEventType.PICK_SKILL, this.HandlePickSkill );
			EventCenter.RemoveListener( UIEventType.DROP_SKILL, this.HandleDropSkill );
			EventCenter.RemoveListener( UIEventType.SKILL_ATTR_CHANGED, this.HandleSkillAttrChanged );
			EventCenter.RemoveListener( UIEventType.ATTR_CHANGED, this.HandleEntityAttrChanged );
			EventCenter.RemoveListener( UIEventType.SKILL_USE_FAILED, this.HandleSkillUseFailed );
			EventCenter.RemoveListener( UIEventType.ENTITY_DIE, this.HandleEntityDie );
			EventCenter.RemoveListener( UIEventType.BATTLE_END, this.HandleBattleEnd );
			EventCenter.RemoveListener( UIEventType.HURT, this.HandleEntityHurt );

			this._skillPanel.Dispose();
			this._skillPanel = null;

			this._statesPanel.Dispose();
			this._statesPanel = null;

			this._rollingText.Dispose();
			this._rollingText = null;

			this._hudManager.Dispose();
			this._hudManager = null;

			BattleManager.Dispose();

			if ( this._root != null )
			{
				this._root.Dispose();
				this._root = null;
			}

			SceneLoader loader = new SceneLoader( string.Empty, "Empty", LoadSceneMode.Single );
			loader.Load( null, null, null, false, true, true );
		}

#if UNITY_EDITOR
		private void OnSceneGUI( UnityEditor.SceneView sceneview )
		{
			NavDebug.Draw( BattleManager.instance.view.pathManager.navmesh, false );
		}
#endif

		public void Update()
		{
			this._hudManager.Update();
			this._rollingText.Update();

#if UNITY_EDITOR
			if ( UnityEngine.Input.GetKeyDown( "n" ) )
			{
				this.showNavMesh = !this.showNavMesh;
			}
#endif
		}

		private void OnJitterInputChanged( EventContext context )
		{
			//int value;
			//int.TryParse( ( string )context.data, out value );
			//todo NetModule.instance.simulationJitter = value;
		}

		private void OnLagInputChanged( EventContext context )
		{
			//int value;
			//int.TryParse( ( string )context.data, out value );
			//todo NetModule.instance.simulationLag = value;
		}

		private void OnQuitBtnClick( EventContext context )
		{
			Windows.CONFIRM_WIN.Open( "确定退出战场吗?", value =>
			{
				if ( value == 0 )
				{
					this._root.ShowModalWait();
					NetModule.instance.Send( ProtocolManager.PACKET_BATTLE_QCMD_LEAVE_BATTLE() );
				}
			} );
		}

		private void HandleLeaveBattle( Packet packet )
		{
			this._root.CloseModalWait();

			_DTO_reply dto = ( ( _PACKET_GENERIC_ACMD_REPLY )packet ).dto;
			PResult result = ( PResult )dto.result;
			PResultUtils.ShowAlter( result );
			if ( result == PResult.SUCCESS )
				UIManager.EnterHall();
		}


		private void HandleEntityCreated( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			if ( uiEvent.entity is VBio bio )
			{
				if ( bio == VPlayer.instance )
				{
					this._root["gold"].asCom["text_gold"].asTextField.text = string.Empty + VPlayer.instance.property.gold;
					GProgressBar expBar = this._root["exp_bar"].asProgress;
					expBar.value = VPlayer.instance.property.exp;
					expBar.maxValue = VPlayer.instance.upgradeExpNeeded;
					this._skillPanel.OnEntityCreated( bio );
					this._statesPanel.OnEntityCreated( bio );
				}
				this._hudManager.OnEntityCreated( bio );
			}
		}

		private void HandleEntityDestroied( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			if ( uiEvent.entity is VBio bio )
				this._hudManager.OnEntityDestroied( bio );
		}

		private void HandleEntityAttrChanged( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;

			VEntity target = uiEvent.entity;
			Attr attr = uiEvent.attr;
			object oldValue = uiEvent.o0;
			object newValue = uiEvent.o1;

			if ( target == VPlayer.instance )
			{
				switch ( attr )
				{
					case Attr.Gold:
						this._root["gold"].asCom["text_gold"].asTextField.text = string.Empty + ( int )newValue;
						break;

					case Attr.Exp:
						GProgressBar expBar = this._root["exp_bar"].asProgress;
						expBar.value = ( int )newValue;
						expBar.maxValue = VPlayer.instance.upgradeExpNeeded;
						break;
				}
				this._skillPanel.OnEntityAttrChanged( target, attr, oldValue, newValue );
				this._statesPanel.OnEntityAttrChanged( target, attr, oldValue, newValue );
			}
			this._hudManager.OnEntityAttrChanged( target, attr, oldValue, newValue );
		}

		private void HandleSkillAttrChanged( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			VEntity target = uiEvent.entity;
			if ( target != VPlayer.instance )
				return;
			Skill skill = uiEvent.skill;
			Attr attr = uiEvent.attr;
			object oldValue = uiEvent.o0;
			object newValue = uiEvent.o1;
			this._skillPanel.OnSkillAttrChanged( target, skill, attr, oldValue, newValue );
		}

		private void HandleSkillUseFailed( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;

			if ( uiEvent.entity != VPlayer.instance )
				return;

			this._rollingText.Create( "无法对该目标使用技能", Color.white );
		}

		private void HandleEntityDie( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;

			if ( uiEvent.entity != VPlayer.instance )
				return;

			GComponent com = this._root["relive_countdown"].asCom;
			com.visible = true;
			com["sec_text"].asTextField.text = string.Empty + VPlayer.instance.reliveTime;
			TaskManager.instance.RegisterTimer( 1f, VPlayer.instance.reliveTime, false, this.OnReliveTimer,
												this.OnReliveCountdownComplete );
		}

		private void OnReliveCountdownComplete()
		{
			GComponent com = this._root["relive_countdown"].asCom;
			com.visible = false;
			FrameActionManager.SetFrameAction( new _DTO_action_info( VPlayer.instance.rid, ( byte )FrameActionType.Relive ) );
		}

		private void OnReliveTimer( int index, float dt, object o )
		{
			int t = int.Parse( this._root["relive_countdown"].asCom["sec_text"].asTextField.text );
			--t;
			this._root["relive_countdown"].asCom["sec_text"].asTextField.text = string.Empty + t;
		}

		private void HandlePickSkill( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			this._skillPanel.OnPickSkill( uiEvent.skill.id );
		}

		private void HandleDropSkill( BaseEvent e )
		{
			this._skillPanel.OnDropSkill();
		}

		private void HandleBattleEnd( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			if ( VPlayer.instance.property.team == uiEvent.i0 )
			{
			}
		}

		private void HandleEntityHurt( BaseEvent e )
		{
			UIEvent uiEvent = ( UIEvent )e;
			VBio target = ( VBio )uiEvent.target;
			float damage = uiEvent.f0;
			this._hudManager.OnEntityHurt( target, damage );
		}
	}
}