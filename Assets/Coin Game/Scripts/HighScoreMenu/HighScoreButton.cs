using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreButton {

    public bool isEnabled;
    public GameObject obj;
    private Text textName;
    private Text textTime;

    public HighScoreButton(GameObject gameObject)
    {
        isEnabled = false;
        try
        {
            obj = gameObject;
            textName = obj.GetComponentsInChildren<Text>()[0];
            textTime = obj.GetComponentsInChildren<Text>()[1];
            //hide initially
            SetEnabled(false);
        }
        catch (Exception e)
        {
            Debug.Log("Error setting up HighScoreButton.");
            Debug.Log(e.Message);
        }
    }

	public void SetEnabled(bool enabled)
    {
        
        if(!enabled)
        {
            Color temp = obj.GetComponent<Image>().color;
            temp.a = 0f;
            // not sure if this will work.....
            //obj.image.color = temp;
            obj.GetComponent<Image>().color = temp;
            SetText("", "");
        }
        else
        {
            obj.GetComponent<Image>().color = Color.white;
            //obj.image.color = Color.white;
        }
    }

    public void SetText(string name, string time)
    {
        textName.text = name;
        textTime.text = time;
    }

    public void SetText(HighScore newScore)
    {
        textName.text = newScore.name;
        textTime.text = newScore.score.ToString("0.000"); //three decimals
    }
}
