using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTutorialController : MonoBehaviour
{
    Animator animator;
    public GameObject trapOrb;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ActivateTrap()
    {
        if(animator != null)
            animator.SetBool("TrapActive", false);
        trapOrb.GetComponent<TrapOrbTutorial>().ActivateAuraParticles();
    }

    public void DeactivateTrap()
    {
        if(animator != null)
            animator.SetBool("TrapActive", true);
    }
}
