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
        spawnPosition = GameManager.Instance.GetEnemy().transform;
        OrbEvents.current.spawnOrb += SpawnOrbs;

        OrbEvents.current.ManageOrbs();

        

    }

   /* private void OnEnable()
    {
        spawnPosition = GameManager.Instance.GetEnemy().transform;
    }*/

    private void OnDestroy()
    {
        OrbEvents.current.spawnOrb -= SpawnOrbs;
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
            TrapOrb.SetActive(false);
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
        /*switch(corpses)
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
        }*/
    }
}
