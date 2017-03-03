using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeDifficultyScript: MonoBehaviour, IPointerEnterHandler
{
	void OnTriggerEnter2D(Collider2D other)
	{
		ChangeDifficultyPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ChangeDifficultyPressed();
	}

	private void ChangeDifficultyPressed()
	{
		Debug.Log ("Coin Game: Change Difficulty Pressed()");
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		cgs.EnableDifficultyVisuals (true);
	}
}
