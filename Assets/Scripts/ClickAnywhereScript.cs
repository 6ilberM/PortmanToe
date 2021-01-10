using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

public class ClickAnywhereScript : MonoBehaviour
{
    public TransitionManager loader;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SoundManager.Instance.PlaySound("TitleClick");
            loader.LoadScene("GameScene");
        }
    }
    IEnumerator Fade()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            this.GetComponent<TMP_Text>().enabled = false;
            yield return new WaitForSeconds(1f);
            this.GetComponent<TMP_Text>().enabled = true;
        }
    }
}
