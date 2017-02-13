using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoinGameScript : MonoBehaviour
{
	private const float COIN_X_BOUND = 8f;
    private const float COIN_Y_BOUND = 4f;
    public Canvas pauseMenu, endGameMenu;
	public Text timeText, startText, pauseText;
    public GameObject skeleton, skeletonPoints;

	private List<GameObject> listOfCoins;
    private float startTime, elapsedTime, initialTime;
    private GameState currentState;
    private bool canPressSpace;

    public enum GameState { NewGame, GameRunning, GameStopped }

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
        SetSkeletonActive(true);
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
            SetSkeletonActive(true);
            
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
            SetSkeletonActive(false);
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
        SetSkeletonActive(true);
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

    private void SetSkeletonActive(bool active)
    {
        skeleton.SetActive(active);
        skeletonPoints.SetActive(active);
    }

	private void InitializeGame()
	{
		Debug.Log("Coin Game: InitializeGame()");
		startTime = 0f;
		elapsedTime = 0f;
		initialTime = 0f;
		currentState = GameState.NewGame;
		canPressSpace = true;
		pauseMenu.enabled = false;
		endGameMenu.enabled = false;
		pauseText.enabled = false;
		startText.enabled = true;
		timeText.text = string.Format("{0:0.00}", Mathf.Round(elapsedTime * 100.0f) / 100.0f);
		for (int i = 0; i < 5; i++)
		{
			// TODO: make sure they don't overlap
			// draggable?
			listOfCoins.Add(new Coin(i, Random.Range(-COIN_X_BOUND, COIN_X_BOUND), Random.Range(-COIN_Y_BOUND, COIN_Y_BOUND)).coinObject);
		}
        skeleton = GameObject.Find("Skeleton");
        skeletonPoints = GameObject.Find("SkeletonPoints");
        SetSkeletonActive(false);

    }
}
