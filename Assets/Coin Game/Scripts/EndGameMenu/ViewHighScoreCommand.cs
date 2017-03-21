using UnityEngine;
using UnityEngine.EventSystems;

/*
 * This class handles:
 * 		The "click" of button to view the high scores
 */
public class ViewHighScoreCommand: MonoBehaviour, ICommand
{
	public static ViewHighScoreCommand singleton { get; private set; }

	void Start ()
	{
		singleton = this;
		Initialize ();
	}

	public void Initialize ()
	{

	}

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
		if (HighScoreManager.singleton.IsHighScore (OverlayManager.singleton.GetElapsedTime ())) {
			Debug.Log ("High score!");
		} else {
			Debug.Log ("No high score :( ");
		}
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Execute ();
	}
}
