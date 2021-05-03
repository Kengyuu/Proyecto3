using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    private PlayerCamera m_PlayerCamera;
    private Camera m_Camera;
    private GameManager GM;

    [Header("Player Stats")]
    public float m_Life;
    public float m_MaxLife;
    //public float m_PlayerInitialCorpses;
    
    [Header("Player Damage")]
    public float m_MaxStunTime;
    public float m_ImpactForceX;
    public float m_ImpactForceY;

    [Header("Helpers")]
    public ScoreManager m_ScoreManager;

    [Header("Map")]
    public GameObject m_Map;


    [Header("Debug")]
    public GameObject enemyTest;
    //bool map_status = false;



    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerCamera = GetComponent<PlayerCamera>();

        m_Camera = Camera.main;

        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;

        //HUD Updaters
        UpdatePlayerHealth();
    }

    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.WIN:
                //Debug.Log("state WIN");
                DisableInputs();
                break;

            case GameState.GAME_OVER:
                //Debug.Log("state GAME OVEEEEEEEEEEEEEEEER");
                DisableInputs();
                EnableDaze();
                Invoke("DisableDaze", 2f);
                break;

            case GameState.GAME:
                //Debug.Log("state GAME");
                EnableInputs();
                /*m_Map.SetActive(false);
                m_PlayerMovement.m_InputSystem.Enable();*/
                break;

            /*case GameState.MAP:
                //Debug.Log("state MAP activado / desactivado");
                m_Map.SetActive(true);
                m_PlayerMovement.m_InputSystem.Disable();
                break;*/
        }
    }


    void Update()
    {
        //Debug:
        /*if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(1, enemyTest);
        }*/
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            map_status = !map_status;
            if (map_status)
            {
                GM.SetGameState(GameState.MAP);
            }
            else
            {
                GM.SetGameState(GameState.GAME);
            }
        }*/
        //--
    }


    public void TakeDamage(int dmg, GameObject obj)
    {
        //Receive impact from the enemy
        Vector3 l_Direction = (transform.position - obj.transform.position).normalized;
        m_PlayerMovement.AddForceX(l_Direction, m_ImpactForceX);
        m_PlayerMovement.AddForceY(m_ImpactForceY);

        //Reduce Life
        m_Life -= dmg;
        
        if (m_Life <= 0)
        {
            //Debug.Log($"Player a {m_Life} de vida, GetStunned()");
            m_Life = 0;
            

            //Añadir aqui si el enemigo tiene 10 cuerpos o más)
            if(m_ScoreManager.GetEnemyCorpses() >= 10)
            {
                GM.SetGameState(GameState.GAME_OVER);
                return;
            }
            GetStunned();
            RemoveCorpse();
        }

        UpdatePlayerHealth();
        OrbEvents.current.ManageOrbs();
    }

    public void RestoreLife()
    {
        //Debug.Log("RestoreLife iniciado, activado inputs");
        m_Life = m_MaxLife;
        EnableInputs();
        DisableDaze();
        UpdatePlayerHealth();
    }

    public void DisableInputs()
    {
        m_PlayerMovement.m_InputSystem.Disable();
        m_PlayerCamera.enabled = false;
    }

    public void EnableInputs()
    {
        m_PlayerMovement.m_InputSystem.Enable();
        m_PlayerCamera.enabled = true;
    }

    public void EnableDaze()
    {
        m_Camera.GetComponent<CameraDaze>().enabled = true;
    }

    public void DisableDaze()
    {
        m_Camera.GetComponent<CameraDaze>().enabled = false;
    }



    // Score Manager Stuff
    private void UpdatePlayerHealth()
    {
        m_ScoreManager.SetPlayerHP(m_Life);
    }

    public void RemoveCorpse()
    {
        if (m_ScoreManager.GetPlayerCorpses() > 0) m_ScoreManager.RemovePlayerCorpse();
    }

    public void AddCorpse()
    {
        m_ScoreManager.AddPlayerCorpse();
    }

    public void GetStunned()
    {
        DisableInputs();
        EnableDaze();
        Invoke("RestoreLife", m_MaxStunTime);
    }
}
