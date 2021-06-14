using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AnimatorController : MonoBehaviour
{
    Animator animator;
    public FSM_SeekPlayer enemy;
    public FSM_CorpseWander enemyCorpse; 
    public Enemy_BLACKBOARD blackboard;

    public GameMusic music;

    [Header("FMOD Events")]
    public string stepEvent;
    public string slashEvent;
    public string roarAttackEvent;
    public string deathEvent;
    public string roarEvent;
    public string summonEvent;
    public string absorbEvent;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayStepSound()
    {
       
        SoundManager.Instance.PlayEvent(stepEvent,transform);
    }
    public void PlayAbsorbSound()
    {

        SoundManager.Instance.PlayEvent(absorbEvent, transform);
    }
    public void PlaySlashSound()
    {
        
        SoundManager.Instance.PlaySound(slashEvent, transform.position);
    }

    public void PlayroarAttackSound()
    {

        SoundManager.Instance.PlayEvent(roarAttackEvent, transform);
    }
    public void PlayDeathSound()
    {

        SoundManager.Instance.PlaySound(deathEvent, transform.position);
    }
    public void PlayRoarSound()
    {

        SoundManager.Instance.PlaySound(roarEvent, transform.position);
    }
    public void PlaySummonSound()
    {

        SoundManager.Instance.PlaySound(summonEvent, transform.position);
    }
    public void WalkAgressiveEnter()
    {
        TransitionWalkCalm(false);
        animator.SetBool("State_Agressive", true);

        int random = Random.Range(0, 2);

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

    public void DecideComboAttack()
    {
        int random = Random.Range(0, 2);
        
        animator.SetInteger("ComboPhase", random);
        
    }

    public void TransitionWalkCalm(bool factor)
    {
        animator.SetBool("State_Calm", factor);
    }

    public void TransitionPesadillaWins()
    {
        animator.SetTrigger("State_Win");
    }

    public void PesadillaHasWon()
    {
        music.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music.chase.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
       
        GameManager.Instance.SetGameState(GameState.GAME_OVER);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; //TBD
    }

    public void PesadillaHasLost()
    {
        music.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music.chase.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.Instance.SetGameState(GameState.WIN);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; //TBD
    }

    public void EnemyCanMove()
    {
        //Debug.Log("Enemycanmove");
        if(enemy.gameObject.activeSelf && enemy.enemy != null)
        {
            enemy.enemy.isStopped = false;
            enemy.enemy.speed = 6;
        }
        if(enemyCorpse.gameObject.activeSelf && enemy.enemy != null)
        {
            enemyCorpse.enemy.isStopped = false;
            enemy.enemy.speed = 6;
        }
        enemy.isAttacking = false;
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
            enemy.enemy.speed = 0;
        }
        if(enemyCorpse.gameObject.activeSelf)
        {
            enemyCorpse.enemy.isStopped = true;
            enemy.enemy.speed = 0;

        }
        
    }

    public void PlayerStunned()
    {
        animator.SetTrigger("PlayerStunned");
        enemy.isProvoking = true;
        enemy.isAttacking = false;
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
        Debug.Log("La vida es una mierda");
    }

    public void FinishInvoking()
    {
        animator.ResetTrigger("SpawnWatcher");
    }

    public void HeadNotLookAtPlayer()
    {
        blackboard.head.weight = 0f;
    }
}
