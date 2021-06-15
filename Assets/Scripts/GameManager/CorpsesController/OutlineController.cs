using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Outline m_Outline;
    public float m_MaxViewDistance = 10;
    private bool m_OutlineStatus;

    private PlayerShoot m_PlayerShoot;

    public List<Outline> m_GOChecked;

    private void Start()
    {
        m_PlayerShoot = GetComponent<PlayerShoot>();
    }


    private void FixedUpdate()
    {
        CheckView();
    }



    private bool CheckView()
    {
        RaycastHit hit;
        var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        if (Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, m_MaxViewDistance))
        {
            if ((hit.transform.CompareTag("Corpse") || hit.transform.CompareTag("CorpseTutorial")) && Vector3.Distance(transform.position, hit.transform.position) < m_PlayerShoot.m_CorpseDetectionDistance)
            {
                foreach(Transform child in hit.transform)
                {
                    if (child.CompareTag("CorpseMesh"))
                    {
                        m_Outline = child.GetComponent<Outline>();
                        m_Outline.enabled = true;
                        if(!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                        return true;
                    }
                }
            }

            if ((hit.collider.CompareTag("WeakPoint") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_WeakPointDetectionDistance))
            {
                m_Outline = hit.collider.gameObject.GetComponent<Outline>();
                m_Outline.enabled = true;
                if (!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                return true;
            }

           


            if (hit.collider.gameObject.CompareTag("PasiveTrapBase") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_TrapDetectionDistance &&
                hit.collider.gameObject.transform.parent.transform.GetComponentInChildren<PassiveTrap>().m_TrapCanBeEnabled)
            {
                m_Outline = hit.transform.gameObject.GetComponent<Outline>();
                m_Outline.enabled = true;
                if (!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                return true;
            }

            if (hit.collider.gameObject.CompareTag("ActiveTrap") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_ButtonDetectionDistance &&
                hit.collider.gameObject.GetComponent<ActiveTrap>().m_TrapCanBeEnabled)
            {
                m_Outline = hit.transform.gameObject.GetComponent<Outline>();
                m_Outline.enabled = true;
                if (!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                return true;
            }

        }


        if (m_GOChecked.Count > 0)
        {
            foreach(Outline obj in m_GOChecked)
            {
                obj.enabled = false;
                
            }
            m_Outline = null;

            m_GOChecked.Clear();
        }



        return false;
    }

}
