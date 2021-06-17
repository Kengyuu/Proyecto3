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
    //public int m_CurrentResolution;


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
        VcaControllerMusic.setVolume(GameManager.Instance.musicVolume);
        m_MusicSlider.value = GameManager.Instance.musicVolume;
       
        VcaControllerSFX = RuntimeManager.GetVCA("vca:/" + Vca_SFX);
        VcaControllerSFX.setVolume(GameManager.Instance.effectVolume);
        m_EffectSlider.value = GameManager.Instance.effectVolume;

        m_ResolutionText.SetText(m_ResolutionList[GameManager.Instance.m_Resolution]);
    }

    public void ChangeMusicVolume(int value)
    {
        m_MusicSlider.value += value;
        VcaControllerMusic.setVolume((float)m_MusicSlider.value);
        GameManager.Instance.musicVolume = (float)m_MusicSlider.value;
    }

    public void ChangeEffectVolume(int value)
    {
        m_EffectSlider.value += value;
        VcaControllerSFX.setVolume((float)m_EffectSlider.value);
        GameManager.Instance.effectVolume = (float)m_EffectSlider.value;
    }

    public void ChangeResolution(int value)
    {
        if (value > 0)
        {
            GameManager.Instance.m_Resolution++;
            if (GameManager.Instance.m_Resolution >= m_ResolutionList.Count)
            {
                GameManager.Instance.m_Resolution = 0;
            }
            m_ResolutionText.SetText(m_ResolutionList[GameManager.Instance.m_Resolution]);
        }
        else
        {
            GameManager.Instance.m_Resolution--;
            if (GameManager.Instance.m_Resolution < 0)
            {
                GameManager.Instance.m_Resolution = m_ResolutionList.Count - 1;
            }
            m_ResolutionText.SetText(m_ResolutionList[GameManager.Instance.m_Resolution]);
        }


        if (GameManager.Instance.m_Resolution == 0)
            Screen.fullScreenMode = FullScreenMode.Windowed;
        else if (GameManager.Instance.m_Resolution == 1)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

}
