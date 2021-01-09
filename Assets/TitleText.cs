using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TitleText : MonoBehaviour
{
    public Light2D lightToFade;
    public float eachFadeTime = 2f;
    public float fadeWaitTime = 5f;

    void Start()
    {
        StartCoroutine(fadeInAndOutRepeat(lightToFade, eachFadeTime, fadeWaitTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator fadeInAndOut(Light2D lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = .5f; // min intensity
        float maxLuminosity = 5; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }
    IEnumerator fadeInAndOutRepeat(Light2D lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            //Fade out
            yield return fadeInAndOut(lightToFade, false, duration);

            //Wait
            yield return waitForXSec;

            //Fade-in 
            yield return fadeInAndOut(lightToFade, true, duration);
        }
    }
}
