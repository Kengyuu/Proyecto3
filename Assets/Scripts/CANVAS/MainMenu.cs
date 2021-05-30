using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	GameManager GM;

	public string m_GameScene;

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
		Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		GM.SetGameState(GameState.LOADING_TUTORIAL);
		Initiate.Fade(m_GameScene, Color.black, 3f);
	}

	public void Quit()
	{
		Debug.Log("Cerrando el juego");
		Application.Quit();
	}
}