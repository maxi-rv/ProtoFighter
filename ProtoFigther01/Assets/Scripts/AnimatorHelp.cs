using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelp : MonoBehaviour
{
    private PlayerController parentScript;

    void Awake()
    {
        parentScript = gameObject.GetComponentInParent<PlayerController>();
    }
    void StopHurt()
    {
        parentScript.StopHurt();
    }

    void StopStartup()
    {
        parentScript.StopStartup();
    }

    void StopActive()
    {
        parentScript.StopActive();
    }

    void StopRecovery()
    {
        parentScript.StopRecovery();
    }
    
}
