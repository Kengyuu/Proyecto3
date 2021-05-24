using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    public static bool m_GameIsPaused;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GM.GetPlayer().GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Pause.triggered)
        {
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
        Debug.Log("entro a PAUSE");
        Time.timeScale = 0f;
        GM.SetGameState(GameState.PAUSE);
        m_GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Debug.Log("entro a RESUME");
        Time.timeScale = 1f;
        GM.SetGameState(GameState.GAME);
        m_GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
