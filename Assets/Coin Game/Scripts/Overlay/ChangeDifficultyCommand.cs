using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeDifficultyCommand: MonoBehaviour, ICommand
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		Execute();
	}

    public void OnMouseEnter()
    {
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Coin Game: Change Difficulty Pressed()");
        OverlayManager.singleton.EnableDifficultyVisuals(true);
        DifficultyMenuManager.singleton.EnableDifficultyVisuals(true);
        OverlayManager.singleton.EnableDifficultyVisuals(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
		Execute();
	}
}
