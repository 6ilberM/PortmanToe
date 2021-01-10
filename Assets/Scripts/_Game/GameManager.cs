using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string activeBlockTag;
    public int pullCharge = 1;
    public bool pullBlock;
    public bool spawnBlock;
    public bool tetrisPaused = true;
    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            tetrisPaused = !tetrisPaused;
        }
    }
}
