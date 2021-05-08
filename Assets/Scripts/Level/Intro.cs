using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
	GameManager GM;
	public string m_NextScene;

	void Awake()
	{
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;
		//Debug.Log("Current game state when Awakes: " + GM.gameState);
	}

	private void OnDestroy()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

	void Start()
	{
		//Debug.Log("Current game state when Starts: " + GM.gameState);
		GM.SetGameState(GameState.MAIN_MENU);
	}



	public void HandleOnStateChange()
	{
		//Debug.Log("Handling state change to: " + GM.gameState);
		GM.OnStateChange -= HandleOnStateChange;
		Invoke("LoadLevel", 3f);
	}

	public void LoadLevel()
	{
		//Debug.Log("Invoking LoadLevel");
		//Application.LoadLevel("MAIN_MENU");
		Initiate.Fade(m_NextScene, Color.black, 1f);
	}
}