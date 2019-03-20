using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerScoreDisplay : MonoBehaviour
{
	// Token: 0x06000582 RID: 1410 RVA: 0x0002F214 File Offset: 0x0002D614
	private void Start()
	{
		this.image.color = this.color;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0002F228 File Offset: 0x0002D628
	private void Update()
	{
		if (this.temporaryDisplayTimeLeft > 0f)
		{
			this.temporaryDisplayTimeLeft -= Time.deltaTime;
			if (Time.time % 0.15f < 0.05f)
			{
				this.text.color = this.player.color;
			}
			else if (Time.time % 0.15f < 0.1f)
			{
				this.text.color = Color.white;
			}
			else
			{
				this.text.color = Color.black;
			}
		}
		else
		{
			this.text.color = this.player.color;
			if (this.useRoundWins)
			{
				this.text.text = this.player.roundWins.ToString();
			}
			else
			{
				this.text.text = this.player.score.ToString();
			}
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0002F32E File Offset: 0x0002D72E
	public void TemorarilyDisplay(string str, float time = 2f)
	{
		this.text.text = str;
		this.temporaryDisplayString = str;
		this.temporaryDisplayTimeLeft = time;
	}

	// Token: 0x04000596 RID: 1430
	public Color color;

	// Token: 0x04000597 RID: 1431
	public Text text;

	// Token: 0x04000598 RID: 1432
	public Image image;

	// Token: 0x04000599 RID: 1433
	public Player player;

	// Token: 0x0400059A RID: 1434
	private string temporaryDisplayString;

	// Token: 0x0400059B RID: 1435
	private float temporaryDisplayTimeLeft;

	// Token: 0x0400059C RID: 1436
	public bool useRoundWins;
}
