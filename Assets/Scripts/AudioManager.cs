using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusic;

    public AudioSource[] SFX;
    private void Awake()
    {
        instance = this;
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayerSFX(int SFXNumber)
    {
        SFX[SFXNumber].Stop();
        SFX[SFXNumber].Play();
    }
}
