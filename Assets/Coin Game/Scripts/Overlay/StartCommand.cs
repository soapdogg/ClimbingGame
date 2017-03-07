using UnityEngine;
using UnityEngine.EventSystems;

public class StartCommand: MonoBehaviour, ICommand
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
        Debug.Log("Coin Game: StartPressed()");
        OverlayManager.singleton.SetStartTimeToNow();
		CoinGameManager.singleton.SetStateToCountdown();
		OverlayManager.singleton.EnableCountdownVisuals (true);
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
		Execute();
	}
}
