using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    public bool CanMove = true;
    public float Speed => (BaseSpeed + _addedSpeed);

    public float BaseSpeed = 1;
    private const float ConstantSpeed = 1250;

    protected Vector2 direction;

    private Animator _animator;
    private bool _facingRight = true;

    protected CharacterStats Character;
    private float _addedSpeed = 0;

    private Rigidbody2D rgbd;

    public virtual void Start()
    {
        direction = gameObject.transform.position;
        _animator = GetComponentInParent<Animator>();
        Character = GetComponentInParent<CharacterStats>();
        rgbd = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        if (CanMove)
        {
            SetMovementAnimation();
            if (Character != null)
            {
                _addedSpeed = Character.BonusMovement;
            }
            
            if (rgbd != null)
            {
                rgbd.AddForce(direction * ConstantSpeed * (Speed) * Time.deltaTime);
            }
        }

    }

    protected void SetMovementAnimation()
    {
        if (_animator != null)
        {
            
            try
            {
                // Stupid warning if animator parameter does not exists
                if (_animator.parameterCount > 0)
                {
                    // unity has no proper way to check if parameter exists
                    foreach (AnimatorControllerParameter param in _animator.parameters)
                    {
                        if (param.name.Equals("is_running"))
                        {
                            if (direction.Equals(Vector2.zero))
                            {   // Idle
                                _animator.SetBool("is_running", false);
                            }
                            else
                            {   // Running
                                _animator.SetBool("is_running", true);
                            }
                        }
                    }
                }
            } catch
            { }
        }
        
        CheckFlip();
    }

    public void CheckFlip()
    {
        if (direction.x < 0 && _facingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && !_facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;

        gameObject.transform.localScale = scale;

        _facingRight = !_facingRight;
    }


    /// <summary>
    /// Prefer to add speed on CharacterStats.BonusMovement
    /// </summary>
    public void AddSpeed(float value)
    {
        if (Character != null)
        {
            Character.BonusMovement += value;
        } else
        {
            _addedSpeed += value;
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

}
