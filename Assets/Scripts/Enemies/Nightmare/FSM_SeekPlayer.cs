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

    public LayerMask layer;

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
        GetComponent<EnemyPriorities>().playerSeen = false;
        GetComponent<EnemyPriorities>().playerDetected = false;
        target = null;
        waypointSelected = false;
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
                enemy.SetDestination(target.transform.position);
                if (DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    GetComponent<EnemyPriorities>().playerSeen = true;
                    ChangeState(State.SEEKINGPLAYER);
                }
                savedCorpse = DetectionFunctions.FindObjectInArea(gameObject, "Corpse", blackboard.corpseDetectionRadius);
                //Debug.Log(corpse.name);
                if (savedCorpse != null)
                {
                    blackboard.lastCorpseSeen = savedCorpse;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                {
                    waypointSelected = false;
                    GetComponent<EnemyPriorities>().playerSeen = false;
                    //ChangeState(State.WANDERING);
                }

                
                break;

            case State.SEEKINGPLAYER:
                enemy.SetDestination(Player.transform.position);
                //transform.LookAt(Player.transform,transform.up);
                if (DetectionFunctions.DistanceToTarget(gameObject,Player) <= blackboard.distanceToAttack)
                {
                    ChangeState(State.ATTACKING);
                    break;
                }

                if (!DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    ChangeState(State.GOTOLASTPLAYERPOSITION);
                }
                break;
            case State.GOTOLASTPLAYERPOSITION:
                enemy.SetDestination(lastPlayerPosition);
                if (DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    ChangeState(State.SEEKINGPLAYER);
                }

                if (enemy.remainingDistance < 0.4f)
                {
                    GetComponent<EnemyPriorities>().playerSeen = false;
                    GetComponent<EnemyPriorities>().playerDetected = false;
                    GetComponent<EnemyPriorities>().ChangePriority();
                
                }


                break;

          
               
            case State.ATTACKING:

                transform.LookAt(Player.transform,transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                
                if (DetectionFunctions.DistanceToTarget(gameObject, Player) > blackboard.distanceToAttack)
                {
                    ChangeState(State.SEEKINGPLAYER);
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
                blackboard.animatorController.WalkAgressiveExit();

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
                blackboard.animatorController.WalkAgressiveEnter();

                break;
            case State.GOTOLASTPLAYERPOSITION:
                lastPlayerPosition = Player.transform.position;

                enemy.SetDestination(lastPlayerPosition);
                break;

            
            case State.ATTACKING:
                enemy.isStopped = true;
                //Arm.SetActive(true);
                if(Player.GetComponent<PlayerController>().m_Life > 0)
                {
                    Arm.GetComponent<Animation>().Play();
                    blackboard.animatorController.AttackStart();
                }
                
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

                    enemy.SetDestination(target.transform.position);
                    waypointSelected = true;
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
            float minDistance = (closest.transform.position - player.transform.position).magnitude;
            for (int i = 1; i < list.Count; i++)
            {
                //dist = (targets[i].transform.position - user.transform.position).magnitude;
                float dist = (list[i].transform.position - player.transform.position).magnitude; 
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = list[i];
                }
            }

            return closest;
        
    }

    /*void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Player.transform.position, blackboard.waypointsNearPlayerRadius);
    }*/
}
