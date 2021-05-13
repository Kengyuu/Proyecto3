using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnableTraps : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;

    [Header("Trap Shoot")]
    public LayerMask m_ShootLayers;
    public float m_TrapDetectionDistance = 5f;


    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerMovement.m_InputSystem.Gameplay.EnableTrap.triggered)
        {
            CheckForward();
        }
    }

    private void CheckForward()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, m_TrapDetectionDistance, m_ShootLayers))
        {
            if (hit.collider.CompareTag("TrapDeactivated"))
            {
                Debug.Log($"Trampa a distancia adecuada: {m_TrapDetectionDistance}");
                hit.transform.GetComponent<PassiveTrap>().EnableTrap();
            }
        }
    }
}