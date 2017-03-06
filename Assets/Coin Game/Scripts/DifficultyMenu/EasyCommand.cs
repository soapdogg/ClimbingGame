using UnityEngine;
using UnityEngine.EventSystems;

public class EasyCommand: MonoBehaviour, IPointerEnterHandler, ICommand
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		EasyPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		EasyPressed();
	}

	private void EasyPressed()
	{
		DifficultyMenuManager.singleton.EasyPressed ();
	}
}
