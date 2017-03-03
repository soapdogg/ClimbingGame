using UnityEngine;
using UnityEngine.EventSystems;

public class NoScript : MonoBehaviour, IPointerEnterHandler
{
    void OnTriggerEnter2D(Collider2D other)
    {
        NoPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        NoPressed();
    }

    private void NoPressed()
    {
        MainMenuScript mms = MainMenuScript.GetMainMenuScript();
        mms.quitMenu.enabled = false;
        mms.startText.enabled = true;
        mms.exitText.enabled = true;
        Debug.Log("Main Menu: NoPressed()");
    }
}
