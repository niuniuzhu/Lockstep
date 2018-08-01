using DG.Tweening;
using Game.Loader;
using Game.Misc;
using UnityEngine;
using View.Builtin;
using View.Controller;
using Logger = Core.Misc.Logger;

namespace View.Graphics
{
	public class Graphic
	{
		public VEntity owner { get; private set; }

		public string id { get; private set; }

		public string name { get { return this.root.name; } set { this.root.name = value; } }

		public Vector3 position { set { this.root.position = value; } get { return this.root.position; } }

		public Quaternion rotation { set { this.root.rotation = value; } get { return this.root.rotation; } }

		private Vector3 _initScale;
		public Vector3 initScale
		{
			get => this._initScale;
			set
			{
				if ( this._initScale == value )
					return;
				this._initScale = value;
				this.UpdateScale();
			}
		}

		private Vector3 _scale = Vector3.one;
		public Vector3 scale
		{
			get => this._scale;
			set
			{
				if ( this._scale == value )
					return;
				this._scale = value;
				this.UpdateScale();
			}
		}

		public Transform root { get; private set; }
		public Transform model { get; private set; }

		public GraphicAnimator animator { get; private set; }
		public GraphicParticle particle { get; private set; }
		public GraphicMaterial material { get; private set; }
		public GraphicElectric electric { get; private set; }
		public GraphicSpare spare { get; private set; }

		public bool isLiving => this.animator.isLiving || this.particle.isLiving;

		private AssetsLoader _loader;

		private BlobShadow _shadow;
		private bool _shadowVisible = true;
		public bool shadowVisible
		{
			get => this._shadowVisible;
			set
			{
				if ( this._shadowVisible == value )
					return;
				this._shadowVisible = value;
				this._shadow.gameObject.SetActive( this._shadowVisible );
			}
		}

		private bool _visible;
		public bool visible
		{
			get => this._visible;
			set
			{
				if ( this._visible == value )
					return;

				this._visible = value;
				this.UpdateVisible();
			}
		}

		public bool expired { get; private set; }

		public Graphic( string id )
		{
			this.id = id;
			this.animator = new GraphicAnimator();
			this.particle = new GraphicParticle();
			this.material = new GraphicMaterial();
			this.electric = new GraphicElectric( this );
			this.spare = new GraphicSpare();
			this.root = new GameObject().transform;
			Object.DontDestroyOnLoad( this.root.gameObject );
			this._shadow = Object.Instantiate( Resources.Load<GameObject>( "Models/BlobShadow" ) ).GetComponent<BlobShadow>();
			this._shadow.caster = this.root;
			this._shadow.gameObject.hideFlags = HideFlags.HideInHierarchy;
		}

		internal void Dispose()
		{
			if ( this._loader != null )
			{
				this._loader.Cancel();
				this._loader = null;
			}
			this.animator.Dispose();
			this.animator = null;
			this.particle.Dispose();
			this.particle = null;
			this.material.Dispose();
			this.material = null;
			this.electric.Dispose();
			this.electric = null;
			this.spare.Dispose();
			this.spare = null;
			Object.Destroy( this._shadow.gameObject );
			this._shadow = null;
			if ( this.model != null )
			{
				Object.Destroy( this.model.gameObject );
				this.model = null;
			}
			Object.Destroy( this.root.gameObject );
			this.root = null;
		}

		private void Load()
		{
			if ( this.model == null )
			{
				this._loader = new AssetsLoader( "model/" + this.id, this.id );
				this._loader.Load( this.OnInternalComplete, null, this.OnInternalError );
			}
			else
				this.SpawnComponents();
		}

		private void OnInternalComplete( object sender, AssetsProxy assetsProxy, object data )
		{
			this._loader = null;
			this.model = Object.Instantiate( assetsProxy.LoadAsset<GameObject>( this.id ) ).transform;
			Object.DontDestroyOnLoad( this.model );
			this.model.name = this.id;
			Utils.AddChild( this.root, this.model, false, true, true );
			Utils.SetLayer( this.root.gameObject, "entity" );
			this.animator.Setup( this.model.GetComponentsInChildren<Animation>() );
			this.particle.Setup( this.model.GetComponentsInChildren<ParticleSystem>() );
			this.material.Setup( this.model.GetComponentsInChildren<Renderer>() );
			this.electric.Setup( this.model.GetComponentsInChildren<ElectricBolt>() );
			this.spare.Setup( this.model );
			this.SpawnComponents();
		}

		private void OnInternalError( object sender, string msg, object data )
		{
			Logger.Warn( msg );
			this._loader = null;
		}

		private void SpawnComponents()
		{
			this.animator.OnSpawn();
			this.particle.OnSpawn();
			this.material.OnSpawn();
			this.electric.OnSpawn();
			this.spare.OnSpawn();
		}

		private void UpdateVisible()
		{
			if ( this._shadowVisible )
				this._shadow.gameObject.SetActive( this._visible );
			this.root.gameObject.SetActive( this._visible );

			//if ( this._visible )
			//	this.root.gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
			//else
			//	this.root.gameObject.hideFlags |= HideFlags.HideInHierarchy;
		}

		private void UpdateScale()
		{
			this.root.localScale = Vector3.Scale( this._initScale, this._scale );
		}

		public void SmoothScale( Vector3 value, float duration )
		{
			DOTween.To( () => this._scale, val => { this.scale = val; }, value, duration ).SetTarget( this );
		}

		internal void OnSpawn( VEntity owner )
		{
			this.owner = owner;
			this.visible = true;
			this.expired = false;
			this.Load();
		}

		internal void OnDespawn()
		{
			if ( this._loader != null )
			{
				this._loader.Cancel();
				this._loader = null;
			}
			this.animator.OnDespawn();
			this.particle.OnDespawn();
			this.material.OnDespawn();
			this.electric.OnDespawn();
			this.spare.OnDespawn();
			DOTween.Kill( this );
			this.scale = Vector3.one;
			this.visible = false;
			this.owner = null;
		}

		internal Collider CreateCollider()
		{
			CapsuleCollider collider = this.root.gameObject.AddComponent<CapsuleCollider>();
			collider.height = this.owner.size.y;
			collider.radius = Mathf.Max( this.owner.size.x, this.owner.size.z );
			collider.center = new Vector3( 0, this.owner.size.y * 0.5f, 0 );
			return collider;
		}

		internal void DestroyCollider()
		{
			Object.Destroy( this.root.gameObject.GetComponent<CapsuleCollider>() );
		}

		internal void Expired()
		{
			if ( this.expired )
				return;
			this.expired = true;
			if ( this.model == null )
				return;
			if ( this.animator.HasAction( this.id + "_stop" ) )
				this.animator.Play( this.id + "_stop" );
			else
				this.animator.Stop();
			this.particle.Stop();
			this.electric.Stop();
			if ( this._loader != null )
			{
				this._loader.Cancel();
				this._loader = null;
			}
		}

		internal void UpdateElectric( float dt )
		{
			if ( this.model == null )
				return;
			this.electric.Update( dt );
		}
	}
}