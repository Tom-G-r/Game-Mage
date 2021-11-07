using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSpell : MonoBehaviour
{

    public int Amount = 1;
    public int Interval = 1000;
    public float Radius = 3;
    public GameObject[] Enemies;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        if (Enemies.Length > 0)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(Summon());

        }
    }


    IEnumerator Summon()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < Amount; i++)
        {

            Vector2 pos = gameObject.transform.position;
            pos = pos.RandomVectorInRadius(Radius);

            // Get position thats not on player
            int attempt = 0;
            while(_player != null && Vector2.Distance(_player.transform.position, pos) < 0.75f && attempt < 5)
            {
                attempt++;
                pos = pos.RandomVectorInRadius(Radius + (0.5f*attempt));
            }

            if (_player != null && Vector2.Distance(_player.transform.position, pos) > 0.75f)
            {
                int rand = Random.Range(0, Enemies.Length);
                GameObject summoned = Instantiate(Enemies[rand], pos, Quaternion.identity);

                CircleCollider2D col = summoned.GetComponent<CircleCollider2D>();

                if (col != null)
                {
                    col.enabled = false;
                    col.enabled = true;
                }
            }

        }

        yield return new WaitForSeconds(Interval/1000);

        StartCoroutine(Summon());
    }

}
