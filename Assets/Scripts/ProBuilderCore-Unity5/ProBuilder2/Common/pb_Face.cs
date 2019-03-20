using System;
using UnityEngine;

namespace ProBuilder2.Common
{
	[Serializable]
	public class pb_Face
	{
		[SerializeField]
		private int[] _indices;
		[SerializeField]
		private int[] _distinctIndices;
		[SerializeField]
		private pb_Edge[] _edges;
		[SerializeField]
		private int _smoothingGroup;
		[SerializeField]
		private pb_UV _uv;
		[SerializeField]
		private Material _mat;
		[SerializeField]
		public bool manualUV;
		[SerializeField]
		public int elementGroup;
		public int textureGroup;
	}
}
