using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    public int DPS = 1;
    public float Duration = 4.1f;
    private GameObject BurningParticleSystem;
    private GameObject ParticleInstance;
    private ParticleSystem Particles;


    protected CharacterStats Character;

    // Start is called before the first frame update
    void Start()
    {
        InitParticles();
        Character = gameObject.GetComponent<CharacterStats>();

        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    IEnumerator Effect()
    {
        yield return new WaitForSeconds(2);

        if (Character != null)
        {
            int dmg = DPS;

            if (Duration < 1)
            {
                Destroy(ParticleInstance);
                Destroy(this);
                // dmg *= Duration;
            }
            Character.ReceiveDamage(dmg);
            Duration -= 1;

            if (Duration > 0)
            {
                StartCoroutine(Effect());
            } else
            {
                Destroy(ParticleInstance);
                Destroy(this);
            }
           
        }
    }

    private void InitParticles()
    {
        BurningParticleSystem = (GameObject)Resources.Load("prefabs/BurningParticleSystem", typeof(GameObject));
        Particles = BurningParticleSystem.GetComponent<ParticleSystem>();
        /*var shape = Particles.shape;
        SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriterenderer != null)
        {
            shape.sprite = spriterenderer.sprite;
        } */
        ParticleInstance = Instantiate(BurningParticleSystem, gameObject.transform);
        ParticleInstance.transform.parent = gameObject.transform;

        //BurningParticleSystem.transform.parent = gameObject.transform;
    }


}
