using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD;
using FMOD.Studio;
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
    public Image icon;
    public float floatSoundCooldown = 0f;

    [Header("Target")]
    public GameObject target;
    public Transform child;
    private EnemyBehaviours behaviours;
    private Orb_Blackboard blackboard;
    public LayerMask mask;

    GameManager GM;
    public ParticleSystem particles;

    [Header("FMOD Events")]
    public string floatstringEvent;
    public string beamChargeEvent;
    public string beamShootEvent;
    EventInstance floatEvent;

    [Header("State")]
    public State currentState;
    public enum State { INITIAL,INVOKING, WANDERING, ALERT ,  ATTACKINGPLAYER};
    

   
    void OnEnable()
    {
        GM = GameManager.Instance;
        behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        child.rotation = Quaternion.LookRotation(gameObject.transform.forward);
        m_Laser.enabled = false;
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
                blackboard.navMesh.SetDestination(target.transform.position);
                floatSoundCooldown -= Time.deltaTime;
                if (floatSoundCooldown <= 0)
                {
                    floatEvent = SoundManager.Instance.PlayEvent(floatstringEvent, transform);

                    floatSoundCooldown = 11f;
                }
                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.navMesh.stoppingDistance)
                {
                    ChangeState(State.WANDERING);
                    break;
                }

                if (behaviours.PlayerFound(blackboard.playerDetectionRadius, blackboard.angleDetectionPlayer) &&
                    GM.GetEnemy().GetComponent<FSM_SeekPlayer>().currentState != FSM_SeekPlayer.State.ATTACKING)
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


            case State.ALERT:
                Rotate();
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

    public void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.INVOKING:
                anim.SetBool("Invoke", false);
                blackboard.navMesh.isStopped = false;
                break;
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
            case State.INVOKING:
                blackboard.navMesh.Warp(blackboard.orbSpawner.transform.position);
                anim.SetBool("Invoke", true);
                blackboard.navMesh.isStopped = true;
                
                break;
            case State.WANDERING:
                blackboard.navMesh.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();
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


    //ATTACK FUNCTIONS

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
            Color32 color = new Color32(212, 62, 55, 255);

            main.startColor = (Color)color;

        }
        if (blackboard.GetOrbHealth() == 2)
        {
            var main = particles.main;
            Color32 color = new Color32(128, 34, 30, 120);

            main.startColor = (Color)color;

        }
        if (blackboard.GetOrbHealth() == 1)
        {
            var main = particles.main;
            Color32 color = new Color32(56, 15, 13, 50);

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

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance, mask))
                {
                    if(raycastPoint.CompareTag("MainRaycastOrbRay"))
                    {
                        Instantiate(blackboard.rayCollisionParticles, l_RaycastHit.point, Quaternion.identity);
                    }
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        if (l_RaycastHit.collider.gameObject.GetComponent<PlayerController>().m_Life >= 2)
                        {
                            GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(2, gameObject, blackboard.XForceImpulseDamage, blackboard.YForceImpulseDamage);
                        }
                        else GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(1, gameObject, blackboard.XForceImpulseDamage, blackboard.YForceImpulseDamage);

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
