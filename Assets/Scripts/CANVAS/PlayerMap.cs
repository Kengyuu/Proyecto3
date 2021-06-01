using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMap : MonoBehaviour
{
    private GameManager GM;
    private PlayerMovement m_PlayerMovement;

    [Header("Map")]
    public GameObject m_Map;
    public float m_CorpseShowRadius = 50f;

    [Header("Debug")]
    [SerializeField] bool map_status = false;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GM.GetPlayer().GetComponent<PlayerMovement>();
        m_Map.SetActive(map_status);

        //GM.OnStateChange += StateChanged;
    }

/*    private void OnDestroy()
    {
        GM.OnStateChange -= StateChanged;
    }*/

/*    private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.GAME:
                
                DisableMap();
                break;

            case GameState.MAP:
                EnableMap();
                break;
        }
    }*/


    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.Map.triggered || m_PlayerMovement.m_InputSystem.Minimap.Map.triggered)
        {
            map_status = !map_status;
            if (map_status)
            {
                UpdateMapCorpses();
                GM.SetGameState(GameState.MAP);
                //m_Map.SetActive(true);
            }
            else
            {
                GM.SetGameState(GameState.GAME);
                //m_Map.SetActive(false);
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_PlayerMovement.transform.position, m_CorpseShowRadius);
    }*/

    private void EnableMap()
    {
        /*m_Map.SetActive(true);
        UpdateMapCorpses();*/



        /*m_PlayerMovement.m_InputSystem.Gameplay.Move.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Disable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Disable();*/
    }

    private void DisableMap()
    {
        /*m_Map.SetActive(false);*/
        /*m_PlayerMovement.m_InputSystem.Gameplay.Move.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Dash.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Run.Enable();
        m_PlayerMovement.m_InputSystem.Gameplay.Shoot.Enable();*/
    }


    public void UpdateMapCorpses()
    {
        Debug.Log("He sido llamado");
        foreach (GameObject corpse in GameManager.Instance.GetGameObjectSpawner().deadBodys)
        {

            if (corpse.activeSelf)
            {
                //Debug.Log($"Cuerpo en el radio con distancia: {l_Distance} y est� {corpse.activeSelf}");
                foreach (Transform t in corpse.transform)
                {
                    float l_Distance = Vector3.Distance(corpse.transform.position, m_PlayerMovement.transform.position);
                    if (t.gameObject.CompareTag("Map") && l_Distance <= m_CorpseShowRadius)
                    {
                        t.gameObject.layer = LayerMask.NameToLayer("UI");
                    }
                    else if( t.gameObject.CompareTag("Map") && l_Distance >= m_CorpseShowRadius )
                    {
                        t.gameObject.layer = LayerMask.NameToLayer("Void");
                    }
                }
            }
        }
    }
}
