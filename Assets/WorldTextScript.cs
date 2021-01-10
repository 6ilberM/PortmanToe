using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldTextScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public void StartType(float delay)
    {
        StartCoroutine("Type", delay);
    }
    
    public IEnumerator Type(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
