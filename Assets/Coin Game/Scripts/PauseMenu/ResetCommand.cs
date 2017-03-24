using UnityEngine;
using UnityEngine.EventSystems;

public class ResetCommand: MonoBehaviour, ICommand
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
        Debug.Log("Coin Game: ResetGamePressed()");
        CoinGameManager.singleton.SetStateToNew();
        OverlayManager.singleton.ResetTimes();
        OverlayManager.singleton.EnableStartVisuals(false);
        PauseMenuManager.singleton.EnablePauseVisuals(false);
        CoinManager.singleton.Initialize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
