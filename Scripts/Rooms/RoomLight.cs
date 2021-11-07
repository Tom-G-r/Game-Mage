using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RoomLight : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private Light2D Light;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        Light = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SetLight(collision, true);
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        SetLight(collision, false);
    }

    void SetLight(Collider2D collision, bool value)
    {
        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                Light.enabled = value;
            }
        }
    }

}