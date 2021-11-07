using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{
    public int Damage;
    public float DisarmedDuration;
    public float ArmedDuration;

    private Animator anim;
    private BoxCollider2D bc2d;

    private AudioSource audio1;
    private AudioSource[] audiosources;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        audiosources = gameObject.GetComponents<AudioSource>();
        if (audiosources != null)
        {
            if (audiosources.Length >= 1 && audiosources[0] != null)
            {
                audio1 = audiosources[0];
            }
        }
        StartCoroutine(StartTrap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTrap()
    {
        while(true)
        {
            bc2d.enabled = false;
            anim.Play("Floor_Spikes_Disarmed");
            yield return new WaitForSeconds(DisarmedDuration);
            anim.Play("Floor_Spikes_Arm");
            audio1.Play();
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            anim.Play("Floor_Spikes_Armed");
            bc2d.enabled = true;
            yield return new WaitForSeconds(ArmedDuration);
            anim.Play("Floor_Spikes_Disarm");
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Legs")
        {
            CharacterStats target = other.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                if (!target.Invincibility)
                {
                    target.ReceiveDamage(Damage);
                }
            }
        }
    }
}
