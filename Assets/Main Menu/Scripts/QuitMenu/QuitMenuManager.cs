using UnityEngine;

public class QuitMenuManager : MonoBehaviour, IManager
{
    public static QuitMenuManager singleton { get; private set; }

    public Canvas quitMenu;

    void Start()
    {
        singleton = this;
        Initialize();
    }

    public void Initialize()
    {
        quitMenu.enabled = false;
    }
}
