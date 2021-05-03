﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    private GameManager GM;

    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    public TextMeshProUGUI m_PlayerHP;
    public TextMeshProUGUI m_centerText;

    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += updateScore;

        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;
    }

    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.WIN:
                m_centerText.text = "EL JUGADOR HA GANADO!";
                break;
            case GameState.GAME_OVER:
                m_centerText.text = "EL ENEMIGO HA GANADO!";
                break;
            case GameState.GAME:
                m_centerText.text = "";
                m_PlayerCorpses.enabled = true;
                m_EnemyCorpses.enabled = true;
                m_RemainingCorpses.enabled = true;
                m_PlayerHP.enabled = true;
                m_centerText.enabled = true;

                break;
            case GameState.MAP:
                m_centerText.text = "";
                m_PlayerCorpses.enabled = false;
                m_EnemyCorpses.enabled = false;
                m_RemainingCorpses.enabled = false;
                m_PlayerHP.enabled = false;
                m_centerText.enabled = false;
                break;
        }
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