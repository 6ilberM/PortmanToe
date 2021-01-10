using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Platformer/Tetriminio Spawner")]
public class TetriminioBlockSpawner : MonoBehaviour
{
    public GameObject IBlock = default;
    public GameObject JBlock = default;
    public GameObject LBlock = default;
    public GameObject OBlock = default;
    public GameObject SBlock = default;
    public GameObject TBlock = default;
    public GameObject ZBlock = default;

    public UnityEvent onTetrisPull;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.pullCharge > 0)
            {
                GameManager.Instance.pullBlock = true;
                SoundManager.Instance.PlaySound("SpawnBlock");

                switch (GameManager.Instance.activeBlockTag)
                {
                    case "I":
                        Debug.Log("Got an I block");
                        break;

                    case "J":
                        Debug.Log("Got an J block");
                        break;

                    case "L":
                        Debug.Log("Got an L block");
                        break;

                    case "O":
                        Debug.Log("Got an O block");
                        break;

                    case "S":
                        Debug.Log("Got an S block");
                        break;

                    case "T":
                        Debug.Log("Got an T block");
                        break;

                    case "Z":
                        Debug.Log("Got an Z block");
                        break;
                }
                GameManager.Instance.pullCharge--;

            }
        }
    }
}
