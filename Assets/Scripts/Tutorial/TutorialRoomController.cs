using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;


public class TutorialRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sideWall;

    public GameObject subRoom;
    public GameObject corpseOrb;
    public GameObject trapOrb;

    public GameObject trigger;

    [Header("FMOD Events")]

    public string platformEvent;

    public void PlatformEffectSound()
    {
        SoundManager.Instance.PlayEvent(platformEvent, trigger.transform);
    }

    public void FinishSetPositionAnimation()
    {
        if(sideWall != null)
            sideWall.SetActive(false);
    }
}