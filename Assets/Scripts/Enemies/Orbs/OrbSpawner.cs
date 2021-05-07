using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class OrbSpawner : MonoBehaviour
{

    public List<GameObject> orbs;
    public GameObject CorpseOrb;
    public GameObject secondOrb;
    public GameObject thirdOrb;

    private Transform spawnPosition;

    private void Awake()
    {
        SelectOrbs();
    }
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
            secondOrb.SetActive(false);
            thirdOrb.SetActive(false);

        }


        else if (corpses >= 3 && corpses < 6)
        {
            if (!secondOrb.activeSelf)
            {
                secondOrb.SetActive(true);
                secondOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                secondOrb.GetComponent<NavMeshAgent>().enabled = true;

            }

            thirdOrb.SetActive(false);
        }


        if (corpses >= 6)
        {
            if (!thirdOrb.activeSelf)
            {
                thirdOrb.SetActive(true);
                thirdOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                thirdOrb.GetComponent<NavMeshAgent>().enabled = true;

            }

        }
       
    }

    public void RespawnOrb(GameObject orb)
    {
        orb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
        orb.GetComponent<Orb_Blackboard>().SetOrbHealth(orb.GetComponent<Orb_Blackboard>().m_maxLife);
        orb.SetActive(true);
    }

    public void SelectOrbs()
    {
       /*GameObject randomSecondOrb = orbs[Random.Range(1, orbs.Count)];
        orbs.Remove(randomSecondOrb);
        secondOrb = randomSecondOrb;

        GameObject randomThirdOrb = orbs[Random.Range(1, orbs.Count)];
        orbs.Remove(randomThirdOrb);
        thirdOrb = randomThirdOrb;*/
    } 

    
}
