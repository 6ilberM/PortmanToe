using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PausedText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Blink()
    {
        Debug.Log("Enumerator Started");
        if (this.enabled)
        {
            while (true)
            {
                if (GameManager.Instance.tetrisPaused)
                {
                    Debug.Log("In the loop");
                    this.GetComponent<TMP_Text>().enabled = true;
                    yield return new WaitForSeconds(1f);
                    this.GetComponent<TMP_Text>().enabled = false;
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    this.GetComponent<TMP_Text>().enabled = false;
                }

            }
        }
    }
}
