using UnityEngine;
using UnityEngine.EventSystems;

public class EasyScript: MonoBehaviour, IPointerEnterHandler
{
	void OnTriggerEnter2D(Collider2D other)
	{
		EasyPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		EasyPressed();
	}

	private void EasyPressed(){
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		cgs.SetDifficulty (CoinGameScript.Difficulty.Easy);
		Debug.Log("Difficulty Set to Easy");
	}
}
