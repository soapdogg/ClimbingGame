using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

public class CoinGameScript : MonoBehaviour
{
	public Canvas pauseMenu, endGameMenu, difficultyMenu;
	public Text timeText, startText, pauseText, difficultyText;
	public GameObject skeleton;

	private List<GameObject> listOfCoins;
	private float startTime, elapsedTime, initialTime;
	private Button submitHighScore;
	private InputField submitHighScoreName;
	private GameState currentState;
	private bool canPressSpace;
	private Difficulty currentDifficulty;

	public enum GameState
	{
		NewGame,
		GameRunning,
		GameStopped
	}

	private enum Difficulty
	{
		Easy,
		Medium,
		Hard}

	;

	void Start ()
	{
		listOfCoins = new List<GameObject> ();
		currentDifficulty = Difficulty.Easy;
		InitializeGame ();
	}

	void Update ()
	{
		if (currentState == GameState.GameRunning) {
			//update timer
			elapsedTime = Time.time - startTime + initialTime;
			timeText.text = string.Format ("{0:0.00}", Mathf.Round (elapsedTime * 100.0f) / 100.0f);

			//debug mode - delete coin on click
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 10f))
					Debug.DrawRay (ray.origin, hit.point);
				if (hit.collider != null)
					RemoveCoin (GameObject.Find (hit.collider.gameObject.name));
			}
		}

		//spacebar can start timer
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (canPressSpace) {
				canPressSpace = false;
				if (currentState == GameState.NewGame)
					StartPressed ();
			}
		}
		if (Input.GetKeyUp (KeyCode.Space))
			canPressSpace = true;
	}

	public void ResumePressed ()
	{
		Debug.Log ("Coin Game: ResumePressed()");
		EnablePauseVisuals (false);
		currentState = GameState.GameRunning;
		startTime = Time.time;
		SetSkeletonActive (true);
	}

	public void GoToMainPressed ()
	{
		Debug.Log ("Coin Game: GoToMainPressed()");
		SceneManager.LoadScene ("MainMenu");
	}

	public void StartPressed ()
	{
		if (currentState == GameState.NewGame) {
			startTime = Time.time;
			currentState = GameState.GameRunning;
			EnableStartVisuals (true);
			GenerateCoins ();
			SetSkeletonActive (true);
		}
	}

	public void PausePressed ()
	{
		Debug.Log ("Coin Game: PausePressed()");
		if (currentState == GameState.GameRunning) {
			currentState = GameState.GameStopped;
			EnablePauseVisuals (true);
			initialTime = Time.time - startTime + initialTime;
			SetSkeletonActive (false);
		}
	}

	public void ResetGamePressed ()
	{
		Debug.Log ("Coin Game: ResetGamePressed()");
		while (listOfCoins.Count > 0) {
			GameObject obj = listOfCoins [0];
			Destroy (obj);
			listOfCoins.Remove (obj);
		}
		SetSkeletonActive (true);
		InitializeGame ();
	}

	public void PlayAgainPressed ()
	{
		InitializeGame ();
	}

	public void EnterNewHighScorePressed ()
	{
		InputField nameField = GameObject.FindGameObjectWithTag ("CoinGameScoreName").GetComponent<InputField> () as InputField;
		if (nameField.text.Length > 0) {
			HighScoreScript.writeHighScore (nameField.text.ToLower (), elapsedTime);
			submitHighScore.interactable = false;
			submitHighScoreName.enabled = false;
			HighScorePressed ();
		} else {
			Debug.Log ("Please enter your name first!");
		}
	}

	public void HighScorePressed ()
	{
		//temp solution
		List<HighScore> scores = HighScoreScript.getOrderedScores ();
		for (int i = 0; i < scores.Count; i++) {
			Debug.Log (String.Format ("{0}) {1}, {2}", i + 1, scores [i].name, scores [i].score));
		}
	}



	public void ChangeDifficultyPressed ()
	{
		Debug.Log ("Coin Game: Change Difficulty Pressed()");
		EnableDifficultyVisuals (true);
	}

	public void EasyPressed ()
	{
		SetDifficulty (Difficulty.Easy);
		Debug.Log ("Difficulty Set to Easy");
	}

	public void MediumPressed ()
	{
		SetDifficulty (Difficulty.Medium);
		Debug.Log ("Difficulty Set to Medium");
	}

	public void HardPressed ()
	{
		SetDifficulty (Difficulty.Hard);
		Debug.Log ("Difficulty Set to Hard");
	}

	private void SetDifficulty (Difficulty d)
	{
		currentDifficulty = d;
		SetDifficultyText ();
		EnableDifficultyVisuals (false);
	}

	private ParticleSystem ps;

	private void RemoveCoin (GameObject obj)
	{
		ps = GameObject.FindGameObjectWithTag ("CoinParticleSystem").GetComponent<ParticleSystem> () as ParticleSystem;
		ps.transform.position = obj.transform.position;
		ps.Play ();
		listOfCoins.Remove (obj);
		Destroy (obj);
		if (listOfCoins.Count == 0) {
			endGameMenu.enabled = true;
			pauseText.enabled = false;
			currentState = GameState.GameStopped;
			Debug.Log ("Game over! Time = " + elapsedTime);
			if (HighScoreScript.isHighScore (elapsedTime)) {
				Debug.Log ("That's a high score! Enter your name");
				submitHighScore.transform.localScale = new Vector3 (1f, 1f, 1f); //show
				submitHighScoreName.transform.localScale = new Vector3 (1f, 1f, 1f); //show
			} else {
				Debug.Log ("No high score :(");
			}
		}
	}

	private void SetSkeletonActive (bool active)
	{
		skeleton.SetActive (active);
	}

	private void InitializeGame ()
	{
		startTime = 0f;
		elapsedTime = 0f;
		initialTime = 0f;
		currentState = GameState.NewGame;
		canPressSpace = true;
		DisableMenus ();
		EnableStartVisuals (false);
		SetDifficultyText ();
		if (submitHighScore == null) {
			submitHighScore = GameObject.FindGameObjectWithTag ("CoinGameSubmitHighScoreButton").GetComponent<Button> () as Button;
			submitHighScoreName = GameObject.FindGameObjectWithTag ("CoinGameScoreName").GetComponent<InputField> () as InputField;
		}
		submitHighScore.interactable = true;
		submitHighScoreName.enabled = true;
		submitHighScore.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially
		submitHighScoreName.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially
		skeleton = GameObject.Find ("SkeletonPoints");
		SetSkeletonActive (false);
	}

	private void SetDifficultyText ()
	{
		difficultyText.text = "Change Difficulty\nCurrent: " + currentDifficulty.ToString (); 	
	}

	private void GenerateCoins ()
	{
		string path = GetPresetCoinPath ();
		StreamReader sr = new StreamReader (path);
		int counter = 0;
		while (!sr.EndOfStream) {
			string line = sr.ReadLine ();
			string[] lineSplit = line.Split (',');
			float x = float.Parse (lineSplit [0]);
			float y = float.Parse (lineSplit [1]);
			Coin c = new Coin (counter++, x, y);
			listOfCoins.Add (c.coinObject);
		}
		sr.Close ();

	}

	private string GetPresetCoinPath ()
	{
		string diff = currentDifficulty.ToString ();
		string presetNum = UnityEngine.Random.Range (1, 5).ToString ();
		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "presets");
		return Path.Combine (path, diff + presetNum + ".txt");
	}

	private void EnableDifficultyVisuals (bool enable)
	{
		difficultyMenu.enabled = enable;
		difficultyText.enabled = !enable;
		startText.enabled = !enable;	
	}

	private void EnableStartVisuals (bool enable)
	{
		startText.enabled = !enable;
		pauseText.enabled = enable;
		timeText.enabled = enable;
		difficultyText.enabled = !enable;	
	}

	private void EnablePauseVisuals (bool enable)
	{
		pauseMenu.enabled = enable;
		pauseText.enabled = !enable;
	}

	private void DisableMenus ()
	{
		pauseMenu.enabled = false;
		endGameMenu.enabled = false;
		difficultyMenu.enabled = false;
	}
}
