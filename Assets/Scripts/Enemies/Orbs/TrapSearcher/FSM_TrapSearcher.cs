using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FSM_TrapSearcher : MonoBehaviour
{
   
    [Header("Attributes")]
    public GameObject target;
    public Transform child;
    public LayerMask mask;
    private Orb_Blackboard blackboard;
    EnemyBehaviours behaviours;
    GameObject trap;
    public Image icon;
    public float cooldown = 4;
    [Header("Attack")]
    public List<Transform> rayPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;
    bool attacking = false;
    bool rotating = true;
    public bool alert = false;

    GameManager GM;

    public ParticleSystem particles;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL,INVOKING, WANDERING, GOINGTOTRAP, DEACTIVATINGTRAP, ALERT, ATTACKINGPLAYER };
    

    void OnEnable()
    {
        GM = GameManager.Instance;
        blackboard = GetComponent<Orb_Blackboard>();
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard.SetOrbHealth(3);
        child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        m_Laser.enabled = false;
        ReEnter();
       

    }

    public void Exit()
    {
       
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
                ChangeState(State.INVOKING);
                break;

            case State.INVOKING:
                blackboard.cooldownToReappear -= Time.deltaTime;
                if (blackboard.cooldownToReappear <= 0)
                {
                    ChangeState(State.WANDERING);
                    blackboard.cooldownToReappear = 4;
                }
               
                break;

            case State.WANDERING:

                trap = behaviours.SearchObject("PasiveTrap", blackboard.trapDetectionRadius);
                blackboard.navMesh.SetDestination(target.transform.position);
                
                if (trap != null)
                {
                    //if (trap.GetComponent<PassiveTrap>() != null && trap.GetComponent<PassiveTrap>().GetTrapActive())
                    ChangeState(State.GOINGTOTRAP);
                    break;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                {
                    ChangeState(State.WANDERING);
                    break;
                }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer)
                    && GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState != FSM_SeekPlayer.State.ATTACKING)
                {
                    //Debug.Log("aTTACKING");
                    ChangeState(State.ATTACKINGPLAYER);
                    break;
                }

                if (alert)
                {
                    ChangeState(State.ALERT);
                    break;
                }
                break;




            case State.GOINGTOTRAP:
               
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughTrapRadius)
                {
                    ChangeState(State.DEACTIVATINGTRAP);
                    break;
                }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer)
                    && GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState != FSM_SeekPlayer.State.ATTACKING)
                {
                   // Debug.Log("aTTACKING");
                    ChangeState(State.ATTACKINGPLAYER);
                    break;
                }

                if (alert)
                {
                    ChangeState(State.ALERT);
                    break;
                }
                break;




            case State.DEACTIVATINGTRAP:
       
                blackboard.cooldownToDeactivateTrap -= Time.deltaTime;
                 if (blackboard.cooldownToDeactivateTrap <= 0)
                 {
                    target.transform.GetChild(1).gameObject.GetComponent<PassiveTrap>().DisableTrap();
                     
                     ChangeState(State.WANDERING);
                     break;
                 }

                break;



            case State.ALERT:
                Rotate();
               // transform.LookAt(GM.GetPlayer().transform, transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                Invoke("StayAlert", 1);
                break;


            case State.ATTACKINGPLAYER:

                if (rotating)
                {
                    Rotate();
                   // transform.LookAt(GM.GetPlayer().transform, transform.up);
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                } 

                if (!GM.GetPlayer().GetComponent<PlayerController>().m_PlayerStunned)
                {
                    TriggerAttack();
                }

                if (GameManager.Instance.GetPlayer().GetComponent<PlayerController>().m_Life <= 0)
                {
                    attacking = false;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, GameManager.Instance.GetPlayer()) > blackboard.maxAttackDistance ||
                                                       !behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer) ||
                                                       GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState == FSM_SeekPlayer.State.ATTACKING)
                {
                    m_Laser.enabled = false;
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
            case State.INVOKING:
                anim.SetBool("Invoke", false);
                blackboard.navMesh.isStopped = false;
                break;
            case State.DEACTIVATINGTRAP:
                //child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
                blackboard.navMesh.isStopped = false;
                ReEnter();
                break;

            case State.ATTACKINGPLAYER:
                blackboard.navMesh.isStopped = false;
                anim.SetBool("AttackOrb", false);
                break;

            case State.ALERT:
                alert = false;          
                break;
        }

        // Enter logic
        switch (newState)
        {
            case State.INVOKING:
                anim.SetBool("Invoke", true);
                blackboard.navMesh.isStopped = true;
                blackboard.navMesh.Warp(blackboard.orbSpawner.transform.position);
                break;
            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();

                break;
            case State.GOINGTOTRAP:
                target = trap;
                blackboard.navMesh.SetDestination(target.transform.position);
                break;

            case State.DEACTIVATINGTRAP:
                transform.LookAt(target.transform.position, transform.up);
                blackboard.navMesh.isStopped = true;
                blackboard.cooldownToDeactivateTrap = 3f;
                break;

            case State.ATTACKINGPLAYER:
                blackboard.navMesh.isStopped = true;
                anim.SetBool("AttackOrb", true);
                break;
            case State.ALERT:
                blackboard.navMesh.isStopped = true;
                break;



        }

        currentState = newState;

    }


    //ATTACK FUNCTIONS

    public void ChangeParticleColor()
    {
        if (blackboard.GetOrbHealth() == 3)
        {
            var main = particles.main;
            Color32 color = new Color32(195, 38, 158, 255);

            main.startColor = (Color)color;

        }
        if (blackboard.GetOrbHealth() == 2)
        {
            var main = particles.main;
            Color32 color = new Color32(118, 26, 96, 120);

            main.startColor = (Color)color;

        }
        if (blackboard.GetOrbHealth() == 1)
        {
            var main = particles.main;
            Color32 color = new Color32(53, 12, 44, 50);

            main.startColor = (Color)color;

        }
    }
    void TriggerAttack()
    {
        if (attacking)
        {
            foreach (Transform raycastPoint in rayPoints)
            {
                Vector3 Direction = raycastPoint.position - castPosition.position;
                Direction.Normalize();
                Ray Ray = new Ray(castPosition.position, Direction);
                RaycastHit l_RaycastHit;

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance,mask))
                {
                    Debug.Log(l_RaycastHit.collider.tag);
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        Debug.Log("Hit by orb");
                        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(1, gameObject, blackboard.XForceImpulseDamage, blackboard.YForceImpulseDamage);
                        attacking = false;

                    }

                }
            }

            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, blackboard.maxAttackDistance));


        }
        ChangeState(State.ATTACKINGPLAYER);

    }

    void StayAlert()
    {
        

        if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer)
                   && GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState != FSM_SeekPlayer.State.ATTACKING)
        {
            //Debug.Log("aTTACKING");
            ChangeState(State.ATTACKINGPLAYER);
        }
        else ChangeState(State.WANDERING);
    }

    void Rotate()
    {
        Vector3 direction = GameManager.Instance.GetPlayer().transform.position - transform.position;

        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }


    //ANIMATION EVENTS
    void setAttackTrue()
    {
        attacking = true;

    }
    void setRotateTrue()
    {
        rotating = true;

    }

    void setRotateFalse()
    {
        rotating = false;

    }

    void setLaserTrue()
    {

        m_Laser.enabled = true;
    }

    void setLaserFalse()
    {

        m_Laser.enabled = false;
    }

    void setAttackFalse()
    {
        attacking = false;
        anim.SetBool("AttackOrb", false);
        m_Laser.enabled = false;
    }
}
