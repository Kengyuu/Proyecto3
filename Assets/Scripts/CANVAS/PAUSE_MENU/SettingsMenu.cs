using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class SettingsMenu : MonoBehaviour
{

    [Header("Sliders")]
    public Slider m_MusicSlider;
    public Slider m_EffectSlider;

    [Header("Resolutions")]
    public TextMeshProUGUI m_ResolutionText;
    public List<string> m_ResolutionList;
    public int m_CurrentResolution;


    [Header("FMOD")]
    private VCA VcaControllerMusic;
    public string Vca_Music;
    private VCA VcaControllerSFX;
    public string Vca_SFX;

    [SerializeField] private float vcaVolume;
    [SerializeField] private float vcaVolumeSFX;

    void Start()
    {
        VcaControllerMusic = RuntimeManager.GetVCA("vca:/" + Vca_Music);
        VcaControllerMusic.setVolume(m_MusicSlider.value);

        VcaControllerSFX = RuntimeManager.GetVCA("vca:/" + Vca_SFX);
        VcaControllerSFX.setVolume(m_EffectSlider.value);
    }


    public void ChangeMusicVolume(int value)
    {
        m_MusicSlider.value += value;
        VcaControllerMusic.setVolume((float)m_MusicSlider.value);
    }

    public void ChangeEffectVolume(int value)
    {
        m_EffectSlider.value += value;
        VcaControllerSFX.setVolume((float)m_EffectSlider.value);
    }

    public void ChangeResolution(int value)
    {
        if (value > 0)
        {
            m_CurrentResolution++;
            if (m_CurrentResolution >= m_ResolutionList.Count)
            {
                m_CurrentResolution = 0;
            }
            m_ResolutionText.SetText(m_ResolutionList[m_CurrentResolution]);
        }
        else
        {
            m_CurrentResolution--;
            if (m_CurrentResolution < 0)
            {
                m_CurrentResolution = m_ResolutionList.Count - 1;
            }
            m_ResolutionText.SetText(m_ResolutionList[m_CurrentResolution]);
        }
    }

}
