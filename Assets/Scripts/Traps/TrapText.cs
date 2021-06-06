using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrapText : MonoBehaviour
{
    public TextMeshProUGUI m_Text;
    public Canvas m_Canvas;

    void Update()
    {
        if (GetComponentInParent<Outline>().enabled)
        {
            //Debug.Log("OutLine ENABLED");
            if (!m_Canvas.enabled)
            {
                //Debug.Log("EL CANVAS ESTABA DESACTIVADO, LO ACTIVO");
                m_Canvas.enabled = true;
            }
                
                
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
            m_Text.transform.position = namePos;
        }
        else
        {
            //Debug.Log("OutLine DISABLED");
            m_Canvas.enabled = false;
        }
    }
}
