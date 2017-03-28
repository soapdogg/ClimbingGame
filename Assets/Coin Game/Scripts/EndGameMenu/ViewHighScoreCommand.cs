using System.Collections.Generic;
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

	public void OnTriggerEnter (Collider other)
	{
		Execute ();
	}

	public void OnMouseEnter ()
	{
		Execute ();
	}

	public void Execute ()
	{
		if (HighScoreMenuScript.singleton.IsViewEnabled ()) {
			Debug.Log ("Enter your name!");
		} else {
			List<HighScore> scores = HighScoreManager.singleton.GetOrderedScores ();
			Debug.Log ("found scores of length " + scores.Count);
			HighScoreDisplayManager.singleton.SetHighScoreList (scores);
			EndGameMenuManager.singleton.EnableEndGameVisuals (false);
			HighScoreMenuScript.singleton.SetEnabled (false);
		}
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		Execute ();
	}
}
