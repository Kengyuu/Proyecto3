using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	GameManager GM;
    ScoreManager m_ScoreManager;

	void Awake()
	{
		GM = GameManager.Instance;
        GM.m_Player = GameObject.FindGameObjectWithTag("Player");
        GM.m_Enemy = GameObject.FindGameObjectWithTag("Enemy");
        GM.m_GameObjectSpawner = GameObject.FindObjectOfType<GameObjectSpawner>();
        GM.m_WaypointsList = GameObject.FindObjectOfType<RoomSpawner>();
        GM.m_GamesPlayed++;
        if(m_ScoreManager == null)
        {
            m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        }
        
        Cursor.lockState = CursorLockMode.Locked;

        GM.SetGameState(GameState.GAME);
    }
}
