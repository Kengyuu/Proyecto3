using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTransitionController : MonoBehaviour
{
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
            Invoke("LoadLevel", 1f);
    }

    void LoadLevel()
    {
        GameManager.Instance.SetGameState(GameState.GAME);
        Initiate.Fade("GAME_SLIDES", Color.black, 1f);
        //SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
    }
}
