using System.Collections.Generic;
using UnityEngine;

public class HighScoreDisplayManager : MonoBehaviour, IManager {

    public static HighScoreDisplayManager singleton { get; private set; }
    public GameObject score1;
    public GameObject score2;
    public GameObject score3;
    public GameObject score4;
    public GameObject score5;
    private HighScoreButton[] listOfHighScoreButtons;
    public Canvas highScoreCanvas;

    void Start()
    {
        singleton = this;
        Initialize();
    }

    public void Initialize()
    {
        highScoreCanvas.enabled = false;
        listOfHighScoreButtons = new[] 
        {
            new HighScoreButton(score1),
            new HighScoreButton(score2),
            new HighScoreButton(score3),
            new HighScoreButton(score4),
            new HighScoreButton(score5)
        };
    }

    public void SetEnabled(bool enable)
    {
        highScoreCanvas.enabled = enable;
    }

    public void SetHighScoreList(List<HighScore> scores)
    {
        //reset
        for (int i = 0; i < listOfHighScoreButtons.Length; i++)
           listOfHighScoreButtons[i].SetEnabled(false);

        SetEnabled(true);

        for (int i = 0; i < scores.Count; i++)
        {
            listOfHighScoreButtons[i].SetEnabled(true);
           listOfHighScoreButtons[i].SetText(scores[i]);
        }
    }

    public void UpdateHighScore(int index, HighScore highScore)
    {
       listOfHighScoreButtons[index].SetText(highScore);
    }
}
