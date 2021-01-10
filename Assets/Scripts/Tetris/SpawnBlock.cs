using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public Transform spawnPointTran;
    public GameObject IBlock;
    public GameObject JBlock;
    public GameObject LBlock;
    public GameObject OBlock;
    public GameObject SBlock;
    public GameObject TBlock;
    public GameObject ZBlock;
    

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.Instance.pullCharge > 0)
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
