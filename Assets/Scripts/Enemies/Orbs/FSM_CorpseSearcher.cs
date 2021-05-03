using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseSearcher : MonoBehaviour
{

    [Header("Parameters")]
    public Orb_Blackboard blackboard;
    public GameObject target;
    

    EnemyBehaviours behaviours;
    GameObject corpse;


    public enum State { INITIAL, WANDERING, GOINGTOCORPSE, RETURNINGTOENEMY, GRABBINGCORPSE };
    public State currentState;



    void OnEnable()
    {
        
       
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
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
                corpse = behaviours.SearchObject("Corpse", blackboard.corpseDetectionRadius);
                //Debug.Log(corpse.name);
                if (corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                 {
                     ChangeState(State.WANDERING);
                 }
                break;
            case State.GOINGTOCORPSE:
                if (target.tag != "Corpse" || !target.activeSelf)
                {
                    ChangeState(State.WANDERING);
                }
                else
                {
                    if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                    {
                        ChangeState(State.GRABBINGCORPSE);
                    }
                }

                break;

            case State.GRABBINGCORPSE:
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                 if (blackboard.cooldownToGrabCorpse <= 0)
                 {
                     behaviours.GrabCorpse(target, blackboard.cooldownToGrabCorpse);
                     ChangeState(State.RETURNINGTOENEMY);
                     break;
                 }
                break;
            case State.RETURNINGTOENEMY:
                  target = behaviours.ReturnToEnemy();
                  if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance + 1)
                  {
                      ChangeState(State.WANDERING);
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
                blackboard.lastCorpseSeen = null;
                break;
            case State.GRABBINGCORPSE:
                target.tag = "Corpse";
                blackboard.orbCorpseStored = corpse;
                blackboard.navMesh.isStopped = false;
                break;
            case State.RETURNINGTOENEMY:
                if (!corpse.activeSelf || corpse == null)
                {
                    behaviours.AddCorpseToScore();
                    corpse = null;
                }
                 blackboard.orbCorpseStored = null;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb(); 
                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.GRABBINGCORPSE:
                blackboard.navMesh.isStopped = true;
                blackboard.cooldownToGrabCorpse = 3f;
                target.tag = "PickedCorpse";
                break;

            case State.RETURNINGTOENEMY:
                blackboard.lastCorpseSeen = corpse;
                break;

        }

        currentState = newState;

    }


    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, blackboard.corpseDetectionRadius);
        if (corpse != null)
            Gizmos.DrawLine(transform.position, corpse.transform.position);
    }
}
