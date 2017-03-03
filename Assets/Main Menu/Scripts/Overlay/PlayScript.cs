using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour, IPointerEnterHandler
{

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayPressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayPressed();
    }

    private void PlayPressed()
    {
        Debug.Log("Main Menu: PlayPressed()");
        SceneManager.LoadScene("CoinGame");
    }
}
