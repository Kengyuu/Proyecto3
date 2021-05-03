using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbEvents : MonoBehaviour
{
    public static OrbEvents current;

    private void Awake()
    {
        current = this;
    }

    public delegate void SpawnOrb(float corpses);
    public delegate void RespawnOrb(GameObject orb);

    public event SpawnOrb spawnOrb;
    public event RespawnOrb respawnOrb;

    private ScoreManager m_ScoreManager;

    private void Start()
    {
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    public void ManageOrbs()
    {
        spawnOrb?.Invoke(m_ScoreManager.GetPlayerCorpses());
    }

    public IEnumerator RespawnOrbs(GameObject orb)
    {
        yield return new WaitForSeconds(3);
        respawnOrb?.Invoke(orb);
    }


}
