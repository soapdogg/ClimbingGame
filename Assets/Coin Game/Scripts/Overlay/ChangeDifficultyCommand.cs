using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeDifficultyCommand: MonoBehaviour, ICommand
{
	public void OnTriggerEnter(Collider other)
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
