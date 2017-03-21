using UnityEngine;
using UnityEngine.EventSystems;

/*
 *  This class handles:
 *		The "click" of the button to save the user's high score
 */
public class SaveHighScore: MonoBehaviour, ICommand
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
		HighScoreManager.singleton.WriteHighScore (
			HighScoreMenuScript.singleton.GetUserInputName (), 
			OverlayManager.singleton.GetElapsedTime ()
		);

		//View the high scores after they are submitted
		ViewHighScoreCommand.singleton.Execute ();
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Execute ();
	}
}
