using UnityEngine;
using UnityEngine.EventSystems;

public class ViewHighScoreCommand: MonoBehaviour, IPointerEnterHandler
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        HighScorePressed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighScorePressed();
    }

	private void HighScorePressed()
	{
		Debug.Log("Todo: make high score scene");
	}
}
