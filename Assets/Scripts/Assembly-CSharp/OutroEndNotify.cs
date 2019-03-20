using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OutroEndNotify : MonoBehaviour
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0002D31D File Offset: 0x0002B71D
	public void Finished()
	{
		SceneManager.LoadScene("TitleScreen");
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0002D329 File Offset: 0x0002B729
	private void Start()
	{
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0002D32B File Offset: 0x0002B72B
	private void Update()
	{
	}
}
