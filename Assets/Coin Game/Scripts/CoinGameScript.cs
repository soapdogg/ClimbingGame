using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

public class CoinGameScript : MonoBehaviour
{
	private static CoinGameScript singleton;

	private ParticleSystem ps;
	private Difficulty currentDifficulty;
	private InputField submitHighScoreName;
	private Button submitHighScore;

    public Canvas pauseMenu, endGameMenu, difficultyMenu;
	public Text timeText, startText, pauseText, difficultyText;
    public GameObject skeleton;
	public List<GameObject> listOfCoins;
    public float startTime, elapsedTime, initialTime;
    public GameState currentState;
    public enum GameState { NewGame, GameRunning, GameStopped }
	public enum Difficulty {Easy, Medium, Hard};

	private CoinGameScript(){}

	void Start ()
	{
		singleton = this;
		Debug.Log("Coin Game Scene Entered");
		listOfCoins = new List<GameObject>();
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
			if (Input.GetMouseButtonDown (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 10f))
					Debug.DrawRay (ray.origin, hit.point);
				if (hit.collider != null)
					RemoveCoin (GameObject.Find (hit.collider.gameObject.name));
			}
		}
	}
		
	public void SetDifficulty(Difficulty d)
	{
		currentDifficulty = d;
		SetDifficultyText ();
		EnableDifficultyVisuals (false);
	}


	private void RemoveCoin (GameObject obj)
	{
		ps = GameObject.FindGameObjectWithTag ("CoinParticleSystem").GetComponent<ParticleSystem> () as ParticleSystem;
		ps.transform.position = obj.transform.position;
		ps.Play ();
		listOfCoins.Remove (obj);
		Destroy (obj);
		if (listOfCoins.Count == 0) 
		{
			endGameMenu.enabled = true;
			pauseText.enabled = false;
			currentState = GameState.GameStopped;
			Debug.Log ("Game over! Time = " + elapsedTime);
			if (HighScoreScript.isHighScore (elapsedTime))
			{
				Debug.Log ("That's a high score! Enter your name");
				submitHighScore.transform.localScale = new Vector3 (1f, 1f, 1f); //show
				submitHighScoreName.transform.localScale = new Vector3 (1f, 1f, 1f); //show
			}
			else
			{
				Debug.Log ("No high score :(");
			}
		}
	}

    public void SetSkeletonActive(bool active)
    {
        skeleton.SetActive(active);
    }

	public void InitializeGame()
	{
		startTime = 0f;
		elapsedTime = 0f;
		initialTime = 0f;
		currentState = GameState.NewGame;
		DisableMenus ();
		EnableStartVisuals (false);
		SetDifficultyText ();
		if (submitHighScore == null)
		{
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

	private void SetDifficultyText()
	{
		difficultyText.text = "Change Difficulty\nCurrent: " + currentDifficulty.ToString (); 	
	}

	public void GenerateCoins()
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
		path = Path.Combine (path, "Presets");
		return Path.Combine (path, diff + presetNum + ".txt");
	}

	public void EnableDifficultyVisuals(bool enable)
	{
		difficultyMenu.enabled = enable;
		difficultyText.enabled = !enable;
		startText.enabled = !enable;	
	}

	public void EnableStartVisuals(bool enable)
	{
		startText.enabled = !enable;
		pauseText.enabled = enable;
		timeText.enabled = enable;
		difficultyText.enabled = !enable;	
	}

	public void EnablePauseVisuals(bool enable)
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

	public static CoinGameScript GetCoinGameScript()
	{
		return singleton;
	}
}
