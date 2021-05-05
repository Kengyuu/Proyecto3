using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseWander : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("AI")]
    public NavMeshAgent enemy;
    public GameObject target;
    private Enemy_BLACKBOARD blackboard;

    EnemyBehaviours behaviours;
    string enemyType;
    GameObject corpse;

    public LayerMask layer;

    
    //float closeEnoughTarget;

   
 
    public enum State {INITIAL, WANDERING, GOINGTOCORPSE, GRABBINGCORPSE};
    public State currentState;

    

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        behaviours = GetComponent<EnemyBehaviours>();
        enemyType = transform.tag;
        //target = behaviours.PickRandomWaypoint();
        //enemy.SetDestination(target.transform.position);
    }

    public void Exit()
    {
        //target = null;
        if(corpse != null)
            corpse.tag = "Corpse";
        //corpse = null;
        enemy.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        Debug.Log("Soy yo mismo");
        this.enabled = true;
        currentState = State.INITIAL;
        
    }

    // Update is called once per frame
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
                //Debug.Log(corpse.name);
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
                if(behaviours.myType == EnemyBehaviours.EnemyType.MAIN) behaviours.SearchPlayer(blackboard.playerDetectionRadius, layer);
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                if (blackboard.cooldownToGrabCorpse <= 0)
                {
                    behaviours.GrabCorpse(target, blackboard.cooldownToGrabCorpse);
                    behaviours.AddCorpseToScore();
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

                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));

                break;

            case State.GRABBINGCORPSE:
                enemy.isStopped = true;
                target.tag = "PickedCorpse";
                blackboard.cooldownToGrabCorpse = 3f;
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

    /*void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blackboard.corpseDetectionRadius);
        if(corpse != null)
            Gizmos.DrawLine(transform.position, target.transform.position);
    }*/
}
