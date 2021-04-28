using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrap : TrapController
{
    public Transform m_SpawnPoint;
    public GameObject m_SpawnItem;


    private void Start()
    {
        m_TrapActive = true;
    }

    public void SpawnTrap()
    {
        Instantiate(m_SpawnItem, m_SpawnPoint.position, Quaternion.identity);
    }

}
