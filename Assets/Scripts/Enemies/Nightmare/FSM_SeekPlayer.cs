using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_SeekPlayer : MonoBehaviour
{

    public NavMeshAgent enemy;
    private Enemy_BLACKBOARD blackboard;
    GameObject Player;

    public GameObject target;
    public GameObject savedCorpse;

    public LayerMask layer;

    public Vector3 lastPlayerPosition;
    public List<GameObject> waypointsNearPlayer;
    public bool waypointSelected = false;

    public float attackCooldown = 0.5f;
    float currentAttackTime = 0f;
    
    float currentProvokeTime = 0f;
    public bool isProvoking = false;
    public bool isAttacking = false;

    public GameObject stunnnedAbsorbParticles;
    int currentComboAttack = 0;
    public GameObject rightArm;
    public GameObject leftArm;
    public enum State { INITIAL, WANDERING, SEEKINGPLAYER, GOTOLASTPLAYERPOSITION, ATTACKING, PROVOKING};
    public State currentState;

    float currentInvokeTime = 0f;

    private GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;

        enemy = GetComponent<NavMeshAgent>();
        Player = GM.GetPlayer();
        blackboard = GetComponent<Enemy_BLACKBOARD>();

    }

    public void Exit()
    {
        GetComponent<EnemyPriorities>().playerSeen = false;
        GetComponent<EnemyPriorities>().playerDetected = false;
        target = null;
        waypointSelected = false;
        currentInvokeTime = 0;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentInvokeTime = 0;
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
                enemy.SetDestination(target.transform.position);
                if (DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    GetComponent<EnemyPriorities>().playerSeen = true;
                    ChangeState(State.SEEKINGPLAYER);
                }
                savedCorpse = DetectionFunctions.FindObjectInArea(gameObject, "Corpse", blackboard.corpseDetectionRadius);

                if (savedCorpse != null)
                {
                    blackboard.lastCorpseSeen = savedCorpse;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                {
                    waypointSelected = false;
                    GetComponent<EnemyPriorities>().playerSeen = false;
                    ChangeState(State.WANDERING);
                }

                
                break;

            case State.SEEKINGPLAYER:
                    enemy.SetDestination(Player.transform.position);
                if (DetectionFunctions.DistanceToTarget(gameObject,Player) <= blackboard.distanceToAttack)
                {
                    ChangeState(State.ATTACKING);
                    break;
                }

                if (!DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    blackboard.animatorController.WalkAgressiveExit();
                    ChangeState(State.GOTOLASTPLAYERPOSITION);
                }
                break;
            case State.GOTOLASTPLAYERPOSITION:
                    enemy.SetDestination(lastPlayerPosition);
                if (DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                {
                    ChangeState(State.SEEKINGPLAYER);
                }

                if (enemy.remainingDistance < 1.1f)
                {
                    ReEnter();
                    GetComponent<EnemyPriorities>().playerSeen = false;
                    GetComponent<EnemyPriorities>().playerDetected = false;
                    GetComponent<EnemyPriorities>().ChangePriority();
                }


                break;

          
               
            case State.ATTACKING:

                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                if(isProvoking)
                {
                    ChangeState(State.PROVOKING);
                }
                
                
                if(currentAttackTime >= attackCooldown)
                {
                    if(isAttacking == false)
                    {
                        if(GetComponent<EnemyPriorities>().playerSeen)
                        {
                            if (DetectionFunctions.DistanceToTarget(gameObject, Player) > blackboard.distanceToAttack)
                                ChangeState(State.SEEKINGPLAYER);
                            else
                                ChangeState(State.ATTACKING);
                        }
                        else
                            ChangeState(State.GOTOLASTPLAYERPOSITION);
                    }
                    
                }
                currentAttackTime += Time.deltaTime;
                break;

            case State.PROVOKING:
                transform.LookAt(Player.transform,transform.up);
                currentProvokeTime += Time.deltaTime;
                if(currentProvokeTime >= blackboard.provokeTime)
                {
                    if (DetectionFunctions.PlayerInCone(blackboard.eyesPosition, Player, blackboard.angleDetectionPlayer, blackboard.playerDetectionRadius, layer))
                    {
                        ChangeState(State.SEEKINGPLAYER);
                    }
                    else
                    {
                        ChangeState(State.GOTOLASTPLAYERPOSITION);
                    }
                    
                    currentProvokeTime = 0;
                }
                isProvoking = false;
                break;
        }


    }

    void ChangeState(State newState)
    {
        
        //EXIT LOGIC
        switch (currentState)
        {  
            case State.ATTACKING:
                if(newState != State.ATTACKING && newState != State.PROVOKING)
                {
                    //enemy.isStopped = false;
                    blackboard.animatorController.WalkAgressiveEnter();
                    isAttacking = false;
                }
                if(newState == State.ATTACKING)
                {
                    blackboard.animatorController.DecideComboAttack();
                }

                break;
            case State.PROVOKING:
                isProvoking = false;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.SEEKINGPLAYER:
                if(!enemy.isStopped)
                    enemy.SetDestination(Player.transform.position);
                blackboard.animatorController.WalkAgressiveEnter();
                GetComponent<HFSM_StunEnemy>().canInvoke = false;
                break;
            case State.GOTOLASTPLAYERPOSITION:
                blackboard.animatorController.WalkAgressiveExit();
                lastPlayerPosition = Player.transform.position;
                if(!enemy.isStopped)
                    enemy.SetDestination(lastPlayerPosition);
                GetComponent<HFSM_StunEnemy>().canInvoke = false;
                break;

            
            case State.ATTACKING:
                transform.LookAt(Player.transform,transform.up);
                currentAttackTime = 0f;
                isAttacking = true;
                if(Player.GetComponent<PlayerController>().m_Life > 0)
                {
                    blackboard.animatorController.AttackStart();
                }
                GetComponent<HFSM_StunEnemy>().canInvoke = false;
                break;

            case State.PROVOKING:
                currentProvokeTime = 0f;
                AbsorbFail();
                break;

            case State.WANDERING:
                blackboard.animatorController.WalkAgressiveExit();
                GetComponent<HFSM_StunEnemy>().canInvoke = true;
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
 
    GameObject FindClosestWaypoint(List<GameObject> list, GameObject player)
        {
        
            GameObject closest = list[1];
            float minDistance = (closest.transform.position - player.transform.position).magnitude;
            for (int i = 1; i < list.Count; i++)
            {
                float dist = (list[i].transform.position - player.transform.position).magnitude; 
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = list[i];
                }
            }

            return closest;
        
    }

    public void EnemyCanMove()
    {
        enemy.isStopped = false;
    }

    public void EnemyCantMove()
    {
        enemy.isStopped = true;
    }

    public void AbsorbFail()
    {
        blackboard.absorbFailParticles.SetActive(true);
        Invoke("EndAbsorbFail", 2f);
    }

    public void EndAbsorbFail()
    {
        blackboard.absorbFailParticles.SetActive(false);
    }
}
