using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseWander : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    private Enemy_BLACKBOARD blackboard;

    EnemyBehaviours behaviours;
    string enemyType;
    GameObject corpse;

    
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
        enemy.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
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
                if(behaviours.myType == EnemyBehaviours.EnemyType.MAIN) behaviours.SearchPlayer();
                
                corpse = behaviours.SearchObject("corpse");
                //Debug.Log(corpse.name);
                if(corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance)
                {
                    ChangeState(State.WANDERING); 
                }
                break;
            case State.GOINGTOCORPSE:
                if(behaviours.myType == EnemyBehaviours.EnemyType.MAIN) behaviours.SearchPlayer();
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.corpsePickUpRadius)
                {
                    ChangeState(State.GRABBINGCORPSE);
                }
                
                break;

            case State.GRABBINGCORPSE:
                if(behaviours.myType == EnemyBehaviours.EnemyType.MAIN) behaviours.SearchPlayer();
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                if (blackboard.cooldownToGrabCorpse <= 0)
                {
                    behaviours.GrabCorpse(target);
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
                behaviours.AddCorpseToScore();
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
                    enemy.SetDestination(blackboard.lastCorpseSeen.transform.position);
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
}
