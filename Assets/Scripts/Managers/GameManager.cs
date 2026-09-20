using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    #region Singleton Pattern
    private static GameManager instance;
    public static GameManager Instance => instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

    #region Lives
    [Range(0, 9)]
    public int startingLives = 3;
    public int maxLives = 9;
    private int _lives = 3;

    public System.Action<int> OnLivesChanged;

    
    public int Lives
    {
        get => _lives;
        set
        {
            if (value > maxLives)
            {
                maxLives = value;
            }
            else if (value < 0)
            {
                _lives = 0;
                GameOver();
            }
            else if (value < _lives)
            {
                _lives = value;
                Respawn();
            }
            else
            {
                _lives = value;
            }

            OnLivesChanged?.Invoke(_lives);
            Debug.Log("Lives: " + _lives.ToString() + " Max Lives: " + maxLives.ToString());

        }
    }
    #endregion

    [SerializeField] private PlayerController playerPrefab;
    private PlayerController playerInstance;
    public PlayerController PlayerInstance => playerInstance;

    private Vector3 currentCheckpoint;


    public delegate void PlayerInstanceDelegate(PlayerController player);
    public event PlayerInstanceDelegate OnPlayerSpawned;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string sceneToLoad = currentSceneName == "1.Title" ? "2.Game" : "1.Title";

            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Lives++;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Lives--;
        }
    }

    public void SpawnPlayer(Vector3 pos)
    {
        Lives = startingLives;

        playerInstance = Instantiate(playerPrefab, pos, Quaternion.identity);
        OnPlayerSpawned?.Invoke(playerInstance);
        UpdateCheckpoint(pos);
    }

    public void UpdateCheckpoint(Vector3 newPos)
    {
        currentCheckpoint = newPos;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    private void Respawn()
    {
        playerInstance.transform.position = currentCheckpoint;
    }
}
