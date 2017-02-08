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
	}

    public void ExitPressed()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPressed()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("GameCountdown");
    }

    public void YesPressed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
