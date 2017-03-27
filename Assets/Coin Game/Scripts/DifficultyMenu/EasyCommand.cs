using UnityEngine;
using UnityEngine.EventSystems;

public class EasyCommand: MonoBehaviour, ICommand
{
	public void OnTriggerEnter(Collider other)
	{
	    Execute();
	}

    public void OnMouseEnter()
    {
        Execute();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }

    public void Execute()
    {
        DifficultyMenuManager.singleton.SetDifficultyToEasy();
        DifficultyMenuManager.singleton.DifficultyChanged();
        Debug.Log("Difficulty Set to Easy");
    }
}
