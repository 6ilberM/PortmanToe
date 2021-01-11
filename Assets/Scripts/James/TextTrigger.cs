using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public WorldTextScript WTS;
    public float delay = 1f;
    bool isTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            if (collision.CompareTag("Player"))
            {
                WTS.StartType(delay);
            }
        }
        isTriggered = true;
    }
}
