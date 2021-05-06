using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class OrbSpawner : MonoBehaviour
{

    
    public Transform spawnPosition;

    public GameObject TrapOrb;

    public GameObject HideOrb;
    public GameObject CorpseOrb;

    void Start()
    {
        spawnPosition = GameManager.Instance.GetEnemy().transform;
        OrbEvents.current.spawnOrb += SpawnOrbs;
        OrbEvents.current.respawnOrb += RespawnOrb;
        OrbEvents.current.ManageOrbs();

    }
    private void OnDestroy()
    {
        OrbEvents.current.spawnOrb -= SpawnOrbs;
        OrbEvents.current.respawnOrb -= RespawnOrb;
    }

    public void SpawnOrbs(float corpses)
    {

        if (corpses >= 0 && corpses < 3)
        {
            if (!CorpseOrb.activeSelf)
            {
                CorpseOrb.SetActive(true);
                CorpseOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                CorpseOrb.GetComponent<NavMeshAgent>().enabled = true;


            }
            //TrapOrb.SetActive(false);
            HideOrb.SetActive(false);

        }


        else if (corpses >= 3 && corpses < 6)
        {
            if (!TrapOrb.activeSelf)
            {
                TrapOrb.SetActive(true);
                TrapOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                TrapOrb.GetComponent<NavMeshAgent>().enabled = true;

            }

            HideOrb.SetActive(false);
        }


        if (corpses >= 6)
        {
            if (!HideOrb.activeSelf)
            {
                HideOrb.SetActive(true);
                HideOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                HideOrb.GetComponent<NavMeshAgent>().enabled = true;

            }

        }
       
    }

    public void RespawnOrb(GameObject orb)
    {
        orb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
        orb.GetComponent<Orb_Blackboard>().SetOrbHealth(orb.GetComponent<Orb_Blackboard>().m_maxLife);
        orb.SetActive(true);
    }


    
}
