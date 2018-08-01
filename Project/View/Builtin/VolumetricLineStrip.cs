using UnityEngine;

namespace View.Builtin
{
	[RequireComponent( typeof( MeshFilter ) )]
	[RequireComponent( typeof( Renderer ) )]
	public class VolumetricLineStrip : MonoBehaviour
	{
		private bool _updateLineColor;
		private bool _updateLineWidth;
		private Renderer _renderer;

		#region member variables
		private Color _lineColor;
		private float _lineWidth;
		#endregion

		#region properties shown in inspector via ExposeProperty
		public bool lineColorAtStart { get; set; }

		public Color lineColor
		{
			get { return this._lineColor; }
			set
			{
				this._lineColor = value;
				this._updateLineColor = true;
			}
		}

		public float lineWidth
		{
			get { return this._lineWidth; }
			set
			{
				this._lineWidth = value;
				this._updateLineWidth = true;
			}
		}
		#endregion

		public Vector3[] lineVertices { get; private set; }

		#region Unity callbacks and public methods

		void Start()
		{
			this.UpdateLineVertices( this.lineVertices );
			this._renderer = this.GetComponent<Renderer>();

			// Need to duplicate the material, otherwise multiple volume lines would interfere
			//this._renderer.material = this._renderer.material;
			if ( this.lineColorAtStart )
			{
				this._renderer.sharedMaterial.color = this._lineColor;
				this._renderer.sharedMaterial.SetFloat( "_LineWidth", this._lineWidth );
			}
			else
			{
				this._lineColor = this._renderer.sharedMaterial.color;
				this._lineWidth = this._renderer.sharedMaterial.GetFloat( "_LineWidth" );
			}
			this._renderer.sharedMaterial.SetFloat( "_LineScale", GetGlobalUniformScaleForLineWidth( this.transform ) );
			this._updateLineColor = false;
			this._updateLineWidth = false;
		}

		private static float GetGlobalUniformScaleForLineWidth( Transform trans )
		{
			return ( trans.lossyScale.x + trans.lossyScale.y + trans.lossyScale.z ) / 3f;
		}

