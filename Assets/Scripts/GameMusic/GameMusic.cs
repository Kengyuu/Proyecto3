using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;


public class GameMusic : MonoBehaviour
{
    private GameManager GM;
    public HFSM_StunEnemy enemy;
    public EventInstance music;
    public EventInstance chase;

    public float cooldownRegularMusic = 0;
    public float cooldownChaseMusic = 0;

    [Header("FMOD Events")]
    public string regularMusicEvent;
    public string chaseMusicEvent;

    private void Awake()
    {
        if (GM == null) GM = GameManager.Instance;
    }

    private void Update()
    {
        if (enemy.currentState == HFSM_StunEnemy.State.SEARCHCORPSES)
        {
            cooldownChaseMusic = 0;
            chase.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (cooldownRegularMusic <= 0)
            {
                music = SoundManager.Instance.PlayEvent(regularMusicEvent, transform);
                cooldownRegularMusic = 900;
            }
           
        }
        else if ((enemy.currentState == HFSM_StunEnemy.State.SEEKPLAYER))
        {
            cooldownRegularMusic = 0;
            music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (cooldownChaseMusic <= 0)
            {
                chase = SoundManager.Instance.PlayEvent(chaseMusicEvent, transform);
                cooldownChaseMusic = 180;
            }
           

        }
    }

}
