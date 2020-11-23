using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject startMenu;
    public Surrounding surrounding;


    void Start()
    {
        Time.timeScale = 0f;
        startMenu.SetActive(true);
    }

    public void OnStartButtonClicked() {
        Time.timeScale = 1f;
        startMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
