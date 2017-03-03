using UnityEngine;
using UnityEngine.EventSystems;

public class YesScript : MonoBehaviour, IPointerEnterHandler
{

    void OnTriggerEnter2D(Collider2D other)
    {
        YesPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        YesPressed();
    }

    private void YesPressed()
    {
        Debug.Log("Main Menu: YesPressed()");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
