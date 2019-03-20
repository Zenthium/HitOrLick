using System;
using UnityEngine;
public class Fly : MonoBehaviour
{
	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000458
	private void Start()
	{
		this.terrainLayer = 1 << LayerMask.NameToLayer("Ground");
		this.velocity = UnityEngine.Random.insideUnitCircle.normalized * 10f;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002098 File Offset: 0x00000498
	private void Update()
	{
		this.updateDirectionDelay -= Time.deltaTime;
		if (this.updateDirectionDelay < 0f)
		{
			this.updateDirectionDelay = UnityEngine.Random.Range(3f, 10f);
			this.UpdateDirection();
		}
		if (!this.BeingIngested)
		{
			this.RunMotion();
		}
		if (base.transform.position.x < global::Terrain.LeftKillPoint || base.transform.position.x > global::Terrain.RightKillPoint || base.transform.position.y > global::Terrain.TopKillPoint || base.transform.position.y < global::Terrain.BotKillPoint)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002174 File Offset: 0x00000574
	private void RunMotion()
	{
		this.velocity = Vector2.MoveTowards(this.velocity, this.targetVelocity, 5f * Time.deltaTime);
		Vector2 v = this.velocity * Time.deltaTime + Vector2.up * Mathf.Sin(Time.time * 8f) * 3f * Time.deltaTime;
		if (v.x < 0f && Physics2D.Raycast(base.transform.position, Vector2.left, Mathf.Abs(v.x) + 1f, this.terrainLayer))
		{
			v.x = 0f;
			this.velocity.x = this.velocity.x * -0.5f;
			this.UpdateDirection();
		}
		if (v.x > 0f && Physics2D.Raycast(base.transform.position, Vector2.right, Mathf.Abs(v.x) + 1f, this.terrainLayer))
		{
			v.x = 0f;
			this.velocity.x = this.velocity.x * -0.5f;
			this.UpdateDirection();
		}
		if (v.y < 0f && Physics2D.Raycast(base.transform.position, Vector2.down, Mathf.Abs(v.y) + 1f, this.terrainLayer))
		{
			v.y = 0f;
			this.velocity.y = this.velocity.y * -0.5f;
			this.UpdateDirection();
		}
		if (v.y > 0f && Physics2D.Raycast(base.transform.position, Vector2.up, Mathf.Abs(v.y) + 1f, this.terrainLayer))
		{
			v.y = 0f;
			this.velocity.y = this.velocity.y * -0.5f;
			this.UpdateDirection();
		}
		if (v.x < 0f)
		{
			this.sprite.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.sprite.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		base.transform.position += (Vector3) v;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000243C File Offset: 0x0000083C
	private void UpdateDirection()
	{
		if (UnityEngine.Random.value < 0.1f)
		{
			this.targetVelocity = Vector2.zero;
			this.updateDirectionDelay = UnityEngine.Random.Range(1f, 3f);
		}
		else
		{
			this.targetVelocity = UnityEngine.Random.insideUnitCircle.normalized * this.maxSpeed;
		}
	}

	// Token: 0x04000001 RID: 1
	private Vector2 velocity;

	// Token: 0x04000002 RID: 2
	private Vector2 targetVelocity;

	// Token: 0x04000003 RID: 3
	private int terrainLayer;

	// Token: 0x04000004 RID: 4
	public SpriteRenderer sprite;

	// Token: 0x04000005 RID: 5
	public float maxSpeed;

	// Token: 0x04000006 RID: 6
	internal bool BeingIngested;

	// Token: 0x04000007 RID: 7
	private float updateDirectionDelay;
}
