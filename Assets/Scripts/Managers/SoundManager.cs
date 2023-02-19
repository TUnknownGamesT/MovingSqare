using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Singleton

    public static SoundManager instance;
    public bool soundOn, musicOn;

    private void Awake()
    {
        instance = FindObjectOfType<SoundManager>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    private AudioSource audioSource;
    public List<Constants.SoundClips> soundClipsList;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioSetup();
    }
    private void AudioSetup()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            musicOn = true;
            this.GetComponent<AudioSource>().Play();
        }
        else
        {
            musicOn = false;
        }
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            soundOn = true;
        }
        else
        {
            soundOn = false;
        }
    }

    public void EnemyCollisionSound()
    {
        if (soundOn)
        {
            audioSource.PlayOneShot(soundClipsList[0].audioClip);
        }
    }

    public void PickCoinSound()
    {
        if (soundOn)
        {
           audioSource.PlayOneShot(soundClipsList[1].audioClip);   
        }
    }


    public void PlayerDeathSound()
    {
        if (soundOn)
        {
            audioSource.PlayOneShot(soundClipsList[2].audioClip);
        }
    }
    
}
