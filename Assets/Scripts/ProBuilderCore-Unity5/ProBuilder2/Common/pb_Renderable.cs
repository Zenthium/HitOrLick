using System;
using UnityEngine;

namespace ProBuilder2.Common
{
	[Serializable]
	public class pb_Renderable : ScriptableObject
	{
		public Mesh mesh;
		public Material[] materials;
		public Transform transform;
	}
}
