using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeCommand: MonoBehaviour, ICommand
{

    public void OnTriggerEnter(Collider other)
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
