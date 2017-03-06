using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour, IManager
{
	public static PauseMenuManager singleton;

	public Canvas pauseMenu;

	private PauseMenuManager(){}

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void ResetPressed()
	{
		Debug.Log("Coin Game: ResetGamePressed()");
		CoinGameManager cgs = CoinGameManager.singleton;
		cgs.SetStateToNew ();
		OverlayManager.singleton.ResetTimes ();
		OverlayManager.singleton.EnableStartVisuals (false);
		OverlayManager.singleton.SetDifficultyText ();
		EnablePauseVisuals (false);
		CoinManager.singleton.Initialize ();
	}

	public void ResumePressed()
	{
		Debug.Log("Coin Game: ResumePressed()");
		CoinGameManager.singleton.SetStateToRunning ();
		OverlayManager.singleton.SetStartTimeToNow ();
		EnablePauseVisuals (false);
		OverlayManager.singleton.EnablePauseVisuals (false);
	}

	public void EnablePauseVisuals(bool enable)
	{
		pauseMenu.enabled = enable;
	}

	public void Initialize()
	{
		pauseMenu.enabled = false;
	}
}

