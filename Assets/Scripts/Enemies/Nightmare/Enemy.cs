using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entities
{
    public int weakPointsMax = 3;
    public int currentWeakPoints = 0;
    public GameObject[] weakPoint;
    List<int> spawnersUsed = new List<int>();

    ScoreManager m_ScoreManager;

    GameManager GM;

    void Start()
    {
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        maxLife = 3;
        
        SpawnWeakPoints();
        m_Life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void TakeDamage(int dmg)
    {
        //base.TakeDamage(dmg);
        m_Life -= dmg;
        //Debug.Log("Au " + m_Life);
        //spawnersUsed.Remove(wp.GetComponent<WeakPoint>().spawnPosition);
        if(m_Life <= 0)
        {
            m_Life = 0;
            GetStunned();
            //SpawnWeakPoints();
            //RestoreLife();
            if(m_ScoreManager.GetPlayerCorpses() >= 10)
            {
                GM.SetGameState(GameState.WIN);
                return;
            }
        }
    }

    public override void GetStunned()
    {
        //GetComponent<NavMeshAgent>().isStopped = true;
        base.GetStunned();
        if(m_ScoreManager.GetEnemyCorpses() > 0)
        {
            int lostEnemyCorpses = Mathf.Max(1, Mathf.RoundToInt(m_ScoreManager.GetEnemyCorpses()/ 3));
            m_ScoreManager.SetEnemyCorpses(m_ScoreManager.GetEnemyCorpses() - lostEnemyCorpses);
           
            //m_ScoreManager.SetRemainingCorpses(m_ScoreManager.GetRemainingCorpses() + lostEnemyCorpses);
            Debug.Log(m_ScoreManager.GetRemainingCorpses());
            Debug.Log(lostEnemyCorpses);
            GM.GetGameObjectSpawner().SpawnBodys(lostEnemyCorpses);
        }
        GetComponent<HFSM_StunEnemy>().isStunned = true;
        
    }



    protected override void RestoreLife()
    {
        base.RestoreLife();
        m_Life = maxLife;
    }

    public void SpawnWeakPoints()
    {
        int spawnQuantity = maxLife - m_Life;
        //Debug.Log("Son: " +  spawnQuantity);
        for (int i = 0; i < spawnQuantity; i++)
        {
            weakPoint[i].SetActive(true);
            //Debug.Log("Spawned");
        }
        RestoreLife();
    }

    public override int GetLife()
    {
        Debug.Log(m_Life + " fui llamado");
        return m_Life;
    }
}
