using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    private PlayerController m_PlayerController;
    private GameManager GM;
    private ScoreManager m_ScoreManager;

    [Header("Shoot")]
    public LayerMask m_ShootLayers;
    public float m_ShootCastingTime = 2f;
    public float m_MaxShootDistance = 100f;
    private bool m_PlayerCanShoot = true;

    [Header("Object detection distances")]
    public float m_CorpseDetectionDistance = 5f;
    public float m_ButtonDetectionDistance = 10f;

    public float m_WeakPointDetectionDistance = 20f;

    public float m_OrbDetectionDistance = 20f;

    [Header("Shoot Noise")]
    public float m_ShootNoise = 50f;

    [Header("Debug")]
    [SerializeField] bool m_IsPlayerShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerController = GetComponent<PlayerController>();
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
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

        if (m_PlayerMovement.m_InputSystem.Gameplay.Shoot.triggered && !m_IsPlayerShooting)
        {
            StartCasting();
        }
    }

    private void StartCasting()
    {
        
        m_IsPlayerShooting = true;
        Invoke("Shoot", m_ShootCastingTime);
    }

    private void Shoot()
    {
        


        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);
            //Debug.Log($"{hit.transform.name} ha sido impactado a una distancia de {l_CurrentDistance}");
            switch (tag)
            {
                case "Corpse":
                    if(l_CurrentDistance < m_CorpseDetectionDistance)
                    {
                        //Debug.Log($"Cadáver a distancia adecuada: {l_CurrentDistance}");
                        hit.transform.gameObject.SetActive(false);
                        m_PlayerController.AddCorpse();
                        OrbEvents.current.ManageOrbs();
                        //m_ScoreManager.RemoveRemainingCorpse();
                        
                    }
                    break;
                case "Button":
                    if (l_CurrentDistance < m_ButtonDetectionDistance)
                    {
                        //Debug.Log($"Botón a distancia adecuada: {l_CurrentDistance}");
                        hit.transform.gameObject.SetActive(false);
                    }
                    break;
                case "WeakPoint":
                    if (l_CurrentDistance < m_WeakPointDetectionDistance)
                    {
                        //Debug.Log($"Weak Point a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<WeakPoint>().TakeDamage();
                    }
                    break;

                case "CorpseOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_CorpseSearcher>().alert = true;

                    }
                    break;

                case "HideOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                       // hit.collider.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                    }
                    break;

                case "TrapOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        //Debug.Log($"Orb a distancia adecuada: {l_CurrentDistance}");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_TrapSearcher>().alert = true;
                    }
            
                    break;
                case "AttackOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        Debug.Log($"Attack Orb Hit");
                        hit.collider.GetComponent<Orb_Blackboard>().TakeDamage(1);
                        hit.collider.GetComponent<FSM_AttackerOrb>().alert = true;
                    }
                    break;
            }
            
        }

        GM.PlayerNoise(m_ShootNoise);
        ResetShoot();
    }

    private void ResetShoot()
    {
        m_IsPlayerShooting = false;
    }

    /*private void CheckShootingCollisions()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;
            float l_CurrentDistance = Vector3.Distance(hit.transform.position, transform.position);
            bool l_InRange = false;
            switch (tag)
            {
                case "Corpse":
                    if (l_CurrentDistance < m_CorpseDetectionDistance)
                    {
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        l_InRange = true;
                    }
                    break;
                case "Button":
                    if (l_CurrentDistance < m_ButtonDetectionDistance)
                    {
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        l_InRange = true;
                    }
                    break;
                case "WeakPoint":
                    if (l_CurrentDistance < m_WeakPointDetectionDistance)
                    {
                        hit.collider.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                        l_InRange = true;
                    }
                    break;

                case "CorpseOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                       
                        l_InRange = true;
                    }
                    break;
                case "HideOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        
                        l_InRange = true;
                    }
                    break;
                case "TrapOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {
                        
                        l_InRange = true;
                    }
                    break;
                case "AttackOrb":
                    if (l_CurrentDistance < m_OrbDetectionDistance)
                    {

                        l_InRange = true;
                    }
                    break;
            }

            if (l_InRange && m_PlayerMovement.m_InputSystem.Gameplay.Shoot.triggered && !m_IsPlayerShooting)
            {
                StartCasting();
            }
        }
    }*/

}
