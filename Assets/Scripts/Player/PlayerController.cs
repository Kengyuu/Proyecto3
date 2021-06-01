using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    private PlayerCamera m_PlayerCamera;
    private PlayerDash m_PlayerDash;
    private Camera m_Camera;
    private GameObjectSpawner spawner;
    private GameManager GM;
    private HudController M_HudController;

    [Header("Player Stats")]
    public float m_Life;
    public float m_MaxLife;
    //public float m_PlayerInitialCorpses;
    
    [Header("Player Damage / Stun")]
    public float m_MaxStunTime;
    public bool m_PlayerStunned = false;

    [Header("Helpers")]
    public ScoreManager m_ScoreManager;

    [Header("Map")]
    public GameObject m_Map;

    [Header("Debug")]
    public GameObject enemyTest;

   // [Header("Animation")]
    


    //bool map_status = false;



    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerCamera = GetComponent<PlayerCamera>();
        m_PlayerDash = GetComponent<PlayerDash>();
        
        m_Camera = Camera.main;

        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (GM == null) GM = GameManager.Instance;

        GM.OnStateChange += StateChanged;


        InitInputs();

        //HUD Updaters
        UpdatePlayerHealth();
    }

    private void OnDestroy()
    {
        GM.OnStateChange -= StateChanged;
    }



    //TESTEO DESHABILITAR ESTA FUNCION PARA NUEVO SISTEMA DE INPUTS

    /*    private void StateChanged()
        {
            switch (GM.gameState)
            {
                case GameState.WIN:
                    //Debug.Log("state WIN");
                    //DisableInputs();
                    m_PlayerCamera.enabled = false;
                    m_PlayerMovement.m_InputSystem.Gameplay.Disable();
                    break;

                case GameState.GAME_OVER:
                    //Debug.Log("state GAME OVEEEEEEEEEEEEEEEER");
                    //DisableInputs();

                    m_PlayerCamera.enabled = false;
                    m_PlayerMovement.m_InputSystem.Gameplay.Disable();
                    EnableDaze();
                    Invoke("DisableDaze", 2f);
                    break;

                case GameState.GAME:
                    //Debug.Log("state GAME");

                    *//*m_Map.SetActive(false);
                    m_PlayerMovement.m_InputSystem.Enable();*//*

                    //EnableInputs();

                    m_PlayerCamera.enabled = true;
                    GM.UpdateModifiers(); //Check again to update correctly skills available
                    m_PlayerMovement.m_InputSystem.Gameplay.Dash.Enable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Enable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Move.Enable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Jump.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.EnableTrap.Enable();
                    m_PlayerMovement.m_InputSystem.Gameplay.MouseScroll.Disable();
                    break;

                case GameState.MAP:
                    //Debug.Log("state MAP activado / desactivado");
                    *//*m_Map.SetActive(true);
                    m_PlayerMovement.m_InputSystem.Disable();*//*
                    m_PlayerMovement.m_InputSystem.Gameplay.Dash.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Move.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.Jump.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.EnableTrap.Disable();
                    m_PlayerMovement.m_InputSystem.Gameplay.MouseScroll.Enable();

                    //Disable just in case:
                    m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();
                    break;
            }
        }*/

    private void InitInputs()
    {
        m_PlayerMovement.m_InputSystem.NoInputs.Disable();
        m_PlayerMovement.m_InputSystem.Minimap.Disable();
        m_PlayerMovement.m_InputSystem.Pause.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Enable();
    }


    private void StateChanged()
    {
        //Old Game State
        Debug.Log($"Old gamestate: {GM.oldGameState}");
        switch (GM.oldGameState)
        {
            case GameState.MAIN_MENU:
                m_PlayerMovement.m_InputSystem.NoInputs.Disable();
                break;

            case GameState.GAME:
                m_PlayerMovement.m_InputSystem.Gameplay.Disable();
                break;

            case GameState.GAME_OVER:
                m_PlayerMovement.m_InputSystem.NoInputs.Disable();
                break;

            case GameState.WIN:
                m_PlayerMovement.m_InputSystem.NoInputs.Disable();
                break;

            case GameState.MAP:
                m_PlayerMovement.m_InputSystem.Minimap.Disable();
                break;

            case GameState.PAUSE:
                m_PlayerMovement.m_InputSystem.Pause.Disable();
                break;
        }

        //New Game State
        Debug.Log($"New gamestate: {GM.gameState}");
        switch (GM.gameState)
        {
            case GameState.MAIN_MENU:
                m_PlayerMovement.m_InputSystem.NoInputs.Enable();
                break;

            case GameState.GAME:
                m_PlayerMovement.m_InputSystem.Gameplay.Enable();
                break;

            case GameState.GAME_OVER:
                m_PlayerMovement.m_InputSystem.NoInputs.Enable();
                break;

            case GameState.WIN:
                m_PlayerMovement.m_InputSystem.NoInputs.Enable();
                break;

            case GameState.MAP:
                m_PlayerMovement.m_InputSystem.Minimap.Enable();
                break;

            case GameState.PAUSE:
                m_PlayerMovement.m_InputSystem.Pause.Enable();
                break;
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
        }
        
        */

        if (Input.GetKeyDown(KeyCode.L))
        {
            RemoveCorpse();
        }
        
        //--
    }




    public void TakeDamage(int dmg, GameObject obj, float XForce, float YForce)
    {
        if (!m_PlayerDash.m_DashEvadeAttacks && !m_PlayerStunned)
        {
            //Receive impact from the enemy
            Vector3 l_Direction = (transform.position - obj.transform.position).normalized;
            if (XForce != 0)
            {
                m_PlayerMovement.AddForceX(l_Direction, XForce);
            }

            m_PlayerMovement.AddForceY(YForce);

            //Reduce Life
            m_Life -= dmg;

           

            if (m_Life <= 0)
            {
                //Debug.Log($"Player a {m_Life} de vida, GetStunned()");
                m_Life = 0;


                //Enemigo con 10+ cuerpos -> game over
                if (m_ScoreManager.GetEnemyCorpses() >= 10)
                {
                    GM.SetGameState(GameState.GAME_OVER);
                    Cursor.lockState = CursorLockMode.None; //TBD
                    return;
                }
                if (m_ScoreManager.GetPlayerCorpses() > 0)
                {
                    RemoveCorpse();
                    
                    
                }
                GetStunned();
                

                //Invoke("RestoreLife", m_MaxStunTime);
            }
            switch (m_Life)
            {
                case 2:
                    M_HudController.healthIcons[0].GetComponent<Animator>().SetTrigger("Break");
                    break;
                case 1:
                    M_HudController.healthIcons[1].GetComponent<Animator>().SetTrigger("Break");
                    break;
                case 0:
                    M_HudController.healthIcons[2].GetComponent<Animator>().SetTrigger("Break");
                    break;
            }

            if (dmg == 3)
            {
                foreach (Image healthIcon in M_HudController.healthIcons)
                {
                    healthIcon.GetComponent<Animator>().SetTrigger("Break");
                }
            }

            UpdatePlayerHealth();
            OrbEvents.current.ManageOrbs();
        }
    }

    public void RestoreLife()
    {
        //Debug.Log("RestoreLife iniciado, activado inputs");
        m_Life = m_MaxLife;
        EnableInputs();
        DisableDaze();
        UpdatePlayerHealth();
        m_PlayerStunned = false;
        foreach (Image healthIcon in M_HudController.healthIcons)
        {
            healthIcon.GetComponent<Animator>().SetTrigger("Heal");
        }
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
    public void UpdatePlayerHealth()
    {
        m_ScoreManager.SetPlayerHP(m_Life);
      
    }

    public void RemoveCorpse()
    {
        if (m_ScoreManager.GetPlayerCorpses() > 0)
        {
            /*int lostPlayerCorpses = Mathf.Max(1, Mathf.RoundToInt(m_ScoreManager.GetPlayerCorpses()/ 3));
            for (int i = 0; i < lostPlayerCorpses; i++)
            {
                m_ScoreManager.RemovePlayerCorpse();
            }*/
            m_ScoreManager.RemovePlayerCorpse();
            M_HudController.UpdateRemoveCorpses(gameObject);
            GameManager.Instance.GetGameObjectSpawner().SpawnBodys(1, gameObject);
        }
        
    }

    public void AddCorpse()
    {
        m_ScoreManager.AddPlayerCorpse();
    }

    public void GetStunned()
    {
        m_PlayerStunned = true;
        DisableInputs();
        EnableDaze();
        Invoke("RestoreLife", m_MaxStunTime);
    }
}
