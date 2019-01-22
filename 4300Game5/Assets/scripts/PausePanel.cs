using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject firstButtonSelectedInPause = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);

        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonSelectedInPause);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void Menu()
    {
        SceneManager.LoadScene("SimonScene");
        Time.timeScale = 1f;
    }
}
