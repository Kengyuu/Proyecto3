using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    private GameManager GM;

    
    


    [Header("Canvas Game")]
    public GameObject m_CanvasGame;
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    public TextMeshProUGUI m_PlayerHP;

    [Header("Canvas Win/Lose")]
    public GameObject m_CanvasEnd;
    public TextMeshProUGUI m_centerText;

    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += updateScore;

        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;

        m_CanvasEnd.SetActive(false);
        m_CanvasGame.SetActive(true);
    }


    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.WIN:
                m_CanvasGame.SetActive(false);
                m_CanvasEnd.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                m_centerText.text = "EL JUGADOR HA GANADO!";
                break;
            case GameState.GAME_OVER:
                m_CanvasGame.SetActive(false);
                m_CanvasEnd.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                m_centerText.text = "EL ENEMIGO HA GANADO!";
                break;
            case GameState.GAME:
                /*m_centerText.text = "";
                m_PlayerCorpses.enabled = true;
                m_EnemyCorpses.enabled = true;
                m_RemainingCorpses.enabled = true;
                m_PlayerHP.enabled = true;
                m_centerText.enabled = true;*/
                m_CanvasGame.SetActive(true);
                break;
            case GameState.MAP:
                m_CanvasGame.SetActive(false);
                /*m_centerText.text = "";
                m_PlayerCorpses.enabled = false;
                m_EnemyCorpses.enabled = false;
                m_RemainingCorpses.enabled = false;
                m_PlayerHP.enabled = false;
                m_centerText.enabled = false;*/
                break;
        }
    }

    private void OnDestroy()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate -= updateScore;
        GM.OnStateChange -= StateChanged;
    }
    public void updateScore(IScoreManager scoreManager)
    {
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
        m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";
        GM.UpdateModifiers();
    }


    public void RestartGame()
    {
        Initiate.Fade("MAIN_MENU", Color.black, 3f);
    }
}