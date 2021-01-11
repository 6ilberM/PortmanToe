using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string activeBlockTag;

    public int activeBlockRot = 0;

    public bool canPlace;
    public int pullCharge = 1;
    public int spawnPoint;
    public int lives = 3;
    public GameObject[] spawnedBlocks;

    //ToDo: Replace With PlaceBlockEvent

    [Space(8)]
    public UnityEvent onPullBlock;

    [Space(6)]
    public UnityEvent onPlaceBlock;

    [Space(6)]
    public UnityEvent onSpawnBlock;

    [Space(6)]
    public UnityEvent onGameOver;

    public bool tetrisPaused = false;
    public GameObject pauseText;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else { _instance = this; }
    }

    [ContextMenu("Debug: GameOver")]
    public void DebugGameOver() => onGameOver?.Invoke();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tetrisPaused = !tetrisPaused;

            pauseText.SetActive(tetrisPaused);
        }
    }
}
