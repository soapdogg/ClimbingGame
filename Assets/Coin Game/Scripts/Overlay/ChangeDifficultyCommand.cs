using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeDifficultyCommand: MonoBehaviour, IPointerEnterHandler
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		ChangeDifficultyPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ChangeDifficultyPressed();
	}

	private void ChangeDifficultyPressed()
	{
		OverlayManager.singleton.ChangeDifficultyPressed ();	
	}
}
