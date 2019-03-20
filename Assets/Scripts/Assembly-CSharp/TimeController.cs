using System;
using UnityEngine;
public class TimeController : MonoBehaviour
{
	// Token: 0x17000125 RID: 293
	// (get) Token: 0x0600054B RID: 1355 RVA: 0x0002D5A8 File Offset: 0x0002B9A8
	public static bool TimeBumpActive
	{
		get
		{
			return TimeController.instance.timeBumpTimeLeft > 0f;
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0002D5BB File Offset: 0x0002B9BB
	private void Awake()
	{
		TimeController.instance = this;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0002D5C4 File Offset: 0x0002B9C4
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if (Time.timeScale > 0.5f)
			{
				Time.timeScale = 0.1f;
			}
			else
			{
				Time.timeScale = 1f;
			}
		}
		if (this.timeBumpTimeLeft > 0f)
		{
			if (this.timeBumpedThisFrame)
			{
				this.timeBumpedThisFrame = false;
			}
			else
			{
				this.timeBumpTimeLeft -= Time.deltaTime / Time.timeScale;
				if (this.timeBumpTimeLeft <= 0f)
				{
					Time.timeScale = 1f;
				}
			}
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0002D664 File Offset: 0x0002BA64
	public static void TimeBumpCharacters(Vector2 center, float durationM, float radius, bool dropOff)
	{
		foreach (Player player in GameController.activePlayers)
		{
			if (player.character != null)
			{
				float num = Vector2.Distance(center, player.character.transform.position);
				if (num < radius)
				{
					float num2 = num / radius;
					player.character.TimeBump(durationM, (!dropOff) ? 0f : (num2 * num2));
				}
			}
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002D710 File Offset: 0x0002BB10
	public static void Timebump(float intensity)
	{
		TimeController.instance.timeBumpTimeLeft = TimeController.instance.timeBumpTime * Mathf.Clamp(intensity, 1f, 5f);
		Time.timeScale = TimeController.instance.timeBumpScale;
		TimeController.instance.timeBumpedThisFrame = true;
	}

	// Token: 0x04000517 RID: 1303
	private static TimeController instance;

	// Token: 0x04000518 RID: 1304
	private float timeBumpTimeLeft;

	// Token: 0x04000519 RID: 1305
	public float timeBumpTime;

	// Token: 0x0400051A RID: 1306
	public float timeBumpScale;

	// Token: 0x0400051B RID: 1307
	private bool timeBumpedThisFrame;
}
