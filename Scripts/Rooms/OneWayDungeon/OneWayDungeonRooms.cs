using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayDungeonRooms : MonoBehaviour
{
    public int Limit => RoomsCount;
    private const int RoomsCount = 3;

    public GameObject[] RoomOne;
    public GameObject[] RoomTwo;
    public GameObject[] RoomThree;
    //public GameObject[] RoomFour;
    //public GameObject[] RoomFive;

    public List<GameObject> rooms;

    void Start()
    {

    }

    public GameObject[] GetRoomList(int n)
    {
        switch (n)
        {
            case 1:
                return RoomOne;
            case 2:
                return RoomTwo;
            case 3:
                return RoomThree;
            /*
            case 4:
                return RoomFour;
            case 5:
                return RoomFive;
            */
            default:
                return null;
        }
    }

}
