using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static float platformSpeed = 2f;

    [System.Serializable]
    public class SoundClips
    {
        public Sounds name;
        public AudioClip audioClip;
    }
    
    public enum Directions
    {
        E,
        V,
        W
    }
    
    public enum Sounds
    {
        DestroyEnemy,
        PickCoin,
        PlayerDeath,
        PickLife,
        PlayerGetHit
    }
}
