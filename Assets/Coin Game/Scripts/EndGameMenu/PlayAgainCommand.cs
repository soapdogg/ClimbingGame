using UnityEngine;
using UnityEngine.EventSystems;

public class PlayAgainCommand: MonoBehaviour, IPointerEnterHandler
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		PlayAgainPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PlayAgainPressed();
	}

	private void PlayAgainPressed()
	{
		EndGameMenuManager.singleton.PlayAgainPressed ();	
	}
}
