using UnityEngine;
using UnityEngine.EventSystems;

public class ExitCommand : MonoBehaviour, ICommand
{
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }

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
        MainMenuOverlayManager.singleton.exitText.enabled = false;
        MainMenuOverlayManager.singleton.startText.enabled = false;
        QuitMenuManager.singleton.quitMenu.enabled = true;
        Debug.Log("Main Menu: ExitPressed()");
    }
}
