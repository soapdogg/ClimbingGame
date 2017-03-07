using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

public class CoinCollectedCommand : MonoBehaviour, ICommand
{
	public GameObject coin;

	public void OnTriggerEnter2D(Collider2D other)
	{
		Execute();
	}

	public void OnMouseEnter()
	{
		Execute();
	}

    public void Execute()
    {
        if (CoinGameManager.singleton.GetGameState() == CoinGameManager.GameState.GameRunning)
		{
            CoinManager.singleton.IncrementNumPressed();
            coin.SetActive(false);
			CoinManager.singleton.coinSound.Play ();
            Debug.Log("Coin Collected");
        }
    }

    void Update () 
	{
		if (CoinGameManager.singleton.GetGameState () == CoinGameManager.GameState.GameRunning) 
		{
			transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);	
		}
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}

