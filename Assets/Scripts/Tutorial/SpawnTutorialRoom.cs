using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTutorialRoom : MonoBehaviour
{
    
    public WPControllerTutorial wpControllerTutorial;

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
