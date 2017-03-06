using UnityEngine;
using UnityEngine.EventSystems;

public class HardCommand: MonoBehaviour, IPointerEnterHandler
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		HardPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		HardPressed();
	}

	private void HardPressed()
	{
		DifficultyMenuManager.singleton.HardPressed ();
	}
}
