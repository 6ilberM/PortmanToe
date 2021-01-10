using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausedText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.tetrisPaused)
        {
            this.GetComponent<TMP_Text>().enabled = true;
        } else
        {
            this.GetComponent<TMP_Text>().enabled = false;
        }
    }
}
