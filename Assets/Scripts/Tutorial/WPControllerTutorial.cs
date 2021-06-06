using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPControllerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weakPoints;
    public int wpLeft;

    int currentPhase;

    public GameObject tutorialRoomOne;

    public GameObject tutorialRoomTwo;
    public GameObject tutorialRoomThree;

    void Start()
    {
        wpLeft = 3;
        currentPhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialControl()
    {
        if(wpLeft <= 0)
        {
            switch(currentPhase)
            {
                case 0:
                    currentPhase++;
                    PhaseOne();
                    break;
                case 1:
                    currentPhase++;
                    PhaseTwo();
                    break;
                case 2:
                    currentPhase++;
                    PhaseThree();
                    break;
            }

            Invoke("RestartWeakPoints", 0.1f);
            
        }
    }

    void PhaseOne()
    {
        tutorialRoomOne.SetActive(true);
    }

    void PhaseTwo()
    {
        tutorialRoomTwo.SetActive(true);
    }

    void PhaseThree()
    {
        tutorialRoomThree.SetActive(true);   
    }

    void RestartWeakPoints()
    {
        foreach(GameObject wp in weakPoints)
        {
            wp.SetActive(true);
        }

        wpLeft = 3;
        
    }
}
