using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public bool InFight = false;

    private Music _music;

    public override void Start()
    {
        DontDestroyOnLoad(gameObject);

        base.Start();

        GameObject musicObj = GameObject.FindGameObjectWithTag("AudioManager");
        if (musicObj != null)
        {
            _music = musicObj.GetComponent<Music>();
        }
    }

    public override void OnDeath()
    {
        PlayerOutOfCombat();
        base.OnDeath();
    }

    public void PlayerCombatStart()
    {
        if (!InFight && _music != null)
        {
            InFight = true;
            _music.PlayBattleMusic();
        }
    }

    public void PlayerOutOfCombat()
    {
        if (InFight && _music != null)
        {
            InFight = false;
            _music.PlayBackgroundMusic();
        }
    }
}
