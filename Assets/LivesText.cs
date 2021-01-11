using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesText : MonoBehaviour
{
    int oldCharge;
    // Start is called before the first frame update
    void Start()
    {
        oldCharge = GameManager.Instance.lives;
        this.GetComponent<TMP_Text>().text = "Lives x " + GameManager.Instance.lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.lives != oldCharge)
        {
            UpdateText();
            oldCharge = GameManager.Instance.lives;
        }
    }
    void UpdateText()
    {
        this.GetComponent<TMP_Text>().text = "Lives x " + GameManager.Instance.lives;
    }
}
