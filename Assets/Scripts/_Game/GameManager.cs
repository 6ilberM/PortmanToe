using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string activeBlockTag;
    public int activeBlockRot;
    public int activeFakeRot;
    public bool canPlace;
    public bool destroyFake;
    public int pullCharge = 1;
    public int spawnPoint;
    public int lives = 3;
    public GameObject[] spawnedBlocks;

    [Space(8)]
    public UnityEngine.Events.UnityEvent onPullBlock;

    public bool spawnBlock;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tetrisPaused = !tetrisPaused;

            pauseText.SetActive(tetrisPaused);
        }
    }
}
