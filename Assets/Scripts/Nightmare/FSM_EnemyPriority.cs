using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_EnemyPriority : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent enemy;
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
        Player = GameManager.Instance.GetPlayer();
        corpseWander = GetComponent<FSM_CorpseWander>();
        seekPlayer = GetComponent<FSM_SeekPlayer>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
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
                    if(blackboard.playerCorpses < blackboard.enemyCorpses && (blackboard.enemyCorpses - blackboard.playerCorpses) > 2 && !playerSeen)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                    if(blackboard.playerCorpses == blackboard.enemyCorpses && blackboard.enemyCorpses != 6 && !playerSeen)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                }

                if(blackboard.enemyCorpses >= 7 && blackboard.enemyCorpses <= 9)
                {
                    if(blackboard.remainingCorpses > 0 && !playerSeen)
                    {
                        ChangeState(State.CORPSEWANDER);
                    }
                }

                if(blackboard.enemyCorpses < 4)
                {
                    if(blackboard.playerCorpses < 8 && !playerSeen)
                    {
                        ChangeState(State.CORPSEWANDER);
                        Debug.Log("Los devuelvo yo");
                    }
                }

                

                break;
            case State.STUNNED:

                currentStunTime += Time.deltaTime;
                if (currentStunTime >= stunTime)
                {
                    currentStunTime = 0;
                    GetComponent<EnemyWeakPointsController>().SpawnWeakPoints();
                    if (playerSeen)
                    {
                        ChangeState(State.SEEKPLAYER);
                    }

                    else ChangeState(State.CORPSEWANDER);
                }
               
                break;


        }


    }

    public void GetStunned()
    {
        ChangeState(State.STUNNED);
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
                playerSeen = false;
                currentStunTime = 0;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.CORPSEWANDER:
                corpseWander.ReEnter();
                //playerSeen = false;
                break;
            case State.SEEKPLAYER:
                seekPlayer.ReEnter();
                break;
            case State.STUNNED:
               
                if (blackboard.enemyCorpses > 0)
                {
                    int lostEnemyCorpses = Mathf.Max(1, Mathf.RoundToInt(blackboard.enemyCorpses / 3));
                    blackboard.enemyCorpses -= lostEnemyCorpses;
                   
                    GameManager.Instance.m_ScoreManager.SetEnemyCorpses(blackboard.enemyCorpses);

                    blackboard.remainingCorpses += lostEnemyCorpses;
                    GameManager.Instance.m_ScoreManager.SetRemainingCorpses(blackboard.remainingCorpses);
                    GameManager.Instance.m_gameObjectSpawner.SpawnBodys(lostEnemyCorpses);
                } 
               
                else blackboard.enemyCorpses = 0;

                enemy.isStopped = true;
                currentStunTime = 0;
                break;

        }

        currentState = newState;

    }

    /*void SpawnOrbs()
    {
        switch(GameManager.Instance.m_ScoreManager.GetPlayerCorpses())
        {
            case 3:
                
                break;
            case 6:
                break;
        }
    }*/
}
