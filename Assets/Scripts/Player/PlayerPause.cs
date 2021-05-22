using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{

    /*
     * 
     * https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/#:~:text=The%20most%20convenient%20method%20for,game%20to%20its%20normal%20speed.
     * 
     */


    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    public static bool gameIsPaused;

    [Header("Pause menu")]
    public GameObject m_PauseCanvas;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GetComponent<PlayerMovement>();

        GM.OnStateChange += StateChanged;
    }

    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.GAME:
                break;

            case GameState.PAUSE:
                DisableInputs();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Pause.triggered)
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            GM.SetGameState(GameState.PAUSE);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            GM.SetGameState(GameState.GAME);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    private void DisableInputs()
    {
        m_PlayerMovement.m_InputSystem.Gameplay.Move.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.MouseScroll.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.EnableTrap.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Map.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();

    }
}
