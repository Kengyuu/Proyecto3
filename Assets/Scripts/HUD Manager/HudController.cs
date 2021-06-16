using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    private GameManager GM;
    private ScoreManager m_ScoreManager;

    public Image[] healthIcons;
    public Image[] corpseTracker;
    public Image Objective;
    public Sprite objectiveNightmare;
    public Sprite objectiveCorpse;

    public GameObject[] totalCorpses;
    public List<GameObject> activeCorpses = new List<GameObject>();
    public Image[] corpseSprites;
    public Sprite playerCorpse;
    public Sprite enemyCorpse;
    public Sprite neutralCorpse;
    public Sprite fullLife;
    public OrbSpawner orbSpawner;

    [Header("Animation")]
    public Animator objectiveAnim;

    [Header("GAME")]
    
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    
    public bool hasMoved = false;
    public bool hasRun = false;
    public bool hasDashed = false;
    public bool hasShot = false;
    public bool hasShotEnemy = false;
    public bool hasRepaired = false;
 
    public bool triggerShot = false;
    public bool triggerShotTrap = false;
    public bool triggerShotTrapD = false;
    public bool triggerShotEnemy = false;
    float cooldownMovement = 5;
    float cooldownRun = 3;
    float cooldownDash = 4;


    [Header("Canvas")]
    public GameObject m_CanvasGame;
    public GameObject m_CanvasGameOver;
    public GameObject m_CanvasVictory;
    public GameObject m_CanvasPauseMenu;
    public GameObject m_CanvasSettingsMenu;
    public GameObject m_CanvasMinimap;


    [Header("FMOD Events")]
    public string nightmareScoreUpEvent;
    public string playerScoreUpEvent;
    public string scoreDownEvent;


    private void Awake()
    {
        totalCorpses = GameObject.FindGameObjectsWithTag("Corpse");
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        foreach(Image image in corpseTracker)
        {
            image.GetComponent<Animator>().keepAnimatorControllerStateOnDisable = true;
        }

    }
    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += UpdateHUD;

       

        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;

        ScoreManager scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());
        m_CanvasGame.SetActive(true);

    }

    private void Update()
    {
     
        if (!hasMoved)
        {
            cooldownMovement -= Time.deltaTime;
        }
        if (hasMoved) cooldownRun -= Time.deltaTime;
        if (hasRun) cooldownDash -= Time.deltaTime;
        
        
    }


    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.WIN:
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(true);
                m_CanvasGame.SetActive(false);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);
                break;
            case GameState.GAME_OVER:
                m_CanvasGameOver.SetActive(true);
                m_CanvasVictory.SetActive(false);
                m_CanvasGame.SetActive(false);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);
                break;
            case GameState.GAME:
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(false);
                m_CanvasGame.SetActive(true);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);

                break;
            case GameState.MAP:
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(false);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(true);
                break;
            case GameState.PAUSE:
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(false);
                m_CanvasGame.SetActive(false);
                m_CanvasPauseMenu.SetActive(true);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);
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
        if(GM.GetEnemy() != null)
            GM.GetEnemy().GetComponent<EnemyPriorities>().ChangePriority();
        GM.UpdateModifiers();
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());

    }

    IEnumerator SetToFalse(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(1);
        text.gameObject.SetActive(false);
    }


    public void UpdateHealth()
    {
       
        int m_Life = (int)GM.GetPlayer().GetComponent<PlayerController>().m_Life;

        foreach (Image img in healthIcons)
        {
            Animator anim = img.GetComponent<Animator>();
            if (m_Life > 0)
            {
                anim.SetBool("Status", true);
                m_Life--;
            }
            else
            {
                 anim.SetBool("Status", false);
            }
        }

    }

    public void UpdateObjective(float playerCorpses, float enemyCorpses)
    {

        if (enemyCorpses >= GM.m_CorpseObjective)
        {
            objectiveAnim.SetTrigger("Hunt Nightmare");
            if(GM.GetEnemy().GetComponent<Enemy_BLACKBOARD>().particlesWinCondition != null)
                GM.GetEnemy().GetComponent<Enemy_BLACKBOARD>().particlesWinCondition.SetActive(true);
            
        }
        else if (playerCorpses >= GM.m_CorpseObjective)
        {
            objectiveAnim.SetBool("Can Kill Nightmare", true);
            GM.GetPlayer().GetComponent<PlayerController>().particlesWinCondition.SetActive(true);
        }
        else
        {
            objectiveAnim.SetTrigger("Hunt Corpse");
            objectiveAnim.SetBool("Can Kill Nightmare", false);
            GM.GetPlayer().GetComponent<PlayerController>().particlesWinCondition.SetActive(false);
            if(GM.GetEnemy().GetComponent<Enemy_BLACKBOARD>().particlesWinCondition != null)
                GM.GetEnemy().GetComponent<Enemy_BLACKBOARD>().particlesWinCondition.SetActive(false);
        }
        
            
        
       
    }

    public void UpdateAddCorpses(GameObject type)
    {
        
        switch (type.tag)
        {
            case "Player":
                corpseTracker[(int)m_ScoreManager.GetPlayerCorpses()-1].GetComponent<Animator>().SetTrigger("CorpseToPlayer");
                SoundManager.Instance.PlayEvent(playerScoreUpEvent, GM.GetPlayer().transform);
                corpseTracker[(int)m_ScoreManager.GetPlayerCorpses()-1].GetComponentInChildren<ParticleSystem>().Play();
                break;

            case "Enemy":
                corpseTracker[corpseTracker.Length - (int)m_ScoreManager.GetEnemyCorpses()].GetComponent<Animator>().SetTrigger("CorpseToNightmare");
                SoundManager.Instance.PlayEvent(nightmareScoreUpEvent,GM.GetPlayer().transform);
                corpseTracker[corpseTracker.Length - (int)m_ScoreManager.GetEnemyCorpses()].GetComponentInChildren<ParticleSystem>().Play();
                break;
        } 
    }

    public void UpdateRemoveCorpses(GameObject type)
    {

        switch (type.tag)
        {
            case "Enemy":
                corpseTracker[corpseTracker.Length - (int)m_ScoreManager.GetEnemyCorpses() - 1].GetComponent<Animator>().SetTrigger("Empty");
                SoundManager.Instance.PlayEvent(scoreDownEvent, GM.GetPlayer().transform);
                break;

            case "Player":
                corpseTracker[(int)m_ScoreManager.GetPlayerCorpses()].GetComponent<Animator>().SetTrigger("Empty");
                SoundManager.Instance.PlayEvent(scoreDownEvent, GM.GetPlayer().transform);
                break;
        }

    }
}