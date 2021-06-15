using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class WPControllerTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> weakPoints;
    public int wpLeft;

    public int currentPhase;

    public GameObject tutorialRoomOne;

    public GameObject tutorialRoomTwo;
    public GameObject tutorialRoomThree;

    public GameObject tutorialRoomFour;
    public GameObject blackHole;
    public GameObject textCenter;

    public GameObject wall;

    public GameObject decalLeft;
    public GameObject decalBottom;
    public GameObject decalRight;



    [Header("FMOD Events")]

    private string blackHoleEvent = "event:/4 ENVIRONMENT/WormHole";
    public string platformEvent;
    public EventInstance black;

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
                case 3:
                    currentPhase++;
                    PhaseFour();
                    break;
            }

            //Invoke("RestartWeakPoints", 0.1f);
            
        }
    }

    public void PlatformEffectSound(GameObject platform)
    {
        SoundManager.Instance.PlayEvent(platformEvent, transform);
    }

    void PhaseOne()
    {
        tutorialRoomOne.SetActive(true);
        decalLeft.SetActive(true);
    }

    void PhaseTwo()
    {
        tutorialRoomTwo.SetActive(true);
        decalBottom.SetActive(true);
    }

    void PhaseThree()
    {
        tutorialRoomThree.SetActive(true);   
        decalRight.SetActive(true);
    }

    void PhaseFour()
    {
        wall.SetActive(false);
        tutorialRoomFour.SetActive(true);
        textCenter.SetActive(false);
        blackHole.SetActive(true);
        GameObject[] lights = GameObject.FindGameObjectsWithTag("Light_Deactivate");
        foreach (GameObject g in lights)
        {
            g.SetActive(false);
        }
        black = SoundManager.Instance.PlayEvent(blackHoleEvent, blackHole.transform);
    }

    public void RestartWeakPoints()
    {
        foreach(GameObject wp in weakPoints)
        {
            wp.SetActive(true);
        }

        wpLeft = 3;
        
    }
}
