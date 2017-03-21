using UnityEngine;
using UnityEngine.UI;

/**
 * This class handles:
 * 		Initializing the GUI objects for the high score
 * 		These GUI objects will only be visible if the user's score is a high score
 */
public class HighScoreMenuScript : MonoBehaviour, IManager
{
	public static HighScoreMenuScript singleton { get; private set; }

	//input field for the user's name
	private InputField submitHighScoreName;
	//button to submit the high score
	private Button submitHighScore;

	void Start ()
	{
		singleton = this;
		Initialize ();
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
	}

	public void SetEnabled (bool enabled)
	{
		if (enabled) {
			submitHighScore.transform.localScale = new Vector3 (1f, 1f, 1f);
			submitHighScoreName.transform.localScale = new Vector3 (1f, 1f, 1f);
		} else {
			submitHighScore.transform.localScale = new Vector3 (0f, 0f, 0f);
			submitHighScoreName.transform.localScale = new Vector3 (0f, 0f, 0f);
		}
		submitHighScore.interactable = enabled;
		submitHighScoreName.enabled = enabled;
	}

	public string GetUserInputName ()
	{
		return submitHighScoreName.text.ToString ();
	}

}