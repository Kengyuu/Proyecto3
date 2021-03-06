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

    Enemy_BLACKBOARD blackboard;

    ScoreManager m_ScoreManager;
    private HudController M_HudController;
    GameManager GM;

    [Header("FMOD Events")]
    public string restoreLifeEvent;
    public string stunnedEvent;

    void Start()
    {
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        GM = GameManager.Instance;
        maxLife = 3;
        m_Life = maxLife;
        SpawnWeakPoints();
    }

    public override void TakeDamage(int dmg)
    {
        m_Life -= dmg;
        
        if(m_Life <= 0)
        {
            m_Life = 0;
            
            
            if(m_ScoreManager.GetPlayerCorpses() >= GM.m_CorpseObjective)
            {
                Die();
                return;
            }
            else
            {
                GetStunned();
            }
        }
    }

   

    public override void GetStunned()
    { 
        base.GetStunned();
        SoundManager.Instance.PlayEvent(stunnedEvent, transform);
        if (m_ScoreManager.GetEnemyCorpses() > 0)
        {
            int lostEnemyCorpses = 1;
            for (int i = 0; i < lostEnemyCorpses; i++)
            {
                m_ScoreManager.RemoveEnemyCorpse();
                M_HudController.UpdateRemoveCorpses(gameObject);
            }
            GM.GetGameObjectSpawner().SpawnBodys(lostEnemyCorpses,gameObject);
        }
        GetComponent<HFSM_StunEnemy>().isStunned = true;
        
    }

    public void Die()
    {
        GetComponent<HFSM_StunEnemy>().isDead = true;
    }

    IEnumerator WaitForDead()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    protected override void RestoreLife()
    {
        base.RestoreLife();
        if (m_Life <= 0) SoundManager.Instance.PlayEvent(restoreLifeEvent, transform);
        m_Life = maxLife;
    }

    public void SpawnWeakPoints()
    {
        int spawnQuantity = maxLife - m_Life;
        
        for (int i = 0; i < spawnQuantity; i++)
        {
            weakPoint[i].SetActive(true);
            weakPoint[i].GetComponent<WeakPoint>().particles.SetActive(true);
        }
        RestoreLife();
    }

    public override int GetLife()
    {
        return m_Life;
    }
}
