using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoinGameScript : MonoBehaviour
{

	private float COIN_X_BOUND = 8f;
    private float COIN_Y_BOUND = 4f;
    public Canvas pauseMenu;
	public Canvas endGameMenu;

	private List<GameObject> listOfCoins;
    private float startTime, elapsedTime;
    private GameState currentState;
    private bool canPressSpace;

    public enum GameState { NewGame, GameRunning, GameStopped }

	void Start ()
	{
		pauseMenu = pauseMenu.GetComponent<Canvas> ();
		endGameMenu = endGameMenu.GetComponent<Canvas> ();

		listOfCoins = new List<GameObject>();
		InitializeGame ();
	}

	void Update () 
	{
		if (currentState == GameState.GameRunning)
		{
			//update timer
			elapsedTime = Time.time - startTime;

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
        pauseMenu.enabled = false;
		currentState = GameState.GameRunning;
    }

    public void GoToMainPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
		
	public void StartPressed()
    {
        if (currentState == GameState.NewGame)
        {
            startTime = Time.time;
            currentState = GameState.GameRunning;
        }
    }

    public void PausePressed()
    {
        if (currentState == GameState.GameRunning)
        {
            currentState = GameState.GameStopped;
			pauseMenu.enabled = true;
        }
    }

    public void ResetGamePressed()
	{
		while (listOfCoins.Count > 0) {
			GameObject obj = listOfCoins [0];
			Destroy (obj);
			listOfCoins.Remove (obj);
		}
		InitializeGame ();
	}

	public void PlayAgainPressed()
	{
		InitializeGame ();
	}

	public void HighScorePressed()
	{
		Debug.Log("Todo: make high score scene");
	}



	private void RemoveCoin(GameObject obj)
	{
		listOfCoins.Remove(obj);
		Destroy(obj);
		if(listOfCoins.Count == 0)
		{
			endGameMenu.enabled = true;
			currentState = GameState.GameStopped;
			Debug.Log("Game over! Time = " + elapsedTime);
		}
	}

	private void OnGUI()
    {
        new GameMenu().createMenu(10, 10, "Coin Game")
            .addGameMenuButton("Start", () => this.StartPressed())
            .addGameMenuButton("Pause", () => this.PausePressed())
            .addGameMenuButton("Reset", () => this.ResetGamePressed())
			.addGameMenuButton("High Scores", () => this.HighScorePressed())
            .build();

        createTimer();

    }
		
	private void createTimer()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 60;
        guiStyle.normal.textColor = Color.white;
        guiStyle.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(10, 10, Screen.width - 30, 100), string.Format("{0:0.00}", Mathf.Round(elapsedTime * 100.0f) / 100.0f), guiStyle);
    }
		
	private void InitializeGame()
	{
		startTime = 0f;
		elapsedTime = 0f;
		currentState = GameState.NewGame;
		canPressSpace = true;
		pauseMenu.enabled = false;
		endGameMenu.enabled = false;

		for (int i = 0; i < 5; i++)
		{
			// TODO: make sure they don't overlap
			// draggable?
			listOfCoins.Add(new Coin(i, Random.Range(-COIN_X_BOUND, COIN_X_BOUND), Random.Range(-COIN_Y_BOUND, COIN_Y_BOUND)).coinObject);
		}     	
	}
}
