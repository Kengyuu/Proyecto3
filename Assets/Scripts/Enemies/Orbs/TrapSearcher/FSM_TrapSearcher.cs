using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_TrapSearcher : MonoBehaviour
{
   
    [Header("Attributes")]
    public GameObject target;
    public Transform child;
    private Orb_Blackboard blackboard;
    EnemyBehaviours behaviours;
    GameObject trap;

    [Header("Attack")]
    public List<Transform> rayPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;
    bool attacking = false;
    bool rotating = true;
    public bool alert = false;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL, WANDERING, GOINGTOTRAP, DEACTIVATINGTRAP, ALERT, ATTACKINGPLAYER };
    

    void OnEnable()
    {
        
        blackboard = GetComponent<Orb_Blackboard>();
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard.SetOrbHealth(3);
        child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
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
                ChangeState(State.WANDERING);
                break;



            case State.WANDERING:

                trap = behaviours.SearchObject("PasiveTrap", blackboard.trapDetectionRadius);
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                
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

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
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

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
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
                     behaviours.DeactivateTrap(target);
                     ChangeState(State.WANDERING);
                     break;
                 }

                break;



            case State.ALERT:

                Rotate();
                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
                {
                    
                    ChangeState(State.ATTACKINGPLAYER);
                    break;
                }
                else StartCoroutine(StayAlert());

                break;


            case State.ATTACKINGPLAYER:

                if (rotating) Rotate();

                TriggerAttack();

                if (GameManager.Instance.GetPlayer().GetComponent<PlayerController>().m_Life <= 0)
                {
                    attacking = false;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, GameManager.Instance.GetPlayer()) > blackboard.maxAttackDistance)
                {
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
            case State.DEACTIVATINGTRAP:
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

            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();

                break;
            case State.GOINGTOTRAP:
                target = trap;
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.DEACTIVATINGTRAP:
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
    void TriggerAttack()
    {
        if (attacking)
        {
            foreach (Transform raycastPoint in rayPoints)
            {
                Vector3 Direction = raycastPoint.position - castPosition.position;
                Direction.Normalize();
                Ray Ray = new Ray(castPosition.position, Direction);
                Debug.DrawRay(castPosition.position, Direction * blackboard.maxAttackDistance, Color.red);
                RaycastHit l_RaycastHit;

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance))
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

    IEnumerator StayAlert()
    {
        yield return new WaitForSeconds(2);
        ChangeState(State.WANDERING);
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





    /*void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blackboard.senseRadius);
    }*/
}
