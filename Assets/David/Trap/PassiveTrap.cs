using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTrap : TrapController
{
    

    void Start()
    {
        m_TrapActive = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (m_TrapActive)
        {
            m_TrapActive = false;
            GetComponent<MeshRenderer>().enabled = false;
            if (col.CompareTag("Enemy"))
            {
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
                    GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
