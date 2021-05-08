using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AttackerOrb : MonoBehaviour
{

    [Header("Attack")]
    public List<Transform> rayPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;
    public bool alert = false;
    bool attacking = false;
    bool rotating = true;

    [Header("Target")]
    public GameObject target;
    public Transform child;
    private EnemyBehaviours behaviours;
    private Orb_Blackboard blackboard;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL, WANDERING, ALERT ,  ATTACKINGPLAYER};
    

   
    void OnEnable()
    {
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        ReEnter();
    }

    public void Exit()
    {
        blackboard.navMesh.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;

    }

    private void Update()
    {
        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;


            case State.WANDERING:
                blackboard.navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));

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


            case State.ALERT:

                Rotate();
                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
                {
                    //Debug.Log("aTTACKING");
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
                }

                
                break;
        }

    }

    public void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.WANDERING:
               
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
                    //Debug.Log(l_RaycastHit.collider.tag);
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        //Debug.Log("Hit by orb");
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

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,10 * Time.deltaTime);
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
