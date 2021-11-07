using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    public Color color;

    public Object sceneToLoad;
    public Transform Boss;
    public GameObject LoadingScreen;

    private GameObject PortalLight;
    private BoxCollider2D bc;
    private Animator anim;
    private AudioSource[] audiosources;
    private AudioSource audio1, audio2;
    private GameObject particles;
    private ParticleSystem particlesystem;
    private SpriteRenderer sr;
    private bool activated;
    private ParticleSystem.MainModule module;

    private GameManager _gamemanager = null;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        audiosources = gameObject.GetComponents<AudioSource>();
        audio1 = audiosources[0];
        audio2 = audiosources[1];
        bc = gameObject.GetComponent<BoxCollider2D>();
        PortalLight = gameObject.transform.GetChild(0).gameObject;
        anim = gameObject.GetComponent<Animator>();
        particles = gameObject.transform.GetChild(1).gameObject;
        particlesystem = particles.GetComponent<ParticleSystem>();
        module = particlesystem.main;
        sr = gameObject.GetComponent<SpriteRenderer>();

        _gamemanager = FindGameManager();

        LoadingScreen.SetActive(false);
    }

    void FixedUpdate()
    { 
        if (!activated)
        {
            if (IsBossDefeated())
            {
                if (!activated)
                {
                    activated = true;
                    StartCoroutine(Activate());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Legs")
        {
            CharacterStats target = other.GetComponentInParent<CharacterStats>();

            if (target != null && target.tag == "Player")
            {
                StartCoroutine(EnterPortal());
            }
        }
    }

    IEnumerator Activate()
    {
        // End Battle Music
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            PlayerStats player = playerObj.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.PlayerOutOfCombat();
            }
        }

        //Color
        sr.material.SetColor("_Color", color);
        module.startColor = new ParticleSystem.MinMaxGradient(new Color(color.r / 2, color.g / 2, color.b / 2));
        // Sound
        audio1.Play();
        // Animation
        anim.Play("activated");
        //wait until sound finished playing
        yield return new WaitWhile(() => audio1.isPlaying);
        // Light & Effects
        particles.SetActive(true);
        PortalLight.SetActive(true);
        // Collider
        bc.enabled = true;
        
    }

    IEnumerator EnterPortal()
    {
        IncreaseLevel();
        if (_gamemanager.IsGameCleared)
        {
            // Show EndScreen
            _gamemanager.ShowGameClearedCanvas();
        } else
        {
            LoadingScreen.SetActive(true);
            audio2.Play();
            yield return new WaitWhile(() => audio2.isPlaying);

            
            PlayerPrefs.Save();

            if (_gamemanager != null)
            {
                _gamemanager.UpdateLevelTextField();
            }

            // Load next dungeon
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }

    private bool IsBossDefeated()
    {
        if (Boss == null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Increases level of the game
    /// </summary>
    private void IncreaseLevel()
    {
        if (_gamemanager != null)
        {
            _gamemanager.IncreaseLevel();
        }
    }

    private GameManager FindGameManager()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameManager");

        if (obj != null)
        {
            GameManager gm = obj.GetComponent<GameManager>();
            if (gm != null)
            {
                return gm;
            }
        }

        return null;
    }

}
