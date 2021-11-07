using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : AttackBehavior
{

    public float AddedCooldown = 0;

    public float Cooldown => _spellCooldown + (AddedCooldown / 1000);

    private GameObject Player;
    private float _spellCooldown = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            if (Spell != null)
            {
                _spellCooldown = Spell.GetComponent<Spell>().Cooldown / 1000;

                StartCoroutine(ShootingPlayer());
            }

        }
    }

    public override void FixedUpdate()
    {
        //base.FixedUpdate();
    }
    IEnumerator ShootingPlayer()
    {
        yield return new WaitForSeconds(Cooldown);

        if (Player != null)
        {
            Position = gameObject.transform.position;
            Vector2 targetPosition = Player.transform.position;
            TargetPoint = targetPosition;
            AttackDirection = (TargetPoint - (Vector2)transform.position).normalized;

            base.FixedUpdate();

            StartCoroutine(ShootingPlayer());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();
            if (target.tag == "Player")
            {
                IsAttacking = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();
            if (target.tag == "Player")
            {
                if (Vector2.Distance(transform.position, target.transform.position) > 4)
                {
                    IsAttacking = false;
                }
            }
        }
    }
}
