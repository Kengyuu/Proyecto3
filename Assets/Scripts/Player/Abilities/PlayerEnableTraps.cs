using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnableTraps : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;

    [Header("Trap Shoot")]
    public LayerMask m_ShootLayers;
    public float m_TrapDetectionDistance = 5f;
    private HudController M_HudController;

    [Header("FMOD Events")]
    public string repairEvent;


    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
    }
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
                M_HudController.hasRepaired = true;
                hit.transform.GetComponent<PassiveTrap>().EnableTrap();
                SoundManager.Instance.PlaySound(repairEvent, transform.position);

            }
        }
    }
}