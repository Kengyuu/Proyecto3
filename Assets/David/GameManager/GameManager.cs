using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum GameState
{
    LEVEL_1,
    LEVEL_2,
    INTRO,
    MAIN_MENU,
    PAUSE,
    GAMEOVER
}

//public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager() { }
    private static GameManager instance = null;
    //public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    [Header("Player data")]
    private GameObject m_Player;
    private Transform m_CurrentCheckpoint;
    private float m_PlayerHealth;
    private float m_PlayerAmmo;
    private float m_PlayerShield;
    private bool m_IsPlayerAlive;
    private bool m_PlayerCanMove;
    private bool m_IsCameraLocked;
    
    private List<string> m_PlayerInventory;

    [Header("Game State")]
    private bool m_IsGameActive;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }
    }

    private void Start()
    {
        //PlayerData
        m_Player = null;
        m_PlayerHealth = 0;
        m_PlayerAmmo = 0;
        m_PlayerShield = 0;
        m_IsPlayerAlive = true;
        m_PlayerCanMove = true;
        m_IsCameraLocked = false;
        m_CurrentCheckpoint = null;
        m_PlayerInventory = null;

        //Game State
        m_IsGameActive = true;
    }

    private void Update()
    {
        if (m_Player == null) m_Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        //OnStateChange();
    }

    public GameState GetGameState()
    {
        return this.gameState;
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }

    //Other
    public IEnumerator DestroyOnTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(obj);
    }
    //--------------------

    //PlayerData
    public GameObject GetPlayer()
    {
        return m_Player;
    }

    public void SetPlayerAmmo(float value)
    {
        m_PlayerAmmo = value;
    }
    public float GetPlayerAmmo()
    {
        return m_PlayerAmmo;
    }

    public void SetPlayerShield(float value)
    {
        m_PlayerShield = value;
    }
    public float GetPlayerShield()
    {
        return m_PlayerShield;
    }

    public void SetPlayerHealth(float value)
    {
        m_PlayerHealth = value;
    }
    public float GetPlayerHealth()
    {
        return m_PlayerHealth;
    }

    public void AddNewInventoryItem(string value)
    {
        m_PlayerInventory.Add(value);
    }
    public List<string> GetPlayerInventory()
    {
        return m_PlayerInventory;
    }

    public bool GetIsPlayerAlive()
    {
        return m_IsPlayerAlive;
    }
    public void SetIsPlayerAlive(bool value)
    {
        m_IsPlayerAlive = value;
    }

    public bool GetPlayerCanMove()
    {
        return m_PlayerCanMove;
    }
    public void SetPlayerCanMove(bool value)
    {
        m_PlayerCanMove = value;
    }

    public void SetCurrentCheckpoint(Transform value)
    {
        m_CurrentCheckpoint = value;
    }

    public Transform GetCurrentCheckpoint()
    {
        return m_CurrentCheckpoint;
    }
    //--------------------
    //Other data
    public bool GetIsGameActive()
    {
        return m_IsGameActive;
    }
    public void SetIsGameActive(bool value)
    {
        m_IsGameActive = value;
    }

    public bool GetIsCameraLocked()
    {
        return m_IsCameraLocked;
    }
    public void SetIsCameraLocked(bool value)
    {
        m_IsCameraLocked = value;
    }

    //--------------------


}//End GameManager
