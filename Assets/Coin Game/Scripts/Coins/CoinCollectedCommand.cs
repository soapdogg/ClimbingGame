using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

public class CoinCollectedCommand : MonoBehaviour, ICommand
{
	public GameObject coin;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Object: " + this.name + " collided with: " + other.name);
        //if (other.name.Equals("13_Hand_Left") || other.name.Equals("23_Hand_Right")) { 
            Execute();
        //}
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
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.collider.name);
        }
		if (CoinGameManager.singleton.GetGameState () == CoinGameManager.GameState.GameRunning) 
		{
			transform.Rotate (new Vector3 (0, 200, 0) * Time.deltaTime);	
		}
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}

