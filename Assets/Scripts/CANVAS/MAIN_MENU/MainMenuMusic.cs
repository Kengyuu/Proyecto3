using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;
public class MainMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("FMOD Events")]
    public string mainMenuEvent;
    public EventInstance music;

    void Start()
    {
       music = SoundManager.Instance.PlayEvent(mainMenuEvent, transform);
    }

}
