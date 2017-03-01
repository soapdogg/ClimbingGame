using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeScript: MonoBehaviour, IPointerEnterHandler
{

    void OnTriggerEnter2D(Collider2D other)
    {
        ResumePressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ResumePressed();
    }

    private void ResumePressed()
    {
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		Debug.Log("Coin Game: ResumePressed()");
		cgs.EnablePauseVisuals (false);
		cgs.currentState = CoinGameScript.GameState.GameRunning;
		cgs.startTime = Time.time;
        cgs.SetSkeletonActive(true);
    }
}
