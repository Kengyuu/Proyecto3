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

    void Start()
    {
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        GM = GameManager.Instance;
        maxLife = 3;
        
        SpawnWeakPoints();
        m_Life = maxLife;
    }

    public override void TakeDamage(int dmg)
    {
        
        m_Life -= dmg;
        
        if(m_Life <= 0)
        {
            m_Life = 0;
            
            
            if(m_ScoreManager.GetPlayerCorpses() >= 10)
            {
                Die();
                //GM.SetGameState(GameState.WIN);
                //Cursor.lockState = CursorLockMode.None; //TBD
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
        if(m_ScoreManager.GetEnemyCorpses() > 0)
        {
            int lostEnemyCorpses = Mathf.Max(1, Mathf.RoundToInt(m_ScoreManager.GetEnemyCorpses()/ 3));
            for (int i = 0; i < lostEnemyCorpses; i++)
            {
                m_ScoreManager.RemoveEnemyCorpse();
                M_HudController.UpdateRemoveCorpses(gameObject);
            }
            
           
            
            //Debug.Log(m_ScoreManager.GetRemainingCorpses());
            //Debug.Log(lostEnemyCorpses);
            GM.GetGameObjectSpawner().SpawnBodys(lostEnemyCorpses,gameObject);
        }
        GetComponent<HFSM_StunEnemy>().isStunned = true;
        
    }

    public void Die()
    {
        GetComponent<HFSM_StunEnemy>().isDead = true;
        
        StartCoroutine(WaitForDead());

    }

    IEnumerator WaitForDead()
    {
        GM.SetGameState(GameState.WIN);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    protected override void RestoreLife()
    {
        base.RestoreLife();
        m_Life = maxLife;
    }

    public void SpawnWeakPoints()
    {
        int spawnQuantity = maxLife - m_Life;
        
        for (int i = 0; i < spawnQuantity; i++)
        {
            weakPoint[i].SetActive(true);
        }
        RestoreLife();
    }

    public override int GetLife()
    {
        //Debug.Log(m_Life + " fui llamado");
        return m_Life;
    }
}
