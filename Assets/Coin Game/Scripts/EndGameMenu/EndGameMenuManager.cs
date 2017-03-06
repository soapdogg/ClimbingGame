using UnityEngine;
using UnityEngine.EventSystems;

public class EndGameMenuManager : MonoBehaviour
{
	public Canvas endGameMenu;

	public static EndGameMenuManager singleton { get; private set; }

	private EndGameMenuManager(){}

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void PlayAgainPressed()
	{
		Debug.Log("Coin Game: PlayAgainPressed()");
		CoinGameManager cgs = CoinGameManager.singleton;
		cgs.SetStateToNew ();
		endGameMenu.enabled = false;
		OverlayManager.singleton.EnableStartVisuals (false);
		OverlayManager.singleton.ResetTimes ();
		CoinManager.singleton.Initialize ();
	}

	public void ViewHighScorePressed()
	{
		Debug.Log("Todo: make high score scene");
	}

	private void Initialize()
	{
		endGameMenu.enabled = false;
	}
}