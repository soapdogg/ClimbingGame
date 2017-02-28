using UnityEngine;
using UnityEngine.EventSystems;

public class ExitScript : MonoBehaviour, IPointerEnterHandler {

   
    public void OnPointerEnter(PointerEventData eventData)
    {
        ExitPressed();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        ExitPressed();
    }

    private void ExitPressed()
    {
        MainMenuScript mms = MainMenuScript.GetMainMenuScript();
        mms.quitMenu.enabled = true;
        mms.startText.enabled = false;
        mms.exitText.enabled = false;
        Debug.Log("Main Menu: ExitPressed()");
    }
}
