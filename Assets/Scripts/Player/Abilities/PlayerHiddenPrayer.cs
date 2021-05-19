using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image cooldownSlider;
    public Image cloakIcon;
    public Image traceIcon;

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
            cooldownSlider.fillAmount = 0;
        }
    }

    private void ResetAbilityAndStartCooldown()
    {
        Debug.Log("Empezando cooldown");
        m_IsPlayerVisibleToEnemy = true;
        
        
        
        Invoke("EnableAbility", m_HiddenPrayerCooldown);
        Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer, false);
    }

    private void EnableAbility()
    {
        cooldownSlider.fillAmount = 1;
        m_AbilityOnCooldown = false;
        Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer);
    }


    private void CheckModifiers()
    {
        //Debug.Log("CheckModifiers del player llamado");

        int l_Corpses = (int)m_ScoreManager.GetEnemyCorpses();

        if (l_Corpses < 3)
        {
            if (Skill_1)
            {
                //Debug.Log($"Habilidad 1 - Oraci�n Oculta DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
                cloakIcon.gameObject.SetActive(false);
                Skill_1 = false;
            }
            if (Skill_2)
            {
                //Debug.Log($"Habilidad 2 - pasiva DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                Skill_2 = false;
                traceIcon.gameObject.SetActive(false);
            }
            return;
        }
        else if (l_Corpses >= 3)
        {
            if (!Skill_1)
            {
                //Debug.Log($"Habilidad 1 - Oraci�n Oculta ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Enable();
                cloakIcon.gameObject.SetActive(true);
                Skill_1 = true;
            }

            if (l_Corpses >= 6)
            {
                if (!Skill_2)
                {
                    //Debug.Log($"Habilidad 2 - pasiva ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                    Skill_2 = true;
                    traceIcon.gameObject.SetActive(true);
                    m_Camera.cullingMask = m_EnemyTracesLayerMask;
                }
                return;
            }
            if (l_Corpses < 6)
            {
                if (Skill_2)
                {
                    //Debug.Log($"Habilidad 2 - pasiva DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                    Skill_2 = false;
                    traceIcon.gameObject.SetActive(false);
                    m_Camera.cullingMask = m_OriginalLayerMask;
                }
            }

        }
    }
}
