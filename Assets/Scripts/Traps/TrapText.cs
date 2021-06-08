using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapText : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public Canvas m_Canvas;

    private Camera m_Camera;


    private void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        if (GetComponentInParent<Outline>().enabled)
        {
            //Debug.Log("OutLine ENABLED");
            if (!m_Canvas.enabled)
            {
                //Debug.Log("EL CANVAS ESTABA DESACTIVADO, LO ACTIVO");
                m_Canvas.enabled = true;
                transform.rotation = Quaternion.LookRotation(m_Camera.transform.forward, m_Camera.transform.up);
            }

            Quaternion newRotation = Quaternion.LookRotation(m_Camera.transform.forward, m_Camera.transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 7.0f);
        }
        else
        {
            //Debug.Log("OutLine DISABLED");
            m_Canvas.enabled = false;
        }
    }
}
