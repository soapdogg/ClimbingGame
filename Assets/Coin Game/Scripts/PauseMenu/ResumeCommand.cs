using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeCommand: MonoBehaviour, ICommand
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
        Debug.Log("Coin Game: ResumePressed()");
        CoinGameManager.singleton.SetStateToRunning();
        OverlayManager.singleton.SetStartTimeToNow();
        PauseMenuManager.singleton.EnablePauseVisuals(false);
        OverlayManager.singleton.EnablePauseVisuals(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
