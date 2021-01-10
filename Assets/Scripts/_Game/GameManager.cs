using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public string activeBlockTag;
    public int pullCharge = 1;

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
            pauseText.SetActive(!tetrisPaused);
            pauseText.GetComponent<DG.Tweening.DOTweenAnimation>().enabled = false;
        }
    }
}
