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
        StartCoroutine(LateStart(1f));
        //onScoreChangedEvent += SpawnOrbs;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        spawnPosition = GameManager.Instance.GetEnemy().transform;
        SpawnOrbs();
    }
    public void SpawnOrbs()
    {
        switch(GameManager.Instance.m_ScoreManager.GetPlayerCorpses())
        {
            case 0:
            if(!CorpseOrb.activeSelf)
                {
                    CorpseOrb.SetActive(true);
                    CorpseOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                    CorpseOrb.GetComponent<NavMeshAgent>().enabled = true;
                    
                }
                break;
            case 3:
                //TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                if(!TrapOrb.activeSelf)
                {
                    TrapOrb.SetActive(true);
                    TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                    TrapOrb.GetComponent<NavMeshAgent>().enabled = true;
                }
                
                break;
            case 6:
                if(!HideOrb.activeSelf)
                {
                    HideOrb.SetActive(true);
                    HideOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                    HideOrb.GetComponent<NavMeshAgent>().enabled = true;
                    
                }
                break;
        }
    }
}
