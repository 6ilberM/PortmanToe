using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PullTextScript : MonoBehaviour
{
    int oldCharge;
    // Start is called before the first frame update
    void Start()
    {
        oldCharge = GameManager.Instance.pullCharge;
        this.GetComponent<TMP_Text>().text = "Pull x " + GameManager.Instance.pullCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.pullCharge != oldCharge)
        {
            UpdateText();
            oldCharge = GameManager.Instance.pullCharge;
        }
    }
    void UpdateText()
    {
        this.GetComponent<TMP_Text>().text = "Pull x " + GameManager.Instance.pullCharge;
    }
}
