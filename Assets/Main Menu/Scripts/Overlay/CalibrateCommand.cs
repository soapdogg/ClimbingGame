using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CalibrateCommand : MonoBehaviour, ICommand
{

    public void OnTriggerEnter(Collider other)
    {
        Execute();
    }

    public void OnMouseEnter()
    {
        Execute();
    }

    public void Execute()
    {
        Debug.Log("Main Menu: CalibratePressed()");
        SceneManager.LoadScene("Calibration");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Execute();
    }
}
