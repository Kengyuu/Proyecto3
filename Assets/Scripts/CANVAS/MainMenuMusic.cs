using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("FMOD Events")]
    public string mainMenuEvent;


    void Start()
    {
        SoundManager.Instance.PlaySound(mainMenuEvent, transform.position);
    }

}
