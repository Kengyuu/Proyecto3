using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game States
public enum GameState
{
    LEVEL_1,
    INTRO,
    MAIN_MENU,
    PAUSE,
    GAMEOVER,
    WIN
}

//public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    protected GameManager() { }
    private static GameManager instance = null;
    //public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    [Header("Player data")]
    [SerializeField] private GameObject m_Player;
    [SerializeField] private bool m_IsPlayerAlive;
    [SerializeField] private bool m_PlayerCanMove;
    [SerializeField] private bool m_IsCameraLocked;

    [Header("Enemy data")]
    [SerializeField] private GameObject m_Enemy;

    [Header("Game State")]
    [SerializeField] private bool m_IsGameActive;

    [Header("Score Manager")]
    public ScoreManager m_ScoreManager;

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
        m_IsPlayerAlive = true;
        m_PlayerCanMove = true;
        m_IsCameraLocked = false;
        m_ScoreManager = null;

        //EnemyData
        m_Enemy = null;

        //Game State
        m_IsGameActive = true;
    }

    private void Update()
    {
        if (m_Player == null) m_Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        if (m_Enemy == null) m_Enemy = GameObject.FindObjectOfType<Enemy_BLACKBOARD>().gameObject;
        if (m_ScoreManager == null) m_ScoreManager = GameObject.FindObjectOfType<ScoreManager>();
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

    //Player
    public GameObject GetPlayer()
    {
        return m_Player;
    }

    //Alive
    public bool GetIsPlayerAlive()
    {
        return m_IsPlayerAlive;
    }
    public void SetIsPlayerAlive(bool value)
    {
        m_IsPlayerAlive = value;
    }

    //Movement
    public bool GetPlayerCanMove()
    {
        return m_PlayerCanMove;
    }
    public void SetPlayerCanMove(bool value)
    {
        m_PlayerCanMove = value;
    }

    //Camera movement
    public bool GetIsCameraLocked()
    {
        return m_IsCameraLocked;
    }
    public void SetIsCameraLocked(bool value)
    {
        m_IsCameraLocked = value;
    }

    //--------------------
    //Game active?
    public bool GetIsGameActive()
    {
        return m_IsGameActive;
    }
    public void SetIsGameActive(bool value)
    {
        m_IsGameActive = value;
    }

    //Enemy
    public GameObject GetEnemy()
    {
        return m_Enemy;
    }


    //--------------------
}//End GameManager


public class DependencyInjector
{
    static Dictionary<Type, System.Object> dependencies = new Dictionary<Type, System.Object>();
    public static T GetDependency<T>()
    {
        if (!dependencies.ContainsKey(typeof(T)))
        {
            Debug.LogError("Cannot find: " + typeof(T).ToString() +".");
            /*return null;*/
            return default(T);
        }
        return (T)dependencies[typeof(T)];
    }
    public static void AddDependency<T>(System.Object obj)
    {
        if (dependencies.ContainsKey(typeof(T)))
        {
            Debug.Log("There's already an object of type: " + typeof(T).ToString());
            Debug.Log("Object 1: " + dependencies[typeof(T)].GetType().ToString());
            Debug.Log("Object 2: " + obj.GetType().ToString());
            dependencies.Remove(typeof(T));
        }
        dependencies.Add(typeof(T), obj);
    }
}