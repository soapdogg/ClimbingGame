using UnityEngine;

public class EndGameMenuManager : MonoBehaviour, IManager
{
	public Canvas endGameMenu;

	public static EndGameMenuManager singleton { get; private set; }

	private EndGameMenuManager ()
	{
	}

	void Start ()
	{
		singleton = this;
		Initialize ();
	}

	public void Initialize ()
	{
		endGameMenu.enabled = false;
	}

	public void EnableEndGameVisuals (bool enable)
	{
		endGameMenu.enabled = enable;
        if(enable && HighScoreManager.singleton.IsHighScore(OverlayManager.singleton.GetElapsedTime()))
        {
            HighScoreMenuScript.singleton.SetEnabled(true);
        }
    }

}