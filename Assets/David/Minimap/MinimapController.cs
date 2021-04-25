using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    public Transform m_Player;

    void Start()
    {
        if(m_Player == null)
            m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = m_Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Si queremos que el minimapa rote con el player:
        transform.rotation = Quaternion.Euler(0f, m_Player.eulerAngles.y, 0f);
    }
}
