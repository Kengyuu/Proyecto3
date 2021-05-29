using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AnimatorController : MonoBehaviour
{
    Animator animator;
    public FSM_SeekPlayer enemy;
    public FSM_CorpseWander enemyCorpse; 

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

    public void StartStunEnemy()
    {
        animator.SetBool("isStunned", true);
    }

    public void EndStunEnemy()
    {
        animator.SetBool("isStunned", false);
    }
    public void AttackStart()
    {
        animator.SetTrigger("StartAttack");
    }

    public void TransitionWalkCalm(bool factor)
    {
        animator.SetBool("State_Calm", factor);
    }

    public void EnemyCanMove()
    {
        if(enemy.gameObject.activeSelf && enemy.enemy != null)
        {
            enemy.enemy.isStopped = false;
        }
        if(enemyCorpse.gameObject.activeSelf && enemy.enemy != null)
        {
            enemyCorpse.enemy.isStopped = false;
        }
    }

    public void ActivateRightArmColliders()
    {
        enemy.rightArm.GetComponent<BoxCollider>().enabled = true;
        enemy.leftArm.GetComponent<BoxCollider>().enabled = false;
    }

    public void DeactivateRightArmColliders()
    {
        enemy.rightArm.GetComponent<BoxCollider>().enabled = false;
        enemy.leftArm.GetComponent<BoxCollider>().enabled = false;
    }

    public void ActivateLeftArmColliders()
    {
        enemy.rightArm.GetComponent<BoxCollider>().enabled = false;
        enemy.leftArm.GetComponent<BoxCollider>().enabled = true;
    }

    public void DeactivateLeftArmColliders()
    {
        enemy.rightArm.GetComponent<BoxCollider>().enabled = false;
        enemy.leftArm.GetComponent<BoxCollider>().enabled = false;
    }
    public void DeactivateArmColliders(BoxCollider arm)
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    public void EnemyCantMove()
    {
        if(enemy.gameObject.activeSelf)
        {
            enemy.enemy.isStopped = true;
        }
        if(enemyCorpse.gameObject.activeSelf)
        {
            enemyCorpse.enemy.isStopped = true;
        }
        
    }

    public void PlayerStunned()
    {
        animator.SetTrigger("PlayerStunned");
    }

    public void Stunned()
    {
        animator.SetTrigger("Stunned");
    }

    public void Dead()
    {
        animator.SetTrigger("State_Dead");
    }
    public void StartCorpseChanneling()
    {
        animator.SetBool("State_Canalizing", true);
        animator.SetBool("State_Agressive", false);
    }

    public void CancelCorpseChanneling()
    {
        animator.SetBool("State_Canalizing", false);
        WalkAgressiveEnter();
    }

    public void FinishCorpseChanneling()
    {
        animator.SetTrigger("CanalizingCompleted");
        animator.SetBool("State_Canalizing", false);
    }

    public void StartInvoking()
    {
        animator.SetTrigger("SpawnWatcher");
    }
}
