using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Singleton

    public static SoundManager instance;

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
    }


    public void EnemyCollisionSound()
    {
        audioSource.PlayOneShot(soundClipsList[0].audioClip);
    }
    
}
