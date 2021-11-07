using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpText : MonoBehaviour
{
    public PowerUpStats Selected;

    private CharacterStats _stats;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _stats = player.GetComponent<CharacterStats>();
        }

        _text = gameObject.GetComponent<Text>();
    }

    public void FixedUpdate()
    {
        if (_text != null && _stats != null)
        {
            _text.text = SelectedStatToString();
        }
    }

    private string SelectedStatToString()
    {
        switch (Selected)
        {
            case PowerUpStats.BonusMaxHearts:
                return _stats.BonusHearts.ToString();
            case PowerUpStats.BonusPower:
                return _stats.BonusDamage.ToString();
            case PowerUpStats.BonusSpeed:
                return Math.Truncate(_stats.BonusMovement * 10).ToString();

                //decimal n = Convert.ToDecimal(_stats.BonusMovement);
                //n = decimal.Round(n, 1);
                //return n.ToString();

            case PowerUpStats.BonusCDR:
                return $" {_stats.CDR * 100}%";
            default:
                return "";
        }
    }

    public enum PowerUpStats {
        BonusMaxHearts,
        BonusPower,
        BonusSpeed,
        BonusCDR
    }
}
