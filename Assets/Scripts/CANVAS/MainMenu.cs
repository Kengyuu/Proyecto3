using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	GameManager GM;

	public string m_SlidesScene;

	public Animator m_MaskAnimator;

	public GameObject m_MainMenu;
	public GameObject m_HuntChoice;



	void Awake()
	{
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;

		GM.m_Player = null;
		GM.m_Enemy = null;
		GM.m_GameObjectSpawner = null;
		GM.m_WaypointsList = null;


		//FOR DEBUG - BORRAR LUEGO
		//GM.SetGameState(GameState.MAIN_MENU);

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
		Invoke("ShowTutorial", 4f);
	}

	private void ShowTutorial()
    {
		m_MainMenu.SetActive(false);
		m_HuntChoice.SetActive(true);
	}

	public void PlayGame()
    {
		//Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		GM.SetGameState(GameState.GAME);
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		Initiate.Fade(m_SlidesScene, Color.black, 3f);
	}

	public void PlayTutorial()
	{
		//Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		GM.SetGameState(GameState.TUTORIAL);
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		Initiate.Fade(m_SlidesScene, Color.black, 3f);
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

}