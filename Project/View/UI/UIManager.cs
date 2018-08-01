using FairyUGUI.Core;
using FairyUGUI.Event;
using FairyUGUI.UI;
using Logic;
using Protocol.Gen;
using UnityEngine;
using View.Input;

namespace View.UI
{
	public static class UIManager
	{
		private static readonly Vector2 RESOLUTION = new Vector2( 1334, 759 );

		public static UILogin login { get; private set; }
		public static UIHall hall { get; private set; }
		public static UIBattle battle { get; private set; }
		public static UIRoom room { get; private set; }
		public static UILoadLevel loadLevel { get; private set; }

		private static IUIModule _currModule;

		public static void Init()
		{
			Stage.Instantiate();
			Stage.inst.referenceResolution = RESOLUTION;
			Stage.inst.matchWidthOrHeight = 1;
			Stage.inst.updateManually = true;

			UIEventProxy.IsOverUIObject = () => EventSystem.instance.IsPointerOverGameObject();

			UIConfig.defaultFont = "STHeitiSC-Medium,Droid Sans Fallback,LTHYSZK,Verdana,SimHei";

			UIPackage.AddPackage( "UI/global" );
			UIConfig.globalModalWaiting = UIPackage.GetItemURL( "global", "ModalWaiting" );
			UIConfig.windowModalWaiting = UIPackage.GetItemURL( "global", "ModalWaiting" );

			UIObjectFactory.SetLoaderExtension( typeof( CustomGLoader ) );

			UIConfig.buttonSound = ( AudioClip )UIPackage.GetItemAsset( "global", "click" );

			login = new UILogin();
			hall = new UIHall();
			room = new UIRoom();
			loadLevel = new UILoadLevel();
			battle = new UIBattle();
		}

		public static void Dispose()
		{
			if ( _currModule != null )
			{
				_currModule.Leave();
				_currModule = null;
			}
			if ( Stage.inst != null )
				Stage.inst.Dispose();
		}

		private static void EnterModule( IUIModule module, object param = null )
		{
			_currModule?.Leave();
			module.Enter( param );
			_currModule = module;
		}

		public static void LeaveModule()
		{
			_currModule?.Leave();
			_currModule = null;
		}

		public static void EnterLogin()
		{
			EnterModule( login );
		}

		public static void EnterHall()
		{
			EnterModule( hall );
		}

		public static void EnterRoom( int roomId )
		{
			EnterModule( room, roomId );
		}

		public static void EnterLoading( _DTO_begin_fight dto )
		{
			EnterModule( loadLevel, dto );
		}

		public static void EnterBattle( BattleParams param )
		{
			EnterModule( battle, param );
		}

		public static void Update()
		{
			_currModule?.Update();
		}

		public static void LateUpdate()
		{
			if ( Stage.inst == null )
				return;
			Stage.inst.Update( Time.deltaTime );
		}
	}
}