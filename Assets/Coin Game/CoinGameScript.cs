using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;

public class CoinGameScript : MonoBehaviour
{
	private const float COIN_X_BOUND = 8f;
    private const float COIN_Y_BOUND = 4f;
    public Canvas pauseMenu, endGameMenu;
	public Text timeText, startText, pauseText;

	private List<GameObject> listOfCoins;
    private float startTime, elapsedTime, initialTime;
    private GameState currentState;
    private bool canPressSpace;

    public enum GameState { NewGame, GameRunning, GameStopped }

	private enum Difficulty {EASY, MEDIUM, HARD};

	void Start ()
	{
		Debug.Log("Coin Game Scene Entered");

		listOfCoins = new List<GameObject>();
		InitializeGame ();
	}

	void Update () 
	{
		if (currentState == GameState.GameRunning)
		{
			//update timer
			elapsedTime = Time.time - startTime + initialTime;
			timeText.text = string.Format("{0:0.00}", Mathf.Round(elapsedTime * 100.0f) / 100.0f);

			//debug mode - delete coin on click
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 10f)) Debug.DrawRay(ray.origin, hit.point);
				if (hit.collider != null) RemoveCoin(GameObject.Find(hit.collider.gameObject.name));
			}
		}

		//spacebar can start timer
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(canPressSpace)
			{
				canPressSpace = false;
				if (currentState == GameState.NewGame) StartPressed();
			}
		}
		if(Input.GetKeyUp(KeyCode.Space)) canPressSpace = true;
	}

    public void ResumePressed()
    {
		Debug.Log("Coin Game: ResumePressed()");
        pauseMenu.enabled = false;
		pauseText.enabled = true;
		currentState = GameState.GameRunning;
		startTime = Time.time;
    }

    public void GoToMainPressed()
    {
		Debug.Log("Coin Game: GoToMainPressed()");
        SceneManager.LoadScene("MainMenu");
    }
		
	public void StartPressed()
    {
		Debug.Log("Coin Game: StartPressed()");
        if (currentState == GameState.NewGame)
        {
            startTime = Time.time;
            currentState = GameState.GameRunning;
			startText.enabled = false;
			pauseText.enabled = true;
        }
    }

    public void PausePressed()
    {
		Debug.Log("Coin Game: PausePressed()");
        if (currentState == GameState.GameRunning)
        {
            currentState = GameState.GameStopped;
			pauseMenu.enabled = true;
			pauseText.enabled = false;
			initialTime = Time.time - startTime + initialTime;
        }
    }

    public void ResetGamePressed()
	{
		Debug.Log("Coin Game: ResetGamePressed()");
		while (listOfCoins.Count > 0) {
			GameObject obj = listOfCoins [0];
			Destroy (obj);
			listOfCoins.Remove (obj);
		}
		InitializeGame ();
	}

	public void PlayAgainPressed()
	{
		Debug.Log("Coin Game: PlayAgainPressed()");
		InitializeGame ();
	}

	public void HighScorePressed()
	{
		Debug.Log("Todo: make high score scene");
	}
		
	private void RemoveCoin(GameObject obj)
	{
		Debug.Log("Coin Game: RemoveCoin()");
		listOfCoins.Remove(obj);
		Destroy(obj);
		if(listOfCoins.Count == 0)
		{
			endGameMenu.enabled = true;
			pauseText.enabled = false;
			currentState = GameState.GameStopped;
			Debug.Log("Game over! Time = " + elapsedTime);
		}
	}

	private void InitializeGame()
	{
		Debug.Log ("Coin Game: InitializeGame()");
		startTime = 0f;
		elapsedTime = 0f;
		initialTime = 0f;
		currentState = GameState.NewGame;
		canPressSpace = true;
		pauseMenu.enabled = false;
		endGameMenu.enabled = false;
		pauseText.enabled = false;
		startText.enabled = true;
		timeText.text = string.Format ("{0:0.00}", Mathf.Round (elapsedTime * 100.0f) / 100.0f);



		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "presets");
		path = Path.Combine(path, "easy1.txt");
		StreamReader sr = new StreamReader (path);
		int counter = 0;
		while (!sr.EndOfStream) 
		{
			string line = sr.ReadLine ();
			string[] lineSplit = line.Split (',');
			float x = float.Parse (lineSplit [0]);
			float y = float.Parse (lineSplit [1]);
			Coin c = new Coin (counter++, x, y);
			listOfCoins.Add (c.coinObject);
		}
		sr.Close ();
	}
}
