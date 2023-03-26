using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public static float platformSpeed = 2f;

    [Serializable]
    public class SoundClips
    {
        public string name;
        public AudioClip audioClip;
    }
    
    public enum Directions
    {
        E,
        V,
        W
    }
}
