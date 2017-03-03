using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    private static MainMenuScript singleton;

    public Canvas quitMenu;
    public Text startText;
    public Text exitText;

    private MainMenuScript()
    {
    }

	void Start ()
	{
	    singleton = this;
        quitMenu.enabled = false;
		Debug.Log("Main Menu: Start()");
	}

    public static MainMenuScript GetMainMenuScript()
    {
        return singleton;
    }
}
