using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinGameManager : MonoBehaviour, IManager
{
	public static CoinGameManager singleton { get; private set; }

    public GameObject skeleton;
    private GameState currentState;
    public enum GameState { NewGame, GameRunning, GameStopped }

	private CoinGameManager(){}

	void Start ()
	{
		singleton = this;
		Initialize ();
		Debug.Log("Coin Game Scene Entered");
	}

	void Update ()
	{
		if (currentState == GameState.GameRunning) 
			OverlayManager.singleton.UpdateTimer();
	}

	public void SetStateToStopped()
	{
		currentState = GameState.GameStopped;
	}

	public void SetStateToRunning()
	{
		currentState = GameState.GameRunning;
	}

	public void SetStateToNew()
	{
		currentState = GameState.NewGame;
	}

	public GameState GetGameState()
	{
		return currentState;
	}

	public void Initialize()
	{
		SetStateToNew ();	
	}
}
