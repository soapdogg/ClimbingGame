using UnityEngine;
using UnityEngine.EventSystems;

public class YesCommand : MonoBehaviour, ICommand
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
        Debug.Log("Main Menu: YesPressed()");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
