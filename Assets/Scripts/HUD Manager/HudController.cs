using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private GameManager GM;

    public Image[] healthIcons;
    public Image Objective;
    public Sprite objectiveNightmare;
    public Sprite objectiveCorpse;


    [Header("Canvas Game")]
    public GameObject m_CanvasGame;
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    //public TextMeshProUGUI m_PlayerHP;

    [Header("Canvas Win/Lose")]
    public GameObject m_CanvasEnd;
    public TextMeshProUGUI m_centerText;

    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += UpdateHUD;

       

        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;

        m_CanvasEnd.SetActive(false);
        m_CanvasGame.SetActive(true);

        ScoreManager scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();


        //START DE HUD
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
        UpdateHealth(scoreManager.GetPlayerHP());
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());
        // m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";


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
        .scoreChangedDelegate -= UpdateHUD;
        GM.OnStateChange -= StateChanged;
    }
    public void UpdateHUD(IScoreManager scoreManager)
    {
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
        //m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";
        GM.UpdateModifiers();
        UpdateHealth(scoreManager.GetPlayerHP());
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());

    }

    public void UpdateHealth(float health)
    {
        switch (health)
        {
            case 3:
                foreach (Image image in healthIcons)
                {
                    image.gameObject.SetActive(true);
                }
                break;
            case 2:
                healthIcons[0].gameObject.SetActive(false);
                healthIcons[1].gameObject.SetActive(true);
                healthIcons[2].gameObject.SetActive(true);
                break;
            case 1:
                healthIcons[0].gameObject.SetActive(false);
                healthIcons[1].gameObject.SetActive(false);
                healthIcons[2].gameObject.SetActive(true);
                break;

            case 0:
                foreach (Image image in healthIcons)
                {
                    image.gameObject.SetActive(false);
                }
                break;

        }
    }

    public void UpdateObjective(float playerCorpses, float enemyCorpses)
    {
        if (playerCorpses >= 10 || enemyCorpses >= 10)
        {
            Objective.sprite = objectiveNightmare;
        }
        else Objective.sprite = objectiveCorpse;
    }


    public void RestartGame()
    {
        Initiate.Fade("MAIN_MENU", Color.black, 3f);
    }
}