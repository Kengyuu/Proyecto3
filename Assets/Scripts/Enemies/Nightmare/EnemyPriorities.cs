using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPriorities : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyStates {LOOKFORPLAYER, SEARCHCORPSES}
    
    public EnemyStates currState;
    FSM_CorpseWander searchCorpse;
    FSM_SeekPlayer seekPlayer;
    
    Enemy_BLACKBOARD blackboard;

    public bool playerSeen;

    public float playerCorpses;
    public float enemyCorpses;
    public float remainingCorpses;

    GameManager GM;

    ScoreManager m_ScoreManager;

    //public delegate void Priority(EnemyStates state);
    //public event Priority changePriority;
    void Start()
    {
        GM = GameManager.Instance;
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        searchCorpse = GetComponent<FSM_CorpseWander>();
        seekPlayer = GetComponent<FSM_SeekPlayer>();
        currState = EnemyStates.SEARCHCORPSES;
        playerSeen = false;
        playerCorpses = m_ScoreManager.GetPlayerCorpses();
        enemyCorpses = m_ScoreManager.GetEnemyCorpses();
        remainingCorpses = m_ScoreManager.GetRemainingCorpses();
    }

    public void DetectPlayerActions()
    {
        if(DetectionFunctions.DistanceToTarget(gameObject, GM.GetPlayer()) < blackboard.detectionSensingRadius)
        {
            playerSeen = true;
            currState = EnemyStates.LOOKFORPLAYER;
            ActivateFSM();
            seekPlayer.target = GM.GetPlayer();
        }
    }

    private void Update()
    {
        ChangePriority();
    }

    public void ChangePriority()
    {
        playerCorpses = m_ScoreManager.GetPlayerCorpses();
        enemyCorpses = m_ScoreManager.GetEnemyCorpses();
        remainingCorpses = m_ScoreManager.GetRemainingCorpses();
        if(playerSeen)
        {
            currState = EnemyStates.LOOKFORPLAYER;
        }
        else
        {
            if(playerCorpses < 4)
            {
                if(enemyCorpses >= 8)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }
                else
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }
                currState = EnemyStates.LOOKFORPLAYER;
            }

            if(enemyCorpses >= 4 && enemyCorpses <= 6)
            {
                if(enemyCorpses < playerCorpses)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }

                if(enemyCorpses > playerCorpses && (enemyCorpses - playerCorpses) <= 2)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }
                if(enemyCorpses == playerCorpses && enemyCorpses == 6)
                {
                    currState = EnemyStates.LOOKFORPLAYER;
                }
            }

            if(remainingCorpses == 0)
            {
                currState = EnemyStates.LOOKFORPLAYER;
            }
            
            if(enemyCorpses >= 4 && enemyCorpses <= 6)
            {
                if(playerCorpses < enemyCorpses && (enemyCorpses - playerCorpses) > 2)
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }

                if(playerCorpses == enemyCorpses && enemyCorpses != 6)
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }
            }

            if(enemyCorpses >= 7 && enemyCorpses <= 9)
            {
                if(remainingCorpses > 0)
                {
                    currState = EnemyStates.SEARCHCORPSES;
                }
            }
        }

        //changePriority.Invoke(currState);
        ActivateFSM();
    }

    public void ActivateFSM()
    {
        switch(currState)
        {
            case EnemyStates.SEARCHCORPSES:
                seekPlayer.Exit();
                searchCorpse.ReEnter();
                break;
            case EnemyStates.LOOKFORPLAYER:
                searchCorpse.Exit();
                seekPlayer.ReEnter();
                break;
        }
    }
}
