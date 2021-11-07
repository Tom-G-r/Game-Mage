using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    public GameObject inGameMenuPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
            
    }

    public void OpenMenu()
    {
        inGameMenuPanel.SetActive(true);
    }
    public void CloseMenu()
    {
        PlayerPrefs.Save();
        inGameMenuPanel.SetActive(false);
    }

    public void ToggleMenu()
    {
        if (inGameMenuPanel != null)
        {
            if (inGameMenuPanel.activeInHierarchy)
            {
                ResumeGame();
                CloseMenu();
            }
            else
            {
                PauseGame();
                OpenMenu();
            }
        }
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
