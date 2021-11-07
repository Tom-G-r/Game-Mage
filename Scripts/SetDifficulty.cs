using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    public Dropdown mydropdown;

    // Start is called before the first frame update
    void Start()
    {
        initDifficulty(mydropdown);
        mydropdown.onValueChanged.AddListener(delegate
        {
            selectvalue(mydropdown);
        });
    }

    private void selectvalue(Dropdown gdropdown)
    {
        switch (gdropdown.value)
        {
            case 0:
                PlayerPrefs.SetString("Difficulty", "Normal");
                break;
            case 1:
                PlayerPrefs.SetString("Difficulty", "Hard");
                break;
            default:
                PlayerPrefs.SetString("Difficulty", "Normal");
                break;
        }
        PlayerPrefs.Save();
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
