using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource clipSource;
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        clipSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound(string s)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == s)
            {
                clipSource.PlayOneShot(sounds[i]);
            }
        }
    }
}
