using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sideWall;
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
    }
}
