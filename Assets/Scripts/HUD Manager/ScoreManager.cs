using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IScoreManager
{
   
    [SerializeField] float m_PlayerCorpses;
    [SerializeField] float m_EnemyCorpses;
    [SerializeField] float m_RemainingCorpses;
    [SerializeField] float m_PlayerHP;

    public event ScoreChanged scoreChangedDelegate;
    void Awake()
    {
        DependencyInjector.AddDependency<IScoreManager>(this);
    }

    private void Start()
    {
        m_PlayerCorpses = 0f;
        m_EnemyCorpses = 0f;
        m_PlayerHP = 3f;
    }
    public void SetPlayerCorpses(float value)
    {
        this.m_PlayerCorpses = value;
        scoreChangedDelegate?.Invoke(this);
    }
    public void AddPlayerCorpse()
    {
        this.m_PlayerCorpses++;
        
        this.m_RemainingCorpses--;
        scoreChangedDelegate?.Invoke(this);

    }
    public void RemovePlayerCorpse()
    {
        this.m_PlayerCorpses--;
       
       this.m_RemainingCorpses++;
        scoreChangedDelegate?.Invoke(this);
    }
    public float GetPlayerCorpses() { return m_PlayerCorpses; }

    public void SetPlayerHP(float value)
    {
        this.m_PlayerHP = value;
        scoreChangedDelegate?.Invoke(this);
    }

    public float GetPlayerHP() { return m_PlayerHP; }

    //Enemy
    public void SetEnemyCorpses(float value)
    {
        this.m_EnemyCorpses = value;
        scoreChangedDelegate?.Invoke(this);
    }
    public void AddEnemyCorpse()
    {
        this.m_EnemyCorpses++;
        this.m_RemainingCorpses--;
        scoreChangedDelegate?.Invoke(this);
    }
    public void RemoveEnemyCorpse()
    {
        this.m_EnemyCorpses--;
        this.m_RemainingCorpses++;
        scoreChangedDelegate?.Invoke(this);
    }
    public float GetEnemyCorpses() { return m_EnemyCorpses; }


    //Remaining Corpses
    public void SetRemainingCorpses(float value)
    {
        this.m_RemainingCorpses = value;
        scoreChangedDelegate?.Invoke(this);
    }
  
    public float GetRemainingCorpses() { return m_RemainingCorpses; }
}

public interface IScoreManager
{
    //Player
    void SetPlayerCorpses(float f);
    void AddPlayerCorpse();
    void RemovePlayerCorpse();
    float GetPlayerCorpses();
    float GetPlayerHP();
    void SetPlayerHP(float f);

    //Enemy
    void SetEnemyCorpses(float f);
    void AddEnemyCorpse();
    void RemoveEnemyCorpse();
    float GetEnemyCorpses();


    //Remaining corpses
    void SetRemainingCorpses(float f);
    float GetRemainingCorpses();
    event ScoreChanged scoreChangedDelegate;
}
public delegate void ScoreChanged(IScoreManager scoreManager);