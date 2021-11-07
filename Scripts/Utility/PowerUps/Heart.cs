using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public int heal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                target.HealCharacter(heal);
                FindObjectOfType<AudioManager>().Play("PowerUp");
                Destroy(gameObject);
            }
        }
    }
}
