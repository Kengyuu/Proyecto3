using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
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

	public float m_DesignWidth = 1920.0f;
	public float m_DesignHeight = 1080.0f;
	public int m_ButtonFontSize = 48;
	public int m_BoxFontSize = 48;

	public void OnGUI()
	{
		//Calculate change aspects
		float resX = (float)(Screen.width) / m_DesignWidth;
		float resY = (float)(Screen.height) / m_DesignHeight;

		//Set matrix
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));

		//declare font styles
		GUIStyle l_ButtonStyle = new GUIStyle(GUI.skin.button);
		GUIStyle l_BoxStyle = new GUIStyle(GUI.skin.box);
		l_BoxStyle.fontSize = m_BoxFontSize;
		l_ButtonStyle.fontSize = m_ButtonFontSize;

		//Menu layout
		GUI.BeginGroup(new Rect(m_DesignWidth / 2 - 500.0f / 2, m_DesignHeight / 2 - 600f / 2, 500, 600));
		GUI.Box(new Rect(0,0, 500, 600), "Menu", l_BoxStyle);
		if (GUI.Button(new Rect(50, 100, 400, 100), "Start", l_ButtonStyle))
		{
			StartGame();
		}
		if (GUI.Button(new Rect(50, 220, 400, 100), "Help", l_ButtonStyle))
		{
			ShowHelp();
		}
		if (GUI.Button(new Rect(50, 340, 400, 100), "Credits", l_ButtonStyle))
		{
			ShowCredits();
		}
		if (GUI.Button(new Rect(50, 460, 400, 100), "Quit", l_ButtonStyle))
		{
			Quit();
		}
		GUI.EndGroup();
	}

	public void ShowCredits()
	{
		// show credits scene or GUI
		GM.SetGameState(GameState.CREDITS);
		//Debug.Log(GM.gameState);
	}

	public void StartGame()
	{
		//start game scene
		if(GM.m_GamesPlayed == 0)
        {
			Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
			GM.SetGameState(GameState.GAME);
			Initiate.Fade(GM.gameState.ToString(), Color.black, 2f);
		}
        else
        {
			Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando escena de modificadores");
			GM.SetGameState(GameState.MODIFIERS);
			Initiate.Fade("MODIFIERS", Color.black, 2f);
		}

		
	}

	public void ShowHelp()
	{
		// show Help scene or GUI
		GM.SetGameState(GameState.HELP);
		//Debug.Log(GM.gameState);
	}

	public void Quit()
	{
		//Debug.Log("Quit!");
		Application.Quit();
	}
}