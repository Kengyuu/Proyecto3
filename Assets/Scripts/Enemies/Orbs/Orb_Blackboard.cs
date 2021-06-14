using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Orb_Blackboard : MonoBehaviour
{
    [Header("General Attributes")]
    public int m_Life;
    public int m_maxLife;
    public float respawnTime;
    public NavMeshAgent navMesh;
    public Sprite icon;
    public string triggerAnim;
    public ParticleSystem partSystem;
    public EnemyBehaviours behaviours;
    public GameObject orbSpawner;
    public float cooldownToReappear = 4;

    [Header("CorpseSearcher")]
    public float closeEnoughCorpseRadius = 3;
    public float cooldownToGrabCorpse = 2;
    public float corpseDetectionRadius = 20;
    public GameObject orbCorpseStored;
    public GameObject lastCorpseSeen;
   

    [Header("TrapSearcher")]
    public float cooldownToDeactivateTrap = 3f;
    public float closeEnoughTrapRadius = 4f;
    public float trapDetectionRadius = 10f;
    public float playerDetectionRadius = 8;

    [Header("AttackerOrb")]
    public float maxAttackDistance = 10;
    public float XForceImpulseDamage = 5f;
    public float YForceImpulseDamage = 5f;
    public float angleDetectionPlayer = 90;


    [Header("FMOD Events")]
    public string hurtEvent;
    public string disappearEvent;

    private void Start()
    {
        behaviours = GetComponent<EnemyBehaviours>();
    }
    public void TakeDamage(int damage)
    {
        m_Life -= damage;
        if (m_Life <= 0)
        {
            SoundManager.Instance.PlaySound(disappearEvent, transform.position);
        }
        else SoundManager.Instance.PlaySound(hurtEvent, transform.position);
        if (gameObject.tag != "HideOrb")
        {
            OrbAuraLight();
        }
            
       
    }


    public float GetOrbHealth()
    {
        return m_Life;
    }

    public void SetOrbHealth(int health)
    {
        m_Life = health;
        if (gameObject.tag != "HideOrb")
        {
            OrbAuraLight();
        }
        
    }

    public void OrbAuraLight()
    {
        ParticleSystem.MainModule main = partSystem.main;

        //colf.color = new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, (GetOrbHealth()/m_maxLife) * 0.7f );
        main.maxParticles =  (int)Mathf.Round(GetOrbHealth()/m_maxLife) * main.maxParticles;
    }
}



