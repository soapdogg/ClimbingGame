using UnityEngine;
using UnityEngine.EventSystems;

public class StartCommand: MonoBehaviour, ICommand
{

	public void OnTriggerEnter2D (Collider2D other)
	{
		Execute ();
	}

	public void OnMouseEnter ()
	{
		Execute ();
	}

	public void Execute ()
	{
		Debug.Log ("Coin Game: StartPressed()");
		OverlayManager.singleton.SetStartTimeToNow ();
		CoinGameManager.singleton.SetStateToRunning ();
		CoinManager.singleton.GenerateCoins ();
		OverlayManager.singleton.EnableStartVisuals (true);
		HighScoreManager.singleton.EnableHighScoreVisuals (false);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Execute ();
	}
}
