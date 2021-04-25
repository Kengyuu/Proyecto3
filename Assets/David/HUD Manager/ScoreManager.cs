using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IScoreManager
{
    [SerializeField] float m_PlayerCorpses;
    [SerializeField] float m_EnemyCorpses;

    public event ScoreChanged scoreChangedDelegate;
    void Awake()
    {
        DependencyInjector.AddDependency<IScoreManager>(this);
    }

    //Player
    public void addPlayerCorpse(float points)
    {
        this.m_PlayerCorpses += points;
        scoreChangedDelegate?.Invoke(this);
    }
    public float getPlayerCorpses() { return m_PlayerCorpses; }

    //Enemy
    public void addEnemyCorpse(float points)
    {
        this.m_EnemyCorpses += points;
        scoreChangedDelegate?.Invoke(this);
    }
    public float getEnemyCorpses() { return m_EnemyCorpses; }
}

public interface IScoreManager
{
    void addPlayerCorpse(float f);
    float getPlayerCorpses();
    void addEnemyCorpse(float f);
    float getEnemyCorpses();
    event ScoreChanged scoreChangedDelegate;
}
public delegate void ScoreChanged(IScoreManager scoreManager);