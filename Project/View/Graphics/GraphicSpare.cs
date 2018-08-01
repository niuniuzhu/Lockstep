using UnityEngine;

namespace View.Graphics
{
	public class GraphicSpare
	{
		public Vector3 footholdPos => this._foothold.position;
		public Vector3 overheadPos => this._overhead.position;
		public Vector3 hitPointPos => this._hitPoint.position;
		public Vector3 lHandPos => this._lHand.position;
		public Vector3 rHandPos => this._rHand.position;
		public Vector3 lFootPos => this._lFoot.position;
		public Vector3 rFootPos => this._rFoot.position;
		public Vector3 headNubPos => this._headNub.position;
		public Vector3 weapon0Pos => this._weapon0.position;
		public Vector3 weapon1Pos => this._weapon1.position;

		private Transform _foothold;
		private Transform _overhead;
		private Transform _hitPoint;
		private Transform _lHand;
		private Transform _rHand;
		private Transform _lFoot;
		private Transform _rFoot;
		private Transform _headNub;
		private Transform _weapon0;
		private Transform _weapon1;

		public void Dispose()
		{
			this._foothold = null;
			this._overhead = null;
			this._hitPoint = null;
			this._lHand = null;
			this._rHand = null;
			this._lFoot = null;
			this._rFoot = null;
			this._headNub = null;
			this._weapon0 = null;
			this._weapon1 = null;
		}

		public void Setup( Transform model )
		{
			Transform[] children = model.GetComponentsInChildren<Transform>();
			int count = children.Length;
			for ( int i = 0; i < count; i++ )
			{
				Transform child = children[i];
				switch ( child.name )
				{
					case "__Foothold":
						this._foothold = child;
						break;
					case "__Overhead":
						this._overhead = child;
						break;
					case "__HitPoint":
						this._hitPoint = child;
						break;
					case "__LHand":
						this._lHand = child;
						break;
					case "__RHand":
						this._rHand = child;
						break;
					case "__LFoot":
						this._lFoot = child;
						break;
					case "__RFoot":
						this._rFoot = child;
						break;
					case "__HeadNub":
						this._headNub = child;
						break;
					case "__Weapon0":
						this._weapon0 = child;
						break;
					case "__Weapon1":
						this._weapon1 = child;
						break;
				}
			}
		}

		public void OnSpawn()
		{

		}

		public void OnDespawn()
		{

		}
	}
}