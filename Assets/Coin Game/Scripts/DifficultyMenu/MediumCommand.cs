using UnityEngine;
using UnityEngine.EventSystems;

public class MediumCommand: MonoBehaviour, IPointerEnterHandler
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		MediumPressed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		MediumPressed();
	}

	private void MediumPressed()
	{
		DifficultyMenuManager.singleton.MediumPressed ();
	}
}
