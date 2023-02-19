using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    
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
