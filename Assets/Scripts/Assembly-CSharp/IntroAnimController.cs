using System;
using FreeLives;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroAnimController : MonoBehaviour
{
	private static bool introAlreadyPlayed;

	// Token: 0x06000531 RID: 1329 RVA: 0x0002CCEA File Offset: 0x0002B0EA
	public void PlaySound(string name)
	{
		if (!this.stopPlayingTapSound)
		{
			SoundController.PlaySoundEffect(this.soundHolder, name);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0002CD03 File Offset: 0x0002B103
	public void StartCameraPan()
	{
		this.cameraPanning = true;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0002CD0C File Offset: 0x0002B10C
	public void StartMusic()
	{
		this.musicAudio.Play();
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0002CD19 File Offset: 0x0002B119
	public void Start()
	{
		this.animator = base.GetComponent<Animator> ();
		this.input = new InputState();
		if (IntroAnimController.introAlreadyPlayed) {
			this.SkipCutscene ();
			this.buttonController.active = true;
		}
	}

	public void SetPanPosition() {
		Vector3 position = Camera.main.transform.position;
		this.cameraProgress += 1f / this.panTime * Time.deltaTime;
		this.cameraProgress = Mathf.Clamp01 (this.cameraProgress);
		position = Vector3.Lerp (this.panFrom, this.panTo, this.cameraPosCurve.Evaluate (this.cameraProgress));
		Camera.main.transform.position = position;
	}

	public void SkipCutscene() {
		this.animator.CrossFade(this.animator.GetCurrentAnimatorStateInfo(-1).shortNameHash, 0f, 0, 0.95f);
		this.cameraPanning = true;
		this.cameraPanDelay = 0f;
		this.cameraProgress = 1f;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0002CD34 File Offset: 0x0002B134
	private void Update()
	{
		if (this.cameraPanning && (this.cameraPanDelay -= Time.deltaTime) < 0f)
		{
			this.SetPanPosition();
		}

		InputReader.GetInput(this.input);
		if (!IntroAnimController.introAlreadyPlayed) {
			if (this.cameraProgress == 1f) {
				this.buttonController.active = true;
				IntroAnimController.introAlreadyPlayed = true;
			} else if (this.input.start && !this.input.wasStart) {
				this.SkipCutscene ();
			}
		}

		if (this.cameraProgress > 0.55f)
		{
			this.shakeDelay -= Time.deltaTime;
			if (this.shakeDelay < 0f)
			{
				this.ShakeBackground();
				this.shakeDelay = UnityEngine.Random.Range(0.2f, 1f);
			}
		}
		this.backgroundTransform.position -= this.shakeOffset;
		this.shakeXCounter += Time.deltaTime * this.shakeXSpeed;
		this.shakeYCounter += Time.deltaTime * this.shakeYSpeed;
		this.shakeOffset.x = Mathf.Sin(this.shakeXCounter) * this.shakeXAmount;
		this.shakeOffset.y = Mathf.Sin(this.shakeYCounter) * this.shakeYAmount;
		this.backgroundTransform.position += this.shakeOffset;
		this.shakeXAmount = Mathf.Lerp(this.shakeXAmount, 0f, this.shakeAmountDecay * Time.deltaTime);
		this.shakeYAmount = Mathf.Lerp(this.shakeYAmount, 0f, this.shakeAmountDecay * Time.deltaTime);
		this.shakeXSpeed = Mathf.Lerp(this.shakeXSpeed, 0f, this.shakeSpeedDecay * Time.deltaTime);
		this.shakeYSpeed = Mathf.Lerp(this.shakeYSpeed, 0f, this.shakeSpeedDecay * Time.deltaTime);
		if (Input.GetKeyDown(KeyCode.S))
		{
			this.ShakeBackground();
		}
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0002D018 File Offset: 0x0002B418
	private void ShakeBackground()
	{
		this.stopPlayingTapSound = true;
		int num = UnityEngine.Random.Range(1, 4);
		SoundController.PlaySoundEffect("BatHit" + num.ToString(), UnityEngine.Random.Range(0.2f, 0.5f), Camera.main.transform.position + Vector3.right * UnityEngine.Random.Range(-5f, 5f));
		if (UnityEngine.Random.value < 0.5f)
		{
			SoundController.PlaySoundEffect("BatHitVoice" + num.ToString(), UnityEngine.Random.Range(0.2f, 0.5f), Camera.main.transform.position + Vector3.right * UnityEngine.Random.Range(-5f, 5f));
		}
		else if (UnityEngine.Random.value < 0.25f)
		{
			SoundController.PlaySoundEffect("LaunchVoice3", UnityEngine.Random.Range(0.2f, 0.5f), Camera.main.transform.position + Vector3.right * UnityEngine.Random.Range(-5f, 5f));
		}
		if (UnityEngine.Random.value < 0.5f)
		{
			this.shakeXSpeed = Mathf.Clamp(this.shakeXSpeed + UnityEngine.Random.Range(0f, this.shakeMaxSpeed), 0f, this.shakeMaxSpeed);
			this.shakeYSpeed = Mathf.Clamp(this.shakeYSpeed + UnityEngine.Random.Range(0f, this.shakeMaxSpeed), 0f, this.shakeMaxSpeed);
			this.shakeXAmount = Mathf.Clamp(this.shakeXAmount + UnityEngine.Random.Range(0f, this.shakeMaxAmount), 0f, this.shakeMaxAmount);
			this.shakeYAmount = Mathf.Clamp(this.shakeYAmount + UnityEngine.Random.Range(0f, this.shakeMaxAmount), 0f, this.shakeMaxAmount);
		}
		if (UnityEngine.Random.value < 0.5f)
		{
			Confetti confetti = UnityEngine.Object.Instantiate<Confetti>(this.characterParticlePrefabs[UnityEngine.Random.Range(0, this.characterParticlePrefabs.Length)], new Vector3(UnityEngine.Random.Range(-5f, 5f), -2f, 0f), Quaternion.identity);
			confetti.velocity = new Vector3(0f, this.characterParticleLaunchVelocity, 0f);
			confetti.GetComponent<SpriteRenderer>().color = EffectsController.GetRandomColor();
			confetti.velocity = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-45f, 45f)) * confetti.velocity;
		}
	}

	// Token: 0x040004F1 RID: 1265
	public Confetti[] characterParticlePrefabs;

	// Token: 0x040004F2 RID: 1266
	public float characterParticleLaunchVelocity;

	// Token: 0x040004F3 RID: 1267
	public AudioSource musicAudio;

	// Token: 0x040004F4 RID: 1268
	public bool cameraPanning;

	// Token: 0x040004F5 RID: 1269
	public float cameraPanDelay;

	// Token: 0x040004F6 RID: 1270
	public SoundHolder soundHolder;

	public ButtonController buttonController;

	// Token: 0x040004F7 RID: 1271
	public Vector3 panTo;
	public Vector3 panFrom;

	// Token: 0x040004F8 RID: 1272
	public float panTime;

	// Token: 0x040004F9 RID: 1273
	public AnimationCurve cameraPosCurve;

	// Token: 0x040004FA RID: 1274
	public float cameraProgress;

	// Token: 0x040004FB RID: 1275
	private Animator animator;

	// Token: 0x040004FC RID: 1276
	public GameObject fly;

	// Token: 0x040004FD RID: 1277
	public Transform backgroundTransform;

	// Token: 0x040004FE RID: 1278
	public float shakeMaxSpeed;

	// Token: 0x040004FF RID: 1279
	public float shakeMaxAmount;

	// Token: 0x04000500 RID: 1280
	private float shakeXCounter;

	// Token: 0x04000501 RID: 1281
	private float shakeYCounter;

	// Token: 0x04000502 RID: 1282
	private float shakeXSpeed;

	// Token: 0x04000503 RID: 1283
	private float shakeYSpeed;

	// Token: 0x04000504 RID: 1284
	private float shakeXAmount;

	// Token: 0x04000505 RID: 1285
	private float shakeYAmount;

	// Token: 0x04000506 RID: 1286
	private bool stopPlayingTapSound;

	// Token: 0x04000507 RID: 1287
	public float shakeSpeedDecay;

	// Token: 0x04000508 RID: 1288
	public float shakeAmountDecay;

	// Token: 0x04000509 RID: 1289
	private InputState input;

	// Token: 0x0400050A RID: 1290
	private float shakeDelay;

	// Token: 0x0400050B RID: 1291
	private Vector3 shakeOffset = Vector3.zero;
}
