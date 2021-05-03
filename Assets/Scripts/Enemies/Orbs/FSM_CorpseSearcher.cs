using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseSearcher : MonoBehaviour
{
    

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    

    EnemyBehaviours behaviours;
    GameObject corpse;

    public Orb_Blackboard blackboard;

    



    public enum State { INITIAL, WANDERING, GOINGTOCORPSE, RETURNINGTOENEMY, GRABBINGCORPSE };
    public State currentState;



    void OnEnable()
    {
        enemy = GetComponent<NavMeshAgent>();
       
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
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
                corpse = behaviours.SearchObject("Corpse", blackboard.corpseDetectionRadius);
                //Debug.Log(corpse.name);
                if (corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance)
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
                  if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance + 1)
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
                enemy.isStopped = false;
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
                enemy.isStopped = false;
                target = behaviours.PickRandomWaypointOrb(); // PILLA DE ENEMY BLACKBOARD
                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.GRABBINGCORPSE:
                enemy.isStopped = true;
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
