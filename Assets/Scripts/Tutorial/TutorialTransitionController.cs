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
        Initiate.Fade("Game", Color.black, 2);
        //SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
    }
}
