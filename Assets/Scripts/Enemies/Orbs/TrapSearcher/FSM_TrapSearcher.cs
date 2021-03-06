using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using FMODUnity;
using FMOD;
using FMOD.Studio;


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
    public float floatSoundCooldown = 0f;
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

    [Header("FMOD Events")]
    public string floatstringEvent;
    public string beamChargeEvent;
    public string beamShootEvent;
    EventInstance floatEvent;

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
                floatSoundCooldown -= Time.deltaTime;
                if (floatSoundCooldown <= 0)
                {
                    floatEvent = SoundManager.Instance.PlayEvent(floatstringEvent, transform);

                    floatSoundCooldown = 11f;
                }
                trap = behaviours.SearchObject("PasiveTrap", blackboard.trapDetectionRadius);
                blackboard.navMesh.SetDestination(target.transform.position);
                
                if (trap != null)
                {
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
                floatSoundCooldown -= Time.deltaTime;
                if (floatSoundCooldown <= 0)
                {
                    floatEvent = SoundManager.Instance.PlayEvent(floatstringEvent, transform);

                    floatSoundCooldown = 11f;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughTrapRadius)
                {
                    ChangeState(State.DEACTIVATINGTRAP);
                    break;
                }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer)
                    && GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState != FSM_SeekPlayer.State.ATTACKING)
                {
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
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                Invoke("StayAlert", 1);
                break;


            case State.ATTACKINGPLAYER:

                if (rotating)
                {
                    Rotate();
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

        switch (currentState)
        {
            case State.INVOKING:
                anim.SetBool("Invoke", false);
                blackboard.navMesh.isStopped = false;
                break;
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
                floatEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                floatSoundCooldown = 0f;
                transform.LookAt(target.transform.position, transform.up);
                blackboard.navMesh.isStopped = true;
                blackboard.cooldownToDeactivateTrap = 3f;
                break;

            case State.ATTACKINGPLAYER:
                floatEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                floatSoundCooldown = 0f;
                blackboard.navMesh.isStopped = true;
                anim.SetBool("AttackOrb", true);
                break;
            case State.ALERT:
                floatEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                floatSoundCooldown = 0f;
                blackboard.navMesh.isStopped = true;
                break;



        }

        currentState = newState;

    }

    public void StartChargeSound()
    {
        SoundManager.Instance.PlayEvent(beamChargeEvent, transform);
    }
    public void StartShootSound()
    {
        SoundManager.Instance.PlayEvent(beamShootEvent, transform);
    }
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
                    if(raycastPoint.CompareTag("MainRaycastOrbRay"))
                    {
                        Instantiate(blackboard.rayCollisionParticles, l_RaycastHit.point, Quaternion.identity);
                    }
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        
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
