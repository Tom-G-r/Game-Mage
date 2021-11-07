using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Rooms : MonoBehaviour
{
    private Rooms_Templates templates;

    void Start(){

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<Rooms_Templates>();
        templates.rooms.Add(this.gameObject);

    }
}
