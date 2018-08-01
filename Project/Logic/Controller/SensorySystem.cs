using System.Collections.Generic;
using Logic.Misc;

namespace Logic.Controller
{
	public class SensorySystem
	{
		private Bio _owner;
		private float _nextUpdateTime;
		private readonly Dictionary<Bio, float> _attackers = new Dictionary<Bio, float>();
		private readonly Dictionary<Bio, float> _hitters = new Dictionary<Bio, float>();
		private readonly List<Bio> _temp = new List<Bio>();

		private const float UPDATE_INTERVAL = 1f;
		private const float EXPRIE_TIME = 3f;

		public Bio killer;

		public SensorySystem( Bio owner )
		{
			this._owner = owner;
		}

		public void Dispose()
		{
			this._owner = null;
		}

		public void Clear()
		{
			this.killer = null;
			foreach ( KeyValuePair<Bio, float> kv in this._hitters )
				kv.Key.RedRef();
			this._hitters.Clear();
			foreach ( KeyValuePair<Bio, float> kv in this._attackers )
				kv.Key.RedRef();
			this._attackers.Clear();
		}

		public void AddAttacaker( Bio attacker )
		{
			if ( !this._attackers.ContainsKey( attacker ) )
				attacker.AddRef();
			this._attackers[attacker] = this._owner.battle.time;
		}

		private bool RemoveAttacker( Bio attacker )
		{
			attacker.RedRef();
			return this._attackers.Remove( attacker );
		}

		public void AddHitter( Bio hitter )
		{
			if ( !this._hitters.ContainsKey( hitter ) )
				hitter.AddRef();
			this._hitters[hitter] = this._owner.battle.time;
		}

		private bool RemoveHitter( Bio hitter )
		{
			hitter.RedRef();
			return this._hitters.Remove( hitter );
		}

		public void Update( UpdateContext context )
		{
			float time = this._owner.battle.time;

			if ( time < this._nextUpdateTime )
				return;

			this._nextUpdateTime = time + UPDATE_INTERVAL;

			foreach ( KeyValuePair<Bio, float> kv in this._attackers )
			{
				Bio bio = kv.Key;
				if ( bio.isDead || time > kv.Value + EXPRIE_TIME )
					this._temp.Add( bio );
			}
			int count = this._temp.Count;
			for ( int i = 0; i < count; i++ )
				this.RemoveAttacker( this._temp[i] );
			this._temp.Clear();

			foreach ( KeyValuePair<Bio, float> kv in this._hitters )
			{
				Bio bio = kv.Key;
				if ( bio.isDead || time > kv.Value + EXPRIE_TIME )
					this._temp.Add( bio );
			}

			count = this._temp.Count;
			for ( int i = 0; i < count; i++ )
				this.RemoveHitter( this._temp[i] );
			this._temp.Clear();

			ListPool<Bio>.Release( this._temp );
		}
	}
}