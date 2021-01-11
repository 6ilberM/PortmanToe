using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Store the different groups of blocks
    public GameObject[] groups;

    private void Awake() => GameManager.Instance.onSpawnBlock.AddListener(SpawnNext);

    void Start() => SpawnNext();

    public void SpawnNext() { StartCoroutine(Spawn()); }

    IEnumerator Spawn()
    {
        yield return new WaitForEndOfFrame();

        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i], transform.position, Quaternion.identity, gameObject.transform);

    }
}
