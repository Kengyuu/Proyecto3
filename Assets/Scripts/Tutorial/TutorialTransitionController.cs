using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTransitionController : MonoBehaviour
{
     public TutorialMusic music;
    public WPControllerTutorial blackholeEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        //SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
    }
}
