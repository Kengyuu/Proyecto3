using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPriorities : MonoBehaviour
{
    public enum EnemyStates {LOOKFORPLAYER, SEARCHCORPSES}
    
    public EnemyStates currState;
    FSM_CorpseWander searchCorpse;
    FSM_SeekPlayer seekPlayer;
    
    Enemy_BLACKBOARD blackboard;

    public bool playerSeen;

    public bool playerDetected;

    public float playerCorpses;
    public float enemyCorpses;
    public float remainingCorpses;

    public float HeadLookAtMaxWeight;

    GameManager GM;

    ScoreManager m_ScoreManager;

    void Start()
    {
        GM = GameManager.Instance;
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        searchCorpse = GetComponent<FSM_CorpseWander>();
        seekPlayer = GetComponent<FSM_SeekPlayer>();
        currState = EnemyStates.SEARCHCORPSES;
        playerSeen = false;
        playerDetected = false;
        playerCorpses = m_ScoreManager.GetPlayerCorpses();
        enemyCorpses = m_ScoreManager.GetEnemyCorpses();
        remainingCorpses = m_ScoreManager.GetRemainingCorpses();
        ActivateFSM();

        GM.OnPlayerNoise += DetectPlayerActions;
    }

    private void OnDestroy()
    {
        GM.OnPlayerNoise -= DetectPlayerActions;
    }

    void Update()
    {
        if(playerSeen)
        {
            if(seekPlayer.currentState == FSM_SeekPlayer.State.PROVOKING)
            {
                 blackboard.head.weight = 0;
            }
            else
            {
                if(blackboard.head.weight < HeadLookAtMaxWeight)
                    blackboard.head.weight = Mathf.Lerp(blackboard.head.weight, HeadLookAtMaxWeight, 0.5f);
            }
            
        }
        else
        {
            if(blackboard.head.weight > 0)
            {
                blackboard.head.weight = Mathf.Lerp(blackboard.head.weight, 0, 0.5f);
            }
        }
    }

   public void DetectPlayerActions(float playerDistance)
    {
        if(GM.gameState == GameState.GAME)
        {
            if(!playerSeen)
            {
                if (DetectionFunctions.DistanceToTarget(gameObject, GM.GetPlayer()) < playerDistance && !GetComponent<HFSM_StunEnemy>().isStunned)
                {
                    playerDetected = true;
                    currState = EnemyStates.LOOKFORPLAYER;
                    ChangePriority();
                    //ActivateFSM();
                    seekPlayer.target = GM.GetPlayer();
                }
            }
            
        }
        
    }
    public void ChangePriority()
    {
        if(m_ScoreManager != null)
        {
            playerCorpses = m_ScoreManager.GetPlayerCorpses();
            enemyCorpses = m_ScoreManager.GetEnemyCorpses();
            remainingCorpses = m_ScoreManager.GetRemainingCorpses();
        }
        EnemyStates prevState = currState;
        if((playerSeen || playerDetected) )
        {
            currState = EnemyStates.LOOKFORPLAYER;
        }
        else
        {
            if(playerCorpses < 4)
            {
                if(enemyCorpses >= 7)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }
                else
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }
            }
            else
            {
                if(enemyCorpses >= 7)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }

                if(enemyCorpses >= 0 && enemyCorpses <= 5)
                {
                    if(enemyCorpses < playerCorpses && playerCorpses < 7)
                    {
                        currState = EnemyStates.LOOKFORPLAYER;
                    }
                }

                
                
                if(enemyCorpses >= 0 && enemyCorpses <= 5)
                {
                    if (enemyCorpses > playerCorpses)
                    {
                        currState = EnemyStates.SEARCHCORPSES;
                    }

                    if (playerCorpses == enemyCorpses)
                    {
                        currState = EnemyStates.SEARCHCORPSES;
                    }

                    if (playerCorpses >= 7)
                    {
                        currState = EnemyStates.SEARCHCORPSES;
                    }
                }

            }

            
            if(enemyCorpses == 6)
            {
                if(remainingCorpses > 0)
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }
            }

            if(remainingCorpses == 0)
            {
                currState = EnemyStates.LOOKFORPLAYER;
            }
        }
        if(currState != prevState || playerSeen || playerDetected)
            ActivateFSM();
    }

    public void ActivateFSM()
    {
        switch(currState)
        {
            case EnemyStates.SEARCHCORPSES:
                if(seekPlayer != null)
                {
                    seekPlayer.Exit();
                    searchCorpse.ReEnter();
                }
              
                break;
            case EnemyStates.LOOKFORPLAYER:
                searchCorpse.Exit();
                seekPlayer.ReEnter();
                break;
        }
    }
}
