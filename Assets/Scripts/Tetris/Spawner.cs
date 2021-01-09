using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Store the different groups of blocks
    public GameObject[] groups;
    void Start()
    {
        spawnNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        var obj = Instantiate(groups[i], transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
    }
}
