using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_TrapSearcher : MonoBehaviour
{
   
    [Header("Attributes")]
    public GameObject target;
    public Orb_Blackboard blackboard;
    EnemyBehaviours behaviours;
    GameObject trap;

    public enum State { INITIAL, WANDERING, GOINGTOTRAP, DEACTIVATINGTRAP };
    public State currentState;

    void OnEnable()
    {
        blackboard.navMesh = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Orb_Blackboard>();
        behaviours = GetComponent<EnemyBehaviours>();

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
                

                trap = behaviours.SearchObject("PasiveTrap", blackboard.closeEnoughTrapRadius);
                
                if (trap != null)
                {
                    //if (trap.GetComponent<PassiveTrap>() != null && trap.GetComponent<PassiveTrap>().GetTrapActive())
                    ChangeState(State.GOINGTOTRAP);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                {
                    ChangeState(State.WANDERING);
                }
                break;
            case State.GOINGTOTRAP:
               
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughTrapRadius)
                {
                    ChangeState(State.DEACTIVATINGTRAP);
                }

                break;

            case State.DEACTIVATINGTRAP:
               
                blackboard.cooldownToDeactivateTrap -= Time.deltaTime;
                 if (blackboard.cooldownToDeactivateTrap <= 0)
                 {
                     behaviours.DeactivateTrap(target);
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
            case State.DEACTIVATINGTRAP:
                blackboard.navMesh.isStopped = false;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();

                break;
            case State.GOINGTOTRAP:
                target = trap;
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.DEACTIVATINGTRAP:
                blackboard.navMesh.isStopped = true;
                blackboard.cooldownToDeactivateTrap = 3f;
                break;

        }

        currentState = newState;

    }


    /*void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blackboard.senseRadius);
    }*/
}
