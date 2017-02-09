using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;

	// Use this for initialization
	void Start ()
	{
	    quitMenu = quitMenu.GetComponent<Canvas>();
	    startText = startText.GetComponent<Button>();
	    exitText = exitText.GetComponent<Button>();
	    quitMenu.enabled = false;
		Debug.Log("Main Menu: Start()");
	}

    public void ExitPressed()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
		Debug.Log("Main Menu: ExitPressed()");
    }

    public void NoPressed()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
		Debug.Log("Main Menu: NoPressed()");
    }

    public void PlayPressed()
    {
		Debug.Log("Main Menu: PlayPressed()");
        SceneManager.LoadScene("CoinGame");
    }

    public void YesPressed()
    {
		Debug.Log("Main Menu: YesPressed()"); 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
