using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Transform m_Player;
   

    void Start()
    {
        if(m_Player == null)
            m_Player = GameManager.Instance.GetPlayer().transform;
    }

   
}
