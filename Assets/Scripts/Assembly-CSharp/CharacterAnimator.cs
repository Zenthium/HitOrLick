using System;
using FreeLives;
using UnityEngine;
public class CharacterAnimator : MonoBehaviour
{
	// Token: 0x060004E8 RID: 1256 RVA: 0x00028AD8 File Offset: 0x00026ED8
	private CharacterAnimator.AttackDirection DetermineAttackDirection()
	{
		if (this.character.attackDir.y == 1f && this.character.attackDir.x == 0f)
		{
			return CharacterAnimator.AttackDirection.Up;
		}
		if (this.character.attackDir.y > 0.45f && Mathf.Abs(this.character.attackDir.x) > 0.45f)
		{
			return CharacterAnimator.AttackDirection.DiagonalUp;
		}
		if (this.character.attackDir.y < -0.45f && Mathf.Abs(this.character.attackDir.x) > 0.45f)
		{
			return CharacterAnimator.AttackDirection.DownForward;
		}
		if (this.character.attackDir.y == -1f)
		{
			return CharacterAnimator.AttackDirection.Down;
		}
		return CharacterAnimator.AttackDirection.Forward;
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00028BAE File Offset: 0x00026FAE
	private float t
	{
		get
		{
			return this.character.t;
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00028BBC File Offset: 0x00026FBC
	private void Start()
	{
		if (this.character.player != null)
		{
			this.rend.color = this.character.player.color;
			this.spriteDefaultOffset = this.rend.transform.localPosition;
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x00028C0C File Offset: 0x0002700C
	private void LateUpdate()
	{
		CharacterAnimator.AnimState animState = this.DetermineAnimState();
		this.rend.transform.localRotation = Quaternion.identity;
		this.rend.transform.localPosition = this.spriteDefaultOffset;
		this.rend.transform.localScale = new Vector3((float)this.character.FacingDirection, 1f, 1f);
		if (this.animState != animState)
		{
			this.frame = 0;
			this.frameCounter = 0f;
			if (this.animState == CharacterAnimator.AnimState.Skidding)
			{
				this.transitionTime = 0.05f;
				this.rend.sprite = this.skidRecover;
			}
			else if (animState == CharacterAnimator.AnimState.Jumping && this.animState == CharacterAnimator.AnimState.WallSlide)
			{
				this.transitionTime = 0.05f;
				this.rend.sprite = this.wallSlideJumpLaunch[0];
			}
			this.variationRandomizer = UnityEngine.Random.Range(0, 10);
			if (animState != CharacterAnimator.AnimState.Tongue)
			{
				this.tongueLine.enabled = false;
				this.tongueTip.gameObject.SetActive(false);
			}
		}
		this.animState = animState;
		if (this.transitionTime > 0f)
		{
			if (this.animState != CharacterAnimator.AnimState.BouncedInAir)
			{
				this.transitionTime -= this.t;
				return;
			}
			this.transitionTime = 0f;
		}
		switch (this.animState)
		{
		case CharacterAnimator.AnimState.Idle:
			this.AnimateIdle();
			break;
		case CharacterAnimator.AnimState.Running:
			this.AnimateRun();
			break;
		case CharacterAnimator.AnimState.Jumping:
			this.AnimateJumping();
			break;
		case CharacterAnimator.AnimState.Skidding:
			this.AnimateSkid();
			break;
		case CharacterAnimator.AnimState.BouncedInAir:
			this.AnimateBouncedFlying();
			break;
		case CharacterAnimator.AnimState.ChargeAttack:
			this.AnimateChargeAttack();
			break;
		case CharacterAnimator.AnimState.Attacking:
			this.AnimateAttack();
			break;
		case CharacterAnimator.AnimState.AttackRecover:
			this.AnimateAttackRecover();
			break;
		case CharacterAnimator.AnimState.WallSlide:
			this.AnimateWallSlide();
			break;
		case CharacterAnimator.AnimState.Tongue:
			this.AnimateTongue();
			break;
		}
		if (this.character.IngestedFly && (this.character.state != CharacterState.Tounge || this.character.tongueState != TongueState.HitFlyBurping))
		{
			this.RunTrailSilhouettePoweredUp();
		}
		else
		{
			this.rend.color = this.character.player.color;
		}
		this.RunFlightAudio();
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x00028E74 File Offset: 0x00027274
	private void AnimateRun()
	{
		int num = this.frame;
		this.RunAnimation(this.run, 0.04f, false, false);
		if (this.frame != num && this.frame % 2 == 1)
		{
			SoundController.PlaySoundEffect("Footstep", 0.1f, base.transform.position);
			EffectsController.CreateDustPuff(base.transform.position, (float)this.character.FacingDirection);
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00028EF0 File Offset: 0x000272F0
	private void AnimateIdle()
	{
		this.frameCounter += this.t;
		if (GameController.State == GameState.RoundFinished && this.character.IsWinningPlayer())
		{
			if (this.frameCounter < 0.3f)
			{
				this.rend.sprite = this.idle[0];
			}
			else if (this.frameCounter < 0.4f)
			{
				this.rend.sprite = this.win[0];
			}
			else if (this.frameCounter % 2f < 0.9f)
			{
				this.rend.sprite = this.win[3];
			}
			else if (this.frameCounter % 2f < 1f)
			{
				this.rend.sprite = this.win[2];
			}
			else if (this.frameCounter % 2f < 1.9f)
			{
				this.rend.sprite = this.win[1];
			}
			else
			{
				this.rend.sprite = this.win[2];
			}
		}
		else if (this.frameCounter < 1f)
		{
			this.rend.sprite = this.idle[0];
		}
		else if (this.frameCounter % 3f < 2.95f)
		{
			this.rend.sprite = this.idle[1];
		}
		else
		{
			this.rend.sprite = this.idle[2];
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x00029088 File Offset: 0x00027488
	private void AnimateChargeAttack()
	{
		CharacterAnimator.AttackDirection attackDirection = this.DetermineAttackDirection();
		if (attackDirection == CharacterAnimator.AttackDirection.Up)
		{
			this.RunAnimation(this.attachChargeUp, Mathf.Lerp(0.2f, 0.03f, this.character.attackChargeM), false, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DiagonalUp)
		{
			this.RunAnimation(this.attackChargeDiagUp, Mathf.Lerp(0.2f, 0.03f, this.character.attackChargeM), false, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.Down)
		{
			this.RunAnimation(this.attackChargeDown, Mathf.Lerp(0.2f, 0.03f, this.character.attackChargeM), false, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DownForward)
		{
			this.RunAnimation(this.attackChargeDownForward, Mathf.Lerp(0.2f, 0.03f, this.character.attackChargeM), false, false);
		}
		else
		{
			this.RunAnimation(this.attackCharge, Mathf.Lerp(0.2f, 0.03f, this.character.attackChargeM), false, false);
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00029194 File Offset: 0x00027594
	private void AnimateTongue()
	{
		if (this.character.tongueState == TongueState.HitFlyBurping)
		{
			if (!this.wasBurp)
			{
				this.frame = 0;
				this.frameCounter = 0f;
				this.wasBurp = true;
			}
			this.RunAnimation(this.tongueBurp, 0.05f, true, false);
		}
		else if (this.character.tongueState == TongueState.HitEnemyTongueStunned)
		{
			this.wasBurp = false;
			this.tongueTip.gameObject.SetActive(false);
			this.tongueLine.enabled = false;
			this.RunAnimation(this.blush, 0.05f, false, false);
		}
		else if (!this.character.OnGround && this.character.tongueDir.y < 0f)
		{
			this.wasBurp = false;
			if (this.character.velocity.y >= 0f)
			{
				this.RunAnimation(this.tongueDownMovingUp, 0.1f, true, false);
			}
			else
			{
				this.RunAnimation(this.tongueDownMovingDown, 0.1f, true, false);
			}
			this.tongueLine.enabled = true;
			this.tongueTip.gameObject.SetActive(true);
			this.tongueLine.SetPosition(1, Vector3.up * 1.5f + (Vector3) this.character.tongueDir * this.character.tongueDistance + Vector3.forward * 1.1f);
			this.tongueTip.transform.localPosition = (Vector3) this.character.tongueOrigin + (Vector3) this.character.tongueDir * this.character.tongueDistance + Vector3.forward;
			this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, this.character.tongueDir));
			if (this.character.tongueDir.y < 0f)
			{
				this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, -Vector2.Angle(Vector2.right, this.character.tongueDir));
			}
			else
			{
				this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, this.character.tongueDir));
			}
		}
		else
		{
			this.wasBurp = false;
			if (this.character.tongueState == TongueState.RetractingHitEnemyTongue)
			{
				this.RunAnimation(this.tongueRetractStunned, 0.05f, false, false);
			}
			else
			{
				this.RunAnimation(this.tongue, 0.1f, true, false);
			}
			this.tongueLine.enabled = true;
			this.tongueTip.gameObject.SetActive(true);
			this.tongueLine.SetPosition(1, Vector3.up * 1.5f + (Vector3) this.character.tongueDir * this.character.tongueDistance + Vector3.forward * 1.1f);
			this.tongueTip.transform.localPosition = (Vector3) this.character.tongueOrigin + (Vector3) this.character.tongueDir * this.character.tongueDistance + Vector3.forward;
			this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, this.character.tongueDir));
			if (this.character.tongueDir.y < 0f)
			{
				this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, -Vector2.Angle(Vector2.right, this.character.tongueDir));
			}
			else
			{
				this.tongueTip.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.Angle(Vector2.right, this.character.tongueDir));
			}
		}
		if (this.character.wasBouncingBeforeTongue)
		{
			this.RunTrailSilhouette();
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x00029608 File Offset: 0x00027A08
	private void AnimateAttack()
	{
		CharacterAnimator.AttackDirection attackDirection = this.DetermineAttackDirection();
		if (attackDirection == CharacterAnimator.AttackDirection.Up)
		{
			this.RunAnimation(this.attackUp, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DiagonalUp)
		{
			this.RunAnimation(this.attackDiagUp, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.Down)
		{
			this.RunAnimation(this.attackDown, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DownForward)
		{
			this.RunAnimation(this.attackDownForward, 0.05f, true, false);
		}
		else
		{
			this.rend.sprite = this.attack[0];
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x000296AC File Offset: 0x00027AAC
	private void AnimateAttackRecover()
	{
		CharacterAnimator.AttackDirection attackDirection = this.DetermineAttackDirection();
		if (attackDirection == CharacterAnimator.AttackDirection.Up)
		{
			this.RunAnimation(this.attackRecoverUp, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DiagonalUp)
		{
			this.RunAnimation(this.attackRecoverDiagUp, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.Down)
		{
			this.RunAnimation(this.attackRecoverDown, 0.05f, true, false);
		}
		else if (attackDirection == CharacterAnimator.AttackDirection.DownForward)
		{
			this.RunAnimation(this.attackRecoverDownForward, 0.05f, true, false);
		}
		else
		{
			this.RunAnimation(this.attackRecover, 0.05f, true, false);
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x00029750 File Offset: 0x00027B50
	private void AnimateBouncedFlying()
	{
		if (this.character.TimeBumpActive && this.character.timeBumpTimeScale == 0f)
		{
			this.RunAnimation(this.impact, 0.05f, false, true);
			this.havePlayedLaunchSound = false;
		}
		else if (!this.character.hasBounceDodged)
		{
			if (!this.havePlayedLaunchSound)
			{
				int num = Mathf.Clamp(this.character.hitsTaken, 0, 5);
				int num2 = 0;
				if (num >= 5)
				{
					num2 = 3;
				}
				else if (num >= 3)
				{
					num2 = 2;
				}
				else if (num >= 1)
				{
					num2 = 1;
				}
				this.havePlayedLaunchSound = true;
				if (num2 > 0)
				{
					SoundController.PlaySoundEffect("Launch" + num2.ToString(), 0.5f, base.transform.position);
					SoundController.PlaySoundEffect("LaunchVoice" + num2.ToString(), 0.5f, base.transform.position);
				}
			}
			if (!this.character.canBounceDodge)
			{
				float num3 = Vector3.Angle(Vector3.up, this.character.velocity);
				if (this.character.velocity.x > 0f)
				{
					num3 = 360f - num3;
				}
				this.rend.transform.localRotation = Quaternion.Euler(0f, 0f, num3);
				Vector3 localPosition = this.spriteDefaultOffset;
				localPosition.y = 1f;
				this.rend.transform.localPosition = localPosition;
				if (this.character.hitsTaken > 4)
				{
					this.RunAnimation(this.bouncedFlyingComet, 0.04f, false, false);
				}
				else if (this.character.hitsTaken > 2)
				{
					this.RunAnimation(this.bouncedFlying, 0.04f, false, false);
				}
				else
				{
					this.RunAnimation(this.bouncedFlyingSpinning, 0.04f, false, false);
				}
				if (this.character.hitsTaken > 2)
				{
					this.particleCounter += this.t;
					if (this.particleCounter > this.trailDelay)
					{
						this.particleCounter -= this.trailDelay;
						EffectsController.CreateStarParticles(this.character.Center, this.character.velocity * 0.1f, 0.5f, 1, new Color[]
						{
							Color.white,
							Color.black,
							this.character.player.color
						});
					}
				}
			}
			else
			{
				float num4 = Vector3.Angle(Vector3.up, this.character.velocity);
				if (this.character.velocity.x > 0f)
				{
					num4 = 360f - num4;
				}
				this.rend.transform.localRotation = Quaternion.Euler(0f, 0f, num4);
				this.RunAnimation(this.bouncedFlyingRecovered, 0.05f, false, false);
			}
		}
		else
		{
			this.RunAnimation(this.somersault, 0.05f, false, false);
		}
		if (this.character.hitsTaken > 0)
		{
			this.RunTrailSilhouette();
			if (!this.character.hasBounceDodged)
			{
				if (Vector2.Distance(this.lastSmokeRingPos, this.character.Center) > this.smokeRingDistance && this.character.velocity.magnitude > this.character.maxRunSpeed && this.character.hitsTaken > 2)
				{
					this.lastSmokeRingPos = this.character.Center;
					EffectsController.CreateSmokeRing(this.character.Center, this.rend.transform.rotation, this.character.player.color);
				}
				this.trailCounter -= this.t;
				if (this.trailCounter < 0f)
				{
					this.trailCounter += this.trailDelay;
					if (this.character.lastHitByPlayer != null)
					{
						for (int i = 0; i < this.character.hitsTaken; i++)
						{
							Color col = Color.white;
							Color color = this.character.player.color;
							if (UnityEngine.Random.value < 0.5f)
							{
								col = Color.Lerp(color, Color.black, UnityEngine.Random.value * 0.3f);
							}
							else
							{
								col = Color.Lerp(color, Color.white, UnityEngine.Random.value * 0.8f);
							}
							if (this.character.velocity.magnitude > this.character.maxRunSpeed)
							{
								EffectsController.CreateLineParticle((Vector3) this.character.Center + (Vector3) UnityEngine.Random.insideUnitCircle * 0.5f + Vector3.forward, this.rend.transform.rotation, col, this.character.velocity * 0f, 0.3f + this.character.velocity.magnitude * this.lineVelocityScale, 0.25f + 0.5f * this.character.velocity.magnitude / this.character.maxRunSpeed);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x00029CE8 File Offset: 0x000280E8
	private void RunTrailSilhouette()
	{
		this.trailFaderCounter -= Time.deltaTime;
		if (this.trailFaderCounter <= 0f)
		{
			this.trailNumber++;
			this.trailFaderCounter += this.trailDelay * 2f;
			EffectsController.CreateTrailFader(this, 0.1f + (float)this.character.hitsTaken * 0.15f, Color.Lerp(this.character.lastHitByPlayer.color, Color.white, Mathf.PingPong((float)this.trailNumber * 0.2f, 1f)), false);
		}
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x00029D90 File Offset: 0x00028190
	private void RunTrailSilhouettePoweredUp()
	{
		this.trailFaderCounter -= Time.deltaTime;
		if (this.trailFaderCounter <= 0f)
		{
			this.trailNumber++;
			this.trailFaderCounter += this.trailDelay * 7f;
			if (this.trailNumber % 3 == 0)
			{
				this.rend.color = Color.white;
			}
			else if (this.trailNumber % 3 == 1)
			{
				this.rend.color = Color.black;
			}
			else
			{
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				this.rend.color = this.character.player.color;
			}
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x00029E4C File Offset: 0x0002824C
	private void RunFlightAudio()
	{
		if (this.character.state == CharacterState.Bouncing)
		{
			if (this.character.TimeBumpActive && this.character.timeBumpTimeScale == 0f)
			{
				if (this.flightAudioSource.isPlaying)
				{
					this.flightAudioSource.Stop();
				}
				this.flightOrigin = this.character.transform.position.y;
			}
			else if (!this.character.hasBounceDodged)
			{
				this.flightAudioSource.volume = 0.15f;
				if (!this.flightAudioSource.isPlaying)
				{
					if (this.character.hitsTaken > 4)
					{
						this.flightAudioSource.clip = this.flight[2];
					}
					else if (this.character.hitsTaken > 2)
					{
						this.flightAudioSource.clip = this.flight[1];
					}
					else if (this.character.hitsTaken > 0)
					{
						this.flightAudioSource.clip = this.flight[0];
					}
					this.flightAudioSource.Play();
				}
				this.flightAudioSource.pitch = 1f + (this.character.transform.position.y - this.flightOrigin) * this.flightHeightPitchMod;
				if (this.modFlightVolume)
				{
					this.flightAudioSource.volume = this.character.velocity.magnitude * this.flightVelocityVolumeMod;
				}
			}
			else if (this.flightAudioSource.isPlaying)
			{
				this.flightAudioSource.Stop();
			}
		}
		else
		{
			this.flightAudioSource.volume = Mathf.MoveTowards(this.flightAudioSource.volume, 0f, Time.deltaTime * 2f);
		}
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0002A034 File Offset: 0x00028434
	private void AnimateJumping()
	{
		if (this.character.velocity.y < this.character.gravityGraceThreshold && this.character.input.aButton && this.character.gravityGraceTimeLeft > 0f)
		{
			float num = 1f - this.character.gravityGraceTimeLeft / this.character.gravityGraceTime;
			this.rend.sprite = this.somersault[(int)(num * (float)this.somersault.Length)];
		}
		else if (this.character.Velocity.y > 0f)
		{
			if (this.frame < this.jumpLaunch.Length)
			{
				this.rend.sprite = this.jumpLaunch[this.frame];
				this.frameCounter += this.t;
				if (this.frameCounter > 0.05f)
				{
					this.frame++;
					this.frameCounter = 0f;
				}
			}
			else
			{
				this.RunAnimation(this.jumpUp, 0.05f, false, false);
			}
		}
		else
		{
			this.RunAnimation(this.jumpDown, 0.05f, false, false);
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0002A180 File Offset: 0x00028580
	private void AnimateSkid()
	{
		if (Mathf.Abs(base.transform.position.x - this.lastSkidXPos) > this.skidEffectDistance && Mathf.Abs(this.character.velocity.x) > this.character.maxRunSpeed)
		{
			this.lastSkidXPos = base.transform.position.x;
			EffectsController.CreateJumpPuffSkew(base.transform.position, (float)this.character.FacingDirection);
		}
		if (this.frame == 0)
		{
			this.rend.sprite = this.skidLand;
			this.frameCounter += this.t;
			if (this.frameCounter > 0.075f)
			{
				this.frame++;
				this.frameCounter = 0f;
			}
		}
		else
		{
			this.RunAnimation(this.skid, 0.05f, false, false);
		}
		if (this.character.hitsTaken > 0)
		{
			this.RunTrailSilhouette();
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0002A2A0 File Offset: 0x000286A0
	private void RunAnimation(Sprite[] frames, float frameDelay, bool clamp = false, bool ignoreCharacterTimescale = false)
	{
		if (ignoreCharacterTimescale)
		{
			this.frameCounter += Time.deltaTime;
		}
		else
		{
			this.frameCounter += this.t;
		}
		if (this.frameCounter > frameDelay || this.frame < 0)
		{
			this.frame++;
			this.frameCounter -= frameDelay;
		}
		if (clamp)
		{
			this.rend.sprite = frames[Mathf.Clamp(this.frame, 0, frames.Length - 1)];
		}
		else
		{
			this.rend.sprite = frames[this.frame % frames.Length];
		}
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x0002A352 File Offset: 0x00028752
	private void AnimateWallSlide()
	{
		this.RunAnimation(this.wallSlide, 0.04f, false, false);
	}

	// Token: 0x060004FA RID: 1274 RVA: 0x0002A368 File Offset: 0x00028768
	private CharacterAnimator.AnimState DetermineAnimState()
	{
		if (this.character.state != CharacterState.Bouncing)
		{
			if (this.character.state == CharacterState.Attacking)
			{
				if (this.character.attackState == AttackState.Charging)
				{
					return CharacterAnimator.AnimState.ChargeAttack;
				}
				if (this.character.attackState == AttackState.Attacking)
				{
					return CharacterAnimator.AnimState.Attacking;
				}
				if (this.character.attackState == AttackState.Recovering)
				{
					return CharacterAnimator.AnimState.AttackRecover;
				}
			}
			else
			{
				if (this.character.state == CharacterState.Tounge)
				{
					return CharacterAnimator.AnimState.Tongue;
				}
				if (this.character.OnGround && Mathf.Abs(this.character.Velocity.x) > 0f)
				{
					return CharacterAnimator.AnimState.Running;
				}
				if (!this.character.OnGround)
				{
					if (this.character.WallSliding)
					{
						return CharacterAnimator.AnimState.WallSlide;
					}
					return CharacterAnimator.AnimState.Jumping;
				}
			}
			return CharacterAnimator.AnimState.Idle;
		}
		if (this.character.OnGround)
		{
			return CharacterAnimator.AnimState.Skidding;
		}
		return CharacterAnimator.AnimState.BouncedInAir;
	}

	// Token: 0x0400044B RID: 1099
	private Vector3 spriteDefaultOffset;

	// Token: 0x0400044C RID: 1100
	public Character character;

	// Token: 0x0400044D RID: 1101
	public SpriteRenderer rend;

	// Token: 0x0400044E RID: 1102
	public LineRenderer tongueLine;

	// Token: 0x0400044F RID: 1103
	public SpriteRenderer tongueTip;

	// Token: 0x04000450 RID: 1104
	private float particleCounter;

	// Token: 0x04000451 RID: 1105
	private float lastSkidXPos;

	// Token: 0x04000452 RID: 1106
	public float skidEffectDistance;

	// Token: 0x04000453 RID: 1107
	public Sprite[] idle;

	// Token: 0x04000454 RID: 1108
	public Sprite[] run;

	// Token: 0x04000455 RID: 1109
	public Sprite[] jumpLaunch;

	// Token: 0x04000456 RID: 1110
	public Sprite[] jumpUp;

	// Token: 0x04000457 RID: 1111
	public Sprite[] jumpDown;

	// Token: 0x04000458 RID: 1112
	public Sprite skidLand;

	// Token: 0x04000459 RID: 1113
	public Sprite[] skid;

	// Token: 0x0400045A RID: 1114
	public Sprite skidRecover;

	// Token: 0x0400045B RID: 1115
	public Sprite[] somersault;

	// Token: 0x0400045C RID: 1116
	public Sprite[] bouncedFlying;

	// Token: 0x0400045D RID: 1117
	public Sprite[] bouncedFlyingComet;

	// Token: 0x0400045E RID: 1118
	public Sprite[] bouncedFlyingRecovered;

	// Token: 0x0400045F RID: 1119
	public Sprite[] bouncedFlyingSpinning;

	// Token: 0x04000460 RID: 1120
	public Sprite[] bouncedFlyingRotationg;

	// Token: 0x04000461 RID: 1121
	public Sprite[] attackCharge;

	// Token: 0x04000462 RID: 1122
	public Sprite[] attack;

	// Token: 0x04000463 RID: 1123
	public Sprite[] attackRecover;

	// Token: 0x04000464 RID: 1124
	public Sprite[] attachChargeUp;

	// Token: 0x04000465 RID: 1125
	public Sprite[] attackUp;

	// Token: 0x04000466 RID: 1126
	public Sprite[] attackRecoverUp;

	// Token: 0x04000467 RID: 1127
	public Sprite[] attackChargeDiagUp;

	// Token: 0x04000468 RID: 1128
	public Sprite[] attackDiagUp;

	// Token: 0x04000469 RID: 1129
	public Sprite[] attackRecoverDiagUp;

	// Token: 0x0400046A RID: 1130
	public Sprite[] attackChargeDown;

	// Token: 0x0400046B RID: 1131
	public Sprite[] attackDown;

	// Token: 0x0400046C RID: 1132
	public Sprite[] attackRecoverDown;

	// Token: 0x0400046D RID: 1133
	public Sprite[] attackChargeDownForward;

	// Token: 0x0400046E RID: 1134
	public Sprite[] attackDownForward;

	// Token: 0x0400046F RID: 1135
	public Sprite[] attackRecoverDownForward;

	// Token: 0x04000470 RID: 1136
	public Sprite[] impact;

	// Token: 0x04000471 RID: 1137
	public Sprite[] wallSlide;

	// Token: 0x04000472 RID: 1138
	public Sprite[] wallSlideJumpLaunch;

	// Token: 0x04000473 RID: 1139
	public Sprite[] tongue;

	// Token: 0x04000474 RID: 1140
	public Sprite[] tongueDownMovingUp;

	// Token: 0x04000475 RID: 1141
	public Sprite[] tongueDownMovingDown;

	// Token: 0x04000476 RID: 1142
	public Sprite[] tongueRetractStunned;

	// Token: 0x04000477 RID: 1143
	public Sprite[] tongueBurp;

	// Token: 0x04000478 RID: 1144
	public Sprite[] blush;

	// Token: 0x04000479 RID: 1145
	public Sprite[] win;

	// Token: 0x0400047A RID: 1146
	public AudioClip[] flight;

	// Token: 0x0400047B RID: 1147
	public AudioSource flightAudioSource;

	// Token: 0x0400047C RID: 1148
	public float flightHeightPitchMod;

	// Token: 0x0400047D RID: 1149
	public bool modFlightVolume;

	// Token: 0x0400047E RID: 1150
	public float flightVelocityVolumeMod;

	// Token: 0x0400047F RID: 1151
	public float smokeRingDistance;

	// Token: 0x04000480 RID: 1152
	public float lineVelocityScale;

	// Token: 0x04000481 RID: 1153
	private Vector2 lastSmokeRingPos;

	// Token: 0x04000482 RID: 1154
	private int frame;

	// Token: 0x04000483 RID: 1155
	private float frameCounter;

	// Token: 0x04000484 RID: 1156
	private CharacterAnimator.AnimState animState;

	// Token: 0x04000485 RID: 1157
	private float trailCounter;

	// Token: 0x04000486 RID: 1158
	private float trailFaderCounter;

	// Token: 0x04000487 RID: 1159
	public float trailDelay;

	// Token: 0x04000488 RID: 1160
	private int variationRandomizer;

	// Token: 0x04000489 RID: 1161
	private float transitionTime;

	// Token: 0x0400048A RID: 1162
	private bool wasBurp;

	// Token: 0x0400048B RID: 1163
	private bool havePlayedLaunchSound;

	// Token: 0x0400048C RID: 1164
	private float flightOrigin;

	// Token: 0x0400048D RID: 1165
	private int trailNumber;

	// Token: 0x020000E7 RID: 231
	private enum AnimState
	{
		// Token: 0x0400048F RID: 1167
		Idle,
		// Token: 0x04000490 RID: 1168
		Running,
		// Token: 0x04000491 RID: 1169
		Jumping,
		// Token: 0x04000492 RID: 1170
		Skidding,
		// Token: 0x04000493 RID: 1171
		BouncedInAir,
		// Token: 0x04000494 RID: 1172
		ChargeAttack,
		// Token: 0x04000495 RID: 1173
		Attacking,
		// Token: 0x04000496 RID: 1174
		AttackRecover,
		// Token: 0x04000497 RID: 1175
		WallSlide,
		// Token: 0x04000498 RID: 1176
		Tongue
	}

	// Token: 0x020000E8 RID: 232
	private enum AttackDirection
	{
		// Token: 0x0400049A RID: 1178
		Forward,
		// Token: 0x0400049B RID: 1179
		Up,
		// Token: 0x0400049C RID: 1180
		DiagonalUp,
		// Token: 0x0400049D RID: 1181
		Down,
		// Token: 0x0400049E RID: 1182
		DownForward
	}
}
