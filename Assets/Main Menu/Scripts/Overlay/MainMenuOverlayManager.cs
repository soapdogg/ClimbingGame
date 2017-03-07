using UnityEngine;
using UnityEngine.UI;

public class MainMenuOverlayManager : MonoBehaviour, IManager
{
    public static MainMenuOverlayManager singleton { get; private set; }

    public Text startText;
    public Text exitText;

	void Start ()
	{
	    singleton = this;
		Initialize();
	}

    public void Initialize()
    {
        Debug.Log("Main Menu: Start()");
    }
}
