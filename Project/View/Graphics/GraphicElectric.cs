using Logic.Model;
using View.Builtin;
using View.Controller;

namespace View.Graphics
{
	public class GraphicElectric
	{
		private ElectricBolt[] _electricBolts;
		private Graphic _graphic;
		public VEntity target;

		public GraphicElectric( Graphic graphic )
		{
			this._graphic = graphic;
		}

		internal void Dispose()
		{
			this._electricBolts = null;
			this._graphic = null;
		}

		internal void Setup( ElectricBolt[] electricBolts )
		{
			this._electricBolts = electricBolts;
		}

		internal void OnSpawn()
		{
		}

		internal void OnDespawn()
		{
			this.target = null;
		}

		public void Stop()
		{
			int count = this._electricBolts.Length;
			for ( int i = 0; i < count; i++ )
				this._electricBolts[i].Stop();
		}

		public void Update( float dt )
		{
			Effect effect = ( Effect ) this._graphic.owner;
			int count = this._electricBolts.Length;
			for ( int i = 0; i < count; i++ )
			{
				ElectricBolt script = this._electricBolts[i];
				script.startPoint = effect.position;
				script.endPoint = effect.GetDockedPosition( this.target, Spare.HitPoint ) +
								  UnityEngine.Random.insideUnitSphere * 0.4f;
				script.InternalUpdate( dt );
			}
		}
	}
}