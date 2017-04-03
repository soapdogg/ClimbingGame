using UnityEngine;

public class CoinGameManager : MonoBehaviour, IManager
{
	public static CoinGameManager singleton { get; private set; }

    public GameObject skeleton;
	public AudioSource backgroundMusic;
    private GameState currentState;
    public enum GameState { NewGame, GameRunning, GameStopped, Countdown }

	private CoinGameManager(){}

	void Start ()
	{
		singleton = this;
		Initialize ();
		Debug.Log("Coin Game Scene Entered");
	}

	void Update ()
	{
		if (currentState == GameState.GameRunning) {
			OverlayManager.singleton.UpdateTimer ();
		} else if (currentState == GameState.Countdown) {
			OverlayManager.singleton.UpdateCountdown ();
		}
	}

	public void SetStateToStopped()
	{
		currentState = GameState.GameStopped;
		backgroundMusic.Play ();
	}

	public void SetStateToRunning()
	{
		currentState = GameState.GameRunning;
		backgroundMusic.Play ();
	}

	public void SetStateToNew()
	{
		currentState = GameState.NewGame;
	}

	public void SetStateToCountdown()
	{
		currentState = GameState.Countdown;
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
