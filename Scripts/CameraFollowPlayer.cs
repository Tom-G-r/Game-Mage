using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform Player;
    public float Smoothing = 0.2f;
    public Vector2 Offset = Vector2.zero;

    private float z_offset;

    // Start is called before the first frame update
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        z_offset = Camera.main.transform.position.z;
    }

    void FixedUpdate()
    {
        if (Player != null)
        {
            Vector3 offset = new Vector3(Offset.x, Offset.y, z_offset);
            Vector3 newPos = Vector3.Lerp(transform.position, Player.transform.position + offset, Smoothing);
            transform.position = newPos;
        }
    }
}
