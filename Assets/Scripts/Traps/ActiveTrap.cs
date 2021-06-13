using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    public Material originalMaterial;
    public Material transparentMaterial;
    public bool m_TrapCanBeEnabled = true;
    public float m_FloorEnableDelay = 2f;
    public float m_FloorDisableTime = 5f;
    public float m_TrapEnableCooldown = 10f;
    public GameObject m_Floor;
    public GameObject m_Floor1;

    public ParticleSystem particles;
    ParticleSystem.RotationOverLifetimeModule rotation;

    [Header("FMOD Events")]

    public string activateTrapEvent;

    //test
    public bool m_CooldownStarted;

    private void Start()
    {
        m_Floor.SetActive(false);
        rotation = particles.rotationOverLifetime;
        
    }


    public void EnableTrap()
    {
        if (m_TrapCanBeEnabled)
        {
            m_TrapCanBeEnabled = false;
            Invoke("ActivateFloor", m_FloorEnableDelay);
        }
    }

    private void ActivateFloor()
    {
        SoundManager.Instance.PlayEvent(activateTrapEvent, transform);
        rotation.z = 1.5f;
        // m_Floor1.SetActive(true);
        m_Floor.SetActive(true);
        Debug.Log(m_Floor.name);
        Invoke("DisableFloor", m_FloorDisableTime);
    }

    private void DisableFloor()
    {
        rotation.z = 0;
        //rotation.enabled = false;
        m_Floor.SetActive(false);
        //m_Floor1.SetActive(false);
        Invoke("ReEnableButton", m_TrapEnableCooldown); //NO USAR
        m_CooldownStarted = true;
    }

    private void ReEnableButton()
    {
        m_CooldownStarted = false;
        m_TrapCanBeEnabled = true;
    }
}
