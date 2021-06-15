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
    public GameObject parentTrap;
    public BoxCollider trigger;
    public float m_TrapEnableCooldown = 10f;
    

    [Header("Force to apply on PlayerCollision")]
    public float XForceImpulseDamage = 2f;
    public float YForceImpulseDamage = 8f;

    [Header("Debug")]
    [SerializeField] public bool m_TrapActive = true;
    [SerializeField] public bool m_TrapCanBeEnabled;

    [Header("FMOD Events")]

    public string activateSpikeEvent;

    
    public bool m_CooldownStarted;

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
                DisableTrap();
                
                anim.SetBool("Active", true);
                col.gameObject.GetComponent<Enemy>().GetStunned();
                Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
            }
            if (col.gameObject.CompareTag("Player"))
            {
                
                trigger.enabled = false;
                DisableTrap();
                PlayerController player = col.gameObject.GetComponent<PlayerController>();
                
                player.TakeDamage(3, gameObject, XForceImpulseDamage, YForceImpulseDamage);
                Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
                

            }
        }
    }
    public void PlayActivateSound()
    {
        SoundManager.Instance.PlaySound(activateSpikeEvent, transform.position);
    }
 
    public void DisableTrap()
    {
        gameObject.tag = "TrapDeactivated";
        baseTrap.tag = "PasiveTrapBase"; 
        parentTrap.tag = "TrapDeactivated";
        trigger.enabled = false;
        m_TrapActive = false;
        GetComponent<MeshRenderer>().material = transparentMaterial;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = transparentMaterial;
        m_TrapCanBeEnabled = false;
        m_CooldownStarted = true;
        Invoke("RestoreTrapCooldown", m_TrapEnableCooldown);
        
    }

    public void EnableTrap()
    {
        if (m_TrapCanBeEnabled)
        {
            trigger.enabled = true;
            gameObject.tag = "Untagged";
            parentTrap.tag = "PasiveTrap";
            baseTrap.tag = "Untagged";
            m_TrapActive = true;
            GetComponent<MeshRenderer>().material = originalMaterial;
            transform.GetChild(0).GetComponent<MeshRenderer>().material = originalMaterial;
        }
    }
    public void SetAnimToFalse()
    {
        anim.SetBool("Active", false);
    }

    public void RestoreTrapCooldown()
    {
        m_CooldownStarted = false;
        m_TrapCanBeEnabled = true;
    }
}
