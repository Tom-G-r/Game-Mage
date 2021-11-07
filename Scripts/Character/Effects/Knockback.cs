using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float Speed = 10;
    public float Duration = 150;

    private int damage = 1;
    private int force = 800;
    private Vector2 direction;
    private Rigidbody2D rgbd;

    // Start is called before the first frame update
    void Start()
    {
        if (HasDuplicates())
        {
            Destroy(this);
        }
    }
    public void Initialize(Vector2 direction, int damage = 0, int force = 800)
    {
        this.direction = direction;
        this.damage = damage;
        this.force = force;

        KnockbackEffect();
    }

    public void KnockbackEffect()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        rgbd.AddForce(direction * force);

        CharacterStats c = gameObject.GetComponent<CharacterStats>();
        if (c != null)
        {
            c.ReceiveDamage(damage);
            c.AddIFrames(4);
        }

        StartCoroutine(DestoryAfterTime(Duration));
    }

    bool HasDuplicates()
    {
        Knockback[] exists = gameObject.GetComponents<Knockback>();

        return (exists.Length >= 1);
    }

    IEnumerator DestoryAfterTime(float time)
    {
        yield return new WaitForSeconds(time/1000);

        Destroy(this);
    }

}
