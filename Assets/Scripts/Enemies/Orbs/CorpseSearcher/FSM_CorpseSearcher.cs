using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FSM_CorpseSearcher : MonoBehaviour
{

    [Header("Parameters")]
    public GameObject target;
    public GameObject corpse;
    public Transform child;
    public LayerMask mask;
    public Image icon;

    [Header("Attack")]
    public List<Transform> rayPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;
    public bool alert = false;
    bool attacking = false;
    bool rotating = true;

    GameManager GM;
    EnemyBehaviours behaviours;

    public ParticleSystem particles;
    private HudController M_HudController;

    private Orb_Blackboard blackboard;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL, WANDERING, GOINGTOCORPSE, RETURNINGTOENEMY, GRABBINGCORPSE,ALERT, ATTACKINGPLAYER };
    



    void OnEnable()
    {
        GM = GameManager.Instance;
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        m_Laser.enabled = false;
        ReEnter(); 
    }

    public void Exit()
    {
        blackboard.navMesh.isStopped = false;
        if(corpse != null)
            corpse.tag = "Corpse";
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
                corpse = behaviours.SearchObject("Corpse", blackboard.corpseDetectionRadius);
                blackboard.navMesh.SetDestination(target.transform.position);

                if (alert)
                {
                    ChangeState(State.ALERT);
                    break;
                }

                if (blackboard.orbCorpseStored != null)
                {
                    ChangeState(State.RETURNINGTOENEMY);
                    break;
                }
                
                if (corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                    break;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                 {
                     ChangeState(State.WANDERING);
                     break;
                 }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer) 
                    && !GM.GetEnemy().GetComponent<EnemyPriorities>().playerSeen)
                {
                    ChangeState(State.ATTACKINGPLAYER);
                    break;
                }

                

               
                break;




            case State.GOINGTOCORPSE:

                if (alert)
                {
                    ChangeState(State.ALERT);
                    break;
                }
                if (target.tag != "Corpse" || !target.activeSelf)
                {
                    ChangeState(State.WANDERING);
                    break;
                }

               

                else
                {
                    if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                    {
                        ChangeState(State.GRABBINGCORPSE);
                        break;
                    }
                }
                break;





            case State.GRABBINGCORPSE:

                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                 if (blackboard.cooldownToGrabCorpse <= 0)
                 {
                    if(target != null)
                    {
                        behaviours.GrabCorpse(target, blackboard.cooldownToGrabCorpse);
                    }
                     
                    ChangeState(State.RETURNINGTOENEMY);
                    break;
                 }
                break;



            case State.RETURNINGTOENEMY:

                if (alert)
                {
                    ChangeState(State.ALERT);
                }

                target = behaviours.ReturnToEnemy();
                  if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance + 1)
                  {
                      ChangeState(State.WANDERING);
                  }

                 if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer)
                    && !GM.GetEnemy().GetComponent<EnemyPriorities>().playerSeen)
                 {
                    
                    ChangeState(State.ATTACKINGPLAYER);
                 }

               

                break;



            case State.ALERT:
                Rotate();
                //transform.LookAt(GM.GetPlayer().transform,transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                Invoke("StayAlert", 1);
                break;



            case State.ATTACKINGPLAYER:
                
                if (rotating)
                {
                    Rotate();
                    //transform.LookAt(GM.GetPlayer().transform, transform.up);
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
                                                       !behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer))
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
                blackboard.lastCorpseSeen = null;
                break;
            case State.GRABBINGCORPSE:
                target.tag = "Corpse";
                blackboard.orbCorpseStored = corpse;
                blackboard.navMesh.isStopped = false;
                break;
            case State.RETURNINGTOENEMY:
                if ((blackboard.orbCorpseStored != null) && newState != State.ATTACKINGPLAYER)
                {
                    blackboard.orbCorpseStored = null;
                    behaviours.AddCorpseToScore();
                    M_HudController.UpdateAddCorpses(GM.GetEnemy());
                    corpse = null;
                }
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
            case State.GOINGTOCORPSE:
                target = corpse;
                blackboard.navMesh.SetDestination(target.transform.position);
                break;

            case State.GRABBINGCORPSE:
                blackboard.navMesh.isStopped = true;
                blackboard.cooldownToGrabCorpse = 3f;
                if (target.tag == "Corpse")
                {
                    target.tag = "PickedCorpse";
                }
                target.GetComponent<CorpseAbsortion>().AbsorbParticles(blackboard.cooldownToGrabCorpse, gameObject);
                break;

            case State.RETURNINGTOENEMY:
                blackboard.lastCorpseSeen = corpse;
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
            Color32 color = new Color32(98, 204, 70, 255);

            main.startColor = (Color)color;
           
        }
        if (blackboard.GetOrbHealth() == 2)
        {
            var main = particles.main;
            Color32 color = new Color32(54, 121, 35, 120);
           
            main.startColor = (Color)color;
            
        }
        if (blackboard.GetOrbHealth() == 1)
        {
            var main = particles.main;
            Color32 color = new Color32(28, 58, 20, 50);

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
                //Debug.DrawRay(castPosition.position, Direction * blackboard.maxAttackDistance, Color.red);
                RaycastHit l_RaycastHit;

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance,mask))
                {
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(1, gameObject, blackboard.XForceImpulseDamage,
                                                                                                     blackboard.YForceImpulseDamage);
                        
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
                   && !GM.GetEnemy().GetComponent<EnemyPriorities>().playerSeen)
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



    /*void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, blackboard.corpseDetectionRadius);
        if (corpse != null)
            Gizmos.DrawLine(transform.position, corpse.transform.position);
    }*/
}
