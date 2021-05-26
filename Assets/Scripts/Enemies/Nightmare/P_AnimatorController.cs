using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AnimatorController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WalkAgressiveEnter()
    {
        TransitionWalkCalm(false);
        animator.SetBool("State_Agressive", true);

        int random = Random.Range(0, 1);

        animator.SetInteger("WalkRandom", random);

    }
    public void WalkAgressiveExit()
    {
        TransitionWalkCalm(true);
        animator.SetBool("State_Agressive", false);
    }

    public void AttackStart()
    {
        animator.SetTrigger("StartAttack");
    }

    public void TransitionWalkCalm(bool factor)
    {
        animator.SetBool("State_Calm", factor);
    }

     
}
