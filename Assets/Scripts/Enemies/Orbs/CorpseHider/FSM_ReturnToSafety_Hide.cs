using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Hide : MonoBehaviour
{
    public GameObject target;
    FSM_CorpseHider corpseHide;
    Orb_Blackboard blackboard;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY,DEAD };
    public State currentState;



    void Start()
    {
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);

        corpseHide = GetComponent<FSM_CorpseHider>();
    }

    public void Exit()
    {
        blackboard.navMesh.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;

    }

  
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
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER)
                {
                    ChangeState(State.DEAD);
                }
                break;

            case State.RETURNINGTOENEMY:
                
                ReEnter();
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER)
                {
                    ChangeState(State.DEAD);
                }
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
                corpseHide.ReEnter();
                break;

            case State.RETURNINGTOENEMY:
                corpseHide.ReEnter();
                Spawn();
                break;
            case State.DEAD:
                blackboard.navMesh.isStopped = true;
                corpseHide.Exit();
                break;

        }

        currentState = newState;

    }

    void Spawn()
    {
        OrbEvents.current.RespawnOrbs(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && currentState == State.NORMALBEHAVIOUR)
        {
            corpseHide.target = blackboard.behaviours.PickRandomWaypointOrb();
        }

    }
}
