using System;
using FreeLives;
using UnityEngine;
public class Character : MonoBehaviour
{
	// Token: 0x060004B2 RID: 1202 RVA: 0x0002580C File Offset: 0x00023C0C
	internal void TimeBump(float durationM, float scale)
	{
		if (this.TimeBumpActive)
		{
			this.timeBumpTimeScale = Mathf.Min(this.timeBumpTimeScale, scale);
		}
		else
		{
			this.timeBumpTimeScale = scale;
		}
		this.timeBumpTimeLeft = Mathf.Max(this.timeBumpTimeLeft, durationM * 0.175f);
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00025863 File Offset: 0x00023C63
	// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0002585A File Offset: 0x00023C5A
	public Vector2 tongueDir { get; protected set; }

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00025874 File Offset: 0x00023C74
	// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0002586B File Offset: 0x00023C6B
	public float tongueDistance { get; protected set; }

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00025885 File Offset: 0x00023C85
	// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0002587C File Offset: 0x00023C7C
	public TongueState tongueState { get; protected set; }

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060004BA RID: 1210 RVA: 0x00025896 File Offset: 0x00023C96
	// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0002588D File Offset: 0x00023C8D
	public bool wasBouncingBeforeTongue { get; protected set; }

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060004BB RID: 1211 RVA: 0x0002589E File Offset: 0x00023C9E
	// (set) Token: 0x060004BC RID: 1212 RVA: 0x000258A6 File Offset: 0x00023CA6
	public float timeBumpTimeScale { get; protected set; }

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x060004BD RID: 1213 RVA: 0x000258AF File Offset: 0x00023CAF
	public bool TimeBumpActive
	{
		get
		{
			return this.timeBumpTimeLeft > 0f;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x060004BE RID: 1214 RVA: 0x000258BE File Offset: 0x00023CBE
	// (set) Token: 0x060004BF RID: 1215 RVA: 0x000258C6 File Offset: 0x00023CC6
	public float t { get; protected set; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000258CF File Offset: 0x00023CCF
	public Vector3 Center
	{
		get
		{
			return base.transform.position + 1f * Vector3.up;
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000258F0 File Offset: 0x00023CF0
	// (set) Token: 0x060004C2 RID: 1218 RVA: 0x000258F8 File Offset: 0x00023CF8
	public Fly ingestingFly { get; protected set; }

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00025901 File Offset: 0x00023D01
	public bool attackFullyCharged
	{
		get
		{
			return this.attackChargeCounter > this.attackChargeTime && this.attackState == AttackState.Charging;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00025920 File Offset: 0x00023D20
	public float attackChargeM
	{
		get
		{
			return Mathf.Clamp01(this.attackChargeCounter / this.attackChargeTime);
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00025934 File Offset: 0x00023D34
	public bool OnGround
	{
		get
		{
			return this.onGround;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0002593C File Offset: 0x00023D3C
	public Vector2 Velocity
	{
		get
		{
			return this.velocity;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00025944 File Offset: 0x00023D44
	public float HitTime
	{
		get
		{
			return this.timeSinceHit;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0002594C File Offset: 0x00023D4C
	public int FacingDirection
	{
		get
		{
			return this.facingDir;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00025954 File Offset: 0x00023D54
	// (set) Token: 0x060004CA RID: 1226 RVA: 0x0002595C File Offset: 0x00023D5C
	public bool WallSliding { get; protected set; }

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x00025965 File Offset: 0x00023D65
	// (set) Token: 0x060004CC RID: 1228 RVA: 0x0002596D File Offset: 0x00023D6D
	public bool IngestedFly { get; protected set; }

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060004CD RID: 1229 RVA: 0x00025976 File Offset: 0x00023D76
	// (set) Token: 0x060004CE RID: 1230 RVA: 0x0002597E File Offset: 0x00023D7E
	public EffectsController.Side WallSlideSide { get; protected set; }

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060004CF RID: 1231 RVA: 0x00025987 File Offset: 0x00023D87
	public bool RecoveringFromBounce
	{
		get
		{
			return this.hasReachedApex;
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0002598F File Offset: 0x00023D8F
	private void Start()
	{
		this.CheckInput();
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00025998 File Offset: 0x00023D98
	private void Awake()
	{
		this.terrainLayer = 1 << LayerMask.NameToLayer("Ground");
		this.groundLayer = (this.terrainLayer | 1 << LayerMask.NameToLayer("OneWayPlatform"));
		this.characterLayer = 1 << LayerMask.NameToLayer("Character");
		this.tongueLayer = 1 << LayerMask.NameToLayer("Tongue");
		this.flyLayer = 1 << LayerMask.NameToLayer("Fly");
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00025A18 File Offset: 0x00023E18
	private void CheckInput()
	{
		if (this.player != null)
		{
			if (GameController.State == GameState.RoundFinished && !this.IsWinningPlayer())
			{
				InputReader.ClearInputState(this.input);
			}
			else
			{
				InputReader.GetInput(this.player.inputDevice, this.input);
			}
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00025A74 File Offset: 0x00023E74
	public bool IsWinningPlayer()
	{
		if (GameController.isTeamMode)
		{
			return GameController.GetWinningPlayer() != null && this.player.team == GameController.GetWinningPlayer().team;
		}
		return this.player == GameController.GetWinningPlayer();
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00025AC4 File Offset: 0x00023EC4
	private void Update()
	{
		this.CheckInput();
		if (this.TimeBumpActive)
		{
			this.timeBumpTimeLeft -= Time.deltaTime;
			this.t = Time.deltaTime * this.timeBumpTimeScale;
		}
		else
		{
			this.t = Time.deltaTime;
		}
		if (this.state != CharacterState.Bouncing)
		{
			this.AddInputMotionNormal();
		}
		else
		{
			this.AddInputMotionBouncing();
		}
		this.RunPhysics();
		this.ClampMotion();
		this.ApplyMotionVector();
		if (this.state == CharacterState.Attacking)
		{
			this.RunAttack();
		}
		else if (this.state == CharacterState.Tounge)
		{
			this.RunTongue();
		}
		if (this.input.yButton && !this.input.wasYButton && Application.isEditor)
		{
			Vector2 vector = Vector2.zero;
			if (this.input.right)
			{
				vector = Vector2.right;
			}
			if (this.input.left)
			{
				vector = -Vector2.right;
			}
			if (this.input.up)
			{
				vector += Vector2.up;
			}
			if (this.input.down)
			{
				vector -= Vector2.up;
			}
			this.GetHit(vector, UnityEngine.Random.value, this);
		}
		this.CheckDeath();
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00025C1C File Offset: 0x0002401C
	private void CheckDeath()
	{
		if (base.transform.position.x < global::Terrain.LeftKillPoint || base.transform.position.x > global::Terrain.RightKillPoint || base.transform.position.y > global::Terrain.TopKillPoint || base.transform.position.y < global::Terrain.BotKillPoint)
		{
			EffectsController.Side killedOnSide;
			if (base.transform.position.x < global::Terrain.LeftKillPoint)
			{
				killedOnSide = EffectsController.Side.Left;
			}
			else if (base.transform.position.x > global::Terrain.RightKillPoint)
			{
				killedOnSide = EffectsController.Side.Right;
			}
			else if (base.transform.position.y > global::Terrain.TopKillPoint)
			{
				killedOnSide = EffectsController.Side.Top;
			}
			else
			{
				killedOnSide = EffectsController.Side.Bottom;
			}
			GameController.RegisterKill(this.lastHitByPlayer, this.player, this.hitsTaken);
			if (this.lastHitByPlayer == null)
			{
				SoundController.PlaySoundEffect("KnockoutSuicide", 0.45f, base.transform.position);
			}
			else if (this.hitsTaken <= 1)
			{
				SoundController.PlaySoundEffect("Knockout1", 0.55f, base.transform.position);
			}
			else if (this.hitsTaken <= 3)
			{
				SoundController.PlaySoundEffect("Knockout2", 0.65f, base.transform.position);
			}
			else
			{
				SoundController.PlaySoundEffect("Knockout3", 0.75f, base.transform.position);
			}
			if (this.lastHitByPlayer != null)
			{
				EffectsController.CreateSideScorePlum(base.transform.position, killedOnSide, (this.hitsTaken != 0) ? this.hitsTaken : 1, this.lastHitByPlayer.color);
			}
			else
			{
				EffectsController.CreateSideScorePlum(base.transform.position, killedOnSide, -1, this.player.color);
			}
			if (base.transform.position.y > global::Terrain.TopKillPoint)
			{
				EffectsController.CreateKnockedUpEffect(base.GetComponent<CharacterAnimator>());
				if (this.player != null)
				{
					this.player.spawnDelay = 3f;
				}
			}
			if (this.IngestedFly)
			{
				UnityEngine.Object.Destroy(this.ingestingFly.gameObject);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00025E90 File Offset: 0x00024290
	private void RunAttack()
	{
		if (this.attackState == AttackState.Charging)
		{
			this.attackDir = (float)this.facingDir * Vector2.right;
			this.attackChargeCounter += this.t;
			if (this.input.up)
			{
				if (!this.input.left && !this.input.right)
				{
					this.attackDir = Vector2.up;
				}
				else if (this.input.left)
				{
					this.attackDir = Vector2.up + Vector2.left;
				}
				else if (this.input.right)
				{
					this.attackDir = Vector2.up + Vector2.right;
				}
			}
			else if (this.input.down && !this.OnGround)
			{
				if (!this.input.left && !this.input.right)
				{
					this.attackDir = Vector2.down;
				}
				else if (this.input.left)
				{
					this.attackDir = Vector2.down + Vector2.left;
				}
				else if (this.input.right)
				{
					this.attackDir = Vector2.down + Vector2.right;
				}
			}
		}
		if (!this.input.xButton && this.input.wasXButton && this.attackState == AttackState.Charging)
		{
			this.attackState = AttackState.Attacking;
			SoundController.PlaySoundEffect("BatSwing", 0.4f + this.attackChargeM * 0.4f, base.transform.position);
			if (this.attackChargeM > 0.25f || this.IngestedFly)
			{
				SoundController.PlaySoundEffect("BatSwingVoice", 0.4f, base.transform.position);
			}
			this.attackTimeLeft = this.attackTime;
			if (this.attackChargeM > 0.5f)
			{
				if (this.attackDir == Vector2.left || this.attackDir == Vector2.right)
				{
					EffectsController.CreateShingEffect((Vector2) this.Center + this.attackDir * 3f + Vector2.up * 0.2f, this.attackDir);
				}
				else if (this.attackDir == Vector2.up)
				{
					EffectsController.CreateShingEffect((Vector2) this.Center + this.attackDir * 3.75f, this.attackDir);
				}
				else if (this.attackDir == Vector2.down)
				{
					EffectsController.CreateShingEffect((Vector2) this.Center + this.attackDir * 2.75f, this.attackDir);
				}
				else if (this.attackDir.y > 0f)
				{
					EffectsController.CreateShingEffect((Vector2) this.Center + this.attackDir * 2.75f, this.attackDir);
				}
				else
				{
					EffectsController.CreateShingEffect((Vector2) this.Center + this.attackDir * 2.75f, this.attackDir);
				}
			}
		}
		else if (this.attackState == AttackState.Attacking)
		{
			this.attackTimeLeft -= this.t;
			if (this.attackTimeLeft <= 0f)
			{
				Vector2 up = Vector2.up;
				float num = 0f;
				if (this.attackChargeM > 0.5f)
				{
					num = this.attackChargeM;
				}
				float num2 = 1.25f;
				if (this.attackDir.y < 0f)
				{
					num2 = 1.75f;
				}
				RaycastHit2D[] array = Physics2D.CircleCastAll((Vector2) base.transform.position + up, num2, this.attackDir, this.attackRange + num, this.characterLayer);
				Debug.DrawLine((Vector2) base.transform.position + up, (Vector2) base.transform.position + up + this.attackDir.normalized * (this.attackRange + num + num2), Color.red, 1f);
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						Character component = array[i].collider.gameObject.GetComponent<Character>();
						if (component != null && component != this)
						{
							this.Hit(component, this.attackDir);
						}
					}
				}
				this.attackState = AttackState.Recovering;
				this.attackRecoverTimeLeft = this.attackRecoverTime;
			}
		}
		else if (this.attackState == AttackState.Recovering)
		{
			this.attackRecoverTimeLeft -= this.t;
			if (this.attackRecoverTimeLeft < 0f)
			{
				this.attackState = AttackState.Idle;
				this.state = CharacterState.Normal;
			}
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000263EC File Offset: 0x000247EC
	private void BounceFromWall(EffectsController.Side side)
	{
		if (this.timeSinceHit > 0.35f)
		{
			if (!this.hasBounceDodged)
			{
				this.canBounceDodge = true;
			}
			if (!this.hasBounceTongued)
			{
				this.canBounceTongue = true;
			}
		}
		if (((side == EffectsController.Side.Left || side == EffectsController.Side.Right) && Mathf.Abs(this.velocity.x) > 5f) || ((side == EffectsController.Side.Bottom || side == EffectsController.Side.Top) && Mathf.Abs(this.velocity.y) > 5f))
		{
			EffectsController.CreateBouncePuff((Vector2) base.transform.position + this.velocityT, side);
			SoundController.PlaySoundEffect("FrogBounce", 0.5f, base.transform.position);
			SoundController.PlaySoundEffect("FrogBounceVoice", 0.4f, base.transform.position);
		}
		if (this.state == CharacterState.Tounge)
		{
			this.state = CharacterState.Bouncing;
		}
		this.wasHitDownwards = false;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x000264F0 File Offset: 0x000248F0
	private void ClampMotion()
	{
		bool flag = this.OnGround;
		bool wallSliding = this.WallSliding;
		this.onGround = false;
		this.WallSliding = false;
		bool flag2 = false;
		Vector2 vector = (Vector2) base.transform.position - Vector2.right * 2f * 0.49f;
		if (this.velocityT.y < 0f)
		{
			Debug.DrawLine(vector, vector + Vector2.down * this.velocityT.y);
			int layerMask = this.groundLayer;
			if ((this.input.down && this.input.aButton) || (this.state == CharacterState.Bouncing && this.wasHitDownwards))
			{
				layerMask = this.terrainLayer;
			}
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.up, this.velocityT.y, layerMask);
			if (raycastHit2D.collider != null)
			{
				this.onGround = true;
				this.velocityT.y = raycastHit2D.point.y - vector.y;
				if ((this.state == CharacterState.Bouncing && !this.RecoveringFromBounce) || (this.state == CharacterState.Tounge && this.wasBouncingBeforeTongue))
				{
					this.BounceFromWall(EffectsController.Side.Bottom);
					flag2 = true;
					this.velocity.y = this.velocity.y * -0.5f;
				}
				else
				{
					this.velocity.y = 0f;
				}
			}
		}
		Vector2 origin = (Vector2) base.transform.position + Vector2.right * 2f * 0.49f;
		if (this.velocityT.y < 0f)
		{
			int layerMask2 = this.groundLayer;
			if ((this.input.down && this.input.aButton) || (this.state == CharacterState.Bouncing && this.wasHitDownwards))
			{
				layerMask2 = this.terrainLayer;
			}
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.up, this.velocityT.y, layerMask2);
			if (raycastHit2D.collider != null)
			{
				this.onGround = true;
				this.velocityT.y = raycastHit2D.point.y - origin.y;
				if ((this.state == CharacterState.Bouncing && !this.RecoveringFromBounce) || (this.state == CharacterState.Tounge && this.wasBouncingBeforeTongue))
				{
					if (!flag2)
					{
						this.BounceFromWall(EffectsController.Side.Bottom);
						this.velocity.y = this.velocity.y * -0.5f;
					}
				}
				else
				{
					this.velocity.y = 0f;
				}
			}
		}
		Vector2 origin2 = (Vector2) base.transform.position - Vector2.right * 2f * 0.49f + Vector2.up * 2f;
		Vector2 origin3 = (Vector2) base.transform.position + Vector2.right * 2f * 0.49f + Vector2.up * 2f;
		flag2 = false;
		if (this.velocityT.y > 0f)
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin3, Vector2.up, this.velocityT.y, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.y = raycastHit2D.point.y - origin3.y;
				this.jumpGraceTimeLeft = 0f;
				if (this.state == CharacterState.Bouncing)
				{
					this.BounceFromWall(EffectsController.Side.Top);
					flag2 = true;
					this.velocity.y = -this.velocity.y;
				}
				else
				{
					this.velocity.y = 0f;
				}
			}
			raycastHit2D = Physics2D.Raycast(origin2, Vector2.up, this.velocityT.y, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.y = raycastHit2D.point.y - origin2.y;
				this.jumpGraceTimeLeft = 0f;
				if (this.state == CharacterState.Bouncing)
				{
					if (!flag2)
					{
						this.velocity.y = -this.velocity.y;
						this.BounceFromWall(EffectsController.Side.Top);
					}
				}
				else
				{
					this.velocity.y = 0f;
				}
			}
		}
		vector = (Vector2) base.transform.position - Vector2.right * 2f * 0.5f + Vector2.up * 0.05f;
		origin = (Vector2) base.transform.position + Vector2.right * 2f * 0.5f + Vector2.up * 0.05f;
		origin2 = (Vector2) base.transform.position - Vector2.right * 2f * 0.5f + Vector2.up * 2f * 0.98f;
		origin3 = (Vector2) base.transform.position + Vector2.right * 2f * 0.5f + Vector2.up * 2f * 0.98f;
		flag2 = false;
		if (this.velocityT.x > 0f)
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.right, this.velocityT.x, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.x = raycastHit2D.point.x - origin.x;
				if (this.state == CharacterState.Bouncing)
				{
					flag2 = true;
					this.velocity.x = -this.velocity.x;
					this.BounceFromWall(EffectsController.Side.Right);
				}
				else
				{
					this.velocity.x = 0f;
					if (this.input.right && !this.OnGround && this.velocity.y < 0f)
					{
						this.WallSliding = true;
						this.WallSlideSide = EffectsController.Side.Right;
					}
				}
			}
			raycastHit2D = Physics2D.Raycast(origin3, Vector2.right, this.velocityT.x, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.x = raycastHit2D.point.x - origin.x;
				if (this.state == CharacterState.Bouncing)
				{
					if (!flag2)
					{
						this.velocity.x = -this.velocity.x;
						this.BounceFromWall(EffectsController.Side.Right);
					}
				}
				else
				{
					this.velocity.x = 0f;
				}
			}
		}
		if (this.velocityT.x < 0f)
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, Vector2.right, this.velocityT.x, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.x = raycastHit2D.point.x - vector.x;
				if (this.state == CharacterState.Bouncing)
				{
					this.velocity.x = -this.velocity.x;
					this.BounceFromWall(EffectsController.Side.Left);
					flag2 = true;
				}
				else
				{
					this.velocity.x = 0f;
					if (this.input.left && !this.OnGround && this.velocity.y < 0f)
					{
						this.WallSliding = true;
						this.WallSlideSide = EffectsController.Side.Left;
					}
				}
			}
		}
		if (this.velocityT.x < 0f)
		{
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin2, Vector2.right, this.velocityT.x, this.terrainLayer);
			if (raycastHit2D.collider != null)
			{
				this.velocityT.x = raycastHit2D.point.x - vector.x;
				if (this.state == CharacterState.Bouncing)
				{
					if (!flag2)
					{
						this.velocity.x = -this.velocity.x;
						this.BounceFromWall(EffectsController.Side.Left);
					}
				}
				else
				{
					this.velocity.x = 0f;
				}
			}
		}
		if (this.OnGround && !flag)
		{
			SoundController.PlaySoundEffect("Land", 0.4f, base.transform.position);
			this.jumpCooldownLeft = 0.1f;
		}
		if (this.WallSliding && !wallSliding)
		{
			SoundController.PlaySoundEffect("Land", 0.4f, base.transform.position);
			this.jumpCooldownLeft = 0.1f;
		}
		if (this.onGround)
		{
			this.jumpGraceTimeLeft = this.jumpGraceTime;
		}
		else if (this.WallSliding)
		{
			this.jumpGraceTimeLeft = this.jumpGraceTime * 0.66f;
		}
		else
		{
			this.jumpGraceTimeLeft -= this.t;
			if (this.velocity.y <= this.gravityGraceThreshold)
			{
				this.gravityGraceTimeLeft -= this.t;
				if (this.gravityGraceTimeLeft < 0f)
				{
					this.gravityGraceTimeLeft = 0f;
				}
			}
		}
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00026EF4 File Offset: 0x000252F4
	private void Hit(Character hitChar, Vector2 hitDir)
	{
		float num = 0f;
		if (this.IngestedFly)
		{
			num = 1.5f;
		}
		if (GameController.isTeamMode)
		{
			if (this.player.team != hitChar.player.team)
			{
				hitChar.GetHit(hitDir, this.attackChargeM + num, this);
			}
		}
		else
		{
			hitChar.GetHit(hitDir, this.attackChargeM + num, this);
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00026F64 File Offset: 0x00025364
	public void GetTongueHit(Vector2 hitDir, Character attacker)
	{
		if (GameController.isTeamMode && this.player.team == attacker.player.team)
		{
			return;
		}
		if (hitDir.y < -0.1f)
		{
			this.wasHitDownwards = true;
		}
		this.hasReachedApex = false;
		this.lastHitByPlayer = attacker.player;
		this.canBounceDodge = false;
		this.hasBounceDodged = false;
		this.canBounceTongue = false;
		this.hasBounceTongued = false;
		this.state = CharacterState.Bouncing;
		this.attackState = AttackState.Idle;
		if (hitDir.y == 0f)
		{
			hitDir.y = 0.1f;
		}
		hitDir.Normalize();
		float d = 25f;
		this.skidRecoverTimeLeft = 0.5f;
		this.velocity = hitDir.normalized * d;
		this.timeSinceHit = 0f;
		SoundController.PlaySoundEffect("TongueCollide", 0.5f, base.transform.position);
		this.TimeBump(0.75f, 0f);
		attacker.TimeBump(0.5f, 0f);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0002707C File Offset: 0x0002547C
	public void GetHitByBouncingCharacter(Vector2 hitVelocity, Character bouncer, Player attackingPlayer)
	{
		this.hitsTaken++;
		if (hitVelocity.y < -0.1f)
		{
			this.wasHitDownwards = true;
		}
		this.hasReachedApex = false;
		this.lastHitByPlayer = attackingPlayer;
		this.canBounceDodge = false;
		this.hasBounceDodged = false;
		this.canBounceTongue = false;
		this.hasBounceTongued = false;
		this.state = CharacterState.Bouncing;
		this.attackState = AttackState.Idle;
		if (hitVelocity.y == 0f)
		{
			hitVelocity.y = 0.33f;
		}
		this.skidRecoverTimeLeft = 0.5f;
		this.velocity = hitVelocity;
		this.timeSinceHit = 0f;
		this.TimeBump((float)bouncer.hitsTaken, 0f);
		bouncer.TimeBump((float)bouncer.hitsTaken, 0f);
		SoundController.PlaySoundEffect("CharacterCollision", 0.5f, base.transform.position);
		TimeController.TimeBumpCharacters(base.transform.position, (float)bouncer.hitsTaken, 15f, true);
		EffectsController.CreateLocalizedShake(base.transform.position + Vector3.up * 2f * 0.5f, this.velocity, this.velocity.magnitude, this.timeBumpTimeLeft);
		EffectsController.CreateHitEffect((this.Center + bouncer.Center) * 0.5f, this.timeBumpTimeLeft, false);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000271F8 File Offset: 0x000255F8
	public void GetHit(Vector2 hitDir, float power, Character attacker)
	{
		this.hitsTaken++;
		MonoBehaviour.print("hit power: " + power);
		if (this.IngestedFly)
		{
			this.IngestedFly = false;
			this.ingestingFly.transform.position = this.Center;
			this.ingestingFly.BeingIngested = false;
			this.ingestingFly.gameObject.SetActive(true);
		}
		if (hitDir.y < -0.1f)
		{
			this.wasHitDownwards = true;
		}
		this.hasReachedApex = false;
		this.lastHitByPlayer = attacker.player;
		this.canBounceDodge = false;
		this.hasBounceDodged = false;
		this.canBounceTongue = false;
		this.hasBounceTongued = false;
		this.state = CharacterState.Bouncing;
		this.attackState = AttackState.Idle;
		if (hitDir.y == 0f)
		{
			hitDir.y = 0.33f;
		}
		hitDir.Normalize();
		float d = 10f + (float)this.hitsTaken * 10f + power * 30f;
		this.skidRecoverTimeLeft = 0.5f;
		this.velocity = hitDir.normalized * d;
		this.timeSinceHit = 0f;
		this.TimeBump((float)this.hitsTaken + power, 0f);
		attacker.TimeBump((float)this.hitsTaken + power, 0f);
		SoundController.PlaySoundEffect("BatHit" + Mathf.Clamp(this.hitsTaken, 1, 5).ToString(), 0.5f, base.transform.position);
		SoundController.PlaySoundEffect("BatHitVoice" + Mathf.Clamp(this.hitsTaken, 1, 5).ToString(), 0.5f, base.transform.position);
		TimeController.TimeBumpCharacters(base.transform.position, (float)this.hitsTaken + power, 15f, true);
		EffectsController.CreateLocalizedShake(base.transform.position + Vector3.up * 2f * 0.5f, this.velocity, this.velocity.magnitude, this.timeBumpTimeLeft);
		EffectsController.CreateHitEffect(base.transform.position + Vector3.up * 2f * 0.5f, this.timeBumpTimeLeft, power >= 1f);
		if (power >= 1f)
		{
			EffectsController.ShakeCamera(hitDir, (float)this.hitsTaken * 0.75f);
		}
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00027498 File Offset: 0x00025898
	private void ApplyMotionVector()
	{
		Vector3 vector = base.transform.position;
		vector += (Vector3) this.velocityT;
		if (this.state == CharacterState.Attacking)
		{
			vector.z = -0.1f;
		}
		else
		{
			vector.z = 0f;
		}
		base.transform.position = vector;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x000274F8 File Offset: 0x000258F8
	private void RunPhysicsBouncing()
	{
		this.timeSinceHit += this.t;
		float num = this.bounceGravityMin;
		if (this.timeSinceHit > this.bounceGravityRestoreDelay)
		{
			this.bounceGravityRestoreCounter += this.t;
			num = Mathf.Lerp(this.bounceGravityMin, this.bounceGravityMax, Mathf.Clamp01(this.bounceGravityRestoreCounter / this.bounceGravityRestoreTime));
		}
		if (this.velocity.y >= 0f && this.velocity.y - num * this.t < 0f)
		{
			this.hasReachedApex = true;
			if (this.hasReachedApex && !this.hasBounceDodged)
			{
				this.canBounceDodge = true;
			}
			if (!this.hasBounceTongued)
			{
				this.canBounceTongue = true;
			}
		}
		if (this.timeSinceHit > 1f && !this.hasBounceDodged)
		{
			this.canBounceDodge = true;
		}
		if (this.timeSinceHit > 1f && !this.hasBounceTongued)
		{
			this.canBounceTongue = true;
		}
		if (this.velocity.y > this.maxFallSpeed)
		{
			this.velocity.y = this.velocity.y - num * this.t;
		}
		if (GameController.charactersBounceEachOther && !GameController.isTeamMode && !this.hasBounceDodged && this.hitsTaken >= 1 && !this.OnGround)
		{
			Collider2D[] array = Physics2D.OverlapCircleAll((Vector2) base.transform.position + Vector2.up * 2f * 0.5f, 0.5f, this.characterLayer);
			if (array.Length > 0)
			{
				foreach (Collider2D collider2D in array)
				{
					Character component = collider2D.GetComponent<Character>();
					if (component != null && component != this && component.state != CharacterState.Bouncing && component.player != this.lastHitByPlayer && (component.state != CharacterState.Attacking || component.attackState != AttackState.Attacking) && (!this.RecoveringFromBounce || !GameController.onlyBounceBeforeRecover) && (!this.TimeBumpActive || this.timeBumpTimeScale != 0f))
					{
						component.GetHitByBouncingCharacter(this.velocity * 0.75f, this, this.lastHitByPlayer);
						if (GameController.weirdBounceTrajectories)
						{
							Vector3 vector = base.transform.position - component.transform.position;
							this.velocity = this.velocity.magnitude * vector.normalized * 0.75f;
						}
					}
				}
			}
		}
		if (this.onGround && this.RecoveringFromBounce)
		{
			if (this.state == CharacterState.Tounge)
			{
				this.state = CharacterState.Bouncing;
			}
			if (this.velocity.x > 0f)
			{
				this.velocity.x = this.velocity.x - this.t * this.skidAccel;
				if (this.velocity.x < 0f)
				{
					this.velocity.x = 0f;
				}
			}
			else if (this.velocity.x < 0f)
			{
				this.velocity.x = this.velocity.x + this.t * this.skidAccel;
				if (this.velocity.x > 0f)
				{
					this.velocity.x = 0f;
				}
			}
			if (Mathf.Abs(this.velocity.x) < this.maxRunSpeed)
			{
				this.skidRecoverTimeLeft -= this.t;
				if (this.skidRecoverTimeLeft <= 0f)
				{
					this.StopBouncing();
				}
			}
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00027904 File Offset: 0x00025D04
	private void RunPhysicsNormal()
	{
		if (this.input.aButton && this.velocity.y <= this.gravityGraceThreshold && this.gravityGraceTimeLeft > 0f)
		{
			float num = 1f - this.gravityGraceTimeLeft / this.gravityGraceTime;
			this.velocity.y = this.velocity.y - this.gravity * num * this.t;
		}
		else
		{
			this.velocity.y = this.velocity.y - this.gravity * this.t;
		}
		if (this.WallSliding)
		{
			if (this.velocity.y < this.maxFallSpeedWallSlide)
			{
				this.velocity.y = this.maxFallSpeedWallSlide;
			}
		}
		else if (this.velocity.y < this.maxFallSpeed)
		{
			this.velocity.y = this.maxFallSpeed;
		}
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00027A00 File Offset: 0x00025E00
	private void RunPhysics()
	{
		if (this.state == CharacterState.Bouncing)
		{
			this.RunPhysicsBouncing();
		}
		else if (this.state == CharacterState.Tounge && this.tongueState == TongueState.AttachedToTerrain)
		{
			this.velocity = this.tongueDir * this.tongueRetractSpeedLatched;
		}
		else if (this.state == CharacterState.Tounge)
		{
			if (this.wasBouncingBeforeTongue)
			{
				this.RunPhysicsBouncing();
			}
			else
			{
				this.RunPhysicsNormal();
			}
		}
		else
		{
			this.RunPhysicsNormal();
		}
		this.velocityT = this.velocity * this.t;
		if (this.state != CharacterState.Attacking && this.state != CharacterState.Tounge)
		{
			if (this.velocity.x > 0f)
			{
				this.facingDir = 1;
			}
			if (this.velocity.x < 0f)
			{
				this.facingDir = -1;
			}
		}
		this.jumpCooldownLeft -= this.t;
		if (this.state == CharacterState.Tounge)
		{
			if (this.tongueDir.x > 0f)
			{
				this.facingDir = 1;
			}
			else if (this.tongueDir.x < 0f)
			{
				this.facingDir = -1;
			}
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00027B54 File Offset: 0x00025F54
	private void AddInputMotionBouncing()
	{
		if (this.input.right)
		{
			if (this.velocity.x < this.maxRunSpeed * 0.5f)
			{
				this.velocity.x = this.velocity.x + this.bounceAccel * this.t;
			}
		}
		else if (this.input.left && this.velocity.x > -this.maxRunSpeed * 0.5f)
		{
			this.velocity.x = this.velocity.x - this.bounceAccel * this.t;
		}
		if (this.canBounceDodge && this.input.aButton && !this.OnGround)
		{
			this.canBounceDodge = false;
			this.hasBounceDodged = true;
			Vector2 vector = Vector2.up;
			if (this.input.up)
			{
				vector += Vector2.up;
			}
			if (this.input.down)
			{
				vector += Vector2.down;
			}
			if (this.input.left)
			{
				vector += Vector2.left;
			}
			if (this.input.right)
			{
				vector += Vector2.right;
			}
			if (vector == Vector2.zero)
			{
				vector = Vector2.up;
			}
			vector.Normalize();
			this.velocity = vector * this.bounceDodgePower;
			this.bounceGravityRestoreCounter = 0f;
		}
		if (this.input.bButton && !this.input.wasBButton)
		{
			this.StartTongueAttack();
		}
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00027D07 File Offset: 0x00026107
	private void StopBouncing()
	{
		this.state = CharacterState.Normal;
		this.wasHitDownwards = false;
		this.hitsTaken = 0;
		this.lastHitByPlayer = null;
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00027D28 File Offset: 0x00026128
	private void AddInputMotionNormal()
	{
		if (this.input.right && this.state == CharacterState.Normal)
		{
			this.facingDir = 1;
			if (this.onGround)
			{
				if (this.velocity.x < 0f)
				{
					if (this.velocity.x < this.maxRunSpeed * 0.9f)
					{
						EffectsController.CreateTurnAroundPuff(base.transform.position, 1f);
					}
					this.velocity.x = 0f;
				}
				this.velocity.x = this.velocity.x + this.t * this.runAccel;
			}
			else
			{
				this.velocity.x = this.velocity.x + this.t * this.airAccel;
			}
			if (this.velocity.x > this.maxRunSpeed)
			{
				this.velocity.x = this.maxRunSpeed;
			}
		}
		else if (this.input.left && this.state == CharacterState.Normal)
		{
			this.facingDir = -1;
			if (this.onGround)
			{
				if (this.velocity.x > 0f)
				{
					if (this.velocity.x > -this.maxRunSpeed * 0.9f)
					{
						EffectsController.CreateTurnAroundPuff(base.transform.position, -1f);
					}
					this.velocity.x = 0f;
				}
				this.velocity.x = this.velocity.x - this.t * this.runAccel;
			}
			else
			{
				this.velocity.x = this.velocity.x - this.t * this.airAccel;
			}
			if (this.velocity.x < -this.maxRunSpeed)
			{
				this.velocity.x = -this.maxRunSpeed;
			}
		}
		else if (this.onGround)
		{
			if (this.velocity.x > 0f)
			{
				this.velocity.x = this.velocity.x - this.t * this.runAccel;
				if (this.velocity.x < 0f)
				{
					this.velocity.x = 0f;
				}
			}
			else if (this.velocity.x < 0f)
			{
				this.velocity.x = this.velocity.x + this.t * this.runAccel;
				if (this.velocity.x > 0f)
				{
					this.velocity.x = 0f;
				}
			}
		}
		if (this.input.xButton)
		{
			if (this.state == CharacterState.Normal)
			{
				this.state = CharacterState.Attacking;
				if (this.attackState == AttackState.Idle)
				{
					this.attackState = AttackState.Charging;
					SoundController.PlaySoundEffect("BatChargeUp", 0.5f, base.transform.position);
					this.attackChargeCounter = 0f;
				}
			}
			if (this.attackState == AttackState.Charging)
			{
				if (this.input.right)
				{
					this.facingDir = 1;
				}
				else if (this.input.left)
				{
					this.facingDir = -1;
				}
			}
		}
		if (this.input.bButton && this.state == CharacterState.Normal && !this.input.wasBButton)
		{
			this.StartTongueAttack();
		}
		if (this.input.aButton && !this.input.down && (this.jumpCooldownLeft <= 0f || (!this.input.wasAButton && this.state != CharacterState.Attacking)))
		{
			if (this.onGround || this.WallSliding)
			{
				this.velocity.y = this.jumpVel;
				this.gravityGraceTimeLeft = this.gravityGraceTime;
				if (this.WallSliding)
				{
					if (this.WallSlideSide == EffectsController.Side.Left)
					{
						this.velocity.x = this.maxRunSpeed;
					}
					else if (this.WallSlideSide == EffectsController.Side.Right)
					{
						this.velocity.x = -this.maxRunSpeed;
					}
				}
				SoundController.PlaySoundEffect("Jump", 0.4f, base.transform.position);
				if (this.WallSliding)
				{
					EffectsController.CreateJumpPuffStraight(base.transform.position, this.WallSlideSide);
				}
				else
				{
					EffectsController.CreateJumpPuffStraight(base.transform.position, EffectsController.Side.Bottom);
				}
			}
			else if (this.jumpGraceTimeLeft > 0f)
			{
				this.velocity.y = this.jumpVel;
			}
		}
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x000281F4 File Offset: 0x000265F4
	private void RunTongue()
	{
		if (this.tongueDelayLeft > 0f)
		{
			this.tongueDelayLeft -= this.t;
			return;
		}
		if (this.tongueState == TongueState.Extending)
		{
			this.tongueDistance += this.tongueSpeed * this.t;
			if (this.tongueDir.x != 0f && Mathf.Sign(this.tongueDir.x) != Mathf.Sign(this.velocity.x))
			{
				this.tongueDistance += Mathf.Abs(this.velocity.x) * this.t;
			}
			if (this.tongueDir.y != 0f && Mathf.Sign(this.tongueDir.y) != Mathf.Sign(this.velocity.y))
			{
				this.tongueDistance += Mathf.Abs(this.velocity.y) * this.t;
			}
			int layerMask = this.terrainLayer;
			if (this.tongueDir.y < 0f)
			{
				layerMask = this.groundLayer;
			}
			Collider2D collider2D = Physics2D.OverlapCircle((Vector2) base.transform.position + this.tongueOrigin + this.tongueDir * this.tongueDistance, 0.5f, this.flyLayer);
			if (collider2D != null)
			{
				if (!collider2D.GetComponent<Fly>().BeingIngested)
				{
					this.tongueState = TongueState.RetractingHitFly;
					this.ingestingFly = collider2D.GetComponent<Fly>();
					this.ingestingFly.BeingIngested = true;
					SoundController.PlaySoundEffect("TongueCollideSurface", 0.5f, this.TongueTipPos);
				}
			}
			else if (Physics2D.OverlapCircle((Vector2) base.transform.position + this.tongueOrigin + this.tongueDir * this.tongueDistance, 0.5f, layerMask) != null)
			{
				if (this.tongueDistance > this.minimumTongueDistance)
				{
					this.tongueState = TongueState.AttachedToTerrain;
					SoundController.PlaySoundEffect("TongueCollideSurface", 0.5f, this.TongueTipPos);
				}
			}
			else if (this.tongueDistance > this.minimumTongueDistance)
			{
				bool flag = false;
				Collider2D[] array = Physics2D.OverlapCircleAll((Vector2) base.transform.position + this.tongueOrigin + this.tongueDir * this.tongueDistance, 1f, this.tongueLayer);
				if (array.Length > 0)
				{
					foreach (Collider2D collider2D2 in array)
					{
						Character componentInParent = collider2D2.GetComponentInParent<Character>();
						if (componentInParent != null && componentInParent != this)
						{
							this.tongueState = TongueState.RetractingHitEnemyTongue;
							EffectsController.CreateTongueHitEffect(this.TongueTipPos, 0.2f);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					array = Physics2D.OverlapCircleAll((Vector2) base.transform.position + this.tongueOrigin + this.tongueDir * this.tongueDistance, 1f, this.characterLayer);
					if (array.Length > 0)
					{
						foreach (Collider2D collider2D3 in array)
						{
							Character component = collider2D3.GetComponent<Character>();
							if (component != null && component != this && (!GameController.isTeamMode || component.player.team != this.player.team))
							{
								component.GetTongueHit(-this.tongueDir, this);
								this.tongueState = TongueState.RetractingHitEnemy;
								EffectsController.CreateTongueHitEffect(this.TongueTipPos, 0.2f);
							}
						}
					}
				}
			}
			if (this.tongueState == TongueState.Extending && this.tongueDistance > this.tongueRange)
			{
				this.tongueState = TongueState.Retracting;
			}
		}
		else if (this.tongueState == TongueState.AttachedToTerrain)
		{
			this.tongueDistance -= this.tongueRetractSpeedLatched * this.t;
			if (this.tongueDistance <= 0f)
			{
				if (this.wasBouncingBeforeTongue)
				{
					this.bounceGravityRestoreCounter = 0f;
					this.state = CharacterState.Bouncing;
				}
				else
				{
					this.state = CharacterState.Normal;
					this.jumpGraceTimeLeft = this.jumpGraceTime;
				}
			}
		}
		else if (this.tongueState == TongueState.Retracting)
		{
			this.tongueDistance -= this.tongueRetractSpeedMissed * this.t;
			if (this.tongueDistance <= 0f)
			{
				if (this.wasBouncingBeforeTongue)
				{
					this.state = CharacterState.Bouncing;
				}
				else
				{
					this.state = CharacterState.Normal;
				}
			}
		}
		else if (this.tongueState == TongueState.RetractingHitEnemy)
		{
			this.tongueDistance -= this.tongueSpeed * this.t;
			if (this.tongueDistance <= 0f)
			{
				if (this.wasBouncingBeforeTongue)
				{
					this.state = CharacterState.Bouncing;
				}
				else
				{
					this.state = CharacterState.Normal;
				}
			}
		}
		else if (this.tongueState == TongueState.RetractingHitEnemyTongue)
		{
			this.tongueDistance -= this.tongueRetractSpeedMissed * this.t;
			if (this.tongueDistance <= 0f)
			{
				if (this.wasBouncingBeforeTongue)
				{
					this.state = CharacterState.Bouncing;
				}
				else
				{
					this.tongueState = TongueState.HitEnemyTongueStunned;
					this.tongueDelayLeft = 0.65f;
				}
			}
		}
		else if (this.tongueState == TongueState.RetractingHitFly)
		{
			this.tongueDistance -= this.tongueRetractSpeedMissed * this.t;
			this.ingestingFly.transform.position = this.TongueTipPos;
			if (this.tongueDistance <= 0f)
			{
				if (this.wasBouncingBeforeTongue)
				{
					this.StopBouncing();
				}
				SoundController.PlaySoundEffect("Burp", 0.5f, this.TongueTipPos);
				this.tongueState = TongueState.HitFlyBurping;
				this.tongueDelayLeft = 0.65f;
				this.IngestedFly = true;
				this.ingestingFly.gameObject.SetActive(false);
			}
		}
		else if (this.tongueState == TongueState.HitEnemyTongueStunned || this.tongueState == TongueState.HitFlyBurping)
		{
			this.state = CharacterState.Normal;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0002887F File Offset: 0x00026C7F
	private Vector2 TongueTipPos
	{
		get
		{
			return (Vector2) base.transform.position + this.tongueOrigin + this.tongueDir * this.tongueDistance;
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x000288B4 File Offset: 0x00026CB4
	private void StartTongueAttack()
	{
		if (this.state == CharacterState.Bouncing)
		{
			if (!this.canBounceTongue)
			{
				return;
			}
			this.wasBouncingBeforeTongue = true;
			this.canBounceTongue = false;
			this.hasBounceDodged = true;
		}
		else
		{
			this.wasBouncingBeforeTongue = false;
		}
		this.state = CharacterState.Tounge;
		this.tongueDistance = 0f;
		this.tongueState = TongueState.Extending;
		this.tongueDelayLeft = this.tongueDelay;
		SoundController.PlaySoundEffect("TongueLaunch", 0.5f, base.transform.position);
		this.tongueDir = (float)this.facingDir * Vector2.right;
		if (this.input.right)
		{
			this.tongueDir = Vector2.right;
		}
		else if (this.input.left)
		{
			this.tongueDir = Vector2.left;
		}
		if (this.input.up)
		{
			if (!this.input.left && !this.input.right)
			{
				this.tongueDir = Vector2.up;
			}
			else if (this.input.left)
			{
				this.tongueDir = Vector2.up + Vector2.left;
			}
			else if (this.input.right)
			{
				this.tongueDir = Vector2.up + Vector2.right;
			}
		}
		else if (this.input.down && !this.OnGround)
		{
			if (!this.input.left && !this.input.right)
			{
				this.tongueDir = Vector2.down;
			}
			else if (this.input.left)
			{
				this.tongueDir = Vector2.down + Vector2.left;
			}
			else if (this.input.right)
			{
				this.tongueDir = Vector2.down + Vector2.right;
			}
		}
		this.tongueDir = this.tongueDir.normalized;
	}

	// Token: 0x04000400 RID: 1024
	[HideInInspector]
	public InputState input = new InputState();

	// Token: 0x04000401 RID: 1025
	[HideInInspector]
	public Player player;

	// Token: 0x04000402 RID: 1026
	[HideInInspector]
	public Player lastHitByPlayer;

	// Token: 0x04000403 RID: 1027
	public CharacterState state;

	// Token: 0x04000404 RID: 1028
	public AttackState attackState;

	// Token: 0x04000405 RID: 1029
	public static bool isTeamMode;

	// Token: 0x04000406 RID: 1030
	private int facingDir = 1;

	// Token: 0x04000407 RID: 1031
	[Header("Standard Motion")]
	public float maxRunSpeed;

	// Token: 0x04000408 RID: 1032
	public float runAccel;

	// Token: 0x04000409 RID: 1033
	public float airAccel;

	// Token: 0x0400040A RID: 1034
	public float skidAccel;

	// Token: 0x0400040B RID: 1035
	public float maxFallSpeed;

	// Token: 0x0400040C RID: 1036
	public float maxFallSpeedWallSlide;

	// Token: 0x0400040D RID: 1037
	public float jumpVel;

	// Token: 0x0400040E RID: 1038
	public float gravity;

	// Token: 0x0400040F RID: 1039
	public float jumpGraceTime;

	// Token: 0x04000410 RID: 1040
	public float gravityGraceTime;

	// Token: 0x04000411 RID: 1041
	public float graceGravityM;

	// Token: 0x04000412 RID: 1042
	public float gravityGraceThreshold;

	// Token: 0x04000413 RID: 1043
	[Header("Bounce Motion")]
	public float bounceAccel;

	// Token: 0x04000414 RID: 1044
	public float bounceGravityMin;

	// Token: 0x04000415 RID: 1045
	public float bounceGravityMax;

	// Token: 0x04000416 RID: 1046
	public float bounceGravityRestoreDelay;

	// Token: 0x04000417 RID: 1047
	public float bounceGravityRestoreTime;

	// Token: 0x04000418 RID: 1048
	public float bounceDodgePower;

	// Token: 0x04000419 RID: 1049
	[HideInInspector]
	public bool canBounceDodge;

	// Token: 0x0400041A RID: 1050
	[HideInInspector]
	public bool hasBounceDodged;

	// Token: 0x0400041B RID: 1051
	private bool canBounceTongue;

	// Token: 0x0400041C RID: 1052
	private bool hasBounceTongued;

	// Token: 0x0400041D RID: 1053
	private bool hasReachedApex;

	// Token: 0x0400041E RID: 1054
	[HideInInspector]
	public bool wasHitDownwards;

	// Token: 0x0400041F RID: 1055
	[HideInInspector]
	public float bounceGravityRestoreCounter;

	// Token: 0x04000420 RID: 1056
	[Header("Attack")]
	public float attackChargeTime;

	// Token: 0x04000421 RID: 1057
	private float attackChargeCounter;

	// Token: 0x04000422 RID: 1058
	[HideInInspector]
	public float gravityGraceTimeLeft;

	// Token: 0x04000423 RID: 1059
	public float attackTime;

	// Token: 0x04000424 RID: 1060
	public float attackRecoverTime;

	// Token: 0x04000425 RID: 1061
	public float attackRange;

	// Token: 0x04000426 RID: 1062
	private float jumpGraceTimeLeft;

	// Token: 0x04000427 RID: 1063
	private const float width = 2f;

	// Token: 0x04000428 RID: 1064
	private const float height = 2f;

	// Token: 0x04000429 RID: 1065
	[Header("Tongue")]
	public float tongueRange;

	// Token: 0x0400042A RID: 1066
	public float tongueSpeed;

	// Token: 0x0400042B RID: 1067
	public float tongueRetractSpeedLatched;

	// Token: 0x0400042C RID: 1068
	public float tongueRetractSpeedMissed;

	// Token: 0x0400042D RID: 1069
	public Vector2 tongueOrigin;

	// Token: 0x0400042E RID: 1070
	public float tongueDelay;

	// Token: 0x0400042F RID: 1071
	public float minimumTongueDistance;

	// Token: 0x04000430 RID: 1072
	private float tongueDelayLeft;

	// Token: 0x04000435 RID: 1077
	private float timeBumpTimeLeft;

	// Token: 0x04000438 RID: 1080
	private float skidRecoverTimeLeft;

	// Token: 0x04000439 RID: 1081
	[HideInInspector]
	public Vector2 velocity;

	// Token: 0x0400043A RID: 1082
	[HideInInspector]
	public Vector2 velocityT;

	// Token: 0x0400043B RID: 1083
	[HideInInspector]
	public int hitsTaken;

	// Token: 0x0400043D RID: 1085
	[HideInInspector]
	public float attackTimeLeft;

	// Token: 0x0400043E RID: 1086
	[HideInInspector]
	public float attackRecoverTimeLeft;

	// Token: 0x04000442 RID: 1090
	private bool onGround;

	// Token: 0x04000443 RID: 1091
	private float timeSinceHit;

	// Token: 0x04000444 RID: 1092
	private int terrainLayer;

	// Token: 0x04000445 RID: 1093
	private int characterLayer;

	// Token: 0x04000446 RID: 1094
	private int groundLayer;

	// Token: 0x04000447 RID: 1095
	private int tongueLayer;

	// Token: 0x04000448 RID: 1096
	private int flyLayer;

	// Token: 0x04000449 RID: 1097
	public Vector2 attackDir;

	// Token: 0x0400044A RID: 1098
	private float jumpCooldownLeft;
}
