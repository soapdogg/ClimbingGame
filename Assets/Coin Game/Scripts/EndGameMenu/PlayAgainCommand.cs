using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAgainCommand: MonoBehaviour, ICommand
{
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
        Debug.Log("Coin Game: PlayAgainPressed()");
        CoinGameManager.singleton.SetStateToNew();
        EndGameMenuManager.singleton.endGameMenu.enabled = false;
        OverlayManager.singleton.EnableStartVisuals(false);
        OverlayManager.singleton.ResetTimes();
        CoinManager.singleton.Initialize();
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
		Execute();
	}
}
