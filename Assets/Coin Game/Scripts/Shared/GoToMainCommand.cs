using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoToMainCommand: MonoBehaviour, IPointerEnterHandler
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        GoToMainPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GoToMainPressed();
    }

    private void GoToMainPressed()
    {
		Debug.Log("Coin Game: GoToMainPressed()");
        SceneManager.LoadScene("MainMenu");
    }
}
