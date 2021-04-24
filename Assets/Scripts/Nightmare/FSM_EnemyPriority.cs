using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_EnemyPriority : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent enemy;
    private Enemy_BLACKBOARD blackboard;
    private FSM_CorpseWander corpseWander;
    private FSM_SeekPlayer seekPlayer;
    GameObject Player;

    //public GameObject target;
    //public GameObject savedCorpse;
    //public Vector3 lastPlayerPosition;
    public enum State { INITIAL, CORPSEWANDER, SEEKPLAYER, STUNNED};
    public State currentState;

    public bool playerSeen = false;

    int totalCorpses;
    public float stunTime;
    public float currentStunTime;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        currentStunTime = 0f;
        Player = GameObject.FindGameObjectWithTag("Player");
        corpseWander = GetComponent<FSM_CorpseWander>();
        seekPlayer = GetComponent<FSM_SeekPlayer>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        totalCorpses = blackboard.enemyCorpses + blackboard.playerCorpses + blackboard.remainingCorpses;
        //child = gameObject.transform.GetChild(2);
        //Arm = child.gameObject;
    }

    public void Exit()
    {



    }

    public void ReEnter()
    {
        currentState = State.INITIAL;
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.CORPSEWANDER);
                break;

            case State.CORPSEWANDER:

                if(GetComponent<EnemyWeakPointsController>().currentWeakPoints <= 0)
                {
                    Debug.Log("Stunned");
                    ChangeState(State.STUNNED);
                }

                if(playerSeen)
                {
                    ChangeState(State.SEEKPLAYER);
                }
                else
                {
                    if(blackboard.enemyCorpses < 4)
                    {
                        if(blackboard.playerCorpses >= 8)
                        {
                            ChangeState(State.SEEKPLAYER);
                        }
                    }

                    if(blackboard.enemyCorpses >= 4 && blackboard.enemyCorpses <= 6)
                    {
                        if(blackboard.enemyCorpses < blackboard.playerCorpses)
                        {
                            ChangeState(State.SEEKPLAYER);
                        }
                        if(blackboard.enemyCorpses > blackboard.playerCorpses && (blackboard.enemyCorpses - blackboard.playerCorpses) <= 2)
                        {
                            ChangeState(State.SEEKPLAYER);
                        }
                        if(blackboard.enemyCorpses == blackboard.playerCorpses && blackboard.enemyCorpses == 6)
                        {
                            ChangeState(State.SEEKPLAYER);
                        }
                    }

                    if(blackboard.remainingCorpses == 0)
                    {
                        ChangeState(State.SEEKPLAYER);
                    }
                }
                
                
                break;
            case State.SEEKPLAYER:

                if(GetComponent<EnemyWeakPointsController>().currentWeakPoints <= 0)
                {
                    Debug.Log("Stunned");
                    ChangeState(State.STUNNED);
                }

                /*if(!playerSeen)
                {
                    ChangeState(State.CORPSEWANDER);
                }*/

                if(blackboard.enemyCorpses >= 4 && blackboard.enemyCorpses <= 6)
                {
                    if(blackboard.playerCorpses < blackboard.enemyCorpses && (blackboard.enemyCorpses - blackboard.playerCorpses) > 2)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                    if(blackboard.playerCorpses == blackboard.enemyCorpses && blackboard.enemyCorpses != 6)
                    {
                        ChangeState(State.SEEKPLAYER);
                    }
                }

                if(blackboard.enemyCorpses >= 7 && blackboard.enemyCorpses <= 9)
                {
                    if(blackboard.remainingCorpses > 0)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                }

                if(blackboard.enemyCorpses < 4)
                {
                    if(blackboard.playerCorpses < 8)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                }

                

                break;
            case State.STUNNED:
               
                currentStunTime += Time.deltaTime;
                if (currentStunTime >= stunTime)
                {
                    currentStunTime = 0;
                    GetComponent<EnemyWeakPointsController>().SpawnWeakPoints();
                    ChangeState(State.CORPSEWANDER);
                }
               
                break;


        }


    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.CORPSEWANDER:
                corpseWander.Exit();
                
                break;
            case State.SEEKPLAYER:
                seekPlayer.Exit();

                break;
            case State.STUNNED:
                enemy.isStopped = false;
                currentStunTime = 0;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.CORPSEWANDER:
                corpseWander.ReEnter();
                playerSeen = false;
                break;
            case State.SEEKPLAYER:
                seekPlayer.ReEnter();
                break;
            case State.STUNNED:
                enemy.isStopped = true;
                currentStunTime = 0;
                break;

        }

        currentState = newState;

    }
}
