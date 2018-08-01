using System.Collections.Generic;
using Logic.Model;
using Logic.Property;

namespace Logic.BuffStateImpl
{
	//levels参数1-特效放大因子,2-特效持续时间
	public class BSRagnarok : BSBase
	{
		protected override void CreateInternal()
		{
			//移除所有控制效果
			this.owner.DestroyAllDisableBuffStates();
			float factor = this.extra[0];//特效放大因子
			this.owner.property.Mul( Attr.Scale, factor );
			//被动效果失效
			List<string> stateIds = this.GetPassiveStateIds();
			int count = stateIds.Count;
			for ( int i = 0; i < count; i++ )
				this.owner.GetBuffState( stateIds[i] ).enable = false;
		}

		protected override void DestroyInternal()
		{
			float factor = this.extra[0];//特效放大因子
			this.owner.property.Mul( Attr.Scale, 1 / factor );
			List<string> stateIds = this.GetPassiveStateIds();
			int count = stateIds.Count;
			for ( int i = 0; i < count; i++ )
			{
				BSBase buffState = this.owner.GetBuffState( stateIds[i] );
				if ( buffState != null )//需要判断是否空,因为有可能该satte被销毁了
					buffState.enable = true;
			}
		}

		private List<string> GetPassiveStateIds()
		{
			//这里别引用this.buff.skill,因为该状态和buff无关,buff没持续时间,已经被销毁了
			if ( this.skillData.passiveBuffs == null )
				return null;

			List<string> stateIds = new List<string>();
			int count = this.skillData.passiveBuffs.Length;
			for ( int i = 0; i < count; i++ )
			{
				BuffData buffData = ModelFactory.GetBuffData( this.skillData.passiveBuffs[i] );
				int c2 = buffData.enterStates.Length;
				for ( int j = 0; j < c2; j++ )
					stateIds.Add( buffData.enterStates[j] );
			}
			return stateIds;
		}
	}
}