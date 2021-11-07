using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDungeonRoomCreation: MonoBehaviour
{
    private static int counter = 1;
    private int Limit;

    private OneWayDungeonRooms templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 1f;

    private const int FullSize = 32;
    private const int HalfSize = 17;
    private const int Spacing = 4;

    void Start(){
        
        //Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<OneWayDungeonRooms>();
        Limit = templates.Limit;


        if (counter <= Limit)
        {
            Spawn();
        }
    }
    
    void Spawn(){
        if (spawned == false)
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            Vector3 spawnpoint = col.transform.position;

            GameObject[] rooms = templates.GetRoomList(counter);

            if (rooms != null)
            {
                rand = Random.Range(0, rooms.Length);

                spawnpoint.x -= 1 - 0.06999999f;
                spawnpoint.y += FullSize + Spacing + 1;

                CreateRoom(rooms[rand], spawnpoint);

                counter++;
                spawned = true;


                // Reset counter after building the dungeon for next scene,
                // because counter is static (and must be static)
                if (counter > Limit)
                {
                    counter = 1;
                }
            }
        }
    }

    private void CreateRoom(GameObject room, Vector3 spawnpoint)
    {
        Instantiate(room, spawnpoint, room.transform.rotation);
    }

}
