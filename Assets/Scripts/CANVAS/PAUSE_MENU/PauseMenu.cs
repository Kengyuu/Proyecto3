using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    public static bool m_GameIsPaused;
    [Header("FMOD Events")]
    public string hoverSoundEvent;
    public string clickSoundEvent;
    public string exitSoundEvent;

    public TutorialMusic musicTutorial;
    public GameMusic musicGame;

    Scene currentScene;
    string sceneName;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GM.GetPlayer().GetComponent<PlayerMovement>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Pause.triggered || m_PlayerMovement.m_InputSystem.Pause.Pause.triggered)
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
        Time.timeScale = 0f;
        GM.SetGameState(GameState.PAUSE);
        m_GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GM.SetGameState(GameState.GAME);
        m_GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        if (sceneName == "TutorialScene")
        {
            musicTutorial.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (sceneName == "Game") 
        {
           musicGame.music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
           musicGame.chase.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
    public void MouseHoverSound()
    {
        SoundManager.Instance.PlaySound(hoverSoundEvent, transform.position);
    }

    public void MouseClickSound()
    {
        SoundManager.Instance.PlaySound(clickSoundEvent, transform.position);
    }
    public void MouseExitSound()
    {
        
        SoundManager.Instance.PlaySound(exitSoundEvent, transform.position);
    }
}
