using UnityEngine;
using UnityEngine.EventSystems;

public class MediumScript: MonoBehaviour, IPointerEnterHandler
{
	void OnTriggerEnter2D(Collider2D other)
	{
		MediumPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		MediumPressed();
	}

	private void MediumPressed()
	{
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		cgs.SetDifficulty (CoinGameScript.Difficulty.Medium);
		Debug.Log("Difficulty Set to Medium");
	}
}
