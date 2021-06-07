using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sideWall;

    public GameObject subRoom;
    public GameObject corpseOrb;
    public GameObject trapOrb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishSetPositionAnimation()
    {
        if(sideWall != null)
            sideWall.SetActive(false);
        if(corpseOrb != null && trapOrb != null)
            StartOrbsAnimations();
    }


    public void StartOrbsAnimations()
    {
        corpseOrb.GetComponent<CorpseOrbTutorial>().ArsorbCorpse();
        trapOrb.GetComponent<TrapOrbTutorial>().ActivateAuraParticles();
    }
}
