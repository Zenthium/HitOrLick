using System;
using System.Collections.Generic;
using FreeLives;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000515 RID: 1301 RVA: 0x0002B655 File Offset: 0x00029A55
	public static GameState State
	{
		get
		{
			return GameController.instance.state;
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0002B664 File Offset: 0x00029A64
	private void Awake()
	{
		if (this.isJoinScreen)
		{
			GameController.isShowDown = false;
			this.state = GameState.JoinScreen;
			this.finishDelay = 5f;
			this.SetupForJoinScreen();
			GameController.inactivePlayers = null;
			GameController.activePlayers.Clear();
			GameController.levelNo = 0;
			GameController.playersCanDropIn = true;
		}
		this.playerScoreDisplays = new List<PlayerScoreDisplay>();
		GameController.instance = this;
		if (GameController.inactivePlayers == null)
		{
			GameController.inactivePlayers = new List<Player>();
			Player item = new Player(InputReader.Device.Gamepad1, this.playerColors[0], 0);
			GameController.inactivePlayers.Add(item);
			item = new Player(InputReader.Device.Gamepad2, this.playerColors[1], 1);
			GameController.inactivePlayers.Add(item);
			item = new Player(InputReader.Device.Gamepad3, this.playerColors[2], 2);
			GameController.inactivePlayers.Add(item);
			item = new Player(InputReader.Device.Gamepad4, this.playerColors[3], 3);
			GameController.inactivePlayers.Add(item);
			item = new Player(InputReader.Device.Keyboard1, this.playerColors[4], 4);
			GameController.inactivePlayers.Add(item);
			item = new Player(InputReader.Device.Keyboard2, this.playerColors[5], 5);
			GameController.inactivePlayers.Add(item);
		}
		else
		{
			int num = 0;
			if (GameController.isTeamMode)
			{
				foreach (Player player in GameController.activePlayers)
				{
					player.score = 0;
					player.spawnDelay = 0.5f + 0.2f * (float)num;
					num++;
				}
				PlayerScoreDisplay playerScoreDisplay = UnityEngine.Object.Instantiate<PlayerScoreDisplay>(this.scoreDisplayPrefab, this.scoreCanvas.transform);
				playerScoreDisplay.color = Color.red;
				playerScoreDisplay.text.color = Color.red;
				this.redTeamScoreDisplay = playerScoreDisplay;
				foreach (Player player2 in GameController.activePlayers)
				{
					if (player2.team == Team.Red)
					{
						playerScoreDisplay.player = player2;
					}
				}
				this.playerScoreDisplays.Add(playerScoreDisplay);
				playerScoreDisplay = UnityEngine.Object.Instantiate<PlayerScoreDisplay>(this.scoreDisplayPrefab, this.scoreCanvas.transform);
				playerScoreDisplay.color = Color.blue;
				playerScoreDisplay.text.color = Color.blue;
				this.blueTeamScoreDisplay = playerScoreDisplay;
				foreach (Player player3 in GameController.activePlayers)
				{
					if (player3.team == Team.Blue)
					{
						playerScoreDisplay.player = player3;
					}
				}
				this.playerScoreDisplays.Add(playerScoreDisplay);
			}
			else
			{
				foreach (Player player4 in GameController.activePlayers)
				{
					player4.score = 0;
					player4.spawnDelay = 0.5f + 0.2f * (float)num;
					num++;
					PlayerScoreDisplay playerScoreDisplay2 = UnityEngine.Object.Instantiate<PlayerScoreDisplay>(this.scoreDisplayPrefab, this.scoreCanvas.transform);
					playerScoreDisplay2.player = player4;
					playerScoreDisplay2.color = player4.color;
					playerScoreDisplay2.text.color = player4.color;
					this.playerScoreDisplays.Add(playerScoreDisplay2);
				}
			}
		}
		if (GameController.isShowDown)
		{
			foreach (PlayerScoreDisplay playerScoreDisplay3 in this.playerScoreDisplays)
			{
				playerScoreDisplay3.gameObject.SetActive(false);
			}
		}
		InputReader.GetInput(this.combinedInput);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0002BA98 File Offset: 0x00029E98
	internal static void SetupPlayersForShowdown()
	{
		List<Player> leadingPlayers = GameController.GetLeadingPlayers();
		GameController.activePlayers.Clear();
		foreach (Player item in leadingPlayers)
		{
			GameController.activePlayers.Add(item);
		}
		GameController.playersCanDropIn = false;
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x0002BB0C File Offset: 0x00029F0C
	private void Start()
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = 18f * num;
		float num3 = 32f / num2;
		Camera.main.orthographicSize = num3 * 18f;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0002BB48 File Offset: 0x00029F48
	private void Update()
	{
		if (this.state == GameState.JoinScreen)
		{
			if (Input.GetKeyDown(KeyCode.Escape)) {
				this.joinScreenBackButton.OnClick();
			}

			for (int i = GameController.inactivePlayers.Count - 1; i >= 0; i--)
			{
				InputReader.GetInput(GameController.inactivePlayers[i].inputDevice, this.input);
				if (this.input.xButton)
				{
					for (int j = 0; j < this.joinCanvas.Length; j++)
					{
						if (!this.joinCanvas[j].HasAssignedPlayer())
						{
							this.joinCanvas[j].AssignPlayer(GameController.inactivePlayers[i]);
							GameController.inactivePlayers.RemoveAt(i);
							j = this.joinCanvas.Length;
						}
					}
				}
			}
			int num = 0;
			for (int k = 0; k < this.joinCanvas.Length; k++)
			{
				if (this.joinCanvas[k].HasAssignedPlayer())
				{
					num++;
				}
			}
			bool flag = this.CheckReadyPlayers();
			if (num == 0)
			{
				InputReader.GetInput(this.combinedInput);
				if (this.combinedInput.start && !this.combinedInput.wasStart && GameController.allowTeamMode)
				{
					GameController.isTeamMode = !GameController.isTeamMode;
					this.joinGameModeText.text = GameController.isTeamMode ? "TEAM" : "FREE  FOR  ALL";
				}
			}
			else if (flag)
			{
				this.finishDelay -= Time.deltaTime;
				this.joinCountdownText.text = ((int)this.finishDelay + 1).ToString();
				if (this.finishDelay <= 0f)
				{
					this.FinishRound();
				}
			}
			else
			{
				this.joinCountdownText.text = string.Empty;
				this.finishDelay = 5f;
			}
		}
		else if (this.state == GameState.Playing)
		{
			if (this.activeFly == null && !GameController.isShowDown && GameController.flyEnabled)
			{
				if (this.flySpawnDelay > 0f)
				{
					this.flySpawnDelay -= Time.deltaTime;
					if (this.flySpawnDelay <= 0f)
					{
						this.activeFly = UnityEngine.Object.Instantiate<Fly>(this.flyPrefab, global::Terrain.GetFlySpawnPoint(), Quaternion.identity);
					}
				}
				else
				{
					this.flySpawnDelay = UnityEngine.Random.Range(15f, 45f);
				}
			}
			for (int l = 0; l < GameController.activePlayers.Count; l++)
			{
				if (GameController.activePlayers[l].character == null)
				{
					GameController.activePlayers[l].spawnDelay -= Time.deltaTime;
					if (GameController.activePlayers[l].spawnDelay < 0f)
					{
						this.SpawnCharacter(GameController.activePlayers[l]);
					}
				}
				if (GameController.activePlayers[l].character != null && GameController.activePlayers[l].character.transform.position.y > global::Terrain.ScreenTop)
				{
					SpriteRenderer spriteRenderer = GameController.activePlayers[l].offscreenDot;
					if (spriteRenderer == null)
					{
						spriteRenderer = (GameController.activePlayers[l].offscreenDot = UnityEngine.Object.Instantiate<SpriteRenderer>(this.offscreenDotPrefab));
						spriteRenderer.color = GameController.activePlayers[l].color;
					}
					spriteRenderer.enabled = true;
					spriteRenderer.transform.position = new Vector3(GameController.activePlayers[l].character.transform.position.x, global::Terrain.ScreenTop, -6f);
				}
				else
				{
					SpriteRenderer offscreenDot = GameController.activePlayers[l].offscreenDot;
					if (offscreenDot != null)
					{
						offscreenDot.enabled = false;
					}
				}
			}
			this.ArrangeScoreboards();
			if (Application.isEditor)
			{
				for (int m = GameController.inactivePlayers.Count - 1; m >= 0; m--)
				{
					InputReader.GetInput(GameController.inactivePlayers[m].inputDevice, this.input);
					if (this.input.xButton)
					{
						MonoBehaviour.print(GameController.inactivePlayers[m].color + ", " + GameController.inactivePlayers[m].inputDevice);
						GameController.inactivePlayers[m].color = this.playerColors[UnityEngine.Random.Range(0, this.playerColors.Length)];
						this.AddPlayer(GameController.inactivePlayers[m]);
						GameController.inactivePlayers.RemoveAt(m);
					}
				}
				if (Input.GetKeyDown(KeyCode.F2))
				{
					this.SpawnCharacter(null);
				}
			}
		}
		else if (this.state == GameState.RoundFinished)
		{
			this.ArrangeScoreboards();
			for (int n = 0; n < GameController.activePlayers.Count; n++)
			{
				if (GameController.activePlayers[n].character == null && this.winningPlayer == GameController.activePlayers[n])
				{
					GameController.activePlayers[n].spawnDelay -= Time.deltaTime;
					if (GameController.activePlayers[n].spawnDelay < 0f)
					{
						this.SpawnCharacter(GameController.activePlayers[n]);
					}
				}
			}
			this.finishDelay -= Time.deltaTime;
			if (this.finishDelay < 0f)
			{
				this.FinishRound();
			}
		}
		if (Input.GetKeyDown(KeyCode.F6))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.F12))
		{
			this.showGui = !this.showGui;
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0002C15E File Offset: 0x0002A55E
	private void SetupForJoinScreen()
	{
		this.availableColors = new List<Color>();
		this.availableColors.AddRange(this.playerColors);
		this.joinGameModeText.text = "FREE FOR ALL";
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0002C17C File Offset: 0x0002A57C
	private bool CheckReadyPlayers()
	{
		int num = 0;
		if (GameController.isTeamMode)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.joinCanvas.Length; i++)
			{
				if (this.joinCanvas[i].HasAssignedPlayer())
				{
					if (this.joinCanvas[i].state == JoinCanvas.State.Ready)
					{
						num++;
						if (this.joinCanvas[i].assignedPlayer.team == Team.Blue)
						{
							flag2 = true;
						}
						else if (this.joinCanvas[i].assignedPlayer.team == Team.Red)
						{
							flag = true;
						}
					}
					else
					{
						num = -100;
					}
				}
			}
			return flag && flag2 && num >= 2;
		}
		for (int j = 0; j < this.joinCanvas.Length; j++)
		{
			if (this.joinCanvas[j].HasAssignedPlayer())
			{
				if (this.joinCanvas[j].state == JoinCanvas.State.Ready)
				{
					num++;
				}
				else
				{
					num = -100;
				}
			}
		}
		return num >= 2;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0002C28C File Offset: 0x0002A68C
	private void ArrangeScoreboards()
	{
		for (int i = 0; i < this.playerScoreDisplays.Count; i++)
		{
			Vector3 localPosition = this.playerScoreDisplays[i].transform.localPosition;
			localPosition.y = Mathf.Lerp(localPosition.y, (float)i * -2f, Time.deltaTime * 3f);
			localPosition.z = 0f;
			this.playerScoreDisplays[i].transform.localPosition = localPosition;
		}
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0002C318 File Offset: 0x0002A718
	private void FinishRound()
	{
		if (this.state == GameState.JoinScreen)
		{
			GameController.levelNo = 0;
			foreach (JoinCanvas joinCanvas in this.joinCanvas)
			{
				if (joinCanvas.HasAssignedPlayer())
				{
					GameController.activePlayers.Add(joinCanvas.assignedPlayer);
				}
			}
			if (GameController.isFreePlay) {
				SceneManager.LoadScene ("LevelSelectScreen");
			} else {
				SceneManager.LoadScene (GameController.levelNames [0]);
			}
		}
		else
		{
			GameController.levelNo++;
			SceneManager.LoadScene("ScoreScreen");
		}
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0002C398 File Offset: 0x0002A798
	private void AddPlayer(Player player)
	{
		GameController.activePlayers.Add(player);
		PlayerScoreDisplay playerScoreDisplay = UnityEngine.Object.Instantiate<PlayerScoreDisplay>(this.scoreDisplayPrefab, this.scoreCanvas.transform);
		playerScoreDisplay.player = player;
		playerScoreDisplay.color = player.color;
		playerScoreDisplay.text.color = player.color;
		this.playerScoreDisplays.Add(playerScoreDisplay);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0002C3F8 File Offset: 0x0002A7F8
	private void SpawnCharacter(Player player)
	{
		Vector3 spawnPoint = global::Terrain.GetSpawnPoint();
		Character character = UnityEngine.Object.Instantiate<Character>(this.characterPrefab, spawnPoint, Quaternion.identity);
		if (player != null)
		{
			character.player = player;
			player.character = character;
			player.spawnDelay = 1f;
			EffectsController.CreateSpawnEffects(spawnPoint + Vector3.up, player.color);
			SoundController.PlaySoundEffect("CharacterSpawn", 0.4f, spawnPoint);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0002C468 File Offset: 0x0002A868
	public static void SpawnCharacterJoinScreen(Player player)
	{
		for (int i = 0; i < GameController.instance.joinCanvas.Length; i++)
		{
			if (GameController.instance.joinCanvas[i].assignedPlayer == player)
			{
				Character character = UnityEngine.Object.Instantiate<Character>(GameController.instance.characterPrefab, global::Terrain.GetSpawnPoint(i), Quaternion.identity);
				character.player = player;
				player.character = character;
			}
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000521 RID: 1313 RVA: 0x0002C4D2 File Offset: 0x0002A8D2
	// (set) Token: 0x06000522 RID: 1314 RVA: 0x0002C4D9 File Offset: 0x0002A8D9
	public static Player lastWinningPlayer { get; private set; }

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000523 RID: 1315 RVA: 0x0002C4E1 File Offset: 0x0002A8E1
	public static bool HasInstance
	{
		get
		{
			return GameController.instance != null;
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0002C4EE File Offset: 0x0002A8EE
	public static Player GetWinningPlayer()
	{
		if (GameController.State == GameState.RoundFinished)
		{
			return GameController.instance.winningPlayer;
		}
		return null;
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0002C508 File Offset: 0x0002A908
	private void SortScoreboard()
	{
		if (GameController.isTeamMode)
		{
			this.redTeamScoreDisplay.player.score = this.redTeamScore;
			this.blueTeamScoreDisplay.player.score = this.blueTeamScore;
		}
		GameController.instance.playerScoreDisplays.Sort((PlayerScoreDisplay x, PlayerScoreDisplay y) => y.player.score * 100 + y.player.sortPriority - (x.player.score * 100 + x.player.sortPriority));
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0002C578 File Offset: 0x0002A978
	internal static void RegisterKill(Player gotPoint, Player gotKilled, int hits)
	{
		if (GameController.State == GameState.RoundFinished)
		{
			return;
		}
		if (GameController.isShowDown)
		{
			bool flag = false;
			if (GameController.activePlayers.Contains(gotKilled))
			{
				GameController.activePlayers.Remove(gotKilled);
				if (gotKilled.offscreenDot != null)
				{
					UnityEngine.Object.Destroy(gotKilled.offscreenDot);
				}
			}
			if (GameController.activePlayers.Count == 1)
			{
				flag = true;
				Player player = GameController.activePlayers[0];
				if (player.character != null)
				{
					player.character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
				}
				GameController.instance.winningPlayer = player;
				GameController.lastWinningPlayer = player;
			}
			else if (GameController.isTeamMode)
			{
				bool flag2 = false;
				bool flag3 = false;
				foreach (Player player2 in GameController.activePlayers)
				{
					if (player2.team == Team.Red)
					{
						flag2 = true;
					}
					else
					{
						flag3 = true;
					}
				}
				if (!flag3 || !flag2)
				{
					flag = true;
					Player player3 = GameController.activePlayers[0];
					if (player3.character != null)
					{
						player3.character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
					}
					GameController.instance.winningPlayer = player3;
					GameController.lastWinningPlayer = player3;
				}
			}
			if (flag)
			{
				SoundController.PlaySoundEffect("VictorySting", 0.5f);
				SoundController.StopMusic();
				GameController.instance.state = GameState.RoundFinished;
			}
			return;
		}
		if (gotPoint != null)
		{
			if (hits <= 0)
			{
				hits = 1;
			}
			if (GameController.isTeamMode)
			{
				if (gotPoint.team == Team.Blue)
				{
					GameController.instance.blueTeamScore += hits;
				}
				else
				{
					GameController.instance.redTeamScore += hits;
				}
			}
			else
			{
				gotPoint.score += hits;
			}
			bool flag4;
			if (GameController.isTeamMode)
			{
				flag4 = ((gotPoint.team == Team.Red && GameController.instance.redTeamScore >= GameController.scoreToWin)
					|| (gotPoint.team == Team.Blue && GameController.instance.blueTeamScore >= GameController.scoreToWin));
			}
			else
			{
				flag4 = (gotPoint.score >= GameController.scoreToWin);
			}
			if (flag4)
			{
				SoundController.PlaySoundEffect("VictorySting", 0.5f);
				SoundController.StopMusic();
				GameController.instance.state = GameState.RoundFinished;
				GameController.instance.GetPlayerScoreDisplay(gotPoint).TemorarilyDisplay("WINNER ! ! !", 5f);
				if (gotPoint.character != null)
				{
					gotPoint.character.GetComponent<ScorePlum>().ShowText("WIN!", 5f);
				}
				GameController.instance.winningPlayer = gotPoint;
				GameController.lastWinningPlayer = gotPoint;
			}
			else
			{
				GameController.instance.GetPlayerScoreDisplay(gotPoint).TemorarilyDisplay("+" + hits.ToString(), 2f);
				if (gotPoint.character != null)
				{
					gotPoint.character.GetComponent<ScorePlum>().ShowText("+" + hits.ToString(), 2f);
				}
			}
		}
		else if (gotKilled != null)
		{
		}
		GameController.instance.SortScoreboard();
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0002C908 File Offset: 0x0002AD08
	private PlayerScoreDisplay GetPlayerScoreDisplay(Player player)
	{
		if (!GameController.isTeamMode)
		{
			for (int i = 0; i < this.playerScoreDisplays.Count; i++)
			{
				if (this.playerScoreDisplays[i].player == player)
				{
					return this.playerScoreDisplays[i];
				}
			}
			return null;
		}
		if (player.team == Team.Red)
		{
			return this.redTeamScoreDisplay;
		}
		return this.blueTeamScoreDisplay;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0002C97C File Offset: 0x0002AD7C
	public void OnGUI()
	{
		if (this.showGui)
		{
			GUILayout.BeginArea(new Rect(0f, 0f, 400f, 400f));
			GameController.charactersBounceEachOther = GUILayout.Toggle(GameController.charactersBounceEachOther, "Characters Bounce Each Other", new GUILayoutOption[0]);
			GameController.weirdBounceTrajectories = GUILayout.Toggle(GameController.weirdBounceTrajectories, "Weird Bounce Trajectories", new GUILayoutOption[0]);
			GameController.onlyBounceBeforeRecover = GUILayout.Toggle(GameController.onlyBounceBeforeRecover, "Only Bounce Before Recover", new GUILayoutOption[0]);
			GUILayout.EndArea();
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0002CA08 File Offset: 0x0002AE08
	public static Color GetAvailableColor()
	{
		Color result = GameController.instance.availableColors[0];
		GameController.instance.availableColors.RemoveAt(0);
		return result;
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0002CA4D File Offset: 0x0002AE4D
	public static void ReturnColor(Color color)
	{
		GameController.instance.availableColors.Add(color);
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0002CA5F File Offset: 0x0002AE5F
	public static void ReturnPlayer(Player player)
	{
		GameController.activePlayers.Remove(player);
		GameController.inactivePlayers.Add(player);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0002CA78 File Offset: 0x0002AE78
	public static List<Player> GetLeadingPlayers()
	{
		int num = -1;
		List<Player> list = new List<Player>();
		foreach (Player player in GameController.activePlayers)
		{
			if (player.roundWins == num)
			{
				list.Add(player);
			}
			else if (player.roundWins > num)
			{
				num = player.roundWins;
				list.Clear();
				list.Add(player);
			}
		}
		return list;
	}

	public static int GetHighestScore() {
		List<Player> players = GameController.GetLeadingPlayers ();
		return players[0].score;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0002CB10 File Offset: 0x0002AF10
	public static bool AreAnyPlayersTiedForVictory()
	{
		int num = -1;
		List<Player> list = new List<Player>();
		foreach (Player player in GameController.activePlayers)
		{
			if (player.roundWins == num)
			{
				list.Add(player);
			}
			else if (player.roundWins > num)
			{
				num = player.roundWins;
				list.Clear();
				list.Add(player);
			}
		}
		if (GameController.isTeamMode)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (Player player2 in list)
			{
				if (player2.team == Team.Red)
				{
					flag = true;
				}
				if (player2.team == Team.Blue)
				{
					flag2 = true;
				}
			}
			return flag && flag2;
		}
		return list.Count > 1;
	}

	public static bool charactersBounceEachOther = true;
	public static bool weirdBounceTrajectories = false;
	public static bool onlyBounceBeforeRecover = true;
	public static bool flyEnabled = true;
	public static int scoreToWin = 5;

	public static bool isFreePlay = false;
	public static int winsNeeded = 3;

	// Token: 0x040004CD RID: 1229
	public static bool allowTeamMode = true;

	// Token: 0x040004CE RID: 1230
	public static List<Player> activePlayers = new List<Player>();

	// Token: 0x040004CF RID: 1231
	private static List<Player> inactivePlayers;

	// Token: 0x040004D0 RID: 1232
	private GameState state;

	// Token: 0x040004D1 RID: 1233
	public static Color overallWinnerColor = Color.red;

	// Token: 0x040004D2 RID: 1234
	public Character characterPrefab;

	// Token: 0x040004D3 RID: 1235
	public Fly flyPrefab;

	// Token: 0x040004D4 RID: 1236
	public Fly activeFly;

	// Token: 0x040004D5 RID: 1237
	private float flySpawnDelay;

	// Token: 0x040004D6 RID: 1238
	public SpriteRenderer offscreenDotPrefab;
	public Canvas scoreCanvas;
	public List<PlayerScoreDisplay> playerScoreDisplays;
	public PlayerScoreDisplay scoreDisplayPrefab;
	private static GameController instance;
	public Color[] playerColors;
	private List<Color> availableColors;
	public bool isJoinScreen;
	public static string[] levelNames = new string[]
	{
		"1BusStop",
		"2DownSmash",
		"3Moon",
		"4FinalFrogstination",
		"5Skyline",
		"6Finale"
	};

	public JoinCanvas[] joinCanvas;
	public BackButton joinScreenBackButton;
	private float finishDelay = 7.5f;
	public Text joinCountdownText;
	public Text joinGameModeText;
	public static bool isTeamMode;
	public static bool playersCanDropIn;
	public static bool isShowDown;
	private int redTeamScore;
	private int blueTeamScore;
	private PlayerScoreDisplay redTeamScoreDisplay;
	private PlayerScoreDisplay blueTeamScoreDisplay;
	private InputState input = new InputState();
	private InputState combinedInput = new InputState();
	public static int levelNo;
	private Player winningPlayer;
	private bool showGui;
}
