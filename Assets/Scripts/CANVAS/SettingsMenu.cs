using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [Header("Sliders")]
    public Slider m_MusicSlider;
    public Slider m_EffectSlider;

    [Header("Resolutions")]
    public TextMeshProUGUI m_ResolutionText;
    public List<string> m_ResolutionList;
    public int m_CurrentResolution;

    [Header("Languages")]
    public TextMeshProUGUI m_LanguageText;
    public List<string> m_LanguageList;
    public int m_CurrentLanguage;

    void Start()
    {
        m_CurrentResolution = 0;
        m_ResolutionText.SetText(m_ResolutionList[m_CurrentResolution]); //BORRAR AL HACER LAS OPCIONES!!!!

        m_CurrentLanguage = 0;
        m_LanguageText.SetText(m_LanguageList[m_CurrentLanguage]); //BORRAR AL HACER LAS OPCIONES!!!!
    }

    public void Apply()
    {
        Debug.Log("Aplicando cambios en las opciones del juego");
    }


    public void ChangeMusicVolume(int value)
    {
        //Debug.Log($"Entro en ChangeMusicVolume con el valor: {value}");
        m_MusicSlider.value += value;
    }

    public void ChangeEffectVolume(int value)
    {
        //Debug.Log($"Entro en ChangeEffectVolume con el valor: {value}");
        m_EffectSlider.value += value;
    }

    public void ChangeResolution(int value)
    {
        if(value > 0)
        {
            m_CurrentResolution++;
            if(m_CurrentResolution >= m_ResolutionList.Count)
            {
                m_CurrentResolution = 0;
            }
            //Debug.Log($"Current resolution index: {m_CurrentResolution}");
            m_ResolutionText.SetText(m_ResolutionList[m_CurrentResolution]);
        }
        else
        {
            m_CurrentResolution--;
            if (m_CurrentResolution < 0)
            {
                m_CurrentResolution = m_ResolutionList.Count-1;
            }
            //Debug.Log($"Current resolution index: {m_CurrentResolution}");
            m_ResolutionText.SetText(m_ResolutionList[m_CurrentResolution]);
        }
    }

    public void ChangeLanguage(int value)
    {
        if (value > 0)
        {
            m_CurrentLanguage++;
            if (m_CurrentLanguage >= m_LanguageList.Count)
            {
                m_CurrentLanguage = 0;
            }
            //Debug.Log($"Current resolution index: {m_CurrentLanguage}");
            m_LanguageText.SetText(m_LanguageList[m_CurrentLanguage]);
        }
        else
        {
            m_CurrentLanguage--;
            if (m_CurrentLanguage < 0)
            {
                m_CurrentLanguage = m_LanguageList.Count - 1;
            }
            //Debug.Log($"Current resolution index: {m_CurrentLanguage}");
            m_LanguageText.SetText(m_LanguageList[m_CurrentLanguage]);
        }
    }
}
