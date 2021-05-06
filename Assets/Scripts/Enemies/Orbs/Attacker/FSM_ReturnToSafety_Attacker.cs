using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Attacker : MonoBehaviour
{
    //public GameObject target;
    FSM_AttackerOrb Attacker;
    Orb_Blackboard blackboard;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY };
    public State currentState;

    void Start()
    {
        blackboard = GetComponent<Orb_Blackboard>();
        //blackboard.navMesh = GetComponent<NavMeshAgent>();

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
                break;

            case State.RETURNINGTOENEMY:
                ReEnter();
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
                break;

            case State.RETURNINGTOENEMY:

                Spawn();
                gameObject.SetActive(false);

                break;

        }

        currentState = newState;

    }

    void Spawn()
    {
        OrbEvents.current.StartCoroutine(OrbEvents.current.RespawnOrbs(gameObject));
    }




}