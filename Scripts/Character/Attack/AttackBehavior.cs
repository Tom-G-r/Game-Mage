using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    public bool CanAttack = true;

    public GameObject Spell;

    protected bool IsAttacking;
    protected Vector3 Position;
    protected Vector2 AttackDirection;
    protected Vector2 TargetPoint;

    private float cooldown;
    private float nextFire;

    protected int _addedDamage = 0;
    protected float _cdr = 0;
    protected CharacterStats Character;

    // Start is called before the first frame update
    public virtual void Start()
    {
        IsAttacking = false;
        Position = Vector3.zero;
        AttackDirection = Vector2.zero;
        TargetPoint = Vector2.zero;
        nextFire = Time.time;
        cooldown = Spell.GetComponent<Spell>().Cooldown / 1000;

        Character = gameObject.GetComponent<CharacterStats>();
    }

    public virtual void FixedUpdate()
    {
        if (CanAttack && IsAttacking && (Time.time >= nextFire))
        {
            nextFire = CalcNextFire();
            
            if (Character != null)
            {
                _addedDamage = Character.BonusDamage;
                _cdr = Character.CDR;
            }

            _ = StartCoroutine(InstantiateSpell(Position, AttackDirection, TargetPoint, _addedDamage));
            // Attack(Position, AttackDirection, TargetPoint, _addedDamage);
        }
    }

    public virtual float CalcNextFire()
    {
        return Time.time + (cooldown * (1 - _cdr));
    }

    public void Attack(Vector3 position, Vector2 direction, Vector2 targetPoint, int addedDamage = 0)
    {
        _ = StartCoroutine(InstantiateSpell(Position, AttackDirection, TargetPoint, _addedDamage));
    }

    /*
    public virtual void Attack(Vector3 position, Vector2 direction, Vector2 targetPoint, int addedDamage = 0)
    {
        Quaternion rotation = transform.rotation;
        Spell spellcomponent = Spell.GetComponent<Spell>();

        if (spellcomponent.Rotate)
        {
            rotation = direction.RotateToDirection();
        }

        GameObject spell = Instantiate(Spell, position, rotation);

        spell.GetComponent<Spell>().Initialize(gameObject, direction, targetPoint, addedDamage);
    }
    */
    IEnumerator InstantiateSpell(Vector3 position, Vector2 direction, Vector2 targetPoint, int addedDamage = 0)
    {
        Quaternion rotation = transform.rotation;
        Spell spellcomponent = Spell.GetComponent<Spell>();

        if (spellcomponent.Rotate)
        {
            rotation = direction.RotateToDirection();
        }

        GameObject spell = Instantiate(Spell, position, rotation);

        spell.GetComponent<Spell>().Initialize(gameObject, direction, targetPoint, addedDamage);

        yield return null;
    }

    /// <summary>
    /// Try to use Character.BonusDamage.
    /// Adds Damage to CharacterStats if exists instead of AttackScript
    /// </summary>
    public void AddDamage(int amount)
    {
        if (Character != null)
        {
            Character.BonusDamage += amount;
        }
        else
        {
            _addedDamage += amount;
        }
    }

    /// <summary>
    /// Try to use Character.CDR.
    /// Adds CDR to CharacterStats if exists instead of AttackScript
    /// </summary>
    public void AddCDR(float amount)
    {
        if (Character != null)
        {
            Character.CDR += amount;
        }
        else
        {
            _cdr += amount;
            if (_cdr >= 0.5)
            {
                _cdr = 0.5f;
            }
        }
    }

}
