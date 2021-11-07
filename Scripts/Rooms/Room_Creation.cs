using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Creation : MonoBehaviour
{
    private static int counter = 1;
    public static int Limit = 7;

    public RoomDirection openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private Rooms_Templates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;

    private const int FullSize = 32;
    private const int HalfSize = 17;
    private const float Spacing = 4f;

    void Start(){
        
        //Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<Rooms_Templates>();

        if(counter < Limit)
        {
            counter++;
            Invoke("Spawn", 0.1f);
        }
        else if(counter == Limit)
        {
            counter++;
            Invoke("SpawnBoss", 0.1f);
        }
    }

    void Spawn(){
        if (spawned == false)
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            Vector3 spawnpoint = col.transform.position;

            Debug.Log($"Spawnpoint: {spawnpoint}");

            if (openingDirection == RoomDirection.Bottom)
            {
                // Need to spawn a room with a bottom door.
                rand = Random.Range(0, templates.bottomRooms.Length);

                spawnpoint.x -= 1 - 0.06999999f;
                spawnpoint.y += FullSize + Spacing + 1;

                //Vector3 spawnpoint = new Vector3(spawnpoint.x, spawnpoint.y );
                Instantiate(templates.bottomRooms[rand], spawnpoint, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == RoomDirection.Top)
            {
                spawnpoint.x -= 1;
                spawnpoint.y -= Spacing - 1;

                // Need to spawn a room with a top door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], spawnpoint, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == RoomDirection.Left)
            {
                spawnpoint.x += HalfSize;
                spawnpoint.y += HalfSize + 1.5f + 0.08499908f;

                // Need to spawn a room with a left door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], spawnpoint, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == RoomDirection.Right)
            {
                spawnpoint.x -= (HalfSize + Spacing);
                spawnpoint.y += HalfSize;

                // Need to spawn a room with a right door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], spawnpoint, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void SpawnBoss()
    {
        // Todo: Change level to global game stats
        int level = 0;

        if (templates.BossRooms.Length > 0)
        {
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            Vector3 spawnpoint = col.transform.position;

            transform.position = new Vector3(transform.position.x - 1, transform.position.y + 17, transform.position.z);
            Instantiate(templates.BossRooms[level], spawnpoint, templates.BossRooms[level].transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("SpawnPoint")){
            Debug.Log("Room_Creation.OnTriggerEnter2D()");

            Room_Creation roomCrationScript = other.GetComponent<Room_Creation>();

            if (roomCrationScript != null && roomCrationScript.spawned == false && spawned == false){
                //transform.position = new Vector3(transform.position.x - 1, transform.position.y + 17, transform.position.z);
                //Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                //Destroy(gameObject);
            }
            spawned = true;
        }
    }



    public enum RoomDirection
    {
        Bottom = 1,
        Top,
        Left,
        Right
    }

}
