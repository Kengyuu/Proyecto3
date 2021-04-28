using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTrap : TrapController
{
    
    public Material originalMaterial;
    public Material transparentMaterial;
    void Start()
    {
        m_TrapActive = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (m_TrapActive)
        {
            
            //GetComponent<MeshRenderer>().enabled = false;
            
            if (col.CompareTag("Enemy"))
            {
                GetComponent<MeshRenderer>().material = transparentMaterial;
                m_TrapActive = false;
                FSM_EnemyPriority target = col.GetComponent<FSM_EnemyPriority>();
                if (target != null)
                {
                    Debug.Log("Enemigo estuneado por TRAMPA");
                    target.GetStunned();
                    m_TrapActive = true;
                }
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (!m_TrapActive)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Player dentro de la trampa");
                PlayerController player = col.GetComponent<PlayerController>();
                if (Input.GetKeyDown(player.m_TrapInteractKeyCode)){
                    Debug.Log("Trap re-enabled by player");
                    m_TrapActive = true;
                    GetComponent<MeshRenderer>().material = originalMaterial;
                    //GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }

    public void SetTrapActive(bool active)
    {
        m_TrapActive = active;
    }

    public bool GetTrapActive()
    {
        return m_TrapActive;
    }
}
