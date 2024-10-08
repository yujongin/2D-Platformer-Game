using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CineCam;
    public LifeDisplayer LifeDisplayerInstance;
    int life = 3;

    public PlayerController Player;

    [SerializeField]
    private GameObject popupCanvas;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public TextMeshProUGUI timeLimitLabel;
    public float TimeLimit = 30;
    public ObjectPool BulletPool;



    private bool isCleared;
    public bool IsCleared
    {
        get { return isCleared; }
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(LevelManager.Instance.SelectedPrefab);
        LifeDisplayerInstance.SetLives(life);
    }

    // Update is called once per frame
    void Update()
    {
        TimeLimit -= Time.deltaTime;
        timeLimitLabel.text = "Time Left" + ((int)TimeLimit);

        if (TimeLimit < 0)
        {
            GameOver();
        }
    }

    public void AddTime(float time)
    {
        TimeLimit += time;
    }
    public void Dead()
    {
        CineCam.SetActive(false);
        life--;
        LifeDisplayerInstance.SetLives(life);

        Invoke("Restart", 2);
    }

    void Restart()
    {
        if (life > 0)
        {
            CineCam.SetActive(true);
            Player.Restart();
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isCleared = false;
        popupCanvas.SetActive(true);
    }

    public void GameClear()
    {
        isCleared = true;
        popupCanvas.SetActive(true);
    }
}
