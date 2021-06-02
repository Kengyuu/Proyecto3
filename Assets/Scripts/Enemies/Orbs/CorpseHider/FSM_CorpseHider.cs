using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FSM_CorpseHider : MonoBehaviour
{
    [Header("Attributes")]
    public GameObject target; 
    private Orb_Blackboard blackboard;
    EnemyBehaviours behaviours;
    public Image icon;
    public Animator anim;
    [Header("State")]
    public State currentState;
    public enum State { INITIAL, INVOKING, WANDERING, RETURNINGTOENEMY };
    

    void OnEnable()
    {
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
                ChangeState(State.INVOKING);
                break;

            case State.INVOKING:
                blackboard.cooldownToReappear -= Time.deltaTime;
                if (blackboard.cooldownToReappear <= 0)
                {
                    ChangeState(State.WANDERING);
                    blackboard.cooldownToReappear = 4;
                }

                break;

            case State.WANDERING:
                blackboard.navMesh.SetDestination(target.transform.position);
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
            case State.INVOKING:
                anim.SetBool("Invoke", false);
                blackboard.navMesh.isStopped = false;
                break;
            case State.WANDERING:
                break;
        }

        // Enter logic
        switch (newState)
        {
            case State.INVOKING:
                anim.SetBool("Invoke", true);
                blackboard.navMesh.isStopped = true;
                blackboard.navMesh.Warp(blackboard.orbSpawner.transform.position);
                break;

            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();
                break;


        }

        currentState = newState;

    }
}
