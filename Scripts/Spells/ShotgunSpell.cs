using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSpell : Spell
{
    public GameObject Spell;

    public float Width = 90f;
    public int Shots = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        Spread(user, direction, targetPoint, addedDamage);

        Destroy(gameObject);
    }

    public void Spread(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        GameObject spell;
        Quaternion rotation = transform.rotation;
        Spell spellcomponent = Spell.GetComponent<Spell>();

        if (Shots > 1)
        {
            float interval = Width / Shots - 1;
            float degrees = (Width / 2) * -1;



            for (int i = 0; i < Shots; i++)
            {
                Vector2 rot_direction = direction.Rotate(degrees);
                Vector2 rot_targetPoint = targetPoint.Rotate(degrees);


                if (spellcomponent.Rotate)
                {
                    float angle = Mathf.Atan2(rot_direction.y, rot_direction.x) * Mathf.Rad2Deg;
                    rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }



                spell = Instantiate(Spell, transform.position, rotation);
                spell.GetComponent<Spell>().Initialize(user, rot_direction, rot_targetPoint, addedDamage);

                degrees += interval;
            }
        } else
        {
            


            if (spellcomponent.Rotate)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }


            // center
            spell = Instantiate(Spell, transform.position, rotation);
            spell.GetComponent<Spell>().Initialize(user, direction, targetPoint, addedDamage);
        }


    }

}
