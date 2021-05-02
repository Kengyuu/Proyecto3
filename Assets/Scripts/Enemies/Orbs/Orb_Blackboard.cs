using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Orb_Blackboard : MonoBehaviour
{
    public int m_Life;
    public int m_maxLife;
    public float m_stunTime;
    public NavMeshAgent navMesh;

    [Header("CorpseSearcher")]
    public float closeEnoughCorpseRadius = 3;
    public float cooldownToGrabCorpse = 2;
    public float corpseDetectionRadius = 20;
    public GameObject orbCorpseStored;
    public GameObject lastCorpseSeen;
    public GameObject waypointsList;

    [Header("TrapSearcher")]
    public float cooldownToDeactivateTrap = 3f;
    public float closeEnoughTrapRadius = 4f;
    public float playerDetectionRadius = 10;

    private void Start()
    {
        waypointsList = GameObject.FindGameObjectWithTag("SpawnersContainer");
    }
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



}
