using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMap : MonoBehaviour
{
    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    [Header("Map")]
    public GameObject m_Map;

    [Header("Debug")]
    [SerializeField] bool map_status = false;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_Map.SetActive(map_status);

        GM.OnStateChange += StateChanged;
    }

    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.GAME:
                
                DisableMap();
                break;

            case GameState.MAP:
                EnableMap();
                break;
        }
    }


    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Map.triggered)
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
    }

    private void EnableMap()
    {
        m_Map.SetActive(true);
        m_PlayerMovement.m_InputSystem.Gameplay.Move.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Disable();
    }

    private void DisableMap()
    {
        m_Map.SetActive(false);
        m_PlayerMovement.m_InputSystem.Gameplay.Move.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Enable();
    }
}
