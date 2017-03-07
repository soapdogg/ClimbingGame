using UnityEngine;
using UnityEngine.EventSystems;

public class MediumCommand: MonoBehaviour, ICommand
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
        DifficultyMenuManager.singleton.SetDifficultyToMedium();
        DifficultyMenuManager.singleton.DifficultyChanged();
        Debug.Log("Difficulty Set to Medium");
    }
}
