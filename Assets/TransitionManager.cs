using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public Animator transition;
   

    public void LoadScene(string name)
    {
        StartCoroutine("SceneLoader", name);
    }

    IEnumerator SceneLoader(string scene)
    {
        transition.SetTrigger("leave");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
}
