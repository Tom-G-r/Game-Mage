using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chest : MonoBehaviour
{
    public GameObject Key;
    public GameObject ChestEnemy;
    public float TransformationChance;

    public float LootChance;
    public GameObject[] Powerups;
    private GameObject Loot;
    private bool _hasLoot;
    private bool _isLooted;

    private Animator anim;
    private BoxCollider2D[] cols;
    private BoxCollider2D col1;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        _isLooted = false;
        int powerupindex = Random.Range(0, Powerups.Length);
        Loot = Powerups[powerupindex];
        float number = Random.Range(0f, 1f);
        if (number <= LootChance)
        {
            _hasLoot = true;
        }
        else
        {
            _hasLoot = false;
        }
        cols = gameObject.GetComponents<BoxCollider2D>();
        col1 = cols[1];
    }

    public void TransformToEnemy()
    {
        Vector2 pos = transform.position;
        var rot = transform.rotation;
        Destroy(gameObject);

        GameObject _player = GameObject.FindGameObjectWithTag("Player");

        // Get position thats not on player
        int attempt = 0;
        while (_player != null && Vector2.Distance(_player.transform.position, pos) < 0.4f && attempt < 5)
        {
            attempt++;
            pos = pos.RandomVectorInRadius(1f + (0.25f * attempt));
        }

        Instantiate(ChestEnemy, pos, rot);
    }

    public void OpenChest()
    {
        float number = Random.Range(0f, 1f);
        if (number <= TransformationChance)
        {
            TransformToEnemy();
        }
        else
        {
            if (_hasLoot)
            {
                StartCoroutine(OpenFullChest());
            }
            else
            {
                OpenEmptyChest();
            }
        }
    }

    public void OpenEmptyChest()
    {
        _isLooted = true;
        anim.Play("OpenEmptyChest");
        FindObjectOfType<AudioManager>().Play("Chest");
    }

    IEnumerator OpenFullChest()
    {
        _isLooted = true;
        anim.Play("OpenFullChest");
        FindObjectOfType<AudioManager>().Play("Chest");
        yield return new WaitForSeconds(0.5f);
        DropLoot();
        anim.Play("OpenedEmptyChest");
    }

    public void DropLoot()
    {
        Instantiate(Loot, transform.position, transform.rotation);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Legs")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                if (Input.GetKey(KeyCode.F) && !_isLooted)
                {
                    col1.enabled = false;
                    OpenChest();
                }
            }
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Legs")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                Key.SetActive(true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Legs")
        {
            CharacterStats target = collision.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                Key.SetActive(false);
            }
        }
    }
}