		public void UpdateLineVertices( Vector3[] newSetOfVertices )
		{
			if ( newSetOfVertices == null || newSetOfVertices.Length < 3 )
				return;

			this.lineVertices = newSetOfVertices;

			// fill vertex positions, and indices
			// 2 for each position, + 2 for the start, + 2 for the end
			Vector3[] vertexPositions = new Vector3[this.lineVertices.Length * 2 + 4];
			// there are #vertices - 2 faces, and 3 indices each
			int[] indices = new int[( this.lineVertices.Length * 2 + 2 ) * 3];
			int v = 0;
			int x = 0;
			vertexPositions[v++] = this.lineVertices[0];
			vertexPositions[v++] = this.lineVertices[0];
			for ( int i = 0; i < this.lineVertices.Length; ++i )
			{
				vertexPositions[v++] = this.lineVertices[i];
				vertexPositions[v++] = this.lineVertices[i];
				indices[x++] = v - 2;
				indices[x++] = v - 3;
				indices[x++] = v - 4;
				indices[x++] = v - 1;
				indices[x++] = v - 2;
				indices[x++] = v - 3;
			}
			vertexPositions[v++] = this.lineVertices[this.lineVertices.Length - 1];
			vertexPositions[v++] = this.lineVertices[this.lineVertices.Length - 1];
			indices[x++] = v - 2;
			indices[x++] = v - 3;
			indices[x++] = v - 4;
			indices[x++] = v - 1;
			indices[x++] = v - 2;
			indices[x++] = v - 3;

			// fill texture coordinates and vertex offsets
			Vector2[] texCoords = new Vector2[vertexPositions.Length];
			Vector2[] vertexOffsets = new Vector2[vertexPositions.Length];
			int t = 0;
			int o = 0;
			texCoords[t++] = new Vector2( 1.0f, 0.0f );
			texCoords[t++] = new Vector2( 1.0f, 1.0f );
			texCoords[t++] = new Vector2( 0.5f, 0.0f );
			texCoords[t++] = new Vector2( 0.5f, 1.0f );
			vertexOffsets[o++] = new Vector2( 1.0f, -1.0f );
			vertexOffsets[o++] = new Vector2( 1.0f, 1.0f );
			vertexOffsets[o++] = new Vector2( 0.0f, -1.0f );
			vertexOffsets[o++] = new Vector2( 0.0f, 1.0f );
			for ( int i = 1; i < this.lineVertices.Length - 1; ++i )
			{
				if ( ( i & 0x1 ) == 0x1 )
				{
					texCoords[t++] = new Vector2( 0.5f, 0.0f );
					texCoords[t++] = new Vector2( 0.5f, 1.0f );
				}
				else
				{
					texCoords[t++] = new Vector2( 0.5f, 0.0f );
					texCoords[t++] = new Vector2( 0.5f, 1.0f );
				}
				vertexOffsets[o++] = new Vector2( 0.0f, 1.0f );
				vertexOffsets[o++] = new Vector2( 0.0f, -1.0f );
			}
			texCoords[t++] = new Vector2( 0.5f, 0.0f );
			texCoords[t++] = new Vector2( 0.5f, 1.0f );
			texCoords[t++] = new Vector2( 0.0f, 0.0f );
			texCoords[t++] = new Vector2( 0.0f, 1.0f );
			vertexOffsets[o++] = new Vector2( 0.0f, 1.0f );
			vertexOffsets[o++] = new Vector2( 0.0f, -1.0f );
			vertexOffsets[o++] = new Vector2( 1.0f, 1.0f );
			vertexOffsets[o++] = new Vector2( 1.0f, -1.0f );


			// fill previous and next positions
			Vector3[] prevPositions = new Vector3[vertexPositions.Length];
			Vector4[] nextPositions = new Vector4[vertexPositions.Length];
			int p = 0;
			int n = 0;
			prevPositions[p++] = this.lineVertices[1];
			prevPositions[p++] = this.lineVertices[1];
			prevPositions[p++] = this.lineVertices[1];
			prevPositions[p++] = this.lineVertices[1];
			nextPositions[n++] = this.lineVertices[1];
			nextPositions[n++] = this.lineVertices[1];
			nextPositions[n++] = this.lineVertices[1];
			nextPositions[n++] = this.lineVertices[1];
			for ( int i = 1; i < this.lineVertices.Length - 1; ++i )
			{
				prevPositions[p++] = this.lineVertices[i - 1];
				prevPositions[p++] = this.lineVertices[i - 1];
				nextPositions[n++] = this.lineVertices[i + 1];
				nextPositions[n++] = this.lineVertices[i + 1];
			}
			prevPositions[p++] = this.lineVertices[this.lineVertices.Length - 2];
			prevPositions[p++] = this.lineVertices[this.lineVertices.Length - 2];
			prevPositions[p++] = this.lineVertices[this.lineVertices.Length - 2];
			prevPositions[p++] = this.lineVertices[this.lineVertices.Length - 2];
			nextPositions[n++] = this.lineVertices[this.lineVertices.Length - 2];
			nextPositions[n++] = this.lineVertices[this.lineVertices.Length - 2];
			nextPositions[n++] = this.lineVertices[this.lineVertices.Length - 2];
			nextPositions[n++] = this.lineVertices[this.lineVertices.Length - 2];

			// Need to set vertices before assigning new Mesh to the MeshFilter's mesh property
			Mesh mesh = this.GetComponent<MeshFilter>().mesh;
			mesh.vertices = vertexPositions;
			mesh.normals = prevPositions;
			mesh.tangents = nextPositions;
			mesh.uv = texCoords;
			mesh.uv2 = vertexOffsets;
			mesh.SetIndices( indices, MeshTopology.Triangles, 0 );
			//this.GetComponent<MeshFilter>().mesh = mesh;
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
			if ( this.lineVertices == null )
				return;
			Gizmos.color = Color.green;
			for ( int i = 0; i < this.lineVertices.Length - 1; ++i )
			{
				Gizmos.DrawLine( this.gameObject.transform.TransformPoint( this.lineVertices[i] ), this.gameObject.transform.TransformPoint( this.lineVertices[i + 1] ) );
			}
		}
		#endregion
	}
}