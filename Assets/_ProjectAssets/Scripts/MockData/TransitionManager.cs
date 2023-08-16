using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public TransitionSettings TransitionSettings;

[ContextMenu("Test")]
    public void LoadScene()
    {
        EasyTransition.TransitionManager.Instance().Transition(1,TransitionSettings,0);
    }
}
