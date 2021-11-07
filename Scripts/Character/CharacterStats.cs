using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public bool GodMode = false;
    private int _iFrames = 0;
    public bool Invincibility => (_iFrames > 0);

    public int MaxHearts = 10;
    public int HudHearts => MaxHearts + BonusHearts;
    public const int HealthPerHeart = 2;


    public int MaxHealth { get { return (MaxHearts + BonusHearts) * HealthPerHeart; } }
    public int Health { get { return _health; } }

    public int ContactDamage;
    public int ContactForce = 800;

    public int BonusHearts = 0;
    public int BonusDamage = 0;
    public float BonusMovement = 0;

    // Cooldown Reduction values
    private float _cdr = 0;
    private const float _cdrMax = 0.5f;
    public float CDR
    {
        get { return _cdr; }

        set
        {
            if (value > _cdrMax)
            {
                _cdr = _cdrMax;
            }
            else if (value < 0)
            {
                _cdr = 0;
            }
            else
            {
                _cdr = value;
            }
        }
    }


    public GameObject[] LootTable;
    public float DropChance;

    private int _health;

    private Animator anim;
    private SpriteRenderer rend;

    private BlackKnight knight;
    private HUD hud;

    void Awake()
    {
        _health = MaxHealth;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        knight = gameObject.GetComponent<BlackKnight>();

        if (this.tag == "Enemy")
        {
            DistributeEnemyPowerUps();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (_iFrames > 0)
        {
            _iFrames--;
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (GodMode) return;

        _health -= damage;

        if (knight != null)
        {
            // Debug.Log("Knight");
            knight.UpdateBossCanvas(_health);
        }

        // print($"ReceiveDamage(): Health = {Health}");

        
        if (!IsDead())
        {
            StartCoroutine(Hit(gameObject.tag));
        }
        else
        {   
            if (gameObject.tag == "Player")
            {
                if (hud == null)
                {
                    GameObject ga = GameObject.FindGameObjectWithTag("HUDCam");
                    if (ga != null)
                    {
                        hud = ga.GetComponent<HUD>();
                    }
                }

                if (hud != null)
                {
                    hud.Death_HeartContainer();
                }

            }

        }
    }

    IEnumerator Hit(string tag)
    {
        rend.color = new Color(1, 0, 0, 1);
        if (tag == "Player")
        {
            anim.SetBool("is_hit", true);
        }
        yield return new WaitForSeconds(.5f);
        if (tag == "Player")
        {
            anim.SetBool("is_hit", false);
        }
        rend.color = new Color(1, 1, 1, 1);
        if (tag == "Player")
        {
            if (hud == null)
            {
                GameObject ga = GameObject.FindGameObjectWithTag("HUDCam");
                if (ga != null)
                {
                    hud = ga.GetComponent<HUD>();
                }
            }

            if (hud != null)
            {
                hud.Damage_HeartContainer();
            }
        }
    }

    private bool IsDead()
    {
        if (GodMode) return false;

        if (_health <= 0)
        {
            if (UnityEngine.Random.Range(0f, 1f) <= DropChance)
            {
                DropLoot();
            }


            OnDeath();


            return true;
        }
        return false;
    }

    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        if (LootTable.Length >= 1)
        {
            int _lootindex = UnityEngine.Random.Range(0, LootTable.Length);
            GameObject _loot = LootTable[_lootindex];
            Instantiate(_loot, transform.position, transform.rotation);
        }
    }

    public void HealCharacter(int heal)
    {
        _health += heal;
        CheckOverheal();

        if (hud == null)
        {
            GameObject ga = GameObject.FindGameObjectWithTag("HUDCam");
            if (ga != null)
            {
                hud = ga.GetComponent<HUD>();
            }
        }

        if (hud != null)
        {
            hud.Heal_HeartContainer();
        }
    }

    private void CheckOverheal()
    {
        if (Health > MaxHealth)
        {
            _health = MaxHealth;
        }
    }

    public void AddMaxHeart(int amount = 1)
    {
        if (MaxHearts < 20)
        {
        }
        BonusHearts += amount;
        _health += amount*HealthPerHeart;


        if (this.tag == "Player")
        {
            GameObject ga = GameObject.FindGameObjectWithTag("HUDCam");
            if (ga != null)
            {
                hud = ga.GetComponent<HUD>();
            }

            if (hud != null)
            {
                hud.Extent_MaxHeartcontainer();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Player" && !this.Invincibility)
        {
            if (collision.tag == "Body" || collision.tag == "Legs")
            {
                CharacterStats collChar = collision.GetComponentInParent<CharacterStats>();

                if (collChar != null && (collChar.tag == "Enemy" || collChar.tag == "Boss"))
                {
                    this.DealContactDamage(collChar, collChar.ContactDamage, collChar.ContactForce);
                }
            }
        }
    }

    private void DealContactDamage(CharacterStats character, int amount, int force)
    {
        Vector2 knockbackDirection = Vector2.zero;
        MovementBehavior movement = character.GetComponent<MovementBehavior>();

        if (movement != null)
        {
            knockbackDirection = movement.GetDirection();
        }

        Knockback kb = this.gameObject.AddComponent<Knockback>();
        kb.Initialize(knockbackDirection, amount, force);
    }

    // Adds invincibility frames
    public void AddIFrames(int i)
    {
        if (i > _iFrames)
        {
            _iFrames = i;
        }
    }

    // Todo: Make this in its own EnemyStats.cs
    private void DistributeEnemyPowerUps()
    {
        GameObject gmObj = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObj != null)
        {
            GameManager gm = gmObj.GetComponent<GameManager>();
            if (gm != null)
            {
                int powerUps = gm.Difficulty;

                for (int i = 0; i < powerUps; i++)
                {
                    GainRandomPowerUp();
                }
            }
        }
    }

    private void GainRandomPowerUp()
    {

        int rnd = UnityEngine.Random.Range(0, 4);

        switch (rnd)
        {
            case 0:
                AddMaxHeart(1);
                break;
            case 1:
                BonusDamage += 1;
                break;
            case 2:
                BonusMovement += 0.25f;
                break;
            case 3:
                _cdr += 0.05f;
                break;
            default:
                break;
        }

    }

}
