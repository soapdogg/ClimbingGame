using UnityEngine;
using UnityEngine.EventSystems;

public class ResetCommand: MonoBehaviour, IPointerEnterHandler
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        ResetPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ResetPressed();
    }

    private void ResetPressed()
    {
		PauseMenuManager.singleton.ResetPressed ();
    }
}
