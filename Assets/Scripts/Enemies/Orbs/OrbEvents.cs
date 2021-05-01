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

    public event SpawnOrb spawnOrb;



    public void ManageOrbs()
    {
        spawnOrb?.Invoke(GameManager.Instance.m_ScoreManager.GetPlayerCorpses());
    }


}
