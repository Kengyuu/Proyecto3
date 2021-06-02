using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEndedMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Reiniciado el juego");
        GameManager.Instance.SetGameState(GameState.GAME);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ReturnToMainMenu()
    {
        Debug.Log("Volviendo al menú principal");
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        Time.timeScale = 1f;
        Initiate.Fade("MAIN_MENU", Color.black, 2f);
    }
}
