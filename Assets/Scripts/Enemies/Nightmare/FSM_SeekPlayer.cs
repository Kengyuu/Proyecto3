using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_SeekPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent enemy;
    private Enemy_BLACKBOARD blackboard;
    GameObject Player;

    public GameObject target;
    public GameObject savedCorpse;
    

    public Vector3 lastPlayerPosition;
    public List<GameObject> waypointsNearPlayer;
    public bool waypointSelected = false;
    //Transform child;
    public GameObject Arm;
    public enum State { INITIAL, WANDERING, SEEKINGPLAYER, GOTOLASTPLAYERPOSITION, ATTACKING};
    public State currentState;

    private GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;

        enemy = GetComponent<NavMeshAgent>();
       // enemyType = transform.tag;
        Player = GM.GetPlayer();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        //child = gameObject.transform.GetChild(2);
        //Arm = child.gameObject;
    }

    public void Exit()
    {
        //Arm.SetActive(false);
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

                savedCorpse = DetectionFunctions.FindObjectInArea(gameObject, "Corpse", blackboard.corpseDetectionRadius);
                //Debug.Log(corpse.name);
                if (savedCorpse != null)
                {
                    blackboard.lastCorpseSeen = savedCorpse;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= 0.5f)
                {
                    Debug.Log("return to Wander");
                    waypointSelected = false;
                    ChangeState(State.WANDERING);
                }

                if (DetectionFunctions.PlayerInCone(gameObject, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius))
                {
                    ChangeState(State.SEEKINGPLAYER);
                }
                break;

            case State.SEEKINGPLAYER:
                enemy.SetDestination(Player.transform.position);

                if (DetectionFunctions.DistanceToTarget(gameObject,Player) <= blackboard.distanceToAttack)
                {
                    ChangeState(State.ATTACKING);
                    break;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, Player)>= blackboard.playerDetectionRadius)
                {
                    ChangeState(State.GOTOLASTPLAYERPOSITION);
                }
                break;
            case State.GOTOLASTPLAYERPOSITION:

                if (enemy.remainingDistance < 0.5f)
                {
                    gameObject.GetComponent<EnemyPriorities>().playerSeen = false;
                    gameObject.GetComponent<EnemyPriorities>().ChangePriority();

                }
                break;

          
               
            case State.ATTACKING:

                transform.LookAt(Player.transform,transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                
                if (DetectionFunctions.DistanceToTarget(gameObject, Player) > blackboard.distanceToAttack)
                {
                    ChangeState(State.SEEKINGPLAYER);
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
            case State.SEEKINGPLAYER:


                break;
            case State.GOTOLASTPLAYERPOSITION:


                break;
            case State.ATTACKING:
                enemy.isStopped = false;
                //Arm.SetActive(false);
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.SEEKINGPLAYER:
                enemy.SetDestination(Player.transform.position);

                break;
            case State.GOTOLASTPLAYERPOSITION:
                lastPlayerPosition = Player.transform.position;

                enemy.SetDestination(lastPlayerPosition);
                break;

            
            case State.ATTACKING:
                enemy.isStopped = true;
                Arm.SetActive(true);
                Arm.GetComponent<Animation>().Play();
                break;

            case State.WANDERING:
              //  Debug.Log("finding");
                /*int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
                target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];*/
                //enemy.SetDestination(target.transform.position);
                if (!waypointSelected)
                {
                    waypointsNearPlayer = DetectionFunctions.FindObjectsInArea(Player, "Waypoint", blackboard.waypointsNearPlayerRadius);
                    int alea = Random.Range(1, 3);

                    switch (alea)
                    {
                        case 1:
                            int randomWaypoint = Random.Range(1, waypointsNearPlayer.Count);
                            target = waypointsNearPlayer[randomWaypoint];
                            waypointSelected = true;
                            break;
                        default:
                            target = FindClosestWaypoint(waypointsNearPlayer, Player);
                            waypointSelected = true;
                            break;
                    }

                    enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                }
               
                break;

        }

        currentState = newState;

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(-1, gameObject);

            StartCoroutine(WaitToGetStunned());
        }
    }

    IEnumerator WaitToGetStunned()
    {
        enemy.isStopped = true;
        yield return new WaitForSeconds(4);
        enemy.isStopped = false;
        
    }*/
        
    GameObject FindClosestWaypoint(List<GameObject> list, GameObject player)
        {
        
            GameObject closest = list[1];

            //float minDistance = (closest.transform.position - user.transform.position).magnitude;
            float minDistance = new Vector3(closest.transform.position.x - player.transform.position.x, 0, 
                                            closest.transform.position.z - player.transform.position.z).magnitude;

            for (int i = 1; i < list.Count; i++)
            {
                //dist = (targets[i].transform.position - user.transform.position).magnitude;
                float dist = new Vector3(list[i].transform.position.x - player.transform.position.x, 0, 
                                         list[i].transform.position.z - player.transform.position.z).magnitude;
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = list[i];
                }
            }

            return closest;
        
    }

    void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.transform.position, blackboard.waypointsNearPlayerRadius);
    }
}
