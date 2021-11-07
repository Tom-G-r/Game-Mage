using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSpell : Spell
{
    public int Force = 1000;
    public int iFrames = 12;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        CharacterStats c = user.GetComponent<CharacterStats>();
        if (c != null)
        {
            c.AddIFrames(iFrames);
        }

        Dash(user, Force);
    }

    void Dash(GameObject user, int Force)
    {
        Rigidbody2D rgbd = user.GetComponent<Rigidbody2D>();

        rgbd.AddForce(MouseController.GetDirectionToMouse(user.transform.position) * Force);

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1);


        Destroy(gameObject);
    }

}
