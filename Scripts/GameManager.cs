using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject EndScreen;
    public GameObject LevelTextField;

    public int Levels = 7;
    public DifficultyScaling difficultyScaling = DifficultyScaling.Normal;

    private int _level = 1;
    public int Level => _level;
    public bool IsGameCleared => Level > Levels;

    private float _timeStarted;
    private float _timeEnded;

    public float Timer => _timeEnded - _timeStarted;

    // Amount of PowerUps the enemies get
    public int Difficulty => (fib(Level+2)-2) * (int)difficultyScaling;

    private GameObject _player;
    private CharacterStats _playerStats;

    private static GameManager instance;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Set difficulty
        string diffSetting = PlayerPrefs.GetString("Difficulty", "Normal");
        System.Enum.TryParse(diffSetting, out DifficultyScaling difficultyScaling);

        Debug.Log($"difficultyScaling: {difficultyScaling}");
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            _playerStats = _player.GetComponent<CharacterStats>();
        }

        _timeStarted = Time.time;
        _timeEnded = Time.time;

        StartCoroutine(CheckGameOver());

        UpdateLevelTextField();
    }

    void FixedUpdate()
    {
        if (!IsGameCleared)
        {
            _timeEnded = Time.time;
        }
    }

    /// <summary>
    /// Checks every second if player is dead. Shows GameOverScreen if player is dead.
    /// </summary>
    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(1);

        if (_player == null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").AddComponent<AudioListener>();
            FindObjectOfType<AudioManager>().Play("MageDeath");

            Instantiate(GameOverScreen);
        } 
        else
        {
            StartCoroutine(CheckGameOver());
        }
    }

    public void IncreaseLevel()
    {
        _level++;

        UpdateLevelTextField();
    }

    private int fib(int n)
    {
        if ((n == 0) || (n == 1))
        {
            return n;
        }
        else
            return fib(n - 1) + fib(n - 2);
    }

    public void ShowGameClearedCanvas()
    {
        // stop time
        _timeEnded = Time.time;

        //_playerStats.ReceiveDamage(_playerStats.Health);
        if (_player != null)
        {
            MovementBehavior mov = _player.GetComponent<MovementBehavior>();
            if (mov != null)
            {
                mov.CanMove = false;
            }

            AttackBehavior att = _player.GetComponent<AttackBehavior>();
            if (att != null)
            {
                att.CanAttack = false;
            }
        }

        Instantiate(EndScreen);
    }

    public void UpdateLevelTextField()
    {
        if (LevelTextField != null)
        {
            Text textField = LevelTextField.GetComponent<Text>();
            if (textField != null)
            {
                textField.text = $"Stage ({_level}/{Levels})";
            }
        }
    }

}
