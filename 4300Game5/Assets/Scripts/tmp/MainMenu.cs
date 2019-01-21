using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void BtnStart()
    {
        SceneManager.LoadScene("Scenes/Dorian");
        Time.timeScale = 1f;
    }

    public void BtnCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void BtnMenu()
    {
        creditsPanel.SetActive(false);
    }


    public void BtnExit()
    {
        Debug.Log("Quit the game");
        Application.Quit();
    }
}
