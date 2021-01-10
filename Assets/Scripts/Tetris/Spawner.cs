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
        if(GameManager.Instance.spawnBlock) { spawnNext(); }
    }

    public void spawnNext()
    {
        StartCoroutine(Spawn());
    }
    
    IEnumerator Spawn()
    {
        GameManager.Instance.spawnBlock = false;
        yield return new WaitForSeconds(0.1f);

        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        var obj = Instantiate(groups[i], transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
        
    }
}
