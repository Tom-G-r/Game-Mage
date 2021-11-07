using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MovementBehavior
{
    public UpdateCycle Reaction = UpdateCycle.Normal;

    private EnemyGroup _group = null;
    private Vector2 _patrollingTarget;
    private GameObject _aggroTarget;

    // Start is called before the first frame update
    public override void Start()
    {
        _aggroTarget = null;

        try
        {
            _group = GetComponentInParent<EnemyGroup>();
        } catch { }

        if (_group == null)
        {
            _patrollingTarget = transform.position;
        }

        base.Start();
        StartCoroutine(TargetPosCycle());
    }

    // Update is called once per frame
    void Update()
    {
        direction = MouseController.GetDirection(transform.position, _patrollingTarget);
    }

    public override void FixedUpdate()
    {
        float dis = Vector2.Distance(transform.position, _patrollingTarget);

        if (CanMove && dis >= 0.01)
        {
            base.FixedUpdate();
        }
    }

    /// <summary>
    /// Gets the position the group is moving to.
    /// </summary>
    public void UpdateTargetPos()
    {
        if (_aggroTarget != null)
        {
            if (_group != null)
            {   // try to spread position with groups
                Vector2 pos = _aggroTarget.transform.position;
                _patrollingTarget = pos.RandomVectorInRadius(_group.TargetSpread);
            }
            else
            {   // without a group
                _patrollingTarget = _aggroTarget.transform.position;
            }
        }
        else if (_group != null)
        {   // patrolling with group
            _patrollingTarget = _group.Target;
        }
    }

    IEnumerator TargetPosCycle()
    {
        yield return new WaitForSeconds(((float)Reaction) / 1000);

        UpdateTargetPos();

        StartCoroutine(TargetPosCycle());
    }

    public void SetAggroTarget(GameObject target)
    {
        _aggroTarget = target;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Body")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();
            if (target.tag == "Player")
            {
                _aggroTarget = target.gameObject;
                if (_group != null)
                {
                    _group.SignalAggro(target.transform.position);

                    //SignalPlayer(target);
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
                if (Vector2.Distance(transform.position, target.transform.position) > 4)
                {
                    _aggroTarget = null;
                }
            }
        }

        
    }

    public enum UpdateCycle
    {
        Fast = 500,
        Normal = 1000,
        Slow = 2000
    }

    private void SignalPlayer(CharacterStats c)
    {
        PlayerStats playerstats = c.GetComponent<PlayerStats>();

        if (playerstats != null)
        {
            playerstats.PlayerCombatStart();
        }
    }


}
