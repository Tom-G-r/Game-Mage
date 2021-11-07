using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDungeonGenerate : MonoBehaviour
{
    private OneWayDungeonRooms templates;

    void Start(){
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<OneWayDungeonRooms>();
        templates.rooms.Add(this.gameObject);
    }
}
