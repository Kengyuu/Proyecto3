using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Events;

public class OrbSpawner : MonoBehaviour
{

    public List<GameObject> orbs;
    public GameObject CorpseOrb;
    public GameObject secondOrb;
    public GameObject thirdOrb;

    GameManager GM;

    public Image second;
    public Image third;
    HudController hud;
    private ScoreManager m_ScoreManager;

    private Transform spawnPosition;
    public bool showingOrb = true;

    private void Awake()
    {
        SelectOrbs();
        GM = GameManager.Instance;
    }
    void Start()
    {
        spawnPosition = GameManager.Instance.GetEnemy().transform;
        OrbEvents.current.spawnOrb += SpawnOrbs;
        OrbEvents.current.respawnOrb += RespawnOrb;
        OrbEvents.current.ManageOrbs();
        if (hud == null) hud = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        /* secondOrb.SetActive(false);
         thirdOrb.SetActive(false);*/

    }

    private void Update()
    {
        SpawnOrbs(m_ScoreManager.GetPlayerCorpses());
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
            if (!CorpseOrb.activeSelf && GM.GetEnemy().GetComponent<HFSM_StunEnemy>().canInvoke )
            {
                CorpseOrb.SetActive(true);
                
                CorpseOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                CorpseOrb.GetComponent<NavMeshAgent>().enabled = true;
               

            }
            secondOrb.SetActive(false);
            second.gameObject.SetActive(false);
            thirdOrb.SetActive(false);
            third.gameObject.SetActive(false);

        }


        else if (corpses >= 3 && corpses < 6)
        {
            
            if (!secondOrb.activeSelf && GM.GetEnemy().GetComponent<HFSM_StunEnemy>().canInvoke)
            {
                GM.GetEnemy().GetComponent<HFSM_StunEnemy>().isInvoking = true;

                secondOrb.SetActive(true);
                hud.objectiveAnim.SetTrigger(secondOrb.GetComponent<Orb_Blackboard>().triggerAnim);
                if (!showingOrb)
                {
                    second.gameObject.SetActive(true);
                }
                secondOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                secondOrb.GetComponent<NavMeshAgent>().enabled = true;
                
            }
            third.gameObject.SetActive(false);
            thirdOrb.SetActive(false);
        }


        if (corpses >= 6)
        {
            if (!thirdOrb.activeSelf && GM.GetEnemy().GetComponent<HFSM_StunEnemy>().canInvoke)
            {
                thirdOrb.SetActive(true);
                
                hud.objectiveAnim.SetTrigger(thirdOrb.GetComponent<Orb_Blackboard>().triggerAnim);
                if (!showingOrb) third.gameObject.SetActive(true);
                thirdOrb.GetComponent<NavMeshAgent>().Warp(spawnPosition.position);
                thirdOrb.GetComponent<NavMeshAgent>().enabled = true;
                GM.GetEnemy().GetComponent<HFSM_StunEnemy>().isInvoking = true;
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
        GameObject randomSecondOrb = orbs[Random.Range(0, orbs.Count)];
        orbs.Remove(randomSecondOrb);
        secondOrb = randomSecondOrb;
        second.sprite = secondOrb.GetComponent<Orb_Blackboard>().icon;

        GameObject randomThirdOrb = orbs[Random.Range(0, orbs.Count)];
        orbs.Remove(randomThirdOrb);
        thirdOrb = randomThirdOrb;
        third.sprite = thirdOrb.GetComponent<Orb_Blackboard>().icon;
    } 

    
}
