using UnityEngine;
using UnityEngine.EventSystems;

public class PauseCommand: MonoBehaviour, IPointerEnterHandler
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        PausePressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PausePressed();
    }

    private void PausePressed()
    {
		OverlayManager.singleton.PausePressed ();	
    }
}
