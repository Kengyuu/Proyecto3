using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTransitionController : MonoBehaviour
{
    public TutorialMusic music;
    public WPControllerTutorial blackholeEffect;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Invoke("LoadLevel", 1.5f);
        }
            
    }

    void LoadLevel()
    {
        music.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        blackholeEffect.black.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.Instance.SetGameState(GameState.GAME);
        Initiate.Fade("GAME_SLIDES", Color.black, 1f);
    }
}
