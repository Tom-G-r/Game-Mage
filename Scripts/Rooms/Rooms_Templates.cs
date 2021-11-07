using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rooms_Templates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;
    public GameObject[] BossRooms;

    public List<GameObject> rooms;

    public float waitTime;
    public bool spawnedBoss;
    public GameObject boss;

    private Vector3 position;

    void Start()
    {

    }


    /*
    void Update(){
        
        if(waitTime <= 0 && spawnedBoss == false){
            //Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            for (int i = 0; i < rooms.Count; i++){
                if(i == rooms.Count-1){
                    Tilemap tilemap = rooms[i].transform.GetChild(0).GetComponent<Tilemap>();
                    //Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    //Instantiate(boss, tilemap., Quaternion.identity);
                    position = new Vector3(rooms[i].transform.position.x - 1, rooms[i].transform.position.y - 17, rooms[i].transform.position.z);
                    Instantiate(boss, position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    
    }
    */

}
