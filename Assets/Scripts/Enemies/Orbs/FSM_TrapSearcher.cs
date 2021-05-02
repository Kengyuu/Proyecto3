using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_TrapSearcher : MonoBehaviour
{
   

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    

     EnemyBehaviours behaviours;

    GameObject trap;


    float closeEnoughTarget;

    public Orb_Blackboard blackboard;


    public enum State { INITIAL, WANDERING, GOINGTOTRAP, DEACTIVATINGTRAP };
    public State currentState;



    void OnEnable()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Orb_Blackboard>();
        behaviours = GetComponent<EnemyBehaviours>();

        blackboard.SetOrbHealth(3);
        ReEnter();
        
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

    
    void Update()
    {

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;

            case State.WANDERING:
                //behaviours.SearchPlayerOrb();

                trap = behaviours.SearchObjectOrb("PasiveTrap");
                //Debug.Log(corpse.name);
                if (trap != null)
                {
                    //if (trap.GetComponent<PassiveTrap>() != null && trap.GetComponent<PassiveTrap>().GetTrapActive())
                    ChangeState(State.GOINGTOTRAP);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance)
                {
                    ChangeState(State.WANDERING);
                }
                break;
            case State.GOINGTOTRAP:
               // behaviours.SearchPlayerOrb();
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughTrapRadius)
                {
                    ChangeState(State.DEACTIVATINGTRAP);
                }

                break;

            case State.DEACTIVATINGTRAP:
                behaviours.SearchPlayerOrb();
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
                enemy.isStopped = false;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                enemy.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();

                break;
            case State.GOINGTOTRAP:
                target = trap;
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.DEACTIVATINGTRAP:
                enemy.isStopped = true;
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
