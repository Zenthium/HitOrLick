using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundParallaxSpawner : MonoBehaviour
{
	// Token: 0x06000552 RID: 1362 RVA: 0x0002D76C File Offset: 0x0002BB6C
	private void Start()
	{
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0002D770 File Offset: 0x0002BB70
	private void Update()
	{
		this.spawnDelay -= Time.deltaTime;
		if (this.spawnDelay < 0f)
		{
			BackgroundParralaxPieceInfo parallaxInfo = this.GetParallaxInfo();
			if (parallaxInfo != null)
			{
				this.spawnDelay = UnityEngine.Random.Range(0f, 0.25f);
				base.StartCoroutine(this.RunParallaxPiece(parallaxInfo));
			}
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0002D7D0 File Offset: 0x0002BBD0
	private IEnumerator RunParallaxPiece(BackgroundParralaxPieceInfo inf)
	{
		float lerpVal = UnityEngine.Random.value;
		Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(inf.spawnRangeX.x, inf.spawnRangeX.y), UnityEngine.Random.Range(inf.spawnRangeY.x, inf.spawnRangeY.y), inf.depth - lerpVal);
		SpriteRenderer p = UnityEngine.Object.Instantiate<SpriteRenderer>(inf.prefabs[UnityEngine.Random.Range(0, inf.prefabs.Length)], spawnPos, Quaternion.identity);
		if (inf.parentToCamera)
		{
			p.transform.parent = Camera.main.transform;
		}
		float life = 10f;
		if (!inf.dontScale)
		{
			p.transform.localScale = Vector3.one * Mathf.Lerp(0.5f, 1.1f, lerpVal);
		}
		bool haveDroppedFrog = false;
		Vector2 speed = Vector2.Lerp(inf.speedMin, inf.speedMax, lerpVal);
		while (life > 0f || p.isVisible)
		{
			life -= Time.deltaTime;
			yield return null;
			p.transform.Translate(speed * Time.deltaTime);
		}
		if (!haveDroppedFrog)
		{
			haveDroppedFrog = true;
			if (inf.dropFrog && UnityEngine.Random.value < 0.5f)
			{
				Confetti confetti = UnityEngine.Object.Instantiate<Confetti>(this.frogParticles[UnityEngine.Random.Range(0, this.frogParticles.Length)], p.transform.position, Quaternion.identity);
				confetti.GetComponent<SpriteRenderer>().color = new Color(0.6666667f, 0.6901961f, 0.345098048f);
				confetti.GetComponent<SpriteRenderer>().material.renderQueue = 3000;
				confetti.GetComponent<SimpleAnim>().animSpeed = 0.1f;
				confetti.velocity = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, 0f);
			}
		}
		UnityEngine.Object.Destroy(p.gameObject);
		yield break;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0002D7F4 File Offset: 0x0002BBF4
	private BackgroundParralaxPieceInfo GetParallaxInfo()
	{
		foreach (BackgroundParralaxPieceInfo backgroundParralaxPieceInfo in this.pieces)
		{
			if (UnityEngine.Random.value < backgroundParralaxPieceInfo.probability)
			{
				return backgroundParralaxPieceInfo;
			}
		}
		return null;
	}

	// Token: 0x04000526 RID: 1318
	public List<BackgroundParralaxPieceInfo> pieces;

	// Token: 0x04000527 RID: 1319
	public Confetti[] frogParticles;

	// Token: 0x04000528 RID: 1320
	private float spawnDelay;
}
