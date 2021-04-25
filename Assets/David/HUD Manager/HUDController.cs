using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;

    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += updateScore;
    }
    private void OnDestroy()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate -= updateScore;
    }
    public void updateScore(IScoreManager scoreManager)
    {
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.getPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.getEnemyCorpses().ToString("0");
    }
}