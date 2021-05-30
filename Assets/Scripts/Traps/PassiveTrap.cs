using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTrap : MonoBehaviour
{
    [Header("Trap Settings")]
    public Material originalMaterial;
    public Material transparentMaterial;
    public Animator anim;
    public GameObject baseTrap;
    public BoxCollider trigger;
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
            if (col.gameObject.CompareTag("Enemy"))
            {
                trigger.enabled = false;
                //target.isStunned = true;
                DisableTrap();
                //HFSM_StunEnemy target = col.GetComponent<HFSM_StunEnemy>();
                if (col != null)
                {
                    anim.SetBool("Active", true);
                    //target.isStunned = true;
                    col.gameObject.GetComponent<Enemy>().GetStunned();
                    Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
                }

            }
            if (col.gameObject.CompareTag("Player"))
            {
                trigger.enabled = false;
                Debug.Log("Daño");
                DisableTrap();
                PlayerController player = col.gameObject.GetComponent<PlayerController>();
                if (player != null)
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
        baseTrap.tag = "PasiveTrapBase";
        m_TrapActive = false;
        GetComponent<MeshRenderer>().material = transparentMaterial;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = transparentMaterial;
        m_TrapCanBeEnabled = false;
        Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
    }

    public void EnableTrap()
    {
        Debug.Log("Reenabling");
        if (m_TrapCanBeEnabled)
        {
            trigger.enabled = true;
            gameObject.tag = "PasiveTrap";
            baseTrap.tag = "Untagged";
            m_TrapActive = true;
            GetComponent<MeshRenderer>().material = originalMaterial;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = originalMaterial;
            // gameObject.tag = "PasiveTrap";
        }
    }
    public void SetAnimToFalse()
    {
        anim.SetBool("Active", false);
    }

    public void RestoreTrapCooldown()
    {
        
        m_TrapCanBeEnabled = true;
    }
}
