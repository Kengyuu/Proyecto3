using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AttackerOrb : MonoBehaviour
{
    public List<Transform> rayPoints;
    public List<Transform> castPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;

    public Orb_Blackboard blackboard;
    private EnemyBehaviours behaviours;
    public GameObject target;

    public enum State { INITIAL, WANDERING, ATTACKINGPLAYER};
    public State currentState;

    bool attacking = false;
    bool rotating = true;
    void OnEnable()
    {


        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
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
                }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
                {
                    ChangeState(State.ATTACKINGPLAYER);
                }

               
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

    void ChangeState(State newState)
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

           

        }

        currentState = newState;

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

          
        }
        ChangeState(State.ATTACKINGPLAYER);
        
    }

    void Rotate()
    {
        Vector3 direction = GameManager.Instance.GetPlayer().transform.position - transform.position;

        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,10 * Time.deltaTime);
    }



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
