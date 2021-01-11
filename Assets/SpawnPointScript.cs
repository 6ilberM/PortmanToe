using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    int spawnIndex;
    void Start()
    {
        if(this.gameObject.name == "SpawnPoint1")
        {
            spawnIndex = 1;
        } else
        {
            spawnIndex = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.spawnPoint = spawnIndex;
        }
    }
}
