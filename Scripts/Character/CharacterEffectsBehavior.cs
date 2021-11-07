using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsBehavior : MonoBehaviour
{

    public static void AddEffect(CharacterEffects effect, GameObject target, Spell spell)
    {
        switch (effect)
        {
            case CharacterEffects.Burning:
                target.AddComponent<Burning>();
                break;
            case CharacterEffects.Knockback:
                target.AddComponent<Knockback>().Initialize(spell.GetDirection());
                break;
            default:
                break;
        }
    }

    public enum CharacterEffects
    {
        None,
        Burning,
        Knockback
    }
}
