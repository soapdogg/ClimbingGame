using UnityEngine;
using UnityEngine.EventSystems;

public class ViewHighScoreCommand: MonoBehaviour, ICommand
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
        Debug.Log("Todo: make high score scene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
