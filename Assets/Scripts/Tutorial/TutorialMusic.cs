using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMusic : MonoBehaviour
{
    [Header("FMOD Events")]
    public string tutorialEvent;


    void Start()
    {
        SoundManager.Instance.PlaySound(tutorialEvent, transform.position);
    }
}
