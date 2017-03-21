using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * This class handles
 *      the command for clicking the 'submit' button to save the user's name and score
 */
public class SubmitHighScoreCommand : MonoBehaviour, ICommand {

    public static SubmitHighScoreCommand singleton { get; private set; }

    void Start()
    {
        singleton = this;
        Initialize();
    }

    public void Initialize()
    {

    }

    public void Execute()
    {
        string name = HighScoreMenuScript.singleton.GetUserInputName();
        float time = OverlayManager.singleton.GetElapsedTime();
        if (name.Length > 0 && !name.Equals("Enter your name here"))
        {
            HighScoreManager.singleton.WriteHighScore(name, time);
            HighScoreMenuScript.singleton.SetEnabled(false);
            ViewHighScoreCommand.singleton.Execute();
        }
        else
        {
            Debug.Log("Enter your name!");
        }
    }

    public void OnMouseEnter() { Execute(); }
    
    public void OnPointerEnter(PointerEventData eventData) { Execute(); }

    public void OnTriggerEnter2D(Collider2D collider) { Execute(); }
}
