using UnityEngine;

public class DifficultyMenuManager :MonoBehaviour, IManager
{
	public Difficulty currentDifficulty { get; private set; }

	public Canvas difficultyMenu;

	public enum Difficulty
	{
Easy,
		Medium,
		Hard}

	;

	public static DifficultyMenuManager singleton { get; private set; }

	void Start ()
	{
		singleton = this;
		Initialize ();
	}

	public void SetDifficultyToEasy ()
	{
		currentDifficulty = Difficulty.Easy;
	}

	public void SetDifficultyToMedium ()
	{
		currentDifficulty = Difficulty.Medium;
	}

	public void SetDifficultyToHard ()
	{
		currentDifficulty = Difficulty.Hard;
	}

	public void EnableDifficultyVisuals (bool enable)
	{
		difficultyMenu.enabled = enable;
	}

	public void Initialize ()
	{
		currentDifficulty = Difficulty.Easy;
		difficultyMenu.enabled = false;
	}

	public void DifficultyChanged ()
	{
		OverlayManager.singleton.SetDifficultyText (currentDifficulty.ToString ());
		EnableDifficultyVisuals (false);
		OverlayManager.singleton.EnableDifficultyVisuals (false);
	}

	public DifficultyMenuManager.Difficulty GetDifficulty ()
	{
		return currentDifficulty;
	}
}