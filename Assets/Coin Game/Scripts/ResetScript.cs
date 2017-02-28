using UnityEngine;
using UnityEngine.EventSystems;

public class ResetScript: MonoBehaviour, IPointerEnterHandler
{

    void OnTriggerEnter2D(Collider2D other)
    {
        ResetPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ResetPressed();
    }

    private void ResetPressed()
    {
		CoinGameScript cgs = CoinGameScript.GetCoinGameScript ();
		Debug.Log("Coin Game: ResetGamePressed()");
		while (cgs.listOfCoins.Count > 0) {
			GameObject obj = cgs.listOfCoins [0];
			Destroy (obj);
			cgs.listOfCoins.Remove (obj);
		}
        cgs.SetSkeletonActive(true);
		cgs.InitializeGame ();
    }
}
