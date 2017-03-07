using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayCommand : MonoBehaviour, ICommand
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
        Debug.Log("Main Menu: PlayPressed()");
        SceneManager.LoadScene("CoinGame");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
