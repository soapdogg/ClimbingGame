using UnityEngine;
using UnityEngine.EventSystems;

public class StartCommand: MonoBehaviour, IPointerEnterHandler
{

	public void OnTriggerEnter2D(Collider2D other)
	{
		StartPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		StartPressed();
	}

	private void StartPressed()
	{
		OverlayManager.singleton.StartPressed ();
	}
}
