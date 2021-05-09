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

    private void Start()
    {
        m_Floor.SetActive(false);
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
        m_Floor.SetActive(true);
        Invoke("DisableFloor", m_FloorDisableTime);
    }

    private void DisableFloor()
    {
        m_Floor.SetActive(false);
        Invoke("ReEnableButton", m_TrapEnableCooldown);
    }

    private void ReEnableButton()
    {
        m_TrapCanBeEnabled = true;
    }
}
