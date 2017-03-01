using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScript: MonoBehaviour, IPointerEnterHandler
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PausePressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PausePressed();
    }

    private void PausePressed()
    {
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		Debug.Log("Coin Game: PausePressed()");
		if (cgs.currentState == CoinGameScript.GameState.GameRunning)
		{
			cgs.currentState = CoinGameScript.GameState.GameStopped;
			cgs.EnablePauseVisuals (true);
			cgs.initialTime = Time.time - cgs.startTime + cgs.initialTime;
			cgs.SetSkeletonActive(false);
		}
    }
}
