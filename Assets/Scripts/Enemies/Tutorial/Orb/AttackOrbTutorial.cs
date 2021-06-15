using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrbTutorial : MonoBehaviour
{
    Animator animator;
    public LineRenderer line;
    bool lookAtPlayer;

    public GameObject player;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(lookAtPlayer)
        {
            transform.LookAt(player.transform.position);
        }
    }

    public void setRotateTrue()
    {
        lookAtPlayer = true;
    }

    public void setRotateFalse()
    {
        lookAtPlayer = false;
    }

    public void setLaserTrue()
    {
        line.enabled = true;
    }

    public void setLaserFalse()
    {
        line.enabled = false;
    }
}
