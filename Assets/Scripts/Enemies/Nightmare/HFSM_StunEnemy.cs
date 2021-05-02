using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HFSM_StunEnemy : MonoBehaviour
{
    public FSM_CorpseWander corpseWander;
    public FSM_SeekPlayer seekPlayer;
    public enum State {INITIAL, SEARCHCORPSES, SEEKPLAYER, STUNNED};
    public State currentState;

    public bool isStunned;
    float currentStunTime;
    float maxStunTime;

    NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        currentStunTime = 0;
        maxStunTime = GetComponent<Enemy_BLACKBOARD>().stunTime;
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
                if(isStunned)
                {
                    ChangeState(State.STUNNED);
                }
                if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.LOOKFORPLAYER)
                {
                    ChangeState(State.SEEKPLAYER);
                }
                break;
            case State.SEEKPLAYER:
            if(isStunned)
                {
                    ChangeState(State.STUNNED);
                }
                if(GetComponent<EnemyPriorities>().currState == EnemyPriorities.EnemyStates.SEARCHCORPSES)
                {
                    ChangeState(State.SEARCHCORPSES);
                }
                break;
            case State.STUNNED:
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
                navMesh.isStopped = false;
                isStunned = false;
                currentStunTime = 0f;
                if(GetComponent<Enemy>().GetLife() <= 0)
                {
                    GetComponent<Enemy>().SpawnWeakPoints();
                }
                
                break;
        }

        switch (newState)
        {

            case State.SEARCHCORPSES:
                navMesh.isStopped = false;
                corpseWander.ReEnter();
                break;
            case State.SEEKPLAYER:
                navMesh.isStopped = false;
                corpseWander.ReEnter();
                break;

            case State.STUNNED:
                navMesh.isStopped = true;
                currentStunTime = 0f;
                break;

        }

        currentState = newState;
    }
}
