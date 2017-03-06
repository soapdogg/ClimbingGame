using UnityEngine;
using UnityEngine.UI;

public class OverlayManager :MonoBehaviour
{
	public static OverlayManager singleton { get; private set; }

	public Text timeText, startText, pauseText, difficultyText;

	private float startTime, initialTime, elapsedTime;

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void ChangeDifficultyPressed()
	{
		Debug.Log ("Coin Game: Change Difficulty Pressed()");
		EnableDifficultyVisuals (true);
		DifficultyMenuManager.singleton.EnableDifficultyVisuals (true);
		OverlayManager.singleton.EnableDifficultyVisuals (true);
	}

	public void PausePressed()
	{
		Debug.Log("Coin Game: PausePressed()");
		CoinGameManager.singleton.SetStateToStopped();
		initialTime = Time.time - startTime + initialTime;
		EnablePauseVisuals (true);
		PauseMenuManager.singleton.EnablePauseVisuals (true);
	}

	public void StartPressed()
	{
		Debug.Log("Coin Game: StartPressed()");
		startTime = Time.time;
		CoinGameManager cms = CoinGameManager.singleton;
		cms.SetStateToRunning ();
		CoinManager.singleton.GenerateCoins ();
		EnableStartVisuals (true);
	}

	public void UpdateTimer()
	{
		elapsedTime = Time.time - startTime + initialTime;
		timeText.text = string.Format ("{0:0.00}", Mathf.Round (elapsedTime * 100.0f) / 100.0f); 
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

	private void Initialize()
	{
		ResetTimes ();
		EnableStartVisuals (false);
		SetDifficultyText ();
	}

	public void EnableDifficultyVisuals(bool enable)
	{
		startText.enabled = !enable;	
		difficultyText.enabled = !enable;
	}

	public void EnableStartVisuals(bool enable)
	{
		startText.enabled = !enable;
		pauseText.enabled = enable;
		timeText.enabled = enable;
		difficultyText.enabled = !enable;
	}

	public void EnablePauseVisuals(bool enable)
	{
		pauseText.enabled = !enable;
	}

	public void SetDifficultyText()
	{
		DifficultyMenuManager dms = DifficultyMenuManager.singleton;
		string currentDifficulty = dms == null ? "Easy" : dms.currentDifficulty.ToString ();
		difficultyText.text = "Change Difficulty\nCurrent: " + currentDifficulty; 	
	}
}
