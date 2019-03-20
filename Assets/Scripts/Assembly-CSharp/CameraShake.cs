using System;
using UnityEngine;
public class CameraShake : MonoBehaviour
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x00024291 File Offset: 0x00022691
	public static CameraShake Instance
	{
		get
		{
			if (CameraShake.instance == null)
			{
				Debug.LogError("No Camerashake instance! Attach to camera transform");
			}
			return CameraShake.instance;
		}
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x000242B2 File Offset: 0x000226B2
	private void Awake()
	{
		CameraShake.instance = this;
		this.xShakeCounter = UnityEngine.Random.value * 100f;
		this.yShakeCounter = UnityEngine.Random.value * 100f;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x000242DC File Offset: 0x000226DC
	private void Update()
	{
		base.transform.position -= this.lastShakeOffset;
		this.xShakeAmount -= Time.deltaTime;
		if (this.xShakeAmount < 0f)
		{
			this.xShakeAmount = 0f;
		}
		this.yShakeAmount -= Time.deltaTime;
		if (this.yShakeAmount < 0f)
		{
			this.yShakeAmount = 0f;
		}
		this.xShakeCounter += this.xShakeSpeed * Time.deltaTime * 6f;
		this.yShakeCounter += this.yShakeSpeed * Time.deltaTime * 6f;
		this.xShakeSpeed = Mathf.Lerp(this.xShakeSpeed, 1f, Time.deltaTime * 2f);
		this.yShakeSpeed = Mathf.Lerp(this.yShakeSpeed, 1f, Time.deltaTime * 2f);
		this.vibrateAmount = Mathf.Lerp(this.vibrateAmount, 0f, Time.deltaTime * 5f);
		this.lastShakeOffset = Vector3.zero;
		this.lastShakeOffset += base.transform.right * Mathf.Sin(this.xShakeCounter) * this.xShakeAmount * 1f;
		this.lastShakeOffset += base.transform.up * Mathf.Sin(this.yShakeCounter) * this.yShakeAmount * 1f;
		this.lastShakeOffset += UnityEngine.Random.insideUnitSphere * this.vibrateAmount;
		base.transform.position += this.lastShakeOffset;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000244CC File Offset: 0x000228CC
	public void Shake(float m, float speedM)
	{
		if (this.xShakeAmount < m)
		{
			this.xShakeAmount = m * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.yShakeAmount < m)
		{
			this.yShakeAmount = m * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.xShakeSpeed < speedM)
		{
			this.xShakeSpeed = speedM * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.yShakeSpeed < speedM)
		{
			this.yShakeSpeed = speedM * UnityEngine.Random.Range(0.8f, 1.25f);
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00024568 File Offset: 0x00022968
	public void Shake(float mX, float speedMX, float mY, float speedMY)
	{
		if (this.xShakeAmount < mX)
		{
			this.xShakeAmount = mX * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.yShakeAmount < mY)
		{
			this.yShakeAmount = mY * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.xShakeSpeed < speedMX)
		{
			this.xShakeSpeed = speedMX * UnityEngine.Random.Range(0.8f, 1.25f);
		}
		if (this.yShakeSpeed < speedMY)
		{
			this.yShakeSpeed = speedMY * UnityEngine.Random.Range(0.8f, 1.25f);
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00024603 File Offset: 0x00022A03
	public void Vibrate(float amount)
	{
		if (this.vibrateAmount < amount)
		{
			this.vibrateAmount = amount;
		}
	}

	// Token: 0x040003A2 RID: 930
	private static CameraShake instance;

	// Token: 0x040003A3 RID: 931
	private const float shakeScale = 1f;

	// Token: 0x040003A4 RID: 932
	private Vector3 lastShakeOffset;

	// Token: 0x040003A5 RID: 933
	private float xShakeCounter;

	// Token: 0x040003A6 RID: 934
	private float yShakeCounter;

	// Token: 0x040003A7 RID: 935
	private float xShakeAmount;

	// Token: 0x040003A8 RID: 936
	private float yShakeAmount;

	// Token: 0x040003A9 RID: 937
	private float xShakeSpeed;

	// Token: 0x040003AA RID: 938
	private float yShakeSpeed;

	// Token: 0x040003AB RID: 939
	private float vibrateAmount;
}
