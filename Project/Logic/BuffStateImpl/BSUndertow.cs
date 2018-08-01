namespace Logic.BuffStateImpl
{
	//逆流投掷
	//参数1-最短持续时间,2-持续时间和距离的比例
	public class BSUndertow : BSBase
	{
		protected override void CreateInternal()
		{
			float distance = ( this.owner.property.position - this.buff.caster.property.position ).Magnitude();
			this.duration = this.extra[0] + distance * this.extra[1];
		}
	}
}