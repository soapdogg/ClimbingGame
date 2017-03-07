using UnityEngine;
using UnityEngine.UI;

public class OverlayManager :MonoBehaviour, IManager
{
	public static OverlayManager singleton { get; private set; }

	public Text timeText, startText, pauseText, difficultyText, countdownText;

	private float startTime, initialTime, elapsedTime;

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void UpdateTimer()
	{
		elapsedTime = Time.time - startTime + initialTime;
		timeText.text = string.Format ("{0:0.00}", Mathf.Round (elapsedTime * 100.0f) / 100.0f); 
	}

	public void UpdateCountdown()
	{
		elapsedTime = Time.time - startTime + initialTime;
		countdownText.text = string.Format ("{0:0}", Mathf.Ceil(3 - Mathf.Round (elapsedTime * 100.0f) / 100.0f)); 
		if (elapsedTime >= 3) {
			SetStartTimeToNow ();
			SetInitialTime ();
			CoinGameManager.singleton.SetStateToRunning();
			CoinManager.singleton.GenerateCoins();
			OverlayManager.singleton.EnableStartVisuals(true);
		}
	}

	public void SetStartTimeToNow()
	{
		startTime = Time.time;
	}

	public void ResetTimes()
	{
		startTime = 0f;
		elapsedTime = 0f;
		initialTime = 0f;	
	}

    public void SetInitialTime()
    {
        initialTime = Time.time - startTime + initialTime;
    }

    public void Initialize()
	{
		ResetTimes ();
		EnableStartVisuals (false);
		SetDifficultyText ("Easy");
	}

	public void EnableDifficultyVisuals(bool enable)
	{
		startText.enabled = !enable;	
		difficultyText.enabled = !enable;
	}

	public void EnableStartVisuals(bool enable)
	{
		startText.enabled = !enable;
		countdownText.enabled = false;
		pauseText.enabled = enable;
		timeText.enabled = enable;
		difficultyText.enabled = !enable;
	}

	public void EnableCountdownVisuals(bool enable){
		countdownText.enabled = enable;
		startText.enabled = false;
		difficultyText.enabled = false;
	}

	public void EnablePauseVisuals(bool enable)
	{
		pauseText.enabled = !enable;
	}

	public void SetDifficultyText(string difficulty)
	{
		difficultyText.text = "Change Difficulty\nCurrent: " + difficulty; 	
	}
}
