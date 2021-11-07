using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCheckpoint : MonoBehaviour
{

    public GameObject Quest1;
    public GameObject Quest2;
    public GameObject Quest3;
    public GameObject KeyBindings;
    private Text keybindingtext;

    void Start()
    {
        keybindingtext = KeyBindings.GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Legs")
        {
            CharacterStats target = col.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                keybindingtext.text = @"Press <color=yellow>[F]</color> to <color=cyan>open chests</color>" + "\n\n" + @"Press <color=yellow>[ESC]</color> to open the <color=cyan>menu</color>";
                Quest1.SetActive(false);
                Quest2.SetActive(false);
                Quest3.SetActive(true);
            }
        }
    }
}
