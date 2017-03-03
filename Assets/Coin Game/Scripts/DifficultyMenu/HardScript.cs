using UnityEngine;
using UnityEngine.EventSystems;

public class HardScript: MonoBehaviour, IPointerEnterHandler
{
	void OnTriggerEnter2D(Collider2D other)
	{
		HardPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HardPressed();
	}

	private void HardPressed()
	{
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		cgs.SetDifficulty (CoinGameScript.Difficulty.Hard);
		Debug.Log("Difficulty Set to Hard");
	}
}
