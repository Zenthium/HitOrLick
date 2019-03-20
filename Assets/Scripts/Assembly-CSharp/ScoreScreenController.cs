using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FreeLives;

// Token: 0x02000104 RID: 260
public class ScoreScreenController : MonoBehaviour
{
	private InputState input = new InputState ();

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000589 RID: 1417 RVA: 0x0002F4C1 File Offset: 0x0002D8C1
	private bool isLastRound {
		get {
			if (GameController.isFreePlay) {
				return GameController.GetHighestScore () >= GameController.winsNeeded;
			} else {
				return GameController.levelNo >= GameController.levelNames.Length;
			}
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0002F4D4 File Offset: 0x0002D8D4
	private void Awake ()
	{
		if (GameController.isFreePlay) {
			this.roundText.text = string.Concat (new object[] {
				"FIRST TO ",
				GameController.winsNeeded,
				" WIN"
			});
			if (GameController.winsNeeded != 1) {
				this.roundText.text += "S";
			}
		} else {
			this.roundText.text = string.Concat (new object[] {
				"ROUND ",
				GameController.levelNo,
				" OF ",
				GameController.levelNames.Length
			});
		}
		this.playerScoreDisplays = new List<PlayerScoreDisplay> ();
		if (GameController.isShowDown) {
			this.updateScoresDelay = 0.05f;
		}
		foreach (Player player in GameController.activePlayers) {
			if (!GameController.isTeamMode || (player.team == Team.Red && this.redScoreDisplay == null) || (player.team == Team.Blue && this.blueScoreDisplay == null)) {
				PlayerScoreDisplay playerScoreDisplay = UnityEngine.Object.Instantiate<PlayerScoreDisplay> (this.scoreDisplayPrefab, this.scoreCanvas.transform);
				playerScoreDisplay.player = player;
				playerScoreDisplay.color = player.color;
				playerScoreDisplay.text.color = player.color;
				this.playerScoreDisplays.Add (playerScoreDisplay);
				playerScoreDisplay.color = player.color;
				playerScoreDisplay.text.text = player.roundWins.ToString ();
				playerScoreDisplay.useRoundWins = true;
				if (GameController.isTeamMode) {
					if (player.team == Team.Red) {
						this.redScoreDisplay = playerScoreDisplay;
					} else {
						this.blueScoreDisplay = playerScoreDisplay;
					}
				}
			}
		}
		this.SortScoreboard ();
		this.ArrangeScoreboard (true);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002F688 File Offset: 0x0002DA88
	private void SortScoreboard ()
	{
		this.playerScoreDisplays.Sort ((PlayerScoreDisplay x, PlayerScoreDisplay y) => y.player.roundWins * 100 + y.player.sortPriority - (x.player.roundWins * 100 + x.player.sortPriority));
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0002F6B4 File Offset: 0x0002DAB4
	private void ArrangeScoreboard (bool instant)
	{
		for (int i = 0; i < this.playerScoreDisplays.Count; i++) {
			Vector3 localPosition = this.playerScoreDisplays [i].transform.localPosition;
			if (instant) {
				localPosition.y = Mathf.Lerp (localPosition.y, (float)i * -2f, 1f);
			} else {
				localPosition.y = Mathf.Lerp (localPosition.y, (float)i * -2f, Time.deltaTime * 3f);
			}
			localPosition.z = 0f;
			this.playerScoreDisplays [i].transform.localPosition = localPosition;
		}
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0002F768 File Offset: 0x0002DB68
	private void Update ()
	{
		InputReader.GetInput (this.input);
		if (this.updateScoresDelay > 0f) {
			this.updateScoresDelay -= Time.deltaTime;
			if (this.input.start && !this.input.wasStart) {
				this.updateScoresDelay = 0;
			} else {
				return;
			}
		}
		if (!this.haveUpdatedScores) {
			this.haveUpdatedScores = true;
			if (GameController.isTeamMode) {
				foreach (Player player in GameController.activePlayers) {
					if (player.team == GameController.lastWinningPlayer.team) {
						player.roundWins++;
					}
				}
			}
			foreach (PlayerScoreDisplay playerScoreDisplay in this.playerScoreDisplays) {
				if (GameController.isTeamMode) {
					if (playerScoreDisplay.player.team == GameController.lastWinningPlayer.team) {
						if (GameController.isShowDown) {
							playerScoreDisplay.TemorarilyDisplay ("WINNER!", 10f);
						} else {
							playerScoreDisplay.TemorarilyDisplay (playerScoreDisplay.player.roundWins.ToString (), 2f);
						}
					}
				} else if (playerScoreDisplay.player == GameController.lastWinningPlayer) {
					playerScoreDisplay.player.roundWins++;
					if (GameController.isShowDown) {
						playerScoreDisplay.TemorarilyDisplay ("WINNER!", 10f);
					} else {
						playerScoreDisplay.TemorarilyDisplay (playerScoreDisplay.player.roundWins.ToString (), 2f);
					}
				}
			}
			this.SortScoreboard ();
			if (this.isLastRound && GameController.AreAnyPlayersTiedForVictory ()) {
				this.roundText.text = "SHOWDOWN ! ! ! ";
				GameController.isShowDown = true;
			}
		}
		this.ArrangeScoreboard (false);
		if (this.isLastRound && GameController.AreAnyPlayersTiedForVictory ()) {
			this.roundText.color = (((double)Time.time % 0.2 >= 0.10000000149011612) ? Color.black : Color.white);
		}
		this.continueDelay -= Time.deltaTime;

		if (this.input.start && !this.input.wasStart) {
			this.continueDelay = 0f;
		}

		if (this.continueDelay <= 0f) {
			if (this.isLastRound && GameController.AreAnyPlayersTiedForVictory ()) {
				GameController.SetupPlayersForShowdown ();
				SceneManager.LoadScene ("7Showdown");
			} else if (this.isLastRound) {
				GameController.overallWinnerColor = this.playerScoreDisplays [0].color;
				SceneManager.LoadScene ("Outro");
			} else {
				if (GameController.isFreePlay) {
					SceneManager.LoadScene ("LevelSelectScreen");
				} else {
					SceneManager.LoadScene (GameController.levelNames [GameController.levelNo]);
				}
			}
		}
	}

	// Token: 0x0400059F RID: 1439
	public PlayerScoreDisplay scoreDisplayPrefab;

	// Token: 0x040005A0 RID: 1440
	public Canvas scoreCanvas;

	// Token: 0x040005A1 RID: 1441
	public Text roundText;

	// Token: 0x040005A2 RID: 1442
	private List<PlayerScoreDisplay> playerScoreDisplays;

	// Token: 0x040005A3 RID: 1443
	private PlayerScoreDisplay redScoreDisplay;

	// Token: 0x040005A4 RID: 1444
	private PlayerScoreDisplay blueScoreDisplay;

	// Token: 0x040005A5 RID: 1445
	private float updateScoresDelay = 1f;

	// Token: 0x040005A6 RID: 1446
	private float continueDelay = 5f;

	// Token: 0x040005A7 RID: 1447
	private bool haveUpdatedScores;
}
