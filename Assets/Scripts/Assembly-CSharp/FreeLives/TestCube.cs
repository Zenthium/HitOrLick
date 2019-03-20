using System;
using UnityEngine;

namespace FreeLives
{
	// Token: 0x020000E1 RID: 225
	public class TestCube : MonoBehaviour
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x000256C7 File Offset: 0x00023AC7
		private void Start()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000256CC File Offset: 0x00023ACC
		private void Update()
		{
			InputReader.GetInput(this.device, this.inputState);
			base.transform.Translate(this.inputState.xAxis * 10f * Time.deltaTime, this.inputState.yAxis * 10f * Time.deltaTime, 0f, Space.World);
			if (this.inputState.aButton && !this.inputState.wasAButton)
			{
				SoundController.PlaySoundEffect("Test");
			}
			if (this.inputState.bButton && !this.inputState.wasBButton)
			{
				SoundController.PlaySoundEffect("Test", 0.5f, base.transform.position);
			}
			if (this.inputState.xButton && !this.inputState.wasXButton)
			{
				CameraShake.Instance.Shake(1f, 5f);
			}
			if (this.inputState.yButton && !this.inputState.wasYButton)
			{
				CameraShake.Instance.Vibrate(1f);
			}
		}

		// Token: 0x040003EB RID: 1003
		public InputReader.Device device;

		// Token: 0x040003EC RID: 1004
		private InputState inputState = new InputState();
	}
}
