using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseWander : MonoBehaviour
{


    [Header("AI")]
    public NavMeshAgent enemy;
    public GameObject target;
    public GameObject absorbParticles;
    private Enemy_BLACKBOARD blackboard;
    private HudController M_HudController;
    EnemyBehaviours behaviours;
    string enemyType;
    GameObject corpse;

    float currentInvokeTime = 0f;
    public LayerMask layer;

    public enum State {INITIAL, WANDERING, GOINGTOCORPSE, GRABBINGCORPSE};
    public State currentState;

    

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        behaviours = GetComponent<EnemyBehaviours>();
        enemyType = transform.tag;
        absorbParticles.SetActive(false);
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
    }


    public void Exit()
    {
        if(corpse != null)
            corpse.tag = "Corpse";
        enemy.isStopped = false;
        absorbParticles.SetActive(false);
        blackboard.animatorController.CancelCorpseChanneling();
        this.enabled = false;
        
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;
        absorbParticles.SetActive(false);
    }
    void Update()
    {

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;

            case State.WANDERING:
                behaviours.SearchPlayer(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer);
                enemy.SetDestination(target.transform.position);
                corpse = behaviours.SearchObject("Corpse", blackboard.corpseDetectionRadius);
                if(corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                {
                    ChangeState(State.WANDERING); 
                }
                break;
            case State.GOINGTOCORPSE:
                behaviours.SearchPlayer(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer);
                enemy.SetDestination(target.transform.position);
                if(target.tag != "Corpse")
                {
                    ChangeState(State.WANDERING);
                }
                else
                {
                    if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.corpsePickUpRadius)
                    {
                        ChangeState(State.GRABBINGCORPSE);
                    }
                }
                
                break;

            case State.GRABBINGCORPSE:
                behaviours.SearchPlayer(blackboard.playerDetectionRadius, layer);
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                if (blackboard.cooldownToGrabCorpse <= 0)
                {
                    behaviours.GrabCorpse(target, blackboard.cooldownToGrabCorpse);
                    behaviours.AddCorpseToScore();
                    M_HudController.UpdateAddCorpses(gameObject);
                    
                    blackboard.animatorController.FinishCorpseChanneling();
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
                blackboard.lastCorpseSeen = null;
                break;
            case State.GRABBINGCORPSE:
                target.tag = "Corpse";
                absorbParticles.SetActive(false);
                corpse = null;
                enemy.isStopped = false;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                enemy.isStopped = false;
                if (blackboard.lastCorpseSeen != null && blackboard.lastCorpseSeen.activeSelf)
                {
                    target = blackboard.lastCorpseSeen;
                    enemy.SetDestination(target.transform.position);
                    
                }
                else
                {
                    target = behaviours.PickRandomWaypoint();
                }
                GetComponent<HFSM_StunEnemy>().canInvoke = true;
                blackboard.animatorController.WalkAgressiveExit();
                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                enemy.SetDestination(target.transform.position);
                GetComponent<HFSM_StunEnemy>().canInvoke = true;
                break;

            case State.GRABBINGCORPSE:
                GetComponent<HFSM_StunEnemy>().canInvoke = false;
                absorbParticles.SetActive(true);
                enemy.isStopped = true;
                target.tag = "PickedCorpse";
                blackboard.animatorController.StartCorpseChanneling();
                blackboard.cooldownToGrabCorpse = 5f;
                target.GetComponent<CorpseAbsortion>().AbsorbParticles(blackboard.cooldownToGrabCorpse, blackboard.absorbObjective);
                transform.LookAt(target.transform.position, gameObject.transform.up);
                break;

        }

        currentState = newState;
        
    }
}
