using UnityEngine;
using UnityEngine.EventSystems;

public class NoCommand : MonoBehaviour, ICommand
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
        MainMenuOverlayManager.singleton.exitText.enabled = true;
        MainMenuOverlayManager.singleton.startText.enabled = true;
        QuitMenuManager.singleton.quitMenu.enabled = false;
        Debug.Log("Main Menu: NoPressed()");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
