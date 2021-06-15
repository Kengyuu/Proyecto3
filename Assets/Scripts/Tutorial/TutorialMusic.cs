using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class TutorialMusic : MonoBehaviour
{
    [Header("FMOD Events")]
    public string tutorialEvent;
    public EventInstance music;

    void Start()
    {
        music = SoundManager.Instance.PlayEvent(tutorialEvent, transform);
    }
}
