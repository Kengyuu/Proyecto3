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
    private SoundManager SM;
    private HudController M_HudController;

    [Header("Player Stats")]
    public float m_Life;
    public float m_MaxLife;
    public float m_TimeSinceLastFootstep = 3;
    
    
    [Header("Player Damage / Stun")]
    public float m_MaxStunTime;
    public bool m_PlayerStunned = false;

    

    [Header("Helpers")]
    public ScoreManager m_ScoreManager;
    public GameMusic music;

    [Header("Map")]
    public GameObject m_Map;

    [Header("Debug")]
    public GameObject enemyTest;

    [Header("FMOD Events")]
    public string hurtEvent;
    public string incapacitateEvent;
    public string healEvent;
    public string deathEvent;
    public string pauseSoundEvent;

    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerCamera = GetComponent<PlayerCamera>();
        m_PlayerDash = GetComponent<PlayerDash>();
        
        m_Camera = Camera.main;

        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (GM == null) GM = GameManager.Instance;
        if (SM == null) SM = SoundManager.Instance;

        GM.OnStateChange += StateChanged;

        
        InitInputs();

        UpdatePlayerHealth();
    }

    private void OnDestroy()
    {
        GM.OnStateChange -= StateChanged;
    }



   

    private void InitInputs()
    {
        m_PlayerMovement.m_InputSystem.NoInputs.Disable();
        m_PlayerMovement.m_InputSystem.Minimap.Disable();
        m_PlayerMovement.m_InputSystem.Pause.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Enable();
    }

    private void DisableAllInputs()
    {
        m_PlayerMovement.m_InputSystem.NoInputs.Disable();
        m_PlayerMovement.m_InputSystem.Minimap.Disable();
        m_PlayerMovement.m_InputSystem.Pause.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Disable();
    }


    private void StateChanged()
    {
       
        DisableAllInputs();
        
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
                SoundManager.Instance.PlaySound(pauseSoundEvent, transform.position);
                m_PlayerMovement.m_InputSystem.Pause.Enable();
                break;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RemoveCorpse();
        }

    }

    public void TakeDamage(int dmg, GameObject obj, float XForce, float YForce)
    {
        if (!m_PlayerDash.m_DashEvadeAttacks && !m_PlayerStunned)
        {
            Vector3 l_Direction = (transform.position - obj.transform.position).normalized;
            if (XForce != 0)
            {
                m_PlayerMovement.AddForceX(l_Direction, XForce);
            }

            m_PlayerMovement.AddForceY(YForce);

            m_Life -= dmg;
            M_HudController.UpdateHealth();
            if (m_Life > 0)
            {
                SM.PlaySound(hurtEvent, transform.position);
            }
           

            

            else if (m_Life <= 0)
            { 
                m_Life = 0;
  
                if (m_ScoreManager.GetEnemyCorpses() >= GM.m_CorpseObjective && obj.CompareTag("EnemyArm"))
                {
                    GM.GetEnemy().GetComponent<HFSM_StunEnemy>().hasWon = true;
                    SoundManager.Instance.PlaySound(deathEvent, transform.position);
                    StartCoroutine("PlayerDeath");
                    return;
                }
                else
                {
                    SM.PlaySound(incapacitateEvent, transform.position);
                }
                 

                if (m_ScoreManager.GetPlayerCorpses() > 0)
                {
                    RemoveCorpse();
                    
                    
                }
                GetStunned();       
            }  
            UpdatePlayerHealth();
            OrbEvents.current.ManageOrbs();
        }
    }

    IEnumerator PlayerDeath()
    {
        music.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music.chase.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GM.SetGameState(GameState.GAME_OVER);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; //TBD
    }


    public List<ParticleSystem> m_HealingParticles;

    public void RestoreLife()
    {
       
        m_Life = m_MaxLife;
        SM.PlaySound(healEvent, transform.position);
        EnableInputs();
        DisableDaze();
        UpdatePlayerHealth();
        m_PlayerStunned = false;
        foreach (Image healthIcon in M_HudController.healthIcons)
        {
            healthIcon.GetComponent<Animator>().SetBool("Status",true);
        }

        foreach(ParticleSystem part in m_HealingParticles)
        {
            part.Play();
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
    public void UpdatePlayerHealth()
    {
        m_ScoreManager.SetPlayerHP(m_Life);
      
    }

    public void RemoveCorpse()
    {
        if (m_ScoreManager.GetPlayerCorpses() > 0)
        {
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
        if(GetComponent<PlayerShoot>().m_CurrentCorpseAbsortion != null)
        {
            GetComponent<PlayerShoot>().m_CurrentCorpseAbsortion.GetComponent<CorpseAbsortion>().systemActive = false;
            GetComponent<PlayerShoot>().m_CurrentCorpseAbsortion.GetComponent<CorpseAbsortion>().StopAbsortion();
        }
        DisableInputs();
        EnableDaze();
        Invoke("RestoreLife", m_MaxStunTime);
    }
}
