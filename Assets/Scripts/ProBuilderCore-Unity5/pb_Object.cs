using UnityEngine;
using ProBuilder2.Common;
using System.Collections.Generic;

public class pb_Object : MonoBehaviour
{
	[SerializeField]
	private pb_Face[] _quads;
	[SerializeField]
	private pb_IntArray[] _sharedIndices;
	[SerializeField]
	private Vector3[] _vertices;
	[SerializeField]
	private Vector2[] _uv;
	[SerializeField]
	private List<Vector4> _uv3;
	[SerializeField]
	private List<Vector4> _uv4;
	[SerializeField]
	private pb_IntArray[] _sharedIndicesUV;
	[SerializeField]
	private Color[] _colors;
	public float angleError;
	public float areaError;
	public float hardAngle;
	public float packMargin;
	public Vector3 previousTransform;
	public bool userCollisions;
	public bool isSelectable;
	public bool dontDestroyMeshOnDelete;
	[SerializeField]
	private int[] m_selectedFaces;
	[SerializeField]
	private pb_Edge[] m_SelectedEdges;
	[SerializeField]
	private int[] m_selectedTriangles;
}
