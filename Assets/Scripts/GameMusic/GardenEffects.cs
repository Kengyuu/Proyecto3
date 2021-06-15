using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;


public class GardenEffects : MonoBehaviour
{
    public string cricketEvent;
    EventInstance crickets;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") crickets = SoundManager.Instance.PlayEvent(cricketEvent, transform);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") crickets.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void OnDestroy()
    {
        crickets.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
