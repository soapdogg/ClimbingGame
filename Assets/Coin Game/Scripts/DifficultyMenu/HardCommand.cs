using UnityEngine;
using UnityEngine.EventSystems;

public class HardCommand: MonoBehaviour, ICommand
{
    public void OnTriggerEnter2D(Collider2D other)
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
        DifficultyMenuManager.singleton.SetDifficultyToHard();
        DifficultyMenuManager.singleton.DifficultyChanged();
        Debug.Log("Difficulty Set to Hard");
    }
}
