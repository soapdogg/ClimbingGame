using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyMenuManager :MonoBehaviour
{
	public Difficulty currentDifficulty { get; private set; }
	public Canvas difficultyMenu;

	public enum Difficulty {Easy, Medium, Hard};

	public static DifficultyMenuManager singleton { get; private set; }

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void EasyPressed()
	{
		currentDifficulty = Difficulty.Easy;
		DifficultyChanged ();
		Debug.Log("Difficulty Set to Easy");
	}

	public void MediumPressed()
	{
		currentDifficulty = Difficulty.Medium;
		DifficultyChanged ();
		Debug.Log("Difficulty Set to Medium");
	}

	public void HardPressed()
	{
		currentDifficulty = Difficulty.Hard;
		DifficultyChanged ();
		Debug.Log("Difficulty Set to Hard");
	}

	public void EnableDifficultyVisuals(bool enable)
	{
		difficultyMenu.enabled = enable;
	
	}

	public void Initialize()
	{
		currentDifficulty = Difficulty.Easy;
		difficultyMenu.enabled = false;
	}

	private void DifficultyChanged()
	{
		OverlayManager.singleton.SetDifficultyText ();
		EnableDifficultyVisuals (false);
		OverlayManager.singleton.EnableDifficultyVisuals (false);
	}

}