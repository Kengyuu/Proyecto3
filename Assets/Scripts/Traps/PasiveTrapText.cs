using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasiveTrapText : MonoBehaviour
{
    //public TextMeshProUGUI m_Text;
    public Canvas m_Canvas;

    public TextMeshProUGUI m_Text;

    PlayerShoot m_Player;
    private Camera m_Camera;

    public PassiveTrap m_Controller;

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
        //if (GetComponentInParent<Outline>().enabled)
        {
            //Debug.Log("OutLine ENABLED");
            if (!m_Text.enabled)
            {
                //Debug.Log("EL CANVAS ESTABA DESACTIVADO, LO ACTIVO");
                m_Text.enabled = true;
            }

            
        }
        else
        {
            //Debug.Log("OutLine DISABLED");
            m_Text.enabled = false;
        }

        if(m_Controller.m_TrapActive) m_Text.enabled = false;

        if (m_Controller.m_CooldownStarted && !m_CooldownActive)
        {
            Debug.Log("ENTRO AQUI");
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
