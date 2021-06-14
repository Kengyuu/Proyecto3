using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorialRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public WPControllerTutorial wpControllerTutorial;

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
            if(wpControllerTutorial.currentPhase < 3)
            {
                wpControllerTutorial.RestartWeakPoints();
            }
            else
            {
                wpControllerTutorial.TutorialControl();
            }

            GetComponent<SpawnTutorialRoom>().enabled = false;
        }
    }
}
