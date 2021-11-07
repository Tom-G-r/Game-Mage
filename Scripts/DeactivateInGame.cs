using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeactivateInGame : MonoBehaviour
{
    private Dropdown drop;

    void Start()
    {
        drop = this.gameObject.GetComponent<Dropdown>();
        initDifficulty(drop);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            drop.interactable = false;
        }
    }

    private int initDifficulty(Dropdown mydropdown)
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        switch (difficulty)
        {
            case "Normal":
                return mydropdown.value = 0;
            case "Hard":
                return mydropdown.value = 1;
            default:
                return mydropdown.value = 0;
        }
    }
}
