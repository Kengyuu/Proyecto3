using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiddenPrayer : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    //private PlayerController m_PlayerController;

    private GameManager GM;
    private ScoreManager m_ScoreManager;
    private Camera m_Camera;



    [Header("Player Modifiers")]
    public float m_HiddenPrayerCooldown = 3f;
    public float m_InvisibilityMaxTime = 5f;
    public LayerMask m_OriginalLayerMask;
    public LayerMask m_EnemyTracesLayerMask;
    public bool m_IsPlayerVisibleToEnemy = true;
    public bool m_AbilityOnCooldown = false;

    [Header("Debug")]
    [SerializeField] bool Skill_1 = false;
    [SerializeField] bool Skill_2 = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        //m_PlayerController = GetComponent<PlayerController>();

        m_Camera = Camera.main;

        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (GM == null) GM = GameManager.Instance;

        GM.OnMofidiersHandler += CheckModifiers;

        //Disable special ability 1 @ startup
        m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug:
        
        //---


        if (m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.triggered && !m_AbilityOnCooldown)
        {
            Debug.Log("He apretado la habilidad especial 1 con la letra 'Q'.");
            m_IsPlayerVisibleToEnemy = false;
            m_AbilityOnCooldown = true;
            Invoke("ResetAbilityAndStartCooldown", m_InvisibilityMaxTime);
        }
    }

    private void ResetAbilityAndStartCooldown()
    {
        m_IsPlayerVisibleToEnemy = true;
        Invoke("EnableAbility", m_HiddenPrayerCooldown);
    }

    private void EnableAbility()
    {
        m_AbilityOnCooldown = false;
    }


    private void CheckModifiers()
    {
        Debug.Log("CheckModifiers del player llamado");

        int l_Corpses = (int)m_ScoreManager.GetPlayerCorpses();

        if (l_Corpses < 3)
        {
            if (Skill_1)
            {
                Debug.Log($"Habilidad 1 - Oración Oculta DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
                Skill_1 = false;
            }
            if (Skill_2)
            {
                Debug.Log($"Habilidad 2 - pasiva DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                Skill_2 = false;
            }
            return;
        }
        else if (l_Corpses >= 3)
        {
            if (!Skill_1)
            {
                Debug.Log($"Habilidad 1 - Oración Oculta ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Enable();
                Skill_1 = true;
            }

            if (l_Corpses >= 6)
            {
                if (!Skill_2)
                {
                    Debug.Log($"Habilidad 2 - pasiva ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                    Skill_2 = true;
                    m_Camera.cullingMask = m_EnemyTracesLayerMask;
                }
                return;
            }
            if (l_Corpses < 6)
            {
                if (Skill_2)
                {
                    Debug.Log($"Habilidad 2 - pasiva DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                    Skill_2 = false;
                    m_Camera.cullingMask = m_OriginalLayerMask;
                }
            }

        }
    }
}
