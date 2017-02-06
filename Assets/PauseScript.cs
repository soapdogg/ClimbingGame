using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{

    public Canvas pauseMenu;
    public Button pauseText;

	void Start ()
	{
	    pauseMenu = pauseMenu.GetComponent<Canvas>();
	    pauseText = pauseText.GetComponent<Button>();
	    pauseMenu.enabled = false;
	}

    public void PausePressed()
    {
        pauseMenu.enabled = true;
        pauseText.enabled = false;
    }

    public void ResumePressed()
    {
        pauseMenu.enabled = false;
        pauseText.enabled = true;
    }

    public void GoToMainPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
