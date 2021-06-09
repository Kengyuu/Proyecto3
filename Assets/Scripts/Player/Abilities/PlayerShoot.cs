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


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStealth = GetComponent<PlayerSpecialAbilities>();
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (GM == null) GM = GameManager.Instance;

        //GM.OnStateChange += StateChanged;
        

    }

    /*    private void StateChanged()
        {
            switch (GM.gameState)
            {
                case GameState.MAP:
                    m_PlayerCanShoot = false;
                    break;
                case GameState.GAME:
                    m_PlayerCanShoot = true;
                    break;
            }
        }*/

    // Update is called once per frame
    void Update()
    {
        //if(m_PlayerCanShoot) CheckShootingCollisions();
        //TutorialCheck();


        if (m_PlayerMovement.m_InputSystem.Gameplay.Shoot.triggered && !m_IsPlayerShooting)
        {
            CheckTarget();
        }

        if (m_PlayerAnimations.m_Animator.GetBool("Absorb") && m_CurrentCorpseAbsortion != null)
        {
            Debug.Log("EL JUGADOR TIENE UN CADAVER CANALIZANDO");
            if (Vector3.Distance(transform.position, m_CurrentCorpseAbsortion.transform.position) > (m_CorpseDetectionDistance + m_CorpseDistanceOffset))
            {
                Debug.Log("JUGADOR ALEJADO DEL CADAVER; DEBERIA PARAR LA ABSORCION!");
                m_PlayerAnimations.m_Animator.SetBool("Absorb", false);
                absorbParticles.SetActive(false);
                m_CurrentCorpseAbsortion = null;
                ResetShoot();
            }
            
        }

    }

    /*private void TutorialCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            switch (tag)
            {
                case "Corpse":
                    if (firstTimeCorpse)
                    {
                        M_HudController.triggerShot = true;
                        firstTimeCorpse = false;
                    }

                    break;
                case "ActiveTrap":
                    if (firstTimeTrap)
                    {
                        M_HudController.triggerShotTrap = true;
                        firstTimeTrap = false;
                    }
                    break;
                case "TrapDeactivated":
                    if (firstTimeTrapD)
                    {
                        M_HudController.triggerShotTrapD = true;
                        firstTimeTrapD = false;
                    }
                    break;

                case "WeakPoint":
                    if (firstTimeShootEnemy)
                    {
                        M_HudController.triggerShotEnemy = true;
                        firstTimeShootEnemy = false;
                    }
                    break;

                case "CorpseOrb":
                    if (firstTimeShootEnemy)
                    {
                        M_HudController.triggerShotEnemy = true;
                        firstTimeShootEnemy = false;
                    }
                    break;
            }
        }
    }*/

