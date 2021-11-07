using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTargetSpell : Spell
{

    public float Size;
    public float StayDuration;

    public float ReleaseDuration;

    public float MinDamage;
    public float MaxDamage;

    public bool AddRigidBody = true;

    private float _endTime;

    // Start is called before the first frame update
    void Start()
    {
        _endTime = Time.time + (StayDuration / 1000);
    }

    void FixedUpdate()
    {
        CheckDuration();
    }

    public override void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        PlaceSpell(targetPoint);
        base.Initialize(user, direction, targetPoint, addedDamage);

    }


    protected Rigidbody2D PlaceSpell(Vector2 targetPoint)
    {
        gameObject.transform.position = targetPoint;

        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
        if (body == null && AddRigidBody)
        {
            body = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
            body.velocity = Vector2.zero;
            body.gravityScale = 0;
            body.mass = 0;
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            body.freezeRotation = true;
        }


        return body;
    }

    public override void OnHitTrigger(CharacterStats target, CharacterStats user)
    {
        Debug.Log("Ground spell hit");

        int dmg = (int)Random.Range(MinDamage, MaxDamage) + AddedDamage;
        target.ReceiveDamage(dmg);
    }

    void CheckDuration()
    {
        if (Time.time > _endTime && State != SpellStates.Release)
        {
            // AnimateRelease();
            StartCoroutine(DestroyAfterRelease());
        }
    }

    IEnumerator DestroyAfterRelease()
    {
        yield return new WaitForSeconds(ReleaseDuration/1000);
        Destroy(gameObject);
    }

}
