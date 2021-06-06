using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public GameObject trapOrb;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTrap()
    {
        if(animator != null)
            animator.SetBool("TrapActive", false);
        trapOrb.GetComponent<TrapOrbTutorial>().ActivateAuraParticles();
    }

    public void DeactivateTrap()
    {
        animator.SetBool("TrapActive", true);
    }
}
