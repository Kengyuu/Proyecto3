using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    public TextMeshProUGUI m_PlayerHP;
    public TextMeshProUGUI m_centerText;

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
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
        m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";
        
    }
}