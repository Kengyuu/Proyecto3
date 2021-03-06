using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Attacker : MonoBehaviour
{
    FSM_AttackerOrb Attacker;
    Orb_Blackboard blackboard;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY,DEAD };
    public State currentState;

    void Start()
    {
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        Attacker = GetComponent<FSM_AttackerOrb>();


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
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER || GameManager.Instance.GetEnemy().GetComponent<HFSM_StunEnemy>().isDead || GameManager.Instance.GetEnemy().GetComponent<HFSM_StunEnemy>().hasWon)
                {
                    ChangeState(State.DEAD);
                }
                break;

            case State.RETURNINGTOENEMY:
             
                ReEnter();
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER || GameManager.Instance.GetEnemy().GetComponent<HFSM_StunEnemy>().isDead || GameManager.Instance.GetEnemy().GetComponent<HFSM_StunEnemy>().hasWon)
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
                Attacker.enabled = false;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.NORMALBEHAVIOUR:
                Attacker.enabled = true;
                Attacker.ReEnter();
                break;

            case State.RETURNINGTOENEMY:
                Attacker.ReEnter();
                Attacker.anim.SetBool("AttackOrb", false);
                Spawn();
                break;
            case State.DEAD:
                blackboard.navMesh.isStopped = true;
                Attacker.Exit();
                break;

        }

        currentState = newState;

    }

    void Spawn()
    {
        Attacker.m_Laser.enabled = false;
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        OrbEvents.current.RespawnOrbs(gameObject);
        blackboard.navMesh.isStopped = false;
        
        Attacker.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && currentState == State.NORMALBEHAVIOUR)
        {
            Attacker.target = blackboard.behaviours.PickRandomWaypointOrb();
        }

    }
}