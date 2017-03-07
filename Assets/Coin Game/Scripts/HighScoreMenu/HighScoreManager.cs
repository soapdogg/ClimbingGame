using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour, IManager
{

	public static HighScoreManager singleton;

	public Canvas highScoreDisplay;
	private InputField submitHighScoreName;
	private Button submitHighScore;
	public Text[] highScoreTexts = new Text[10];


	private HighScoreManager ()
	{
		
	}

	void Start ()
	{
		singleton = this;
		Initialize ();
	}

	public void EnableHighScoreVisuals (bool enable)
	{
		highScoreDisplay.enabled = enable;
	}

	public void Initialize ()
	{
		if (submitHighScore == null) {
			submitHighScore = GameObject.FindGameObjectWithTag ("CoinGameSubmitHighScoreButton").GetComponent<Button> () as Button;
			submitHighScoreName = GameObject.FindGameObjectWithTag ("CoinGameScoreName").GetComponent<InputField> () as InputField;
		}
		submitHighScore.interactable = true;
		submitHighScoreName.enabled = true;
		submitHighScore.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially
		submitHighScoreName.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially

		List<HighScore> scores = HighScoreScript.getOrderedScores ();
		for (int i = 0; i < scores.Count; i++) {
			highScoreTexts [i * 2].text = scores [i].name;
			highScoreTexts [i * 2 + 1].text = scores [i].score.ToString ();
		}

	}
}