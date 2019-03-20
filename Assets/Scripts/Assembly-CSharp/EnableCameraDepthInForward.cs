using System;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class EnableCameraDepthInForward : MonoBehaviour
{
	// Token: 0x060004A7 RID: 1191 RVA: 0x000252EE File Offset: 0x000236EE
	private void Start()
	{
		this.Set();
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x000252F6 File Offset: 0x000236F6
	private void Set()
	{
		if (base.GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
		{
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
		}
	}
}
