using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseWander : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent enemy;
    public GameObject target;

    private Enemy_BLACKBOARD blackboard;
    
    GameObject corpse;

    
    //float closeEnoughTarget;

   
 
    public enum State {INITIAL, WANDERING, GOINGTOCORPSE, GRABBINGCORPSE};
    public State currentState;

    

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
       
        int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
        enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
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
        //distanceToTarget = new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z).magnitude;

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;

            case State.WANDERING:
                if (DetectionFunctions.FindObjectInArea(gameObject,"Player", blackboard.playerDetectionRadius ))
                {
                    gameObject.GetComponent<FSM_EnemyPriority>().playerSeen = true;
                }
                corpse = DetectionFunctions.FindObjectInArea(gameObject, "Corpse", blackboard.corpseDetectionRadius);
                //Debug.Log(corpse.name);
                if(corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= 0.5f)
                {
                    ChangeState(State.WANDERING); 
                }
                
                break;
            case State.GOINGTOCORPSE:
                if (DetectionFunctions.FindObjectInArea(gameObject,"Player", blackboard.playerDetectionRadius ))
                {
                    gameObject.GetComponent<FSM_EnemyPriority>().playerSeen = true;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.corpsePickUpRadius)
                {
                    ChangeState(State.GRABBINGCORPSE);
                }
                
                break;

            case State.GRABBINGCORPSE:
                if (DetectionFunctions.FindObjectInArea(gameObject,"Player", blackboard.playerDetectionRadius ))
                {
                    gameObject.GetComponent<FSM_EnemyPriority>().playerSeen = true;
                }
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                if (blackboard.cooldownToGrabCorpse <= 0)
                {
                    target.SetActive(false);
                    ChangeState(State.WANDERING);
                    blackboard.cooldownToGrabCorpse = 3f;
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
                //target = null;
                break;
            case State.GOINGTOCORPSE:
                //target = null;
                
                blackboard.enemyCorpses++;
                blackboard.remainingCorpses--;
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
                    int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
                    target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
                    //enemy.SetDestination(target.transform.position);
                    enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
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
}
