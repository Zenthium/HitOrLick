using System;
using FreeLives;
using UnityEngine;
using UnityEngine.UI;

public class JoinCanvas : MonoBehaviour
{
	// Token: 0x0600057C RID: 1404 RVA: 0x0002EC2F File Offset: 0x0002D02F
	private void Start()
	{
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0002EC34 File Offset: 0x0002D034
	private void Update()
	{
		foreach (Text text in this.texts)
		{
			text.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * 3f, 1f));
		}
		if (this.state == JoinCanvas.State.ChooseColor)
		{
			InputReader.GetInput(this.assignedPlayer.inputDevice, this.input);
			if (GameController.isTeamMode)
			{
				if (this.input.bButton && !this.input.wasBButton)
				{
					if (this.assignedPlayer.team == Team.Red)
					{
						this.assignedPlayer.team = Team.Blue;
					}
					else
					{
						this.assignedPlayer.team = Team.Red;
					}
					this.colorLerpAmount = UnityEngine.Random.Range(0f, 0.7f);
					if (this.assignedPlayer.team == Team.Red)
					{
						this.color = (this.assignedPlayer.color = Color.Lerp(Color.red, Color.white, this.colorLerpAmount));
					}
					else
					{
						this.color = (this.assignedPlayer.color = Color.Lerp(Color.blue, Color.white, this.colorLerpAmount));
					}
					this.frogImage.color = this.color;
					EffectsController.CreateSpawnEffects(this.effectPos.position, this.color);
					SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, this.effectPos.position);
				}
				if (this.input.left)
				{
					this.colorLerpAmount = Mathf.MoveTowards(this.colorLerpAmount, 0f, Time.deltaTime * 0.5f);
				}
				else if (this.input.right)
				{
					this.colorLerpAmount = Mathf.MoveTowards(this.colorLerpAmount, 0.7f, Time.deltaTime * 0.5f);
				}
				Player player = this.assignedPlayer;
				Color color = Color.Lerp((this.assignedPlayer.team != Team.Red) ? Color.blue : Color.red, Color.white, this.colorLerpAmount);
				this.frogImage.color = color;
				this.color = (player.color = color);
			}
			else if (this.input.bButton && !this.input.wasBButton)
			{
				Color availableColor = GameController.GetAvailableColor();
				GameController.ReturnColor(this.color);
				this.color = (this.assignedPlayer.color = availableColor);
				this.frogImage.color = this.color;
				EffectsController.CreateSpawnEffects(this.effectPos.position, this.color);
				SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, this.effectPos.position);
			}
			if (this.input.yButton && !this.input.wasYButton)
			{
				this.UnAssignPlayer();
			}
			else if (this.input.xButton && !this.input.wasXButton)
			{
				this.assignedPlayer.color = this.color;
				GameController.SpawnCharacterJoinScreen(this.assignedPlayer);
				this.state = JoinCanvas.State.Ready;
				this.chooseColorCanvas.enabled = false;
				this.backPromptCanvas.enabled = true;
			}
		}
		else if (this.state == JoinCanvas.State.Ready)
		{
			InputReader.GetInput(this.assignedPlayer.inputDevice, this.input);
			if (this.input.yButton && !this.input.wasYButton)
			{
				this.UnAssignPlayer();
			}
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0002EFE8 File Offset: 0x0002D3E8
	public void AssignPlayer(Player player)
	{
		this.assignedPlayer = player;
		this.state = JoinCanvas.State.ChooseColor;
		this.joinPromptCanvas.enabled = false;
		this.chooseColorCanvas.enabled = true;
		if (GameController.isTeamMode)
		{
			this.teamChangeColorObject.SetActive(true);
			this.changeColorText.text = "CHANGE TEAM";
			player.team = ((UnityEngine.Random.value >= 0.5f) ? Team.Blue : Team.Red);
			if (this.assignedPlayer.team == Team.Red)
			{
				this.color = (this.assignedPlayer.color = Color.Lerp(Color.red, Color.white, UnityEngine.Random.Range(0f, 0.7f)));
			}
			else
			{
				this.color = (this.assignedPlayer.color = Color.Lerp(Color.blue, Color.white, UnityEngine.Random.Range(0f, 0.7f)));
			}
			this.frogImage.color = this.color;
		}
		else
		{
			this.teamChangeColorObject.SetActive(false);
			this.changeColorText.text = "CHANGE COLOR";
			this.color = GameController.GetAvailableColor();
			player.color = this.color;
			this.frogImage.color = this.color;
		}
		InputReader.GetInput(this.assignedPlayer.inputDevice, this.input);
		EffectsController.CreateSpawnEffects(this.effectPos.position, this.color);
		SoundController.PlaySoundEffect("CharacterSpawn", 0.3f, this.effectPos.position);
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0002F17C File Offset: 0x0002D57C
	public bool HasAssignedPlayer()
	{
		return this.assignedPlayer != null;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0002F18C File Offset: 0x0002D58C
	private void UnAssignPlayer()
	{
		GameController.ReturnPlayer(this.assignedPlayer);
		GameController.ReturnColor(this.color);
		if (this.assignedPlayer.character != null)
		{
			UnityEngine.Object.Destroy(this.assignedPlayer.character.gameObject);
		}
		this.state = JoinCanvas.State.Join;
		this.joinPromptCanvas.enabled = true;
		this.chooseColorCanvas.enabled = false;
		this.backPromptCanvas.enabled = false;
		this.assignedPlayer = null;
	}

	// Token: 0x04000584 RID: 1412
	public Transform effectPos;

	// Token: 0x04000585 RID: 1413
	public Canvas joinPromptCanvas;

	// Token: 0x04000586 RID: 1414
	public Canvas chooseColorCanvas;

	// Token: 0x04000587 RID: 1415
	public Canvas backPromptCanvas;

	// Token: 0x04000588 RID: 1416
	public GameObject teamChangeColorObject;

	// Token: 0x04000589 RID: 1417
	public Text changeColorText;

	// Token: 0x0400058A RID: 1418
	public Text[] texts;

	// Token: 0x0400058B RID: 1419
	public Player assignedPlayer;

	// Token: 0x0400058C RID: 1420
	public JoinCanvas.State state;

	// Token: 0x0400058D RID: 1421
	private Color color;

	// Token: 0x0400058E RID: 1422
	public Image frogImage;

	// Token: 0x0400058F RID: 1423
	private float delay = 1f;

	// Token: 0x04000590 RID: 1424
	private InputState input = new InputState();

	// Token: 0x04000591 RID: 1425
	private float colorLerpAmount;

	// Token: 0x02000101 RID: 257
	public enum State
	{
		// Token: 0x04000593 RID: 1427
		Join,
		// Token: 0x04000594 RID: 1428
		ChooseColor,
		// Token: 0x04000595 RID: 1429
		Ready
	}
}
