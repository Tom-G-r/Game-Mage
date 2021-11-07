using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Transform[] Patrolling;
    public int PatrolDuration = 10;
    public float TargetSpread = 3;

    public Vector2 Target { get { return _target.RandomVectorInRadius(TargetSpread); } }
    protected Vector2 _target;

    protected GroupState State = GroupState.Patrolling;
    private int currentPatrollingPosition = 0;

    private float HasteCooldown = 10000;
    private float NextHasteTimer = 0;

    // Start is called before the first frame update
    void Start()
    {

        if (Patrolling == null || Patrolling.Length == 0)
        {
            Patrolling = new Transform[] { transform };
        }

        StartCoroutine(GoPatrolling());
    }

    public enum GroupState
    {
        Patrolling,
        Aggro
    }


    IEnumerator GoPatrolling()
    {
        if (State == GroupState.Patrolling) {
            if (++currentPatrollingPosition >= Patrolling.Length)
            {
                currentPatrollingPosition = 0;
            }

            _target = Patrolling[currentPatrollingPosition].position;
        }

        yield return new WaitForSeconds(PatrolDuration);
        StartCoroutine(GoPatrolling());
    }

    public void SignalAggro(Vector2 pos)
    {

        _target = pos.RandomVectorInRadius(TargetSpread);

        foreach (Transform child in transform)
        {
            if (child.tag == "Enemy")
            {
                // Apply Haste Buff
                if (Time.time >= NextHasteTimer)
                {
                    child.gameObject.AddComponent<Haste>();
                }

                // Update Moving Destination
                EnemyMovement mov = child.GetComponent<EnemyMovement>();
                if (mov != null)
                {
                    mov.UpdateTargetPos();
                }
            }
        }

        NextHasteTimer = Time.time + (HasteCooldown/1000);
    }

}
