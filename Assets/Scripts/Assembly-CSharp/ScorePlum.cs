using System;
using UnityEngine;
using UnityEngine.UI;
public class ScorePlum : MonoBehaviour
{
	// Token: 0x06000586 RID: 1414 RVA: 0x0002F354 File Offset: 0x0002D754
	private void Update()
	{
		if (base.GetComponent<Character>().player == null)
		{
			return;
		}
		if (this.displayTimeLeft > 0f)
		{
			this.displayTimeLeft -= Time.deltaTime;
			if (Time.time % 0.15f < 0.05f)
			{
				this.text.color = base.GetComponent<Character>().player.color;
			}
			else if (Time.time % 0.15f < 0.1f)
			{
				this.text.color = Color.white;
			}
			else
			{
				this.text.color = Color.black;
			}
			if (this.displayTimeLeft > 1.5f)
			{
				this.text.transform.localScale = Vector3.one * Mathf.Clamp(1f + (this.displayTimeLeft - 1.5f) * 2f, 1f, 1.5f);
			}
			else
			{
				this.text.transform.localScale = Vector3.one;
			}
			if (this.displayTimeLeft <= 0f)
			{
				this.text.text = string.Empty;
			}
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0002F48E File Offset: 0x0002D88E
	public void ShowText(string text, float time = 2f)
	{
		this.text.text = text;
		this.displayTimeLeft = time;
	}

	// Token: 0x0400059D RID: 1437
	private float displayTimeLeft;

	// Token: 0x0400059E RID: 1438
	public Text text;
}