/*    private void StartCasting()
    {
        
        
        Shoot();
        //Invoke("Shoot", m_ShootCastingTime);
    }*/

    private void ShootAnimStart()
    {
        Debug.Log("PLAYER DISPARA");
        m_IsPlayerShooting = true;
        crosshairAnim.SetBool("Shot", true); 
    }

    private void CheckTarget()
    {
        //SHOOT CASTING TIME FLOAT ANTERIOR -> 0.46f
        Debug.Log("Entro en CheckTarget Shoot");

        if (!m_PlayerStealth.m_IsPlayerVisibleToEnemy)
        {
            m_PlayerStealth.ResetAbilityAndStartCooldown();
        }
        GM.PlayerNoise(m_ShootNoise);

        RaycastHit hit;
        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_ShootLayers))
        {
            Debug.Log("Esto es el raycast de Shoot");
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);

            if ((tag == "Corpse" || tag == "CorpseTutorial") && l_CurrentDistance < m_CorpseDetectionDistance)
            {
                CorpseAbsorb(l_CurrentDistance, hit);
                return;
            }
            else
            {
                Shoot(l_CurrentDistance, tag, hit);
                return;
            }
        }
        else
        {
            //ResetShoot();
            /*m_IsPlayerShooting = false;
            crosshairAnim.SetBool("Shot", false);*/
        }
        Shoot(0f, "", hit); //Random shoot to AIR

        
    }


    private void CorpseAbsorb(float l_CurrentDistance, RaycastHit hit)
    {
        Debug.Log("Inicio absorción de CUERPO");
        ShootAnimStart();
        /*
         * - start animacion
	     -> eventos de animacion (FUNCIONES)
	    - Al final Sumar cadaver por evento
	    - al final de animacion ResetShoot() -> supuestamente hecho
        */
        if (l_CurrentDistance < m_CorpseDetectionDistance)
        {
            absorbParticles.SetActive(true);
            M_HudController.hasShot = true;
            //corpseHit = true;
            m_CurrentCorpseAbsortion = hit.transform.gameObject;
            m_PlayerAnimations.StartAbsorb();
            if(hit.collider.CompareTag("Corpse"))
                hit.collider.GetComponent<CorpseAbsortion>().AbsorbParticles(2.3f, absorbObjective);
            if(hit.collider.CompareTag("CorpseTutorial"))
                hit.collider.GetComponent<CorpseAbsorbTutorial>().AbsorbParticles(2.3f, absorbObjective);
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

    private void Shoot(float l_CurrentDistance, string tag, RaycastHit hit)
    {
        Debug.Log("Inicio FUNCION DE DISPARO");
        ShootAnimStart();
        /*
         * 	- Play animacion Beam + particulas
	    - Introducir el Switch actual de disparo
	    - al final de animacion ResetShoot()
        */
        chargeBeam.Play();
        handLight.Play();
        SoundManager.Instance.PlaySound(chargeEvent, transform.position);
        //Invoke("ShootBeam", m_ShootCastingTime);
        m_PlayerAnimations.StartShoot(l_CurrentDistance, tag, hit);

        
    }


/*    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        StopAnim();
    }
    private void StopAnim()
    {
        
        ResetShoot();
    }*/

    public void ResetShoot()
    {
        crosshairAnim.SetBool("Shot", false);
        m_IsPlayerShooting = false;
        //m_PlayerAnimations.StopAbsorb();
    }

    public void ShootBeam(float l_CurrentDistance, string tag, RaycastHit hit)
    {
        ParticleSystem particles = beam.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particles.shape;
        //shape.length = distance/5;
        //Debug.Log(distance);
        //beam.transform.localScale =
        
        beam.SetActive(true);
        mainBeam.SetActive(true);
        splashBeam.SetActive(true);
        SoundManager.Instance.PlaySound(shootEvent, transform.position);
        StartCoroutine(WaitBeam());

        switch (tag)
        {
            case "ActiveTrap":
                if (l_CurrentDistance < m_ButtonDetectionDistance)
                {
                    hit.transform.GetComponent<ActiveTrap>().EnableTrap();
                }
                break;
            case "WeakPoint":
                if (l_CurrentDistance < m_WeakPointDetectionDistance)
                {
                    WeakPoint wp = hit.collider.GetComponent<WeakPoint>();
                    wp.Invoke("TakeDamage", m_ShootCastingTime);
                }
                break;
            case "WeakPointTutorial":
                WeakPointsTutorial wpt = hit.collider.GetComponent<WeakPointsTutorial>();
                wpt.Invoke("DestroyWP", m_ShootCastingTime);
                break;
            case "CorpseOrb":
                if (l_CurrentDistance < m_OrbDetectionDistance)
                {
                    WaitAttackOrb(hit.collider.gameObject);
                    hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                    hit.collider.GetComponent<FSM_CorpseSearcher>().alert = true;
                    hit.collider.GetComponent<FSM_CorpseSearcher>().ChangeParticleColor();
                }
                break;

            case "HideOrb":
                if (l_CurrentDistance < m_OrbDetectionDistance)
                {
                    hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                }
                break;

            case "TrapOrb":
                if (l_CurrentDistance < m_OrbDetectionDistance)
                {
                    hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                    hit.collider.GetComponent<FSM_TrapSearcher>().alert = true;
                }

                break;
            case "AttackOrb":
                if (l_CurrentDistance < m_OrbDetectionDistance)
                {
                    hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                    hit.collider.GetComponent<FSM_AttackerOrb>().alert = true;
                }
                break;
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
        //ResetShoot();
    }
}