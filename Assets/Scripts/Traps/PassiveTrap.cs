using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    public Material originalMaterial;
    public Material transparentMaterial;
    public float m_TrapEnableCooldown = 10f;
    

    [Header("Force to apply on PlayerCollision")]
    public float XForceImpulseDamage = 2f;
    public float YForceImpulseDamage = 8f;

    [Header("Debug")]
    [SerializeField] private bool m_TrapActive = true;
    [SerializeField] private bool m_TrapCanBeEnabled;

    private void Start()
    {
        m_TrapActive = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (m_TrapActive)
        {
            if (col.CompareTag("Enemy"))
            {
                DisableTrap();
                HFSM_StunEnemy target = col.GetComponent<HFSM_StunEnemy>();
                if (target != null)
                {
                    target.isStunned = true;
                    Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
                }
               
            }
            if (col.CompareTag("Player"))
            {
                DisableTrap();
                PlayerController player = col.GetComponent<PlayerController>();
                if(player != null)
                {
                    player.TakeDamage(3, gameObject, XForceImpulseDamage, YForceImpulseDamage);
                    Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
                }
                
            }
        }
    }

    public void DisableTrap()
    {
        gameObject.tag = "TrapDeactivated";
        m_TrapActive = false;
        GetComponent<MeshRenderer>().material = transparentMaterial;
        m_TrapCanBeEnabled = false;
        Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
    }

    public void EnableTrap()
    {
        Debug.Log("Reenabling");
        if (m_TrapCanBeEnabled)
        {
            
            m_TrapActive = true;
            GetComponent<MeshRenderer>().material = originalMaterial;
           // gameObject.tag = "PasiveTrap";
        }
    }

    public void RestoreTrapCooldown()
    {
        
        m_TrapCanBeEnabled = true;
    }
}
