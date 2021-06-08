using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveTrapText : MonoBehaviour
{
    //Helpers
    public Canvas m_Canvas;

    private Camera m_Camera;
    PlayerShoot m_Player;

    //public RawImage m_Image;
    [Header("Left Button tooltip")]
    public RawImage m_MouseButton;
    public ActiveTrap m_Controller;
    public float m_DistanceOffset;

    [Header("Cooldown tooltip")]
    public Image m_Cooldown;
    public bool m_CooldownActive;

    private void Start()
    {
        m_Camera = Camera.main;
        m_Player = GameManager.Instance.GetPlayer().GetComponent<PlayerShoot>();
        m_Cooldown.enabled = false;
    }

    void Update()
    {
        Quaternion newRotation = Quaternion.LookRotation(m_Camera.transform.forward, m_Camera.transform.up);
        m_Canvas.transform.rotation = Quaternion.Slerp(m_Canvas.transform.rotation, newRotation, Time.deltaTime * 7.0f);


        float l_Distance = Vector3.Distance(transform.position, m_Player.gameObject.transform.position);
        if (m_Controller.m_TrapCanBeEnabled && l_Distance < (m_Player.m_TrapDetectionDistance + m_DistanceOffset))
        {
            //Debug.Log("OutLine ENABLED");
            if (!m_MouseButton.enabled)
            {
                m_MouseButton.enabled = true;
            }
        }
        else
        {
            //Debug.Log("OutLine DISABLED");
            m_MouseButton.enabled = false;
        }


        if (m_Controller.m_CooldownStarted && !m_CooldownActive)
        {
            m_CooldownActive = true;
            m_Cooldown.fillAmount = 1f;
            m_Cooldown.enabled = true;
        }
        else
        {
            if (m_Cooldown.fillAmount == 0)
            {
                m_CooldownActive = false;
                m_Cooldown.enabled = false;
                return;
            }
            m_Cooldown.fillAmount -= 1 / m_Controller.m_TrapEnableCooldown * Time.deltaTime;
        }
    }


}
