using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCheckpoint : MonoBehaviour
{
    public GameObject KeyBindings;
    public GameObject Quest1;
    public GameObject Quest2;
    public GameObject Quest3;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Legs")
        {
            CharacterStats target = col.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                KeyBindings.SetActive(false);
                Quest1.SetActive(false);
                Quest2.SetActive(false);
                Quest3.SetActive(false);
            }
        }
    }
}
