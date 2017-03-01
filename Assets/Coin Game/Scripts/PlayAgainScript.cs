using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAgainScript: MonoBehaviour, IPointerEnterHandler
{

	void OnTriggerEnter2D(Collider2D other)
	{
		PlayAgainPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PlayAgainPressed();
	}

	private void PlayAgainPressed()
	{
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		Debug.Log("Coin Game: PlayAgainPressed()");
		cgs.InitializeGame ();
	}
}
