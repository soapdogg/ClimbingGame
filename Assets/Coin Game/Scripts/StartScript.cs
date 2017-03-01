using UnityEngine;
using UnityEngine.EventSystems;

public class StartScript: MonoBehaviour, IPointerEnterHandler
{

	void OnTriggerEnter2D(Collider2D other)
	{
		StartPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartPressed();
	}

	private void StartPressed()
	{
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		Debug.Log("Coin Game: StartPressed()");
        if (cgs.currentState == CoinGameScript.GameState.NewGame)
        {
            cgs.startTime = Time.time;
            cgs.currentState = CoinGameScript.GameState.GameRunning;
			cgs.EnableStartVisuals (true);
			cgs.GenerateCoins ();
            cgs.SetSkeletonActive(true);
        }
	}
}
