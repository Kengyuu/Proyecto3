using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrbTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public LineRenderer line;
    bool lookAtPlayer;

    public GameObject player;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
