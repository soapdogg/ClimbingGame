using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenuScript : MonoBehaviour
{
	public static HighScoreMenuScript singleton { get; private set; }

	private InputField submitHighScoreName;
	private Button submitHighScore;

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void Initialize()
	{
		if (submitHighScore == null)
		{
			submitHighScore = GameObject.FindGameObjectWithTag ("CoinGameSubmitHighScoreButton").GetComponent<Button> () as Button;
			submitHighScoreName = GameObject.FindGameObjectWithTag ("CoinGameScoreName").GetComponent<InputField> () as InputField;
		}
		submitHighScore.interactable = true;
		submitHighScoreName.enabled = true;
		submitHighScore.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially
		submitHighScoreName.transform.localScale = new Vector3 (0f, 0f, 0f); //hide initially
	}

}