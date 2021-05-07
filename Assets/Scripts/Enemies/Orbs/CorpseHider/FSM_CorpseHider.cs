using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseHider : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject target; 
    private Orb_Blackboard blackboard;
    EnemyBehaviours behaviours;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL, WANDERING, RETURNINGTOENEMY };
    

    void OnEnable()
    {
        blackboard.navMesh = GetComponent<NavMeshAgent>();
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(3);
        ReEnter();
      
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
                ChangeState(State.WANDERING);
                break;

            case State.WANDERING:
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                {
                    ChangeState(State.WANDERING);
                    break;
                }
                break;
        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.WANDERING:
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();
                break;

        }

        currentState = newState;

    }
}
