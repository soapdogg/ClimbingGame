using UnityEngine;
using UnityEngine.EventSystems;

public class HardCommand: MonoBehaviour, ICommand
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
        DifficultyMenuManager.singleton.SetDifficultyToHard();
        DifficultyMenuManager.singleton.DifficultyChanged();
        Debug.Log("Difficulty Set to Hard");
    }
}
