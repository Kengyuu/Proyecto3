using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;
public class MainMenuMusic : MonoBehaviour
{

    [Header("FMOD Events")]
    public string mainMenuEvent;
    public EventInstance music;

    void Start()
    {
       music = SoundManager.Instance.PlayEvent(mainMenuEvent, transform);
    }

}
