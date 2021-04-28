using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class OrbSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    //public event ScoreChanged onScoreChangedEvent;
    public Transform spawnPosition;

    public GameObject TrapOrb;

    public GameObject HideOrb;
    public GameObject CorpseOrb;

    void Start()
    {
        //onScoreChangedEvent += SpawnOrbs;
        SpawnOrbs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnOrbs()
    {
        switch(GameManager.Instance.m_ScoreManager.GetPlayerCorpses())
        {
            case 0:
            if(!CorpseOrb.activeSelf)
                {
                    CorpseOrb.SetActive(true);
                    CorpseOrb.transform.parent = null;
                    TrapOrb.GetComponent<NavMeshAgent>().enabled = true;
                    TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                }
                break;
            case 3:
                //TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                if(!TrapOrb.activeSelf)
                {
                    TrapOrb.SetActive(true);
                    TrapOrb.transform.parent = null;
                    TrapOrb.GetComponent<NavMeshAgent>().enabled = true;
                    TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                }
                
                break;
            case 6:
                if(!HideOrb.activeSelf)
                {
                    HideOrb.SetActive(true);
                    HideOrb.transform.parent = null;
                    TrapOrb.GetComponent<NavMeshAgent>().enabled = true;
                    TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                }
                break;
        }
    }
}
