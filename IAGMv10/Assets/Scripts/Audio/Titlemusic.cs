using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlemusic : MonoBehaviour
{
    public AudioSource audiosource;
    float v;

    void Start()
    {
        audiosource.volume = 0.5f;
    }
    void Update()
    {
        audiosource.volume = PlayerPrefs.GetFloat("volume");
    }
}
