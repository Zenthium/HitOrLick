using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000031 RID: 49
	public class TouchButtonControl : TouchControl
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00009565 File Offset: 0x00007965
		public override void CreateControl()
		{
			this.button.Create("Button", base.transform, 1000);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009582 File Offset: 0x00007982
		public override void DestroyControl()
		{
			this.button.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000095AD File Offset: 0x000079AD
		public override void ConfigureControl()
		{
			base.transform.position = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, this.lockAspectRatio);
			this.button.Update(true);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000095E4 File Offset: 0x000079E4
		public override void DrawGizmos()
		{
			this.button.DrawGizmos(this.ButtonPosition, Color.yellow);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000095FC File Offset: 0x000079FC
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
			else
			{
				this.button.Update();
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009628 File Offset: 0x00007A28
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			if (this.currentTouch == null && this.allowSlideToggle)
			{
				this.ButtonState = false;
				int touchCount = TouchManager.TouchCount;
				for (int i = 0; i < touchCount; i++)
				{
					this.ButtonState = (this.ButtonState || this.button.Contains(TouchManager.GetTouch(i)));
				}
			}
			base.SubmitButtonState(this.target, this.ButtonState, updateTick, deltaTime);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000096A3 File Offset: 0x00007AA3
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitButton(this.target);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000096B1 File Offset: 0x00007AB1
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			if (this.button.Contains(touch))
			{
				this.ButtonState = true;
				this.currentTouch = touch;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000096DE File Offset: 0x00007ADE
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			if (this.toggleOnLeave && !this.button.Contains(touch))
			{
				this.ButtonState = false;
				this.currentTouch = null;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009717 File Offset: 0x00007B17
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.ButtonState = false;
			this.currentTouch = null;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00009734 File Offset: 0x00007B34
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000973C File Offset: 0x00007B3C
		private bool ButtonState
		{
			get
			{
				return this.buttonState;
			}
			set
			{
				if (this.buttonState != value)
				{
					this.buttonState = value;
					this.button.State = value;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000975D File Offset: 0x00007B5D
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000978A File Offset: 0x00007B8A
		public Vector3 ButtonPosition
		{
			get
			{
				return (!this.button.Ready) ? base.transform.position : this.button.Position;
			}
			set
			{
				if (this.button.Ready)
				{
					this.button.Position = value;
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000097A8 File Offset: 0x00007BA8
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000097B0 File Offset: 0x00007BB0
		public TouchControlAnchor Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				if (this.anchor != value)
				{
					this.anchor = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000097CC File Offset: 0x00007BCC
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000097D4 File Offset: 0x00007BD4
		public Vector2 Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				if (this.offset != value)
				{
					this.offset = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000097F5 File Offset: 0x00007BF5
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000097FD File Offset: 0x00007BFD
		public TouchUnitType OffsetUnitType
		{
			get
			{
				return this.offsetUnitType;
			}
			set
			{
				if (this.offsetUnitType != value)
				{
					this.offsetUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x040001D6 RID: 470
		[Header("Position")]
		[SerializeField]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomRight;

		// Token: 0x040001D7 RID: 471
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x040001D8 RID: 472
		[SerializeField]
		private Vector2 offset = new Vector2(-10f, 10f);

		// Token: 0x040001D9 RID: 473
		[SerializeField]
		private bool lockAspectRatio = true;

		// Token: 0x040001DA RID: 474
		[Header("Options")]
		public TouchControl.ButtonTarget target = TouchControl.ButtonTarget.Action1;

		// Token: 0x040001DB RID: 475
		public bool allowSlideToggle = true;

		// Token: 0x040001DC RID: 476
		public bool toggleOnLeave;

		// Token: 0x040001DD RID: 477
		[Header("Sprites")]
		public TouchSprite button = new TouchSprite(15f);

		// Token: 0x040001DE RID: 478
		private bool buttonState;

		// Token: 0x040001DF RID: 479
		private Touch currentTouch;

		// Token: 0x040001E0 RID: 480
		private bool dirty;
	}
}
