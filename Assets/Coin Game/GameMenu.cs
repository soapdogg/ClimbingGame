using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu {

    private static readonly float WIDTH_OF_MENU = 120;
    private static readonly float HEIGHT_OF_ROWS = 30;
    private static readonly float MENU_BUTTON_X = 20;
    private static readonly float MENU_BUTTON_WIDTH = 100;
    private static readonly float MENU_BUTTON_HEIGHT = 20;

    private int buttonCount = 0;
    private float initialY;
    private float initialX;
    private string title;


    public GameMenu createMenu(float xPos, float yPos, string gameMenuTitle)
    {
        this.initialX = xPos;
        this.initialY = yPos;
        this.title = gameMenuTitle;
        return this;
    }


    public GameMenu addGameMenuButton(string buttonText, Action a)
    {
        buttonCount++;
        if (GUI.Button(new Rect(MENU_BUTTON_X, initialY + HEIGHT_OF_ROWS * buttonCount, MENU_BUTTON_WIDTH, MENU_BUTTON_HEIGHT), buttonText))
        {
            GameMenuButton button = new GameMenuButton();
            button.onClick(a);
        }

        return this;
    }

    public void build()
    {
        GUI.Box(new Rect(initialX, initialY, WIDTH_OF_MENU, HEIGHT_OF_ROWS * (buttonCount + 1)), title);
    }
   
    public class GameMenuButton : IMenuButton
    {
        public void onClick(Action a)
        {
            a();
        }
    }

    public interface IMenuButton
    {
        void onClick(Action a);
    }
}
