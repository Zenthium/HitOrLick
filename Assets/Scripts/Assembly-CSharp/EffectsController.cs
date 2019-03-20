using System;
using UnityEngine;
public class EffectsController : MonoBehaviour
{
	// Token: 0x060004FC RID: 1276 RVA: 0x0002A46C File Offset: 0x0002886C
	public static void CreateSmokeRing(Vector3 pos, Quaternion rot, Color col)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.smokeRing, pos - Vector3.forward * 3f, rot);
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0002A4A0 File Offset: 0x000288A0
	public static void CreateLineParticle(Vector3 pos, Quaternion rot, Color col, Vector2 velocity, float velocityScale, float lifeScale)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.lineParticle, pos, rot);
		gameObject.GetComponent<SpriteRenderer>().color = col;
		gameObject.GetComponent<Confetti>().velocity = velocity;
		gameObject.transform.localScale = new Vector3(1f, velocityScale, 1f);
		gameObject.GetComponent<SimpleAnim>().animSpeed *= lifeScale * UnityEngine.Random.Range(0.9f, 1.2f);
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x0002A51C File Offset: 0x0002891C
	public static void CreateSmokePuff(Vector3 pos, Color col)
	{
		SpriteRenderer spriteRenderer = UnityEngine.Object.Instantiate<SpriteRenderer>(EffectsController.instance.smokePuff[UnityEngine.Random.Range(0, EffectsController.instance.smokePuff.Length)], pos, Quaternion.identity);
		spriteRenderer.color = col;
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0002A55C File Offset: 0x0002895C
	public static void CreateSpawnEffects(Vector2 pos, Color col)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.spawnPuff, (Vector3) pos - Vector3.forward + Vector3.up * 0.2f, Quaternion.identity);
		gameObject.GetComponent<SpriteRenderer>().color = col;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0002A5AE File Offset: 0x000289AE
	public static void ShakeCamera(Vector2 shakeVector, float intensity)
	{
		EffectsController.instance.cameraShakeVelocity += shakeVector * (EffectsController.instance.shakePower * intensity + 10f);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0002A5E0 File Offset: 0x000289E0
	private void Update()
	{
		if (!GameController.HasInstance)
		{
			return;
		}
		this.cameraWobbleXCounter += Time.deltaTime;
		this.cameraWobbleYCounter += Time.deltaTime;
		Vector3 a = new Vector3(Mathf.Sin(this.cameraWobbleXCounter * this.cameraWobbleSpeed.x) * this.cameraWobbleAmount.x, Mathf.Sin(this.cameraWobbleYCounter * this.cameraWobbleSpeed.y) * this.cameraWobbleAmount.y, 0f);
		if (GameController.State == GameState.Playing || (this.startWinZoomDelay -= Time.deltaTime) > 0f)
		{
			Vector3 a2 = Camera.main.transform.position;
			a2 += (Vector3) this.cameraShakeVelocity * Time.deltaTime;
			if (a2.x > 1f && this.cameraShakeVelocity.x > 0f)
			{
				this.cameraShakeVelocity.x = this.cameraShakeVelocity.x * -0.9f;
				a2.x = 1f;
			}
			if (a2.y > 1f && this.cameraShakeVelocity.y > 0f)
			{
				this.cameraShakeVelocity.y = this.cameraShakeVelocity.y * -0.9f;
				a2.y = 1f;
			}
			if (a2.x < -1f && this.cameraShakeVelocity.x < 0f)
			{
				this.cameraShakeVelocity.x = this.cameraShakeVelocity.x * -0.9f;
				a2.x = -1f;
			}
			if (a2.y < -1f && this.cameraShakeVelocity.y < 0f)
			{
				this.cameraShakeVelocity.y = this.cameraShakeVelocity.y * -0.9f;
				a2.y = -1f;
			}
			if (Mathf.Sign(this.cameraShakeVelocity.x) == Mathf.Sign(a2.x))
			{
				this.cameraShakeVelocity.x = this.cameraShakeVelocity.x - a2.x * this.cameraSpring * Time.deltaTime;
			}
			if (Mathf.Sign(this.cameraShakeVelocity.y) == Mathf.Sign(a2.y))
			{
				this.cameraShakeVelocity.y = this.cameraShakeVelocity.y - a2.y * this.cameraSpring * Time.deltaTime;
			}
			Camera.main.transform.position = Vector3.Lerp(a2, a - Vector3.forward * 10f, Time.deltaTime * 3f);
		}
		else if (GameController.State == GameState.RoundFinished)
		{
			Character character = GameController.GetWinningPlayer().character;
			if (character != null)
			{
				Vector3 vector = Camera.main.transform.position;
				vector = Vector3.Lerp(vector, character.Center, Time.deltaTime * 5f);
				vector.z = Camera.main.transform.position.z;
				Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 7.5f, Time.deltaTime * 2f);
				float orthographicSize = Camera.main.orthographicSize;
				float num = orthographicSize * (float)Screen.width / (float)Screen.height;
				vector.y = Mathf.Clamp(vector.y, -(18f - orthographicSize), 18f - orthographicSize);
				vector.x = Mathf.Clamp(vector.x, -(32f - num), 32f - num);
				Camera.main.transform.position = vector;
				this.confettiDelay -= Time.deltaTime;
				if (this.confettiDelay < 0f)
				{
					this.confettiDelay = 0.03f;
					float y = vector.y + Camera.main.orthographicSize * UnityEngine.Random.Range(0.75f, 1.5f);
					float x = UnityEngine.Random.Range(vector.x - num * UnityEngine.Random.Range(0.5f, 1.5f), vector.x + num);
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.confetti, new Vector3(x, y, -1f), Quaternion.identity);
					gameObject.GetComponent<SpriteRenderer>().color = EffectsController.GetRandomColor();
					gameObject.GetComponent<Confetti>().velocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.5f, -2f), 0f);
				}
			}
			else
			{
				Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 18f, Time.deltaTime * 1f);
				Vector3 vector2 = Camera.main.transform.position;
				vector2 = Vector3.Lerp(vector2, Vector3.zero, Time.deltaTime * 1f);
				vector2.z = Camera.main.transform.position.z;
				Camera.main.transform.position = vector2;
			}
		}
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0002AB24 File Offset: 0x00028F24
	private static Quaternion GetRotationForSide(EffectsController.Side side)
	{
		switch (side)
		{
		case EffectsController.Side.Top:
			return Quaternion.Euler(0f, 0f, 180f);
		case EffectsController.Side.Bottom:
			return Quaternion.Euler(0f, 0f, 0f);
		case EffectsController.Side.Left:
			return Quaternion.Euler(0f, 0f, -90f);
		case EffectsController.Side.Right:
			return Quaternion.Euler(0f, 0f, 90f);
		default:
			return Quaternion.Euler(0f, 0f, 0f);
		}
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0002ABB4 File Offset: 0x00028FB4
	public static void CreateShingEffect(Vector2 pos, Vector2 attackDirection)
	{
		float z;
		if (attackDirection.x > 0f)
		{
			z = -Vector3.Angle(Vector3.up, attackDirection);
		}
		else
		{
			z = Vector3.Angle(Vector3.up, attackDirection);
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.shingEffect, (Vector3) pos - Vector3.forward, Quaternion.Euler(0f, 0f, z));
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002AC30 File Offset: 0x00029030
	private static Vector3 GetOffsetForSide(EffectsController.Side side)
	{
		switch (side)
		{
		case EffectsController.Side.Top:
			return new Vector3(0f, 2f, 0f);
		case EffectsController.Side.Bottom:
			return new Vector3(0f, 0f, 0f);
		case EffectsController.Side.Left:
			return new Vector3(-1f, 1f, 0f);
		case EffectsController.Side.Right:
			return new Vector3(1f, 1f, 0f);
		default:
			return new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x0002ACC0 File Offset: 0x000290C0
	public static void CreateJumpPuffStraight(Vector2 pos, EffectsController.Side orientation)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.jumpPuffStraight, (Vector3) pos + EffectsController.GetOffsetForSide(orientation) + Vector3.forward, EffectsController.GetRotationForSide(orientation));
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0002AD00 File Offset: 0x00029100
	public static void CreateJumpPuffSkew(Vector2 pos, float direction)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.jumpPuffSkew, (Vector3) pos + Vector3.forward, Quaternion.identity);
		gameObject.transform.localScale = new Vector3(direction, 1f, 1f);
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0002AD50 File Offset: 0x00029150
	public static void CreateBouncePuff(Vector2 pos, EffectsController.Side orientation)
	{
		GameObject original = EffectsController.instance.bouncePuff;
		if (orientation == EffectsController.Side.Left || orientation == EffectsController.Side.Right)
		{
			original = EffectsController.instance.wallPuff;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, (Vector3) pos + Vector3.forward + EffectsController.GetOffsetForSide(orientation), EffectsController.GetRotationForSide(orientation));
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0002ADAC File Offset: 0x000291AC
	public static void CreateDustPuff(Vector2 pos, float direction)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.dustPuff, (Vector3) pos + Vector3.forward, Quaternion.identity);
		gameObject.transform.localScale = new Vector3(direction, 1f, 1f);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0002ADF9 File Offset: 0x000291F9
	private void Awake()
	{
		EffectsController.instance = this;
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0002AE04 File Offset: 0x00029204
	public static void CreateHitEffect(Vector2 pos, float life, bool powerHit)
	{
		HitEffect hitEffect = UnityEngine.Object.Instantiate<HitEffect>(EffectsController.instance.hitEffectPrefab, (Vector3) pos + Vector3.forward * 2f, Quaternion.identity);
		hitEffect.life = life;
		hitEffect.scale = 1f + life * 0.4f;
		hitEffect = UnityEngine.Object.Instantiate<HitEffect>(EffectsController.instance.hitStarPrefab, (Vector3) pos + Vector3.forward * 3f, Quaternion.identity);
		hitEffect.life = life;
		hitEffect.scale = 1f + life * 0.4f;
		if (powerHit)
		{
			hitEffect = UnityEngine.Object.Instantiate<HitEffect>(EffectsController.instance.hitStarPowerHitPrefab, (Vector3) pos + Vector3.forward * 4f, Quaternion.identity);
			hitEffect.life = life;
			hitEffect.scale = 1.5f + life * 0.4f;
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0002AEF4 File Offset: 0x000292F4
	public static void CreateLocalizedShake(Vector2 pos, Vector2 direction, float velocity, float life)
	{
		LocalizedShake localizedShake = UnityEngine.Object.Instantiate<LocalizedShake>(EffectsController.instance.localizedShakePrefab, (Vector3) pos - Vector3.forward * 9f, Quaternion.identity);
		localizedShake.life = 0.1f + life * 0.1f;
		localizedShake.velocity = velocity;
		localizedShake.Magnitude = direction;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0002AF54 File Offset: 0x00029354
	public static void CreateTongueHitEffect(Vector2 pos, float life)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.tongueHitEffect, (Vector3) pos - Vector3.forward, Quaternion.identity);
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0002AF88 File Offset: 0x00029388
	public static void CreateTrailFader(CharacterAnimator animator, float lifeM, Color color, bool grow = false)
	{
		TrailFader trailFader = UnityEngine.Object.Instantiate<TrailFader>(EffectsController.instance.faderTrailPrefab, animator.rend.transform.position + Vector3.forward * 3.5f, animator.rend.transform.rotation);
		trailFader.transform.localScale = animator.rend.transform.localScale;
		trailFader.spriteRenderer.color = color;
		trailFader.spriteRenderer.sprite = animator.rend.sprite;
		color.a = 0.5f;
		trailFader.spriteRenderer.color = color;
		trailFader.lifeM = 1f + lifeM;
		trailFader.transform.parent = animator.rend.transform;
		trailFader.transform.localPosition = Vector3.forward;
		trailFader.copySpriteRenderer = animator.rend;
		trailFader.grow = grow;
		if (grow)
		{
			trailFader.transform.localPosition = (Vector2) trailFader.transform.localPosition + UnityEngine.Random.insideUnitCircle * 0.25f;
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0002B0B0 File Offset: 0x000294B0
	internal static void CreateKnockedUpEffect(CharacterAnimator characterAnimator)
	{
		KnockedUpEffect knockedUpEffect = UnityEngine.Object.Instantiate<KnockedUpEffect>(EffectsController.instance.knockedUpEffect, characterAnimator.transform.position - Vector3.forward * 5f, Quaternion.identity);
		knockedUpEffect.spriteRenderer.color = characterAnimator.rend.color;
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0002B108 File Offset: 0x00029508
	public static void SprayParticles(Vector2 pos, Vector2 direction, float force, int amount, float life, float angle)
	{
		for (int i = 0; i < amount; i++)
		{
			HitParticle hitParticle = UnityEngine.Object.Instantiate<HitParticle>(EffectsController.instance.hitParticlePrefab, pos + UnityEngine.Random.insideUnitCircle, Quaternion.identity);
			float t = ((float)i + 1f) / ((float)amount + 2f);
			hitParticle.velocity = Quaternion.Euler(0f, 0f, Mathf.Lerp(-angle, angle, t)) * direction * force;
			hitParticle.life = life * UnityEngine.Random.Range(0.9f, 1.1f);
			Vector2 one = Vector2.one;
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0002B1B4 File Offset: 0x000295B4
	public static void CreateStarParticles(Vector2 pos, Vector2 velocity, float life, int amount, Color[] colors)
	{
		for (int i = 0; i < amount; i++)
		{
			HitParticle hitParticle = UnityEngine.Object.Instantiate<HitParticle>(EffectsController.instance.hitParticlePrefab, pos + UnityEngine.Random.insideUnitCircle, Quaternion.identity);
			float num = (float)i / (float)amount;
			hitParticle.velocity = velocity * UnityEngine.Random.Range(0.75f, 1.25f);
			hitParticle.life = life * UnityEngine.Random.Range(0.9f, 1.1f);
			hitParticle.startSize = 0.15f;
			hitParticle.endSize = 0f;
			if (colors != null)
			{
				hitParticle.SetColors(colors);
			}
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0002B255 File Offset: 0x00029655
	public static Color GetRandomColor()
	{
		return EffectsController.instance.randomColors[UnityEngine.Random.Range(0, EffectsController.instance.randomColors.Length)];
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0002B280 File Offset: 0x00029680
	internal static void CreateSideScorePlum(Vector3 position, EffectsController.Side killedOnSide, int hitsTaken, Color playerColor)
	{
		Quaternion rotation = Quaternion.identity;
		position.x = Mathf.Clamp(position.x, -31f, 31f);
		position.y = Mathf.Clamp(position.y, -17f, 17f);
		int num = Mathf.Clamp(hitsTaken, 0, int.MaxValue);
		if (killedOnSide == EffectsController.Side.Left)
		{
			position.x = -32f;
			EffectsController.ShakeCamera(Vector2.left, (float)hitsTaken);
		}
		else if (killedOnSide == EffectsController.Side.Right)
		{
			position.x = 32f;
			rotation = Quaternion.Euler(0f, 0f, 180f);
			EffectsController.ShakeCamera(Vector2.right, (float)hitsTaken);
		}
		else if (killedOnSide == EffectsController.Side.Top)
		{
			position.y = 18f;
			rotation = Quaternion.Euler(0f, 0f, -90f);
			EffectsController.ShakeCamera(Vector2.up, (float)hitsTaken);
		}
		else if (killedOnSide == EffectsController.Side.Bottom)
		{
			position.y = -18f;
			rotation = Quaternion.Euler(0f, 0f, 90f);
			EffectsController.ShakeCamera(Vector2.down, (float)hitsTaken);
		}
		position.z = -1f;
		SideScorePlum sideScorePlum = UnityEngine.Object.Instantiate<SideScorePlum>(EffectsController.instance.sideScorePlumPrefab, position, rotation);
		sideScorePlum.SetPoints(hitsTaken);
		sideScorePlum.transform.parent = Camera.main.transform;
		position.z = 11f;
		sideScorePlum.transform.localPosition = position;
		if (hitsTaken > 0)
		{
			sideScorePlum.SetText("+" + hitsTaken, playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("OOPS!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("DERP!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("FAIL!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("CRAP!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("SHIT!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("CRUD!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText(":(", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("DARN!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("BUTTS!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("FAIL!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("DRAT!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("OH DEAR!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("OH NO!", playerColor);
		}
		else if (UnityEngine.Random.value < 0.2f)
		{
			sideScorePlum.SetText("FAREWELL, CRUEL WORLD!", playerColor);
		}
		else
		{
			sideScorePlum.SetText("~_~", playerColor);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0002B5E4 File Offset: 0x000299E4
	internal static void CreateTurnAroundPuff(Vector3 position, float dir)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EffectsController.instance.turnAroundPuff, position + Vector3.forward, Quaternion.identity);
		gameObject.transform.localScale = new Vector3(dir, 1f, 1f);
	}

	// Token: 0x0400049F RID: 1183
	public HitEffect hitEffectPrefab;

	// Token: 0x040004A0 RID: 1184
	public HitEffect hitStarPrefab;

	// Token: 0x040004A1 RID: 1185
	public HitEffect hitStarPowerHitPrefab;

	// Token: 0x040004A2 RID: 1186
	public GameObject directionalHitEffect;

	// Token: 0x040004A3 RID: 1187
	public HitParticle hitParticlePrefab;

	// Token: 0x040004A4 RID: 1188
	public TrailFader faderTrailPrefab;

	// Token: 0x040004A5 RID: 1189
	private static EffectsController instance;

	// Token: 0x040004A6 RID: 1190
	public KnockedUpEffect knockedUpEffect;

	// Token: 0x040004A7 RID: 1191
	public GameObject tongueHitEffect;

	// Token: 0x040004A8 RID: 1192
	public SideScorePlum sideScorePlumPrefab;

	// Token: 0x040004A9 RID: 1193
	public LocalizedShake localizedShakePrefab;

	// Token: 0x040004AA RID: 1194
	public GameObject jumpPuffSkew;

	// Token: 0x040004AB RID: 1195
	public GameObject jumpPuffStraight;

	// Token: 0x040004AC RID: 1196
	public GameObject bouncePuff;

	// Token: 0x040004AD RID: 1197
	public GameObject wallPuff;

	// Token: 0x040004AE RID: 1198
	public GameObject dustPuff;

	// Token: 0x040004AF RID: 1199
	public GameObject turnAroundPuff;

	// Token: 0x040004B0 RID: 1200
	public GameObject smokeRing;

	// Token: 0x040004B1 RID: 1201
	public GameObject lineParticle;

	// Token: 0x040004B2 RID: 1202
	public GameObject confetti;

	// Token: 0x040004B3 RID: 1203
	public GameObject shingEffect;

	// Token: 0x040004B4 RID: 1204
	public ParticleSystem spawnPuffParticleSystem;

	// Token: 0x040004B5 RID: 1205
	public SpriteRenderer[] smokePuff;

	// Token: 0x040004B6 RID: 1206
	public GameObject spawnPuff;

	// Token: 0x040004B7 RID: 1207
	private Vector2 cameraShakeVelocity;

	// Token: 0x040004B8 RID: 1208
	private float confettiDelay;

	// Token: 0x040004B9 RID: 1209
	public float cameraSpring;

	// Token: 0x040004BA RID: 1210
	public float shakePower;

	// Token: 0x040004BB RID: 1211
	private float startWinZoomDelay = 1.5f;

	// Token: 0x040004BC RID: 1212
	public Vector2 cameraWobbleSpeed;

	// Token: 0x040004BD RID: 1213
	public Vector2 cameraWobbleAmount;

	// Token: 0x040004BE RID: 1214
	private float cameraWobbleXCounter;

	// Token: 0x040004BF RID: 1215
	private float cameraWobbleYCounter;

	// Token: 0x040004C0 RID: 1216
	public Color[] randomColors;

	// Token: 0x020000EA RID: 234
	public enum Side
	{
		// Token: 0x040004C2 RID: 1218
		Top,
		// Token: 0x040004C3 RID: 1219
		Bottom,
		// Token: 0x040004C4 RID: 1220
		Left,
		// Token: 0x040004C5 RID: 1221
		Right
	}
}
