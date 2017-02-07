using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    private float COIN_X_BOUND = 8f;
    private float COIN_Y_BOUND = 4f;

    private List<GameObject> listOfCoins;
    private float startTime, elapsedTime;
    private GameState currentState;
    private bool canPressSpace;

    public enum GameState { NewGame, GameRunning, GameStopped }

    void Start () {
        listOfCoins = new List<GameObject>();
        startTime = 0f;
        elapsedTime = 0f;
        currentState = GameState.NewGame;
        canPressSpace = true;

        for (int i = 0; i < 5; i++)
        {
            // TODO: make sure they don't overlap
            // draggable?
            listOfCoins.Add(new Coin(i, Random.Range(-COIN_X_BOUND, COIN_X_BOUND), Random.Range(-COIN_Y_BOUND, COIN_Y_BOUND)).coinObject);
        }     
	}

    //called every frame
    void Update () {
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
                if (hit.collider != null) removeCoin(GameObject.Find(hit.collider.gameObject.name));
            }
        }

        //spacebar can start timer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canPressSpace)
            {
                canPressSpace = false;
                if (currentState == GameState.NewGame) startTimer();
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)) canPressSpace = true;
        
    }

    public void startTimer()
    {
        if (currentState == GameState.NewGame)
        {
            startTime = Time.time;
            currentState = GameState.GameRunning;
        }
    }

    void stopTimer()
    {
        if (currentState == GameState.GameRunning)
        {
            currentState = GameState.GameStopped;
        }
    }

    void resetGame()
    {
        while (listOfCoins.Count > 0)
        {
            GameObject obj = listOfCoins[0];
            Destroy(obj);
            listOfCoins.Remove(obj);
        }
        Start();
        
    }

    void removeCoin(GameObject obj)
    {
        listOfCoins.Remove(obj);
        Destroy(obj);
        if(listOfCoins.Count == 0)
        {
            stopTimer();
            Debug.Log("Game over! Time = " + elapsedTime);
        }
    }

    void launchHighScoreScene()
    {
        Debug.Log("Todo: make high score scene");
    }

    private void OnGUI()
    {
        new GameMenu().createMenu(10, 10, "Coin Game")
            .addGameMenuButton("Start", () => this.startTimer())
            .addGameMenuButton("Stop", () => this.stopTimer())
            .addGameMenuButton("Reset", () => this.resetGame())
            .addGameMenuButton("High Scores", () => this.launchHighScoreScene())
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
}
