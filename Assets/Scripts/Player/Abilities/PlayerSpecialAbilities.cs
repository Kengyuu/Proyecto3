using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpecialAbilities : MonoBehaviour
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
    public bool m_SliderOnCooldown = false;

    public Image cooldownSlider;
    public Image chainsInv;
    public Image chainsTrace;
    public Image invIcon;
    public TextMeshProUGUI q;
    public Image traceIcon;

    [Header("Animation")]
    public Animator chainsInvAnim;
    public Animator chainsTraceAnim;

    [Header("Invisibility Materials")]
    public List<GameObject> m_Arms;
    public Material m_OriginalArmMaterial;
    public Material m_OriginalBraceletMaterial;
    public Material m_TransparentArmMaterial;
    public Material m_TransparentBraceletMaterial;

    [Header("Debug")]
    [SerializeField] bool Skill_1 = false;
    [SerializeField] bool Skill_2 = false;

    [Header("Animations")]
    public PlayerAnimations m_PlayerAnimations;

    [Header("FMOD Events")]
    public string cloakEvent;

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


        if (m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.triggered && !m_AbilityOnCooldown && Skill_1)
        {
            SoundManager.Instance.PlaySound(cloakEvent, transform.position);
            Debug.Log("He apretado la habilidad especial 1 con la letra 'Q'.");
            m_IsPlayerVisibleToEnemy = false;
            m_AbilityOnCooldown = true;
            Debug.Log("MECMECMECMECMEC");
            m_PlayerAnimations.StartStealth();

            SwapTransparent();
            StartCoroutine(FadeTo(0.9f, 0.7f));


            Invoke("ResetAbilityAndStartCooldown", m_InvisibilityMaxTime);
            cooldownSlider.fillAmount = 0;
        }

        if (m_SliderOnCooldown)
        {
            cooldownSlider.fillAmount += 1 / m_HiddenPrayerCooldown * Time.deltaTime;
            
        }
    }

    private void SwapTransparent()
    {
        foreach (GameObject obj in m_Arms)
        {
            MeshRenderer bracelet = obj.GetComponent<MeshRenderer>();
            SkinnedMeshRenderer arms = obj.GetComponent<SkinnedMeshRenderer>();
            if (bracelet != null)
            {
                bracelet.material = m_TransparentBraceletMaterial;
            }
            else if (arms != null)
            {
                arms.material = m_TransparentArmMaterial;
            }
        }
    }

    private void SwapOpaque()
    {
        foreach (GameObject obj in m_Arms)
        {
            MeshRenderer bracelet = obj.GetComponent<MeshRenderer>();
            SkinnedMeshRenderer arms = obj.GetComponent<SkinnedMeshRenderer>();
            if (bracelet != null)
            {
                bracelet.material = m_OriginalBraceletMaterial;
            }
            else if (arms != null)
            {
                arms.material = m_OriginalArmMaterial;
            }
        }
    }

    public void ResetAbilityAndStartCooldown()
    {
        Debug.Log("INVISIBILIDAD TERMINADA / INTERRUMPIDA");
        m_IsPlayerVisibleToEnemy = true;
        m_SliderOnCooldown = true;
        StartCoroutine(FadeTo(0.0f, 0.9f));
        SwapOpaque();
        Invoke("EnableAbility", m_HiddenPrayerCooldown);
        Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer, false);
    }

    IEnumerator FadeTo(float targetColor, float fadeDuration)
    {
        
        float alpha = m_Arms[1].GetComponent<MeshRenderer>().material.GetFloat("_IntensityTransparentMap");
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            foreach(GameObject obj in m_Arms)
            {
                MeshRenderer bracelet = obj.GetComponent<MeshRenderer>();
                SkinnedMeshRenderer arms = obj.GetComponent<SkinnedMeshRenderer>();
                if(bracelet != null)
                {
                    bracelet.material.SetFloat("_IntensityTransparentMap", Mathf.Lerp(alpha, targetColor, elapsedTime / fadeDuration));
                }
                else if(arms != null)
                {
                    arms.material.SetFloat("_IntensityTransparentMap", Mathf.Lerp(alpha, targetColor, elapsedTime / fadeDuration));
                }
                
            }
            
            yield return null;
        }
    }

    private void EnableAbility()
    {
        cooldownSlider.fillAmount = 1;
        m_AbilityOnCooldown = false;
        m_SliderOnCooldown = false;
        Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer);
    }


    private void CheckModifiers()
    {
        //Debug.Log("CheckModifiers del player llamado");

        int l_Corpses = (int)m_ScoreManager.GetEnemyCorpses();

        if (l_Corpses < 3)
        {
            if (Skill_1 || m_SliderOnCooldown)
            {
                //Debug.Log($"Habilidad 1 - Oraci�n Oculta DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
                chainsInv.gameObject.SetActive(true);
                chainsInvAnim.SetBool("Chain In", true);
                q.gameObject.SetActive(false);
                invIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 0.3f);
                Skill_1 = false;
            }
            if (Skill_2)
            {
                //Debug.Log($"Habilidad 2 - pasiva DESACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                Skill_2 = false;
                chainsTrace.gameObject.SetActive(true);
                chainsTraceAnim.SetBool("Chain In", true);
                traceIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 0.3f);
            }
            return;
        }
        else if (l_Corpses >= 3)
        {
            if (!Skill_1)
            {
                //Debug.Log($"Habilidad 1 - Oraci�n Oculta ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Enable();
                chainsInvAnim.SetBool("Chain Out", true);
                q.gameObject.SetActive(true);
                invIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 1f);
                Skill_1 = true;
            }

            if (l_Corpses >= 6)
            {
                if (!Skill_2)
                {
                    //Debug.Log($"Habilidad 2 - pasiva ACTIVADA con {m_ScoreManager.GetPlayerCorpses()} cuerpos.");
                    Skill_2 = true;
                    chainsTraceAnim.SetBool("Chain Out", true);
                    traceIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 1f);
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
                    chainsTrace.gameObject.SetActive(true);
                    chainsTraceAnim.SetBool("Chain In", true);
                    traceIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 0.3f);
                    m_Camera.cullingMask = m_OriginalLayerMask;
                }
            }

        }
    }

    public void SetInvToFalse()
    {
        chainsInv.gameObject.SetActive(false);
    }
    public void SetTraceToFalse()
    {
        chainsTrace.gameObject.SetActive(false);
    }
}
