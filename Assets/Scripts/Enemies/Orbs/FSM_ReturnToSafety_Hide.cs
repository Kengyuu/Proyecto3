using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Hide : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    Vector3 spawnPosition;
    //private Enemy_BLACKBOARD blackboard;

    //EnemyBehaviours behaviours;

    GameObject corpse;

    FSM_CorpseHider corpseHide;
    Orb_Blackboard blackboard;



    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY };
    public State currentState;



    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);

        corpseHide = GetComponent<FSM_CorpseHider>();


    }

    public void Exit()
    {
        enemy.isStopped = false;
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

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.NORMALBEHAVIOUR);
                break;

            case State.NORMALBEHAVIOUR:
                if (blackboard.GetOrbHealth() <= 0)
                {
                    ChangeState(State.RETURNINGTOENEMY);
                }
                break;

            case State.RETURNINGTOENEMY:
                ChangeState(State.INITIAL);
                
                break;


        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.NORMALBEHAVIOUR:
                corpseHide.enabled = false;
                break;

        }

        // Enter logic
        switch (newState)
        {

            case State.NORMALBEHAVIOUR:
                corpseHide.enabled = true;
                break;

            case State.RETURNINGTOENEMY:

                enemy.Warp(GameManager.Instance.GetEnemy().transform.position);

                blackboard.SetOrbHealth(blackboard.m_maxLife);
                break;

        }

        currentState = newState;

    }


}
