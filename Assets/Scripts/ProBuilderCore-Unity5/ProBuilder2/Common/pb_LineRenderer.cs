using System.Collections.Generic;
using UnityEngine;

namespace ProBuilder2.Common
{
	public class pb_LineRenderer : pb_MonoBehaviourSingleton<pb_LineRenderer>
	{
		public List<Mesh> gizmos;
		public Material mat;
	}
}
