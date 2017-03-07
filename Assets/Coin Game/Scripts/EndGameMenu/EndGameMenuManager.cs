using UnityEngine;

public class EndGameMenuManager : MonoBehaviour, IManager
{
	public Canvas endGameMenu;

	public static EndGameMenuManager singleton { get; private set; }

	private EndGameMenuManager(){}

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void Initialize()
	{
		endGameMenu.enabled = false;
	}
}