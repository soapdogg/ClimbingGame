using UnityEngine;
using UnityEngine.EventSystems;

public class MediumCommand: MonoBehaviour, ICommand
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
        DifficultyMenuManager.singleton.SetDifficultyToMedium();
        DifficultyMenuManager.singleton.DifficultyChanged();
        Debug.Log("Difficulty Set to Medium");
    }
}
