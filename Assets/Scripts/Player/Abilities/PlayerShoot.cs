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
    

    [Header("Shoot")]
    public LayerMask m_ShootLayers;
    public float m_ShootCastingTime = 2f;
    public float m_MaxShootDistance = 100f;
    private bool m_PlayerCanShoot = true;
    public Animator crosshairAnim;
    public GameObject absorbObjective;
    
    
    [Header("ShootEffects")]
    public ParticleSystem chargeBeam;
    public GameObject beam;
    public GameObject mainBeam;
    public GameObject splashBeam;
    public ParticleSystem handLight;

    [Header("Object detection distances")]
    public float m_CorpseDetectionDistance = 5f;
    public float m_ButtonDetectionDistance = 10f;
    public float m_TrapDetectionDistance = 5f;

    public float m_WeakPointDetectionDistance = 20f;

    public float m_OrbDetectionDistance = 20f;

    [Header("Shoot Noise")]
    public float m_ShootNoise = 10f;

    [Header("Debug")]
    [SerializeField] bool m_IsPlayerShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerController = GetComponent<PlayerController>();
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
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            bool firstTimeCorpse = true;
            bool firstTimeTrap = true;
            bool firstTimeShootEnemy = true;
            //Debug.Log($"{hit.transform.name} ha sido impactado a una distancia de {l_CurrentDistance}");
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
        if (m_PlayerMovement.m_InputSystem.Gameplay.Shoot.triggered && !m_IsPlayerShooting)
        {
            StartCasting();
        }
    }

    private void StartCasting()
    {
        m_IsPlayerShooting = true;
        crosshairAnim.SetBool("Shot", true);
        

        chargeBeam.Play();
        handLight.Play();
        StartCoroutine(Wait());

        
        Invoke("Shoot", m_ShootCastingTime);
    }

    private void Shoot()
    {
        //Debug.Log("PLAYER DISPARA");

        bool corpseHit = false;
        float distanceToObjective = 2f;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);
            distanceToObjective = Vector3.Distance(beam.transform.position, hit.point);
            //Debug.Log($"{hit.transform.name} ha sido impactado a una distancia de {l_CurrentDistance}");
            switch (tag)
            {
                case "Corpse":
                    if (l_CurrentDistance < m_CorpseDetectionDistance)
                    {
                        M_HudController.hasShot = true;
                        //AQUÍ PARA LLAMAR AL SISTEMA DE PARTÍCULAS
                        hit.collider.GetComponent<CorpseAbsortion>().AbsorbParticles(2.5f, absorbObjective);
                        corpseHit = true;
                        
                        //Debug.Log($"Cadáver a distancia adecuada: {l_CurrentDistance}");
                        //hit.transform.gameObject.SetActive(false);
                        m_PlayerController.AddCorpse();
                        M_HudController.UpdateAddCorpses(gameObject);
                        OrbEvents.current.ManageOrbs();
                        //GM.m_GameObjectSpawner.ClearBodys(hit.collider.GetComponent<CorpseControl>().spawnPosition);
                        //m_ScoreManager.RemoveRemainingCorpse();
                    }
                    break;
                case "ActiveTrap":
                    if (l_CurrentDistance < m_ButtonDetectionDistance)
                    {
                        //Debug.Log($"Botón a distancia adecuada: {l_CurrentDistance}");
                        hit.transform.GetComponent<ActiveTrap>().EnableTrap();
                    }
                    break;
                /*case "TrapDeactivated":
                    if (l_CurrentDistance < m_TrapDetectionDistance)
                    {
                        //Debug.Log($"Trampa a distancia adecuada: {l_CurrentDistance}");
                        hit.transform.GetComponent<PassiveTrap>().EnableTrap();
                    }
                    break;*/
                case "WeakPoint":
                    if (l_CurrentDistance < m_WeakPointDetectionDistance)
                    {
                        //Debug.Log($"Weak Point a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<WeakPoint>().TakeDamage();
                        beam.transform.LookAt(hit.point);
                        mainBeam.transform.LookAt(hit.point);
                        splashBeam.transform.LookAt(hit.point);
                    }
                    break;

                case "CorpseOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_CorpseSearcher>().alert = true;
                        hit.collider.GetComponent<FSM_CorpseSearcher>().ChangeParticleColor();
                        beam.transform.LookAt(hit.point);
                        mainBeam.transform.LookAt(hit.point);
                        splashBeam.transform.LookAt(hit.point);
                    }
                    break;

                case "HideOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        // hit.collider.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        beam.transform.LookAt(hit.point);
                        mainBeam.transform.LookAt(hit.point);
                        splashBeam.transform.LookAt(hit.point);
                    }
                    break;

                case "TrapOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_TrapSearcher>().alert = true;
                        beam.transform.LookAt(hit.point);
                        mainBeam.transform.LookAt(hit.point);
                        splashBeam.transform.LookAt(hit.point);
                    }

                    break;
                case "AttackOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        //Debug.Log($"Attack Orb Hit");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_AttackerOrb>().alert = true;
                        beam.transform.LookAt(hit.point);
                        mainBeam.transform.LookAt(hit.point);
                        splashBeam.transform.LookAt(hit.point);
                    }
                    break;
            }

        }
        if(!corpseHit)
        {
            
            ShootBeam(distanceToObjective);
            
        }
        GM.PlayerNoise(m_ShootNoise);
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        StopAnim();
    }
    private void StopAnim()
    {
        crosshairAnim.SetBool("Shot", false);
    }

    public void ResetShoot()
    {
        m_IsPlayerShooting = false;
    }

    private void ShootBeam(float distance)
    {
        ParticleSystem particles = beam.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shape = particles.shape;
        //shape.length = distance/5;
        //Debug.Log(distance);
        //beam.transform.localScale =
        
        beam.SetActive(true);
        mainBeam.SetActive(true);
        splashBeam.SetActive(true);
        StartCoroutine(WaitBeam());
    }

    IEnumerator WaitBeam()
    {
        yield return new WaitForSeconds(0.2f);
        beam.SetActive(false);
        mainBeam.SetActive(false);
        splashBeam.SetActive(false);
        ResetShoot();
    }
}