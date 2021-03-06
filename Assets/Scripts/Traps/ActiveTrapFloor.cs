using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrapFloor : MonoBehaviour
{
    [Header("Force to apply on PlayerCollision")]
    public float XForceImpulseDamage = 0.5f;
    public float YForceImpulseDamage = 0.5f;

    
    public float m_DamageCooldown = 0f;
    public float m_DamageMaxCooldown = 1f;
    [SerializeField] private bool m_AllowDamage = true;

    private void Update()
    {
        m_DamageCooldown -= Time.deltaTime;
        if(m_DamageCooldown <= 0f)
        {
            m_DamageCooldown = 0f;
            m_AllowDamage = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(m_DamageCooldown <= 0f)
        {
            if (col.CompareTag("Enemy"))
            {
                HFSM_StunEnemy target = col.GetComponent<HFSM_StunEnemy>();
                if (target != null)
                {
                    col.gameObject.GetComponent<Enemy>().GetStunned();
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }
            if (col.CompareTag("Player"))
            {
                PlayerController player = col.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(3, gameObject, XForceImpulseDamage, YForceImpulseDamage);
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }
            if (col.CompareTag("CorpseOrb"))
            {
                Orb_Blackboard target = col.GetComponent<Orb_Blackboard>();
                if (target != null)
                {
                    
                    target.TakeDamage(3);
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }
            if (col.CompareTag("HideOrb"))
            {
                Orb_Blackboard target = col.GetComponent<Orb_Blackboard>();
                if (target != null)
                {
                    target.TakeDamage(3);
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }
            if (col.CompareTag("TrapOrb"))
            {
                Orb_Blackboard target = col.GetComponent<Orb_Blackboard>();
                if (target != null)
                {
                    target.TakeDamage(3);
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }
            if (col.CompareTag("AttackOrb"))
            {
                Orb_Blackboard target = col.GetComponent<Orb_Blackboard>();
                if (target != null)
                {
                    target.TakeDamage(3);
                }
                m_DamageCooldown = m_DamageMaxCooldown;
                m_AllowDamage = false;
            }

        }
        
    }
}
