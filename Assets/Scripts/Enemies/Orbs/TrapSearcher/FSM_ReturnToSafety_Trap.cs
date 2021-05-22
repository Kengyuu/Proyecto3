using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Trap : MonoBehaviour
{
    public GameObject target;
    FSM_TrapSearcher trapSearch;
    Orb_Blackboard blackboard;

    private Quaternion rotation;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY };
    public State currentState;


    void Start()
    {
        //blackboard.navMesh = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);

        trapSearch = GetComponent<FSM_TrapSearcher>();
        rotation = transform.rotation;

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
                trapSearch.enabled = false;
                break;

            case State.RETURNINGTOENEMY:
                ReEnter();
                break;

        }

        // Enter logic
        switch (newState)
        {

            case State.NORMALBEHAVIOUR:
                Debug.Log("Reenable");
                trapSearch.enabled = true;
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
        trapSearch.m_Laser.enabled = false;
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        trapSearch.ChangeParticleColor();
        OrbEvents.current.StartCoroutine(OrbEvents.current.RespawnOrbs(gameObject));
        blackboard.navMesh.isStopped = false;
       
        trapSearch.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && currentState == State.NORMALBEHAVIOUR)
        {
            trapSearch.target = blackboard.behaviours.PickRandomWaypointOrb();
        }

    }

}
