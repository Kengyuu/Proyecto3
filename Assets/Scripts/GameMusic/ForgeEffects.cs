using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;


public class ForgeEffects : MonoBehaviour
{
    public string lavaEvent;
    EventInstance lava;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") lava = SoundManager.Instance.PlayEvent(lavaEvent, transform);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") lava.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    private void OnDestroy()
    {
        lava.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
