using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private AudioSource audioSource;

    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource warningMusic;
    [SerializeField] private AudioSource buttonSFX;
    [SerializeField] private AudioSource correctSFX;
    [SerializeField] private AudioSource incorrectSFX;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
    }


    public void PlayMenuMusic()
    {
        if (!menuMusic) return;

        PlayMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        if (!gameMusic) return;

        PlayMusic(gameMusic);
    }

    public void PlayWarningMusic()
    {
        if (!warningMusic) return;

        PlayMusic(warningMusic);
    }

    private void PlayMusic(AudioSource music)
    {
        if (music.clip == audioSource.clip) return;
        audioSource.Stop();
        audioSource.clip = music.clip;
        audioSource.outputAudioMixerGroup = music.outputAudioMixerGroup;
        audioSource.Play();
    }

    public void PlayButtonSFX()
    {
        if (!buttonSFX) return;

        buttonSFX.Play();
    }

    public void PlayAnswerSFX(bool isCorrect)
    {
        if (!correctSFX || !incorrectSFX) return;

        if (isCorrect)
        {
            correctSFX.Play();
        }
        else
        {
            incorrectSFX.Play();
        }
    }
    
}
