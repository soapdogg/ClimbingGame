using UnityEngine;
using UnityEngine.EventSystems;

public class PauseCommand: MonoBehaviour, ICommand
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
        Debug.Log("Coin Game: PausePressed()");
        CoinGameManager.singleton.SetStateToStopped();
        OverlayManager.singleton.SetInitialTime();
        OverlayManager.singleton.EnablePauseVisuals(true);
        PauseMenuManager.singleton.EnablePauseVisuals(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
