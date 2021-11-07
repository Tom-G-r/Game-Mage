using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackKnight : MonoBehaviour
{
    public int DashSpeedAdded = 7;

    public int AddedHeartsPerLevel = 5;
    public float AddedSpeedPerLevel = 0.25f;


    private GameObject _target;
    private Animator _animator;
    private EnemyMovement _movement;
    private CharacterStats _stats;
    private GameObject _BossCanvas;
    private GameObject _HPSlider;
    private Slider _BossHP;

    private AudioSource audio1, audio2;
    private AudioSource[] audiosources;

    private BlackKnightStatus _status = BlackKnightStatus.Asleep;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMovement>();
        _BossCanvas = GameObject.FindGameObjectWithTag("BossCanvas");
        _HPSlider = GameObject.FindGameObjectWithTag("BossHP");
        _BossHP = _HPSlider.GetComponent<Slider>();
        _stats = GetComponent<CharacterStats>();
        ActivateBossCanvas(false);


        audiosources = gameObject.GetComponents<AudioSource>();
        if (audiosources != null)
        {
            if (audiosources[0] != null )
            {
                audio1 = audiosources[0];
            }
            if (audiosources[1] != null)
            {
                audio2 = audiosources[1];
            }
        }
        

        if (_target != null)
        {
        }

        if (_movement != null)
        {
            _movement.CanMove = false;
            _movement.SetAggroTarget(_target);
        } else
        {

        }

        GameObject gmObj = GameObject.FindGameObjectWithTag("GameManager");
        if (gmObj != null)
        {
            GameManager gm = gmObj.GetComponent<GameManager>();
            if (gm != null)
            {
                int _level = gm.Difficulty;
                ScalePower(_level);
            }
        }
        InitBossHPSlider();
    }


    void FixedUpdate()
    {
        if (_target != null)
        {
            _movement.SetAggroTarget(_target);
        }


        /*
        Vector2 dir = MouseController.GetDirection(transform.position, _target.transform.position);
        _movement.SetDirection(dir);*/
    }

    /// <summary>
    /// Awaken Animation. Starts when Player gets in range.
    /// </summary>
    /// <returns></returns>
    IEnumerator AwakeBehavior()
    {
        PlayBattleMusic();

        yield return new WaitForSeconds(1f);

        Animate("Awaken");
        if (audio2 != null)
        {
            audio2.Play();
        }
        StartCoroutine(Immobilize(1.9f));

        yield return new WaitForSeconds(1.9f);

        _status = BlackKnightStatus.Walking;
        StartCoroutine(Fight());
    }

    IEnumerator WonBehavior()
    {
        Animate("Awaken_Backwards");
        _movement.CanMove = false;

        _status = BlackKnightStatus.Asleep;

        

        yield return new WaitForSeconds(1.9f);
    }

    /// <summary>
    /// Main loop of knight ai
    /// </summary>
    IEnumerator Fight()
    {
        _movement.CheckFlip();


        int rand = Random.Range(0, 500);

        if (_status == BlackKnightStatus.Walking)
        {
            Vector2 playerPos = transform.position;
            if (_target != null)
                playerPos = _target.transform.position;


            float dis = Vector2.Distance(transform.position, playerPos);
            // x pos difference between boss and player
            float xDiff = transform.position.x - playerPos.x;


            if (dis < 3 && (Mathf.Abs(xDiff) >= 2))
            {
                StartCoroutine(SwordAttack());
            }
            else if (dis > 6 || rand >= 495)
            {
                _status = BlackKnightStatus.Charging;

                Animate("Charge1");


                StartCoroutine(Immobilize(0.75f));
                yield return new WaitForSeconds(0.75f);
   
                if (_target != null)
                    playerPos = _target.transform.position;

                Vector2 chargeTarget = playerPos;
                StartCoroutine(ChargeAttack(chargeTarget));
            }
            else
            {
                // Walk
                Animate("Walk");
            }

        }

        yield return new WaitForFixedUpdate();

        if (_target != null)
        {
            StartCoroutine(Fight());
        } else
        {
            StartCoroutine(WonBehavior());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            PlayerStats target = collision.GetComponentInParent<PlayerStats>();
            if (target != null && target.tag == "Player")
            {
                if (_status == BlackKnightStatus.Asleep)
                {
                    ActivateBossCanvas(true);
                    StartCoroutine(AwakeBehavior());

                    target.PlayerCombatStart();
                }
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
            }
        }
    }

    private void ActivateBossCanvas(bool toggle)
    {
        _BossCanvas.SetActive(toggle);
    }

    public void UpdateBossCanvas(int CurrentHealth)
    {
        _BossHP.value = Mathf.MoveTowards(_BossHP.value, (float)CurrentHealth, _BossHP.maxValue);
    }

    private void InitBossHPSlider()
    {
        _BossHP.maxValue = _stats.MaxHealth;
        _BossHP.value = _BossHP.maxValue;
    }

    enum BlackKnightStatus
    {
        Asleep,
        Awaking,
        Charging,
        Attacking,
        Walking
    }

    void Animate(string name)
    {
        if (_animator != null)
        {
            try
            {
                _animator.Play(name);
            }
            catch
            {
                Debug.Log($"BlackKnight: Could not play animation \"{name}\".");
            }
        }
    }

    IEnumerator SwordAttack()
    {
        _status = BlackKnightStatus.Attacking;
        Animate("Attack");
        if (audio1 != null)
        {
            audio1.Play();
        }
        StartCoroutine(Immobilize(0.4f));

        yield return new WaitForSeconds(0.4f);
        _status = BlackKnightStatus.Walking;
    }

    IEnumerator ChargeAttack(Vector2 chargeTarget)
    {
        // distance to target
        float dis = Vector2.Distance(transform.position, chargeTarget);

        if (dis >= 1f)
        {
            _status = BlackKnightStatus.Charging;

            Animate("ChargeAttack");

            // Move our position a step closer to the target.
            float step = (_movement.Speed + DashSpeedAdded) * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, chargeTarget, step);

            // recursion
            yield return new WaitForFixedUpdate();
            StartCoroutine(ChargeAttack(chargeTarget));
        } else
        {
            // end charge
            Animate("Charge2");
            StartCoroutine(Immobilize(0.25f));
            yield return new WaitForSeconds(0.25f);
            _status = BlackKnightStatus.Walking;
        }

    }

    /// <summary>
    /// Duration the knight cannot move
    /// </summary>
    IEnumerator Immobilize(float duration)
    {
        if (_movement != null)
        {
            _movement.CanMove = false;

            yield return new WaitForSeconds(duration);

            _movement.CanMove = true;
        }
    }

    private void ScalePower(int level)
    {
        if (level > 0)
        {
            _stats.AddMaxHeart(Mathf.RoundToInt(level * AddedHeartsPerLevel));
            _stats.BonusDamage += Mathf.RoundToInt(level * 1f);
            _stats.ContactDamage += 1;
            _movement.AddSpeed(AddedSpeedPerLevel * level);
        }
    }

    void PlayBattleMusic()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("AudioManager");

        if (audioObj != null)
        {
            Music music = audioObj.GetComponent<Music>();
            if (music != null)
            {
                music.PlayBattleMusic();
            }
        }
    }

}
