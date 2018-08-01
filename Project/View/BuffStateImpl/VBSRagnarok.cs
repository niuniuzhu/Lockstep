namespace View.BuffStateImpl
{
	public class VBSRagnarok : VBSBase
	{
		protected override void CreateInternal()
		{
			//float factor = this.extra[0];//特效放大因子
			//Vector3 scale = this.owner.graphic.scale * factor;
			//float d = this.extra[1];
			//this.owner.graphic.SmoothScale( scale, d );
		}


		protected override void DestroyInternal()
		{
			//float factor = this.extra[0];//特效放大因子
			//Vector3 scale = this.owner.graphic.scale / factor;
			//float d = this.extra[1];
			//this.owner.graphic.SmoothScale( scale, d );
		}
	}
}