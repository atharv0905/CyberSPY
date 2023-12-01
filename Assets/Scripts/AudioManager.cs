using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusic;

    private void Awake()
    {
        instance = this;
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}
