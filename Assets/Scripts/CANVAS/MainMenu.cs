using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	GameManager GM;

	public string m_GameScene;

	public Animator m_MaskAnimator;


	void Awake()
	{
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;

		GM.m_Player = null;
		GM.m_Enemy = null;
		GM.m_GameObjectSpawner = null;
		GM.m_WaypointsList = null;

	}

	private void OnDestroy()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

    public void HandleOnStateChange()
	{
		//Debug.Log("OnStateChange! Unfollowing event.");
		//Apply this when we have a real scene change:
		GM.OnStateChange -= HandleOnStateChange;
	}


	public void StartGame()
	{
		m_MaskAnimator.SetTrigger("Hunt Button Press");
		Invoke("Game", 4f);
	}

	private void Game()
    {
		Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		GM.SetGameState(GameState.LOADING_TUTORIAL);
		Initiate.Fade(m_GameScene, Color.black, 3f);
	}

	public void Quit()
	{
		Debug.Log("Cerrando el juego");
		Application.Quit();
	}

    public void MouseHoverHunt()
    {
		m_MaskAnimator.SetBool("On Hunt Button", true);
		
		Debug.Log("Raton encima");
    }

    public void MouseExitHunt()
    {
		m_MaskAnimator.SetBool("On Hunt Button", false);
		Debug.Log("Raton fuera");
    }


    private void Update()
    {
		Debug.Log($"Bool On Hunt Button: {m_MaskAnimator.GetBool("On Hunt Button")} ");
	}


}