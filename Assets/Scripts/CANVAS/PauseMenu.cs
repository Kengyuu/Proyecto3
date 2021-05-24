using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    public static bool m_GameIsPaused;

    [Header("Canvas")]
    public GameObject m_PauseCanvas;
    public GameObject m_SettingsCanvas;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GM.GetPlayer().GetComponent<PlayerMovement>();

        GM.OnStateChange += StateChanged;
    }

    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.GAME:
                break;

            case GameState.PAUSE:
                //DisableInputs();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Pause.triggered)
        {
            //m_GameIsPaused = !m_GameIsPaused;

            if (m_GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        GM.SetGameState(GameState.PAUSE);
        m_PauseCanvas.SetActive(true);
        m_GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GM.SetGameState(GameState.GAME);
        //m_PauseCanvas.SetActive(false);
        m_GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


 /*   public void LoadSettings()
    {
        Debug.Log("Abriendo menú de opciones");
        m_PauseCanvas.SetActive(false);
        m_SettingsCanvas.SetActive(true);
    }*/

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }


 /*   private void DisableInputs()
    {
        m_PlayerMovement.m_InputSystem.Gameplay.Move.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.MouseScroll.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.EnableTrap.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Map.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.SpecialAbility_1.Disable();

    }*/

    private void OnDestroy()
    {
        GM.OnStateChange -= StateChanged;
    }
}
