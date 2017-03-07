using UnityEngine;

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

	public void EnablePauseVisuals(bool enable)
	{
		pauseMenu.enabled = enable;
	}

	public void Initialize()
	{
		pauseMenu.enabled = false;
	}
}

