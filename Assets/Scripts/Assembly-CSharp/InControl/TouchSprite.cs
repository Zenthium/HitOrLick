using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class TouchSprite
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000B6A4 File Offset: 0x00009AA4
		public TouchSprite()
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B714 File Offset: 0x00009B14
		public TouchSprite(float size)
		{
			this.size = Vector2.one * size;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B792 File Offset: 0x00009B92
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000B79A File Offset: 0x00009B9A
		public bool Dirty { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B7A3 File Offset: 0x00009BA3
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000B7AB File Offset: 0x00009BAB
		public bool Ready { get; set; }

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B7B4 File Offset: 0x00009BB4
		public void Create(string gameObjectName, Transform parentTransform, int sortingOrder)
		{
			this.spriteGameObject = this.CreateSpriteGameObject(gameObjectName, parentTransform);
			this.spriteRenderer = this.CreateSpriteRenderer(this.spriteGameObject, this.idleSprite, sortingOrder);
			this.spriteRenderer.color = this.idleColor;
			this.Ready = true;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B800 File Offset: 0x00009C00
		public void Delete()
		{
			this.Ready = false;
			UnityEngine.Object.Destroy(this.spriteGameObject);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B814 File Offset: 0x00009C14
		public void Update()
		{
			this.Update(false);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B820 File Offset: 0x00009C20
		public void Update(bool forceUpdate)
		{
			if (this.Dirty || forceUpdate)
			{
				if (this.spriteRenderer != null)
				{
					this.spriteRenderer.sprite = ((!this.State) ? this.idleSprite : this.busySprite);
				}
				if (this.sizeUnitType == TouchUnitType.Pixels)
				{
					Vector2 a = TouchUtility.RoundVector(this.size);
					this.ScaleSpriteInPixels(this.spriteGameObject, this.spriteRenderer, a);
					this.worldSize = a * TouchManager.PixelToWorld;
				}
				else
				{
					this.ScaleSpriteInPercent(this.spriteGameObject, this.spriteRenderer, this.size);
					if (this.lockAspectRatio)
					{
						this.worldSize = this.size * TouchManager.PercentToWorld;
					}
					else
					{
						this.worldSize = Vector2.Scale(this.size, TouchManager.ViewSize);
					}
				}
				this.Dirty = false;
			}
			if (this.spriteRenderer != null)
			{
				Color color = (!this.State) ? this.idleColor : this.busyColor;
				if (this.spriteRenderer.color != color)
				{
					this.spriteRenderer.color = Utility.MoveColorTowards(this.spriteRenderer.color, color, 5f * Time.deltaTime);
				}
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B984 File Offset: 0x00009D84
		private GameObject CreateSpriteGameObject(string name, Transform parentTransform)
		{
			return new GameObject(name)
			{
				transform = 
				{
					parent = parentTransform,
					localPosition = Vector3.zero,
					localScale = Vector3.one
				},
				layer = parentTransform.gameObject.layer
			};
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B9D8 File Offset: 0x00009DD8
		private SpriteRenderer CreateSpriteRenderer(GameObject spriteGameObject, Sprite sprite, int sortingOrder)
		{
			SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sprite = sprite;
			spriteRenderer.sortingOrder = sortingOrder;
			spriteRenderer.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
			spriteRenderer.sharedMaterial.SetFloat("PixelSnap", 1f);
			return spriteRenderer;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000BA28 File Offset: 0x00009E28
		private void ScaleSpriteInPixels(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			float num = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.bounds.size.x;
			float num2 = TouchManager.PixelToWorld * num;
			float x = num2 * size.x / spriteRenderer.sprite.rect.width;
			float y = num2 * size.y / spriteRenderer.sprite.rect.height;
			spriteGameObject.transform.localScale = new Vector3(x, y);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BAF0 File Offset: 0x00009EF0
		private void ScaleSpriteInPercent(GameObject spriteGameObject, SpriteRenderer spriteRenderer, Vector2 size)
		{
			if (spriteGameObject == null || spriteRenderer == null || spriteRenderer.sprite == null)
			{
				return;
			}
			if (this.lockAspectRatio)
			{
				float num = Mathf.Min(TouchManager.ViewSize.x, TouchManager.ViewSize.y);
				float x = num * size.x / spriteRenderer.sprite.bounds.size.x;
				float y = num * size.y / spriteRenderer.sprite.bounds.size.y;
				spriteGameObject.transform.localScale = new Vector3(x, y);
			}
			else
			{
				float x2 = TouchManager.ViewSize.x * size.x / spriteRenderer.sprite.bounds.size.x;
				float y2 = TouchManager.ViewSize.y * size.y / spriteRenderer.sprite.bounds.size.y;
				spriteGameObject.transform.localScale = new Vector3(x2, y2);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BC3C File Offset: 0x0000A03C
		public bool Contains(Vector2 testWorldPoint)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				float num = (testWorldPoint.x - this.Position.x) / this.worldSize.x;
				float num2 = (testWorldPoint.y - this.Position.y) / this.worldSize.y;
				return num * num + num2 * num2 < 0.25f;
			}
			float num3 = Utility.Abs(testWorldPoint.x - this.Position.x) * 2f;
			float num4 = Utility.Abs(testWorldPoint.y - this.Position.y) * 2f;
			return num3 <= this.worldSize.x && num4 <= this.worldSize.y;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BD18 File Offset: 0x0000A118
		public bool Contains(Touch touch)
		{
			return this.Contains(TouchManager.ScreenToWorldPoint(touch.position));
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BD30 File Offset: 0x0000A130
		public void DrawGizmos(Vector3 position, Color color)
		{
			if (this.shape == TouchSpriteShape.Oval)
			{
				Utility.DrawOvalGizmo(position, this.WorldSize, color);
			}
			else
			{
				Utility.DrawRectGizmo(position, this.WorldSize, color);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BD66 File Offset: 0x0000A166
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000BD6E File Offset: 0x0000A16E
		public bool State
		{
			get
			{
				return this.state;
			}
			set
			{
				if (this.state != value)
				{
					this.state = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000BD8A File Offset: 0x0000A18A
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000BD92 File Offset: 0x0000A192
		public Sprite BusySprite
		{
			get
			{
				return this.busySprite;
			}
			set
			{
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000BDB3 File Offset: 0x0000A1B3
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000BDBB File Offset: 0x0000A1BB
		public Sprite IdleSprite
		{
			get
			{
				return this.idleSprite;
			}
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000BDDC File Offset: 0x0000A1DC
		public Sprite Sprite
		{
			set
			{
				if (this.idleSprite != value)
				{
					this.idleSprite = value;
					this.Dirty = true;
				}
				if (this.busySprite != value)
				{
					this.busySprite = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000BE1C File Offset: 0x0000A21C
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000BE24 File Offset: 0x0000A224
		public Color BusyColor
		{
			get
			{
				return this.busyColor;
			}
			set
			{
				if (this.busyColor != value)
				{
					this.busyColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000BE45 File Offset: 0x0000A245
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000BE4D File Offset: 0x0000A24D
		public Color IdleColor
		{
			get
			{
				return this.idleColor;
			}
			set
			{
				if (this.idleColor != value)
				{
					this.idleColor = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000BE6E File Offset: 0x0000A26E
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000BE76 File Offset: 0x0000A276
		public TouchSpriteShape Shape
		{
			get
			{
				return this.shape;
			}
			set
			{
				if (this.shape != value)
				{
					this.shape = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000BE92 File Offset: 0x0000A292
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000BE9A File Offset: 0x0000A29A
		public TouchUnitType SizeUnitType
		{
			get
			{
				return this.sizeUnitType;
			}
			set
			{
				if (this.sizeUnitType != value)
				{
					this.sizeUnitType = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000BEB6 File Offset: 0x0000A2B6
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000BEBE File Offset: 0x0000A2BE
		public Vector2 Size
		{
			get
			{
				return this.size;
			}
			set
			{
				if (this.size != value)
				{
					this.size = value;
					this.Dirty = true;
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000BEDF File Offset: 0x0000A2DF
		public Vector2 WorldSize
		{
			get
			{
				return this.worldSize;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000BEE7 File Offset: 0x0000A2E7
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000BF13 File Offset: 0x0000A313
		public Vector3 Position
		{
			get
			{
				return (!this.spriteGameObject) ? Vector3.zero : this.spriteGameObject.transform.position;
			}
			set
			{
				if (this.spriteGameObject)
				{
					this.spriteGameObject.transform.position = value;
				}
			}
		}

		// Token: 0x04000277 RID: 631
		[SerializeField]
		private Sprite idleSprite;

		// Token: 0x04000278 RID: 632
		[SerializeField]
		private Sprite busySprite;

		// Token: 0x04000279 RID: 633
		[SerializeField]
		private Color idleColor = new Color(1f, 1f, 1f, 0.5f);

		// Token: 0x0400027A RID: 634
		[SerializeField]
		private Color busyColor = new Color(1f, 1f, 1f, 1f);

		// Token: 0x0400027B RID: 635
		[SerializeField]
		private TouchSpriteShape shape;

		// Token: 0x0400027C RID: 636
		[SerializeField]
		private TouchUnitType sizeUnitType;

		// Token: 0x0400027D RID: 637
		[SerializeField]
		private Vector2 size = new Vector2(10f, 10f);

		// Token: 0x0400027E RID: 638
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x0400027F RID: 639
		[SerializeField]
		[HideInInspector]
		private Vector2 worldSize;

		// Token: 0x04000280 RID: 640
		private Transform spriteParentTransform;

		// Token: 0x04000281 RID: 641
		private GameObject spriteGameObject;

		// Token: 0x04000282 RID: 642
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000283 RID: 643
		private bool state;
	}
}
