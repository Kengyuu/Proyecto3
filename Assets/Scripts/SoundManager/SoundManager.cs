using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance = null;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public static SoundManager Instance
    {
        get
        {
            if (instance == null) instance = new SoundManager();
            return instance;
        }
    }
    void Start()
    {
        
    }

    public void PlaySound(string path, Vector3 trans)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, trans);
        //UnityEngine.Debug.Log("Sound");
    }

    public EventInstance PlayEvent(string path,Transform trans)
    {
        EventInstance sound = RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, trans, trans.gameObject.GetComponent<Rigidbody>());
        sound.set3DAttributes(RuntimeUtils.To3DAttributes(trans));
        
        sound.start();
        return sound;
    }

    public EventInstance StopEvent(EventInstance sound)
    {
        sound.clearHandle();
        sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        return sound;
    }







}
