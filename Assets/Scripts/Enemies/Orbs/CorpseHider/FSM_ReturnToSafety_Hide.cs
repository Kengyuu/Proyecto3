using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Hide : MonoBehaviour
{
    public GameObject target;
    FSM_CorpseHider corpseHide;
    Orb_Blackboard blackboard;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY };
    public State currentState;



    void Start()
    {
        //blackboard.navMesh = GetComponent<NavMeshAgent>();
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
                Spawn();
               // gameObject.SetActive(false);
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
