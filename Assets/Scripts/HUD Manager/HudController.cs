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
    //public GameObject m_CanvasGame;
    public TextMeshProUGUI m_PlayerCorpses;
    public TextMeshProUGUI m_EnemyCorpses;
    public TextMeshProUGUI m_RemainingCorpses;
    //public TextMeshProUGUI m_PlayerHP;

    /*[Header("Prompts")]
    public TextMeshProUGUI[] promptList;
    public TextMeshProUGUI movementPrompt;
    public TextMeshProUGUI runPrompt;
    public TextMeshProUGUI dashPrompt;
    public TextMeshProUGUI shootCorpsePrompt;
    public TextMeshProUGUI shootTrapPrompt;
    public TextMeshProUGUI shootEnemyPrompt;
    public TextMeshProUGUI trapRepairPrompt;*/
    
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
        /*foreach (GameObject corpse in totalCorpses)
        {
            if (corpse.activeSelf)
            {
                activeCorpses.Add(corpse);
            }
        }*/

    }
    private void Start()
    {
        DependencyInjector.GetDependency<IScoreManager>()
        .scoreChangedDelegate += UpdateHUD;

       

        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;

        //m_CanvasGame.SetActive(true);

        ScoreManager scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();


        //START DE HUD
        m_PlayerCorpses.text = "Player Corpses: " + scoreManager.GetPlayerCorpses().ToString("0");
        m_EnemyCorpses.text = "Enemy Corpses: " + scoreManager.GetEnemyCorpses().ToString("0");
        m_RemainingCorpses.text = "Remaining Corpses: " + scoreManager.GetRemainingCorpses().ToString("0");
       // UpdateHealth(scoreManager.GetPlayerHP());
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());
        // m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";


        m_CanvasGame.SetActive(true);

    }

    private void Update()
    {
        ManagePrompts();
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
                /*m_CanvasGame.SetActive(false);
                m_CanvasEnd.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                m_centerText.text = "EL JUGADOR HA GANADO!";*/
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(true);
                m_CanvasGame.SetActive(false);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);
                break;
            case GameState.GAME_OVER:
                /*m_CanvasGame.SetActive(false);
                m_CanvasEnd.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                m_centerText.text = "EL ENEMIGO HA GANADO!";*/
                m_CanvasGameOver.SetActive(true);
                m_CanvasVictory.SetActive(false);
                m_CanvasGame.SetActive(false);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);
                break;
            case GameState.GAME:
                /*m_centerText.text = "";
                m_PlayerCorpses.enabled = true;
                m_EnemyCorpses.enabled = true;
                m_RemainingCorpses.enabled = true;
                m_PlayerHP.enabled = true;
                m_centerText.enabled = true;*/
                //m_CanvasGame.SetActive(true); //REWORK AQUI
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(false);
                m_CanvasGame.SetActive(true);
                m_CanvasPauseMenu.SetActive(false);
                m_CanvasSettingsMenu.SetActive(false);
                m_CanvasMinimap.SetActive(false);

                break;
            case GameState.MAP:
                //m_CanvasGame.SetActive(false); //REWORK AQUI
                /*m_centerText.text = "";
                m_PlayerCorpses.enabled = false;
                m_EnemyCorpses.enabled = false;
                m_RemainingCorpses.enabled = false;
                m_PlayerHP.enabled = false;
                m_centerText.enabled = false;*/
                m_CanvasGameOver.SetActive(false);
                m_CanvasVictory.SetActive(false);
                //m_CanvasGame.SetActive(false);
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
        //m_PlayerHP.text = "PlayerHP: " + scoreManager.GetPlayerHP().ToString("0") + " / 3";
        GM.UpdateModifiers();
        //UpdateHealth(scoreManager.GetPlayerHP());
        UpdateObjective(scoreManager.GetPlayerCorpses(), scoreManager.GetEnemyCorpses());

    }

    public void ManagePrompts()
    {
        /*if (cooldownMovement <= 0 && !hasMoved && !CheckIfPromptActive())
        {
            movementPrompt.gameObject.SetActive(true);
            StartCoroutine(SetToFalse(movementPrompt));

        }
        if (hasMoved) StartCoroutine(SetToFalse(movementPrompt));

        if (cooldownRun <= 0 && !hasRun && !CheckIfPromptActive())
        {
            runPrompt.gameObject.SetActive(true);
            StartCoroutine(SetToFalse(runPrompt));
        }
        if (hasRun) StartCoroutine(SetToFalse(runPrompt));

        if (cooldownDash <= 0 && !hasDashed && !CheckIfPromptActive())
        {
            dashPrompt.gameObject.SetActive(true);
            StartCoroutine(SetToFalse(dashPrompt));
        }
        if (hasDashed) StartCoroutine(SetToFalse(dashPrompt));

        if (triggerShot && !CheckIfPromptActive())
        {
            shootCorpsePrompt.gameObject.SetActive(true);
            triggerShot = false;
            StartCoroutine(SetToFalse(shootCorpsePrompt));
        }
        if (hasShot)StartCoroutine(SetToFalse(shootCorpsePrompt));

        if (triggerShotTrap && !CheckIfPromptActive())
        {
            shootTrapPrompt.gameObject.SetActive(true);
            triggerShotTrap = false;
            StartCoroutine(SetToFalse(shootTrapPrompt));
        }
        if (hasShot) StartCoroutine(SetToFalse(shootTrapPrompt));

        if (triggerShotEnemy && !CheckIfPromptActive())
        {
            shootEnemyPrompt.gameObject.SetActive(true);
            triggerShotEnemy = false;
            StartCoroutine(SetToFalse(shootEnemyPrompt));
        }
        if (hasShot) StartCoroutine(SetToFalse(shootEnemyPrompt));

        if (triggerShotTrapD && !CheckIfPromptActive())
        {
            trapRepairPrompt.gameObject.SetActive(true);
            triggerShotTrapD = false;
            StartCoroutine(SetToFalse(trapRepairPrompt));
        }
        if (hasRepaired) StartCoroutine(SetToFalse(trapRepairPrompt));*/

    }

    /*public bool CheckIfPromptActive()
    {
        foreach (TextMeshProUGUI text in promptList)
        {
            if (text.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }*/

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
        }
        else if (playerCorpses >= GM.m_CorpseObjective)
        {
            objectiveAnim.SetBool("Can Kill Nightmare", true);
        }
        else
        {
            objectiveAnim.SetTrigger("Hunt Corpse");
            objectiveAnim.SetBool("Can Kill Nightmare", false);
        }
        
            
        
       
    }

    public void UpdateAddCorpses(GameObject type)
    {
        
        switch (type.tag)
        {
            case "Player":
                corpseTracker[(int)m_ScoreManager.GetPlayerCorpses()-1].GetComponent<Animator>().SetTrigger("CorpseToPlayer");
                SoundManager.Instance.PlayEvent(playerScoreUpEvent, GM.GetPlayer().transform);
                break;

            case "Enemy":
                corpseTracker[corpseTracker.Length - (int)m_ScoreManager.GetEnemyCorpses()].GetComponent<Animator>().SetTrigger("CorpseToNightmare");
                SoundManager.Instance.PlayEvent(nightmareScoreUpEvent,GM.GetPlayer().transform);
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