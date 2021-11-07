using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstSpell : Spell
{
    public GameObject Spell;
    public int Amount = 2;
    public float Rate = 0.05f;
    public bool FollowMouse = false;

    private GameObject _player;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position = _player.transform.position;

            if (FollowMouse && _player.tag == "Player")
            {
                _direction = MouseController.GetDirectionToMouse(transform.position);
            }
        }
    }

    public override void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        _player = user;
        _direction = direction;

        //base.Initialize(user, direction, targetPoint, addedDamage);
        StartCoroutine(Burst(Amount, user, targetPoint, addedDamage));
    }


    IEnumerator Burst(int count, GameObject user, Vector2 targetPoint, int addedDamage)
    {
        GameObject spell;
        Quaternion rotation = transform.rotation;
        Spell spellcomponent = Spell.GetComponent<Spell>();


        if (spellcomponent.Rotate)
        {
            rotation = _direction.RotateToDirection();
        }

        spell = Instantiate(Spell, transform.position, rotation);
        spell.GetComponent<Spell>().Initialize(user, _direction, targetPoint, addedDamage);


        count--;
        if (count > 0)
        {
            yield return new WaitForSeconds(Rate);
            StartCoroutine(Burst(count, user, targetPoint, addedDamage));
        } else
        {
            Destroy(gameObject);
        }
    }

}
