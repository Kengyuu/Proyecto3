using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    private PlayerController m_PlayerController;
    private GameManager GM;
    private ScoreManager m_ScoreManager;
    private HudController M_HudController;

    bool firstTimeCorpse = true;
    bool firstTimeTrap = true;
    bool firstTimeTrapD = true;
    bool firstTimeShootEnemy = true;
    [Header("Shoot")]
    public LayerMask m_ShootLayers;
    public float m_ShootCastingTime = 2f;
    public float m_MaxShootDistance = 100f;
    private bool m_PlayerCanShoot = true;

    public float absorbDuration = 2.5f;
    public Animator crosshairAnim;
    public GameObject absorbObjective;

    public Transform shootObjective;
    
    
    [Header("ShootEffects")]
    public ParticleSystem chargeBeam;
    public GameObject beam;
    public GameObject mainBeam;
    public GameObject splashBeam;
    public ParticleSystem handLight;
    public GameObject absorbParticles;
    public GameObject m_CurrentCorpseAbsortion;

    public GameObject shootCollisionParticles;

    [Header("Object detection distances")]
    public float m_CorpseDetectionDistance = 5f;
    public float m_CorpseDistanceOffset = 3f;
    public float m_ButtonDetectionDistance = 10f;
    public float m_TrapDetectionDistance = 5f;

    public float m_WeakPointDetectionDistance = 20f;

    public float m_OrbDetectionDistance = 20f;

    [Header("Shoot Noise")]
    public float m_ShootNoise = 10f;

    [Header("Debug")]
    [SerializeField] bool m_IsPlayerShooting = false;

    [Header("Animations")]
    public PlayerAnimations m_PlayerAnimations;

    [Header("Stealth Shutdown")]
    public PlayerSpecialAbilities m_PlayerStealth;

    [Header("FMOD Events")]
    public string chargeEvent;
    public string shootEvent;
    public string absorbEvent;

    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStealth = GetComponent<PlayerSpecialAbilities>();
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (GM == null) GM = GameManager.Instance;

    }
    void Update()
    {
        
        if (m_PlayerMovement.m_InputSystem.Gameplay.Shoot.triggered && !m_IsPlayerShooting)
        {
            CheckTarget();
        }

        if (m_PlayerAnimations.m_Animator.GetBool("Absorb") && m_CurrentCorpseAbsortion != null)
        {
            if (Vector3.Distance(transform.position, m_CurrentCorpseAbsortion.transform.position) > (m_CorpseDetectionDistance + m_CorpseDistanceOffset))
            {
                if (m_CurrentCorpseAbsortion.CompareTag("Corpse"))
                {
                    m_CurrentCorpseAbsortion.GetComponent<CorpseAbsortion>().systemActive = false;
                    m_CurrentCorpseAbsortion.GetComponent<CorpseAbsortion>().StopAbsortion();
                }
                else
                {
                    m_CurrentCorpseAbsortion.GetComponent<CorpseAbsorbTutorial>().systemActive = false;
                    m_CurrentCorpseAbsortion.GetComponent<CorpseAbsorbTutorial>().StopAbsortion();
                }
                
                m_PlayerAnimations.m_Animator.SetBool("Absorb", false);
                absorbParticles.SetActive(false);
                m_CurrentCorpseAbsortion = null;
                ResetShoot();
            }
            
        }

    }
    private void ShootAnimStart()
    {
        m_IsPlayerShooting = true;
        crosshairAnim.SetBool("Shot", true); 
    }

    private void CheckTarget()
    {
        if (!m_PlayerStealth.m_IsPlayerVisibleToEnemy)
        {
            m_PlayerStealth.ResetAbilityAndStartCooldown();
        }
        GM.PlayerNoise(m_ShootNoise);

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);

            if ((tag == "Corpse" || tag == "CorpseTutorial") && l_CurrentDistance < m_CorpseDetectionDistance)
            {
                CorpseAbsorb();
                return;
            }
            Shoot();
            return;
        }

    }


    private void CorpseAbsorb()
    {
        ShootAnimStart();
        SoundManager.Instance.PlayEvent(absorbEvent, transform);
        RaycastHit hit; 
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);

            if (l_CurrentDistance < m_CorpseDetectionDistance)
            {
                absorbParticles.SetActive(true);
                M_HudController.hasShot = true;
                m_CurrentCorpseAbsortion = hit.transform.gameObject;
                m_PlayerAnimations.StartAbsorb();
                if (hit.collider.CompareTag("Corpse"))
                    hit.collider.GetComponent<CorpseAbsortion>().AbsorbParticles(absorbDuration, absorbObjective);
                if (hit.collider.CompareTag("CorpseTutorial"))
                    hit.collider.GetComponent<CorpseAbsorbTutorial>().AbsorbParticles(absorbDuration, absorbObjective);
            }
        }

            
    }

    public void CorpseAbsorbEnd()
    {
        m_CurrentCorpseAbsortion = null;
        m_PlayerController.AddCorpse();
        M_HudController.UpdateAddCorpses(gameObject);
        
        if(OrbEvents.current != null)
            OrbEvents.current.ManageOrbs();
        absorbParticles.SetActive(false);
    }

    private void Shoot()
    {   
        ShootAnimStart();
        chargeBeam.Play();
        handLight.Play();
        SoundManager.Instance.PlaySound(chargeEvent, transform.position);
        m_PlayerAnimations.StartShoot();

        
    }
    public void ResetShoot()
    {
        crosshairAnim.SetBool("Shot", false);
        m_IsPlayerShooting = false;
    }

    public void ShootBeam()
    {
        ParticleSystem particles = beam.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particles.shape;

        beam.SetActive(true);
        mainBeam.SetActive(true);
        splashBeam.SetActive(true);
        SoundManager.Instance.PlaySound(shootEvent, transform.position);
        StartCoroutine(WaitBeam());

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);
            switch (tag)
            {
                case "ActiveTrap":
                    if (l_CurrentDistance < m_ButtonDetectionDistance)
                    {
                        hit.transform.GetComponent<ActiveTrap>().EnableTrap();
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;
                case "WeakPoint":
                    if (l_CurrentDistance < m_WeakPointDetectionDistance)
                    {
                        WeakPoint wp = hit.collider.GetComponent<WeakPoint>();
                        wp.Invoke("TakeDamage", m_ShootCastingTime);
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;
                case "WeakPointTutorial":
                    if (l_CurrentDistance < m_WeakPointDetectionDistance)
                    {
                        WeakPointsTutorial wpt = hit.collider.GetComponent<WeakPointsTutorial>();
                        wpt.Invoke("DestroyWP", m_ShootCastingTime);
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;
                case "CorpseOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        WaitAttackOrb(hit.collider.gameObject);
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_CorpseSearcher>().alert = true;
                        hit.collider.GetComponent<FSM_CorpseSearcher>().ChangeParticleColor();
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;

                case "HideOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;

                case "TrapOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_TrapSearcher>().alert = true;
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }

                    break;
                case "AttackOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_AttackerOrb>().alert = true;
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;
                default:
                    if(l_CurrentDistance < 10f)
                    {
                        Instantiate(shootCollisionParticles, hit.point, Quaternion.identity);
                    }
                    break;
            }
        }
        
    }

    IEnumerator WaitAttackOrb(GameObject orb)
    {
        yield return new WaitForSeconds(m_ShootCastingTime);
        orb.GetComponent<Orb_Blackboard>().TakeDamage(1);
        switch(orb.tag)
        {
            case "CorpseOrb":
                orb.GetComponent<FSM_CorpseSearcher>().alert = true;
                orb.GetComponent<FSM_CorpseSearcher>().ChangeParticleColor();
                break;
            case "TrapOrb":
                orb.GetComponent<FSM_TrapSearcher>().alert = true;
                break;
            case "AttackOrb":
                orb.GetComponent<FSM_AttackerOrb>().alert = true;
                break;
        }
    }
    IEnumerator WaitBeam()
    {
        yield return new WaitForSeconds(0.2f);
        beam.SetActive(false);
        mainBeam.SetActive(false);
        splashBeam.SetActive(false);
    }
}