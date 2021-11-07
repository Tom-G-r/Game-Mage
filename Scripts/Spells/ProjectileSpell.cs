using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterEffectsBehavior;
using Random = UnityEngine.Random;

public class ProjectileSpell : Spell
{
    public float ProjectileSpeed;
    public float Distance;
    public float MinDamage;
    public float MaxDamage;

    private Vector2 spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = transform.position;
    }

    void FixedUpdate()
    {
        CheckDistance();
    }

    public override void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage)
    {
        base.Initialize(user, direction, targetPoint, addedDamage);

        ShootProjectile(direction);
    }

    protected Rigidbody2D ShootProjectile(Vector2 direction)
    {
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
        if (body == null)
        {
            body = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        }

        body.velocity = direction * ProjectileSpeed;
        body.gravityScale = 0;
        body.mass = 0;

        return body;
    }

    void CheckDistance()
    {
        float _distance = Vector2.Distance(spawnpoint, transform.position);

        if (_distance > Distance)
        {
            Destroy(gameObject);
        }
    }

    public override void OnHitTrigger(CharacterStats target, CharacterStats user)
    {
        int dmg = (int)Random.Range(MinDamage, MaxDamage) + AddedDamage;
        target.ReceiveDamage(dmg);
    }

}
