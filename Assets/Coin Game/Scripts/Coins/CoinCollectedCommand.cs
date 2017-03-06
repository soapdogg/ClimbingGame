using UnityEngine;
using UnityEngine.EventSystems;

public class CoinCollectedCommand : MonoBehaviour
{
	public GameObject coin;

	public void OnTriggerEnter2D(Collider2D other)
	{
		CoinPressed ();	
	}

	public void OnMouseEnter()
	{
		CoinPressed ();
	}
		
	void Update () 
	{
		if (CoinGameManager.singleton.GetGameState () == CoinGameManager.GameState.GameRunning) 
		{
			transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);	
		}
	}

	private void CoinPressed()
	{
		if (CoinGameManager.singleton.GetGameState () == CoinGameManager.GameState.GameRunning)
		{
			CoinManager.singleton.IncrementNumPressed ();
			coin.SetActive (false);
			Debug.Log ("Coin Collected");
		}
	}
}

