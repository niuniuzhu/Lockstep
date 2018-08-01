using UnityEngine;

namespace View.Builtin
{
	[RequireComponent( typeof( MeshFilter ) )]
	[RequireComponent( typeof( Renderer ) )]
	public class VolumetricLine : MonoBehaviour
	{
		private bool _updateLineColor;
		private bool _updateLineWidth;

		#region private variables
		private Vector3 _startPos;
		private Vector3 _endPos = new Vector3( 0f, 0f, 100f );
		private Color _lineColor;
		private float _lineWidth;

		private static readonly Vector2[] VLINE_TEX_COORDS = {
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.5f, 1.0f),
			new Vector2(0.5f, 0.0f),
			new Vector2(0.5f, 0.0f),
			new Vector2(0.5f, 1.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
		};

		private static readonly Vector2[] VLINE_VERTEX_OFFSETS = {
			 new Vector2(1.0f,	 1.0f),
			 new Vector2(1.0f,	-1.0f),
			 new Vector2(0.0f,	 1.0f),
			 new Vector2(0.0f,	-1.0f),
			 new Vector2(0.0f,	 1.0f),
			 new Vector2(0.0f,	-1.0f),
			 new Vector2(1.0f,	 1.0f),
			 new Vector2(1.0f,	-1.0f)
		};

		private static readonly int[] VLINE_INDICES =
		{
			2, 1, 0,
			3, 1, 2,
			4, 3, 2,
			5, 4, 2,
			4, 5, 6,
			6, 5, 7
		};

		private Renderer _renderer;

		#endregion

		#region properties shown in inspector via ExposeProperty
		public Vector3 startPos
		{
			get { return this._startPos; }
			set
			{
				this._startPos = value;
				this.SetStartAndEndPoints( this._startPos, this._endPos );
			}
		}

		public Vector3 endPos
		{
			get { return this._endPos; }
			set
			{
				this._endPos = value;
				this.SetStartAndEndPoints( this._startPos, this._endPos );
			}
		}

		public bool setLinePropertiesAtStart { get; set; }

		public Color lineColor
		{
			get { return this._lineColor; }
			set { this._lineColor = value; this._updateLineColor = true; }
		}

		public float lineWidth
		{
			get { return this._lineWidth; }
			set { this._lineWidth = value; this._updateLineWidth = true; }
		}
		#endregion

		#region methods
		/// <summary>
		/// Sets the start and end points - updates the data of the Mesh.
		/// </summary>
		public void SetStartAndEndPoints( Vector3 startPoint, Vector3 endPoint )
		{
			Vector3[] vertexPositions = {
				startPoint,
				startPoint,
				startPoint,
				startPoint,
				endPoint,
				endPoint,
				endPoint,
				endPoint,
			};

			Vector3[] other = {
				endPoint,
				endPoint,
				endPoint,
				endPoint,
				startPoint,
				startPoint,
				startPoint,
				startPoint,
			};

			var mesh = this.GetComponent<MeshFilter>().sharedMesh;
			if ( null != mesh )
			{
				mesh.vertices = vertexPositions;
				mesh.normals = other;
			}
		}

		// Vertex data is updated only in Start() unless m_dynamic is set to true
		void Start()
		{
			Vector3[] vertexPositions = {
				this._startPos,
				this._startPos,
				this._startPos,
				this._startPos,
				this._endPos,
				this._endPos,
				this._endPos,
				this._endPos,
			};

			Vector3[] other = {
				this._endPos,
				this._endPos,
				this._endPos,
				this._endPos,
				this._startPos,
				this._startPos,
				this._startPos,
				this._startPos,
			};

			// Need to set vertices before assigning new Mesh to the MeshFilter's mesh property
			Mesh mesh = new Mesh();
			mesh.vertices = vertexPositions;
			mesh.normals = other;
			mesh.uv = VLINE_TEX_COORDS;
			mesh.uv2 = VLINE_VERTEX_OFFSETS;
			mesh.SetIndices( VLINE_INDICES, MeshTopology.Triangles, 0 );
			this.GetComponent<MeshFilter>().mesh = mesh;

			this._renderer = this.GetComponent<Renderer>();
			// Need to duplicate the material, otherwise multiple volume lines would interfere
			//this._renderer.material = this._renderer.material;
			if ( this.setLinePropertiesAtStart )
			{
				this._renderer.sharedMaterial.color = this._lineColor;
				this._renderer.sharedMaterial.SetFloat( "_LineWidth", this._lineWidth );
			}
			else
			{
				this._lineColor = this._renderer.sharedMaterial.color;
				this._lineWidth = this._renderer.sharedMaterial.GetFloat( "_LineWidth" );
			}
			this._renderer.sharedMaterial.SetFloat( "_LineScale", GetGlobalUniformScaleForLineWidth(this.transform) );
			this._updateLineColor = false;
			this._updateLineWidth = false;
		}

		private static float GetGlobalUniformScaleForLineWidth( Transform trans )
		{
			return ( trans.lossyScale.x + trans.lossyScale.y + trans.lossyScale.z ) / 3f;
		}

		void Update()
		{
			if ( this.transform.hasChanged )
			{
				this._renderer.sharedMaterial.SetFloat( "_LineScale", GetGlobalUniformScaleForLineWidth( this.transform ) );
			}
			if ( this._updateLineColor )
			{
				this._renderer.sharedMaterial.color = this._lineColor;
				this._updateLineColor = false;
			}
			if ( this._updateLineWidth )
			{
				this._renderer.sharedMaterial.SetFloat( "_LineWidth", this._lineWidth );
				this._updateLineWidth = false;
			}
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine( this.gameObject.transform.TransformPoint( this._startPos ), this.gameObject.transform.TransformPoint( this._endPos ) );
		}
		#endregion
	}
}