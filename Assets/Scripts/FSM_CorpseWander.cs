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
    GameObject waypointsList;
    GameObject corpse;

    //float closeEnoughTarget;

   
 
    public enum State {INITIAL, WANDERING, GOINGTOCORPSE};
    public State currentState;

    

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        waypointsList = GameObject.FindGameObjectWithTag("SpawnersContainer");
        int spawnPosition = Random.Range(0, waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        target = waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
        enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        //enemy.SetDestination(target.transform.position);
    }

    public void Exit()
    {
        

        
    }

    public void ReEnter()
    {
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
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.corpsePickUpRadius)
                 {
                    target.SetActive(false);
                    ChangeState(State.WANDERING);
                 }

                break;
            
        }

        if (DetectionFunctions.FindObjectInArea(gameObject,"Player", blackboard.playerDetectionRadius ))
        {
            gameObject.GetComponent<FSM_SeekPlayer>().enabled = true;
            gameObject.GetComponent<FSM_SeekPlayer>().ReEnter();
            this.enabled = false;
        }
        

    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.WANDERING:
                
                break;
            case State.GOINGTOCORPSE:
                target = null;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                int spawnPosition = Random.Range(0, waypointsList.GetComponent<RoomSpawner>().spawners.Count);
                target = waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
                //enemy.SetDestination(target.transform.position);
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));

                //SE TIENE QUE PROGRAMAR QUE SE QUEDE QUIETO CUANDO LLEGUE A UN OBJETIVO

                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

        }

        currentState = newState;
        
    }
}
