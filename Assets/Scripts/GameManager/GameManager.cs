using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    INTRO,
    MAIN_MENU,
    GAME,
    CREDITS,
    HELP,
    GAME_OVER,
    WIN,
    MAP
}

public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour
{
    // <- GAME MANAGER ->
    protected GameManager() { }
    private static GameManager instance = null;
    public event OnStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }

    [Header("Debug")]
    [SerializeField] private GameObject m_Player;
    [SerializeField] private GameObject m_Enemy;
    [SerializeField] private GameObjectSpawner m_GameObjectSpawner;

    [SerializeField] private RoomSpawner m_WaypointsList;
    

    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Enemy = GameObject.FindGameObjectWithTag("Enemy");
        m_GameObjectSpawner = GameObject.FindObjectOfType<GameObjectSpawner>();
        m_WaypointsList = GameObject.FindObjectOfType<RoomSpawner>();

        SetGameState(GameState.GAME);
    }

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Enemy = GameObject.FindGameObjectWithTag("Enemy");
        m_GameObjectSpawner = GameObject.FindObjectOfType<GameObjectSpawner>();
        m_WaypointsList = GameObject.FindObjectOfType<RoomSpawner>();
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }
    }

    private void Update()
    {
        Debug.Log($"Current game state: {gameState}");
    }

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange?.Invoke();
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }
    // <- END GAME MANAGER ->


    // <- Functions ->
    public GameObject GetPlayer()
    {
        return m_Player;
    }

    public GameObject GetEnemy()
    {
        return m_Enemy;
    }

    public GameObjectSpawner GetGameObjectSpawner()
    {
        return m_GameObjectSpawner;
    }

    public RoomSpawner GetWaypointsList()
    {
        return m_WaypointsList;
    }


    // <- End Functions ->


    //Other
    public IEnumerator DestroyOnTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(obj);
    }
    //--------------------
}


public class DependencyInjector
{
    static Dictionary<Type, System.Object> dependencies = new Dictionary<Type, System.Object>();
    public static T GetDependency<T>()
    {
        if (!dependencies.ContainsKey(typeof(T)))
        {
            Debug.LogError("Cannot find: " + typeof(T).ToString() + ".");
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