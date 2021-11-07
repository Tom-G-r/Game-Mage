using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator Anim;
    private bool is_open;
    //private BoxCollider2D[] BoxColliders;
    //private BoxCollider2D DoorCollider, ColliderLeft, ColliderRight;
    // Start is called before the first frame update
    private Transform DoorCollider;

    private AudioSource audio1;
    private AudioSource[] audiosources;

    void Start()
    {
        is_open = false;
        Anim = gameObject.GetComponent<Animator>();
        //BoxColliders = gameObject.GetComponents<BoxCollider2D>();
        DoorCollider = gameObject.transform.parent.transform.Find("DoorCollider");
        //ColliderRight = BoxColliders[1];
        //ColliderLeft = BoxColliders[2];
        //DoorCollider = gameObject.transform.parent.GetChild(3);

        
        audiosources = gameObject.GetComponents<AudioSource>();
        if (audiosources != null)
        {
            if (audiosources.Length >= 1 && audiosources[0] != null)
            {
                audio1 = audiosources[0];
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Open()
    {
        is_open = true;
        Anim.Play("Door_Open");
        yield return new WaitForSeconds(Anim.GetCurrentAnimatorStateInfo(0).length);
        DoorCollider.gameObject.SetActive(false);
        Anim.Play("Door_Opened");
        if (audio1 != null)
        {
            audio1.Play();
        }

    }

    void ForceOpen()
    {
        DoorCollider.gameObject.SetActive(false);
        Anim.Play("Door_Opened");
    }

    /*void Close()
    {
        DoorCollider.enabled = true;
    }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Legs")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                
                //ForceOpen();
                if (!is_open)
                StartCoroutine(Open());
            }
        }
    }
}
