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

    [Header("FMOD Events")]
    public string mapSoundEvent;

    void Start()
    {
        GM = GameManager.Instance;
        m_PlayerMovement = GM.GetPlayer().GetComponent<PlayerMovement>();
        m_Map.SetActive(map_status);
    }
    void Update()
    {
        if ((m_PlayerMovement.m_InputSystem.Gameplay.Map.triggered || m_PlayerMovement.m_InputSystem.Minimap.Map.triggered) && GM.gameState != GameState.TUTORIAL)
        {
            map_status = !map_status;
            if (map_status)
            {
                UpdateMapCorpses();
                SoundManager.Instance.PlaySound(mapSoundEvent, transform.position);
                GM.SetGameState(GameState.MAP);
            }
            else

            {
                SoundManager.Instance.PlaySound(mapSoundEvent, transform.position);
                GM.SetGameState(GameState.GAME);
            }
        }
    }
    public void UpdateMapCorpses()
    {
        foreach (GameObject corpse in GameManager.Instance.GetGameObjectSpawner().deadBodys)
        {

            if (corpse.activeSelf)
            {
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
