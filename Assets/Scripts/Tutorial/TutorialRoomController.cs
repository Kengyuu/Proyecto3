using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sideWall;
    public GameObject corpseOrb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishSetPositionAnimation()
    {
        sideWall.SetActive(false);
        if(corpseOrb != null)
            StartOrbsAnimations();
    }


    public void StartOrbsAnimations()
    {
        corpseOrb.GetComponent<CorpseOrbTutorial>().ArsorbCorpse();
    }
}
