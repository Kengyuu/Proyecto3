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
	}

	private void OnDestroy()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

	void Start()
	{
		GM.SetGameState(GameState.MAIN_MENU);
	}



	public void HandleOnStateChange()
	{
		GM.OnStateChange -= HandleOnStateChange;
		Invoke("LoadLevel", 5f);
	}

	public void LoadLevel()
	{

		Initiate.Fade(m_NextScene, Color.black, 5f);
	}
}