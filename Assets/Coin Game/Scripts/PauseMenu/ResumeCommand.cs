using UnityEngine;
using UnityEngine.EventSystems;

public class ResumeCommand: MonoBehaviour, IPointerEnterHandler
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        ResumePressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ResumePressed();
    }

    private void ResumePressed()
    {
		PauseMenuManager.singleton.ResumePressed ();	
    }
}
