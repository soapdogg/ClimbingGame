using UnityEngine;
using UnityEngine.EventSystems;

public class ViewHighScoreCommand: MonoBehaviour, ICommand
{
	public void OnTriggerEnter2D (Collider2D other)
	{
		Execute ();
	}

	public void OnMouseEnter ()
	{
		Execute ();
	}

	public void Execute ()
	{
		HighScoreManager.singleton.EnableHighScoreVisuals (true);
		EndGameMenuManager.singleton.EnableEndGameVisuals (false);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Execute ();
	}
}
