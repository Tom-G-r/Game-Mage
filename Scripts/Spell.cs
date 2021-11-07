using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterEffectsBehavior;

public class Spell : MonoBehaviour
{
    public float Cooldown;
    public float UseCost;

    public bool Pierce = false;
    public bool Rotate;
    public CharacterEffects TargetOnHitEffect;
    public GameObject ExplosionParticles;

    public Animator Animator;

    protected Vector2 Direction;
    protected Vector2 TargetPoint;
    protected int AddedDamage;

    protected SpellStates State;

    // protected GameObject User;
    public float HitCooldown = 250;
    protected float internal_timer_next_hit = 0;

    private CharacterStats _userStats;
    private string _userTag;

    public Spell()
    {
    }

    public virtual void Initialize(GameObject user, Vector2 direction, Vector2 targetPoint, int addedDamage = 0)
    {

        //User = user;
        Direction = direction;
        TargetPoint = targetPoint;
        AddedDamage = addedDamage;

        if (user != null)
        {
            _userStats = user.GetComponent<CharacterStats>();
            _userTag = user.tag;
        }

        // AnimateSpawn();
    }

    public void SpellHit(Collider2D collision)
    {
        if (!IsInternalCooldownReady())
            return;

        // Debug.Log(collision.tag);

        if (collision.tag == "Wall")
        {
            Explode();
            Destroy(gameObject);
            return;
        }

        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target.tag == "Enemy" && _userTag == "Boss" || target.tag == "Boss" && _userTag == "Enemy")
            {
                return;
            }

            if (target != null && target.tag != _userTag && !target.Invincibility)
            {
                OnHitTrigger(target, _userStats);

                if (TargetOnHitEffect != CharacterEffects.None)
                {
                    AddEffect(TargetOnHitEffect, target.gameObject, this);
                }

                Explode();

                if (!Pierce)
                {
                    Destroy(gameObject);
                }
                internal_timer_next_hit = Time.time + HitCooldown / 1000;
            }
        }
    }

    private void Explode()
    {
        if (ExplosionParticles != null)
        {
            Vector3 temp = transform.position;
            temp.x += .5f;
            var instance = (GameObject)Instantiate(ExplosionParticles, temp, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SpellHit(collision);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Pierce)
        {
            Explode();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        SpellHit(collision);
    }

    public virtual void OnHitTrigger(CharacterStats target, CharacterStats user)
    {
    }

    protected bool IsInternalCooldownReady()
    {
        if (Time.time >= internal_timer_next_hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 GetDirection()
    {
        return Direction;
    }

    void Animate(string animationName)
    {
        if (Animator != null)
        {
            try
            {
                Animator.Play(animationName);
            }
            catch (Exception)
            {
            }
        }
    }

    protected void AnimateSpawn()
    {
        Animate("Spawn");
        State = SpellStates.Stay;
    }
    protected void AnimateStay()
    {
        Animate("Stay");
        State = SpellStates.Stay;
    }
    protected void AnimateRelease()
    {
        Animate("Release");
        State = SpellStates.Release;
    }

    public enum SpellStates
    {
        Spawn,
        Stay,
        Release
    }

}
