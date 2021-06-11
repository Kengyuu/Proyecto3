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

    public void FinishSetPositionAnimation()
    {
        if(sideWall != null)
            sideWall.SetActive(false);
    }
}