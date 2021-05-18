using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orb_Blackboard : MonoBehaviour
{
    [Header("General Attributes")]
    public int m_Life;
    public int m_maxLife;
    public float respawnTime;
    public NavMeshAgent navMesh;

    public ParticleSystem particleSystem;

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
    public float playerDetectionRadius = 10;

    [Header("AttackerOrb")]
    public float maxAttackDistance = 10;
    public float XForceImpulseDamage = 5f;
    public float YForceImpulseDamage = 5f;
    public float angleDetectionPlayer = 90;


    public void TakeDamage(int damage)
    {
        m_Life -= damage;
    }


    public float GetOrbHealth()
    {
        return m_Life;
    }

    public void SetOrbHealth(int health)
    {
        m_Life = health;
    }

    public void OrbAuraLight()
    {

    }
}



