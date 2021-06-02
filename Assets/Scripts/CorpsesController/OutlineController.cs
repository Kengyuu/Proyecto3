using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Outline m_Outline;
    public float m_MaxViewDistance = 10;
    private bool m_OutlineStatus;
    //LayerMask m_OutlineMask;

    private PlayerShoot m_PlayerShoot;

    public List<Outline> m_GOChecked;

    private void Start()
    {
        m_PlayerShoot = GetComponent<PlayerShoot>();
    }


    private void FixedUpdate()
    {

        /*m_OutlineStatus = CheckView();

        if (!m_OutlineStatus)
        {
            if(m_Outline != null)
            {
                m_Outline.enabled = false;
                m_Outline = null;
            }
            
        }*/

        CheckView();
    }



    private bool CheckView()
    {
        RaycastHit hit;
        var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        if (Physics.Raycast(cameraCenter, Camera.main.transform.forward, out hit, m_MaxViewDistance))
        {
            if (hit.transform.CompareTag("Corpse") && Vector3.Distance(transform.position, hit.transform.position) < m_PlayerShoot.m_CorpseDetectionDistance)
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

            if (hit.collider.CompareTag("WeakPoint") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_WeakPointDetectionDistance)
            {
                m_Outline = hit.collider.gameObject.GetComponent<Outline>();
                m_Outline.enabled = true;
                if (!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                return true;
            }

           


            if (hit.collider.gameObject.CompareTag("PasiveTrapBase") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_TrapDetectionDistance &&
                hit.collider.gameObject.transform.parent.transform.GetComponentInChildren<PassiveTrap>().m_TrapCanBeEnabled)
            {
                Debug.Log("HOLA ENTRO AQUI DENTRO");
                m_Outline = hit.transform.gameObject.GetComponent<Outline>();
                m_Outline.enabled = true;
                if (!m_GOChecked.Contains(m_Outline)) m_GOChecked.Add(m_Outline);
                return true;
            }

            if (hit.collider.gameObject.CompareTag("ActiveTrap") && Vector3.Distance(transform.position, hit.collider.transform.position) < m_PlayerShoot.m_ButtonDetectionDistance &&
                hit.collider.gameObject.GetComponent<ActiveTrap>().m_TrapCanBeEnabled)
            {
                //Debug.Log("HOLA ENTRO AQUI DENTRO");
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



    /*private bool IsInView(GameObject origin, GameObject toCheck)
    {
        Vector3 pointOnScreen = Camera.main.WorldToScreenPoint(toCheck.GetComponentInChildren<Renderer>().bounds.center);

        //Is in front
        if (pointOnScreen.z < 0)
        {
            //Debug.Log("Behind: " + toCheck.name);
            return false;
        }

        //Is in FOV
        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
                (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
        {
            //Debug.Log("OutOfBounds: " + toCheck.name);
            return false;
        }

        RaycastHit hit;
        Vector3 heading = toCheck.transform.position - origin.transform.position;
        Vector3 direction = heading.normalized;// / heading.magnitude;

        if (Physics.Linecast(Camera.main.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, out hit))
        {
            if (hit.transform.name != toCheck.name)
            {

                *//* Debug.DrawLine(Camera.main.transform.position, toCheck.GetComponentInChildren<Renderer>().bounds.center, Color.red);
                 Debug.LogError(toCheck.name + " occluded by " + hit.transform.name);*//*

                //Debug.Log(toCheck.name + " occluded by " + hit.transform.name);
                return false;
            }
        }
        return true;
    }*/
}
