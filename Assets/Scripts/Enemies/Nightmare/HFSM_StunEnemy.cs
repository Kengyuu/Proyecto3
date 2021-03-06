using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HFSM_StunEnemy : MonoBehaviour
{
    public FSM_CorpseWander corpseWander;

    public Enemy_BLACKBOARD blackboard;
    public FSM_SeekPlayer seekPlayer;
    public enum State {INITIAL, SEARCHCORPSES, SEEKPLAYER, STUNNED, INVOKE, DEAD, WIN};
    public State currentState;

    private ScoreManager m_ScoreManager;
    public bool isStunned;
    public bool isInvoking;
    public bool canInvoke;
    public bool isDead;
    public bool hasWon;

    float currentInvokeTime;
    float currentStunTime;
    float maxStunTime;

    NavMeshAgent navMesh;

    void Start()
    {
        isInvoking = false;
        canInvoke = false;
        isDead = false;
        isStunned = false;
        hasWon = false;
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        currentStunTime = 0;
        currentInvokeTime = 0f;
        maxStunTime = blackboard.stunTime;
        corpseWander = GetComponent<FSM_CorpseWander>();
        seekPlayer = GetComponent<FSM_SeekPlayer>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void Exit()
    {
        navMesh.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.INITIAL:
                ChangeState(State.SEARCHCORPSES);
                break;
            case State.SEARCHCORPSES:

                if(isDead)
                {
                    ChangeState(State.DEAD);
                }
                if (hasWon)
                {
                    ChangeState(State.WIN);
                }
                if (isStunned)
                {
                    ChangeState(State.STUNNED);
                }
                if(isInvoking && canInvoke)
                {
                    ChangeState(State.INVOKE);
                }
                if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.LOOKFORPLAYER)
                {
                    ChangeState(State.SEEKPLAYER);
                }
                break;
            case State.SEEKPLAYER:
                if(isDead)
                {
                    ChangeState(State.DEAD);
                }
                if (hasWon)
                {
                    ChangeState(State.WIN);
                }
                if (isStunned)
                {
                    ChangeState(State.STUNNED);
                }
                if(isInvoking && canInvoke)
                {
                    ChangeState(State.INVOKE);
                }
                if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.SEARCHCORPSES)
                {
                    ChangeState(State.SEARCHCORPSES);
                }
                break;
            
            case State.INVOKE:
                if(isDead)
                {
                    ChangeState(State.DEAD);
                }
                if (hasWon)
                {
                    ChangeState(State.WIN);
                }
                currentInvokeTime += Time.deltaTime;
                if(currentInvokeTime >= blackboard.invokeTime)
                {
                    currentInvokeTime = 0;
                    if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.LOOKFORPLAYER)
                    {
                        ChangeState(State.SEEKPLAYER);
                    }
                    if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.SEARCHCORPSES)
                    {
                        ChangeState(State.SEARCHCORPSES);
                    }
                }
                
                break;
            case State.STUNNED:
                if(isDead)
                {
                    ChangeState(State.DEAD);
                }
                if (hasWon)
                {
                    ChangeState(State.WIN);
                }
                currentStunTime += Time.deltaTime;
                if(currentStunTime >= maxStunTime)
                {
                    currentStunTime = 0;
                    if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.LOOKFORPLAYER)
                    {
                        ChangeState(State.SEEKPLAYER);
                    }
                    if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.SEARCHCORPSES)
                    {
                        ChangeState(State.SEARCHCORPSES);
                    }
                    GetComponent<EnemyPriorities>().ChangePriority();
                }
                break;

        }
    }

    void ChangeState(State newState)
    {
        switch (currentState)
        {
            case State.SEARCHCORPSES:
                corpseWander.Exit();
                break;
            case State.SEEKPLAYER:
                seekPlayer.Exit();
                break;
            case State.STUNNED:
                
                isStunned = false;
                currentStunTime = 0f;
                if(GetComponent<Enemy>().GetLife() <= 0)
                {
                    GetComponent<Enemy>().SpawnWeakPoints();
                }
                
                break;
            case State.INVOKE:
                
                currentInvokeTime = 0f;
                isInvoking = false;
                canInvoke = false;
                break;
        }

        switch (newState)
        {

            case State.SEARCHCORPSES:
                
                corpseWander.ReEnter();
                break;
            case State.SEEKPLAYER:
               
                seekPlayer.ReEnter();
                break;

            case State.STUNNED:
                
                currentStunTime = 0f;
                blackboard.animatorController.Stunned();
                break;
            case State.INVOKE:
                navMesh.isStopped = true;
               
                currentInvokeTime = 0f;
                blackboard.animatorController.StartInvoking();
                break;
            case State.DEAD:
                blackboard.animatorController.Dead();
                corpseWander.Exit();
                seekPlayer.Exit();
                GetComponent<EnemyPriorities>().enabled = false;
                break;
            case State.WIN:
                blackboard.animatorController.TransitionPesadillaWins();
                break;

        }

        currentState = newState;
    }
}
