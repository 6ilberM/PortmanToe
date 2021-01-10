using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisIW : MonoBehaviour
{
    public GameObject tetris;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            tetris.SetActive(true);
            //tetris.GetComponent<Animator>().SetTrigger("Play");
            Destroy(this.gameObject);
        }

    }
}
