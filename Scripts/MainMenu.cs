using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject controlsPanel;
    public GameObject LoadoutPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    
    public void LoadMainMenu()
    {
        PlayerPrefs.Save();
        GameObject mage = GameObject.FindGameObjectWithTag("Player");
        GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
        if (manager != null)
        {
            Destroy(manager);
        }
        if (mage != null)
        {
            Destroy(mage);
        }

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui != null)
        {
            Destroy(ui);
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }
    
    public void OpenControls()
    {
        controlsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }
    public void CloseLoadout()
    {
        LoadoutPanel.SetActive(false);
    }
    public void OpenLoadout()
    {
        LoadoutPanel.SetActive(true);
    }

    public void ChooseFireMage()
    {
        PlayerPrefs.SetString("Mage", "FireMage");
        PlayerPrefs.Save();
        StartGame();
    }
    public void ChooseStoneMage()
    {
        PlayerPrefs.SetString("Mage", "StoneMage");
        PlayerPrefs.Save();
        StartGame();
    }
    public void ChooseEnergyMage()
    {
        PlayerPrefs.SetString("Mage", "EnergyMage");
        PlayerPrefs.Save();
        StartGame();
    }
    public void ChooseNecromancer()
    {
        PlayerPrefs.SetString("Mage", "Necromancer");
        PlayerPrefs.Save();
        StartGame();
    }
}
