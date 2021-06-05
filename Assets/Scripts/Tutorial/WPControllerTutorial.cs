using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPControllerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weakPoints;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialControl()
    {
        
    }

    void PhaseOne()
    {

    }

    void PhaseTwo()
    {

    }

    void PhaseThree()
    {
        
    }

    void RestartWeakPoints()
    {
        foreach(GameObject wp in weakPoints)
        {
            wp.SetActive(true);
        }
    }
}
