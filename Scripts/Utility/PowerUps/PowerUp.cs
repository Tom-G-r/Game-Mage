using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int bonusHearts;
    public int bonusDMG;
    public float bonusCDR; // Cooldownreduction
    public float bonusSpeed;

    private bool _istriggered;

    // Start is called before the first frame update
    void Start()
    {
        _istriggered = false;
    }

    IEnumerator WaitAndDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_istriggered)
        {
            if (collision.tag == "Body")
            {
                CharacterStats target = collision.GetComponentInParent<CharacterStats>();

                if (target != null && target.tag == "Player")
                {
                    _istriggered = true;

                    ApplyMaxHearts(target);
                    ApplyBonusDamage(target);
                    ApplyCDR(target);
                    ApplyBonusSpeed(target);

                    StartCoroutine(WaitAndDestroy(1.5f));
                }
            }
        }
    }

    private void ApplyBonusDamage(CharacterStats target)
    {
        if (bonusDMG > 0)
        {
            target.BonusDamage += bonusDMG;
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }

    private void ApplyBonusSpeed(CharacterStats target)
    {
        if (bonusSpeed > 0)
        {
            target.BonusMovement += bonusSpeed;
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }

    private void ApplyMaxHearts(CharacterStats target)
    {
        if (bonusHearts > 0)
        {
            target.AddMaxHeart(bonusHearts);
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }

    private void ApplyCDR(CharacterStats target)
    {
        if (bonusCDR > 0)
        {
            target.CDR += bonusCDR;
            FindObjectOfType<AudioManager>().Play("PowerUp");
        }
    }

}
