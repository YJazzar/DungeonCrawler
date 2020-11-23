using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{

    public GameObject gameOverMenu;
    public Surrounding surrounding;

    public void displayGameOverCanvas() 
    {
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void RestartGame()
    {
        Time.timeScale = 1f;
        surrounding.ResetAll();
    }


}
