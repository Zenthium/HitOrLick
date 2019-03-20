using System;
using UnityEngine;

namespace ProBuilder2.Common
{
	[Serializable]
	public class pb_ElementGraphics : pb_MonoBehaviourSingleton<pb_ElementGraphics>
	{
		[SerializeField]
		private Material faceMaterial;
		[SerializeField]
		private Material vertexMaterial;
		[SerializeField]
		private Material wireframeMaterial;
		[SerializeField]
		private pb_MeshRenderer pbRenderer;
		[SerializeField]
		private Color faceSelectionColor;
		[SerializeField]
		private Color edgeSelectionColor;
		[SerializeField]
		private Color vertSelectionColor;
		[SerializeField]
		private Color wireframeColor;
		[SerializeField]
		private Color vertexDotColor;
	}
}
