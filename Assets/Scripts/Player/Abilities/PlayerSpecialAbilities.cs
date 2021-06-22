using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PlayerSpecialAbilities : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;

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
    public float firstTier = 3;
    public float secondTier = 6;

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

    [Header("Post-processing")]
    public PostProcessVolume m_PostProVolume;
    public float m_FadeSpeed;

    [Header("Debug")]
    [SerializeField] bool Skill_1 = false;
    [SerializeField] bool Skill_2 = false;

    [Header("Animations")]
    public PlayerAnimations m_PlayerAnimations;

    [Header("FMOD Events")]
    public string cloakEvent;
    public string unlockedModifierEvent;
    public string lockedModifierEvent;

    void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();

        m_PostProVolume.weight = 0f;

        m_Camera = Camera.main;

        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (GM == null) GM = GameManager.Instance;

        GM.OnMofidiersHandler += CheckModifiers;

        m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
    }

    void Update()
    {

        if (m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.triggered && !m_AbilityOnCooldown && Skill_1)
        {
            SoundManager.Instance.PlaySound(cloakEvent, transform.position);
            m_IsPlayerVisibleToEnemy = false;
            m_AbilityOnCooldown = true;

           
            StartCoroutine(FadePostProTo(1f, m_FadeSpeed));

            m_PlayerAnimations.StartStealth();

            SwapTransparent();
            StartCoroutine(FadeTo(0.9f, 0.7f));


            Invoke("ResetAbilityAndStartCooldown", m_InvisibilityMaxTime);
            cooldownSlider.fillAmount = 0.22f;
        }

        if (m_SliderOnCooldown)
        {
            cooldownSlider.fillAmount += 1 / m_HiddenPrayerCooldown * Time.deltaTime;
            
        }
    }


    IEnumerator FadePostProTo(float aValue, float aTime)
    {
        //float alpha = m_PostProVolume.weight;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            m_PostProVolume.weight = Mathf.Lerp(m_PostProVolume.weight, aValue, t);
            yield return null;
        }
    }

    IEnumerator FadePostProToCero(float aValue, float aTime)
    {
        //float alpha = m_PostProVolume.weight;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            m_PostProVolume.weight = Mathf.Lerp(m_PostProVolume.weight, aValue, t);
            yield return null;
        }
        Debug.Log("LLEGO AQUIIII");
        m_PostProVolume.weight = 0f;
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
        m_IsPlayerVisibleToEnemy = true;
        m_SliderOnCooldown = true;
        StartCoroutine(FadePostProToCero(0f, m_FadeSpeed));
        SoundManager.Instance.PlaySound(cloakEvent, transform.position);
        StartCoroutine(FadeTo(0.0f, 0.9f));
        SwapOpaque();
        Invoke("EnableAbility", m_HiddenPrayerCooldown);
        if(GM.GetEnemy() != null)
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

        if(GM.GetEnemy() != null)
            Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer);
    }


    private void CheckModifiers()
    {

        int l_Corpses = (int)m_ScoreManager.GetEnemyCorpses();

        if (l_Corpses < firstTier)
        {
            if (Skill_1 || m_SliderOnCooldown)
            {
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
                chainsInv.gameObject.SetActive(true);
                chainsInvAnim.SetBool("Chain In", true);
                q.gameObject.SetActive(false);
                SoundManager.Instance.PlayEvent(unlockedModifierEvent, transform);
                invIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 0.3f);
                Skill_1 = false;
            }
            if (Skill_2)
            {
                Skill_2 = false;
                chainsTrace.gameObject.SetActive(true);
                chainsTraceAnim.SetBool("Chain In", true);
                SoundManager.Instance.PlayEvent(lockedModifierEvent, transform);
                traceIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 0.3f);
            }
            return;
        }
        else if (l_Corpses >= firstTier)
        {
            if (!Skill_1)
            {
                m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Enable();
                chainsInvAnim.SetBool("Chain Out", true);
                q.gameObject.SetActive(true);
                SoundManager.Instance.PlayEvent(unlockedModifierEvent, transform);
                invIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 1f);
                Skill_1 = true;
            }

            if (l_Corpses >= secondTier)
            {
                if (!Skill_2)
                {
                    Skill_2 = true;
                    chainsTraceAnim.SetBool("Chain Out", true);
                    SoundManager.Instance.PlayEvent(unlockedModifierEvent, transform);
                    traceIcon.color = new Color(invIcon.color.r, invIcon.color.g, invIcon.color.b, 1f);
                    m_Camera.cullingMask = m_EnemyTracesLayerMask;
                }
                return;
            }
            if (l_Corpses < secondTier)
            {
                if (Skill_2)
                {
                    Skill_2 = false;
                    chainsTrace.gameObject.SetActive(true);
                    chainsTraceAnim.SetBool("Chain In", true);
                    SoundManager.Instance.PlayEvent(lockedModifierEvent, transform);
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
