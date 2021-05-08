using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    private PlayerController m_PlayerController;
    private PlayerHiddenPrayer m_PlayerHiddenPrayer;
    private GameManager GM;

    [Header("Dash")]
    public Vector3 m_DashDirection;
    public float m_DashForce = 15f;
    public float m_DashMaxCooldown = 2f;
    public float m_DashCooldown = 0f;
    public bool m_PlayerCanDash = true;

    [Header("Dash Evasion")]
    public float m_MaxDashEvasionTime = 0.5f;
    public bool m_DashEvadeAttacks = false;

    [Header("DashNoise")]
    public float m_DashNoise = 30f;

    [Header("Debug")]
    [SerializeField] bool m_DashOnCooldown = false;
    

    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerController = GetComponent<PlayerController>();

        if (GM == null) GM = GameManager.Instance;

        //GM.OnStateChange += StateChanged;

        if (m_MaxDashEvasionTime > m_DashMaxCooldown) m_MaxDashEvasionTime = m_DashMaxCooldown;
    }


    /*private void StateChanged()
    {
        switch (GM.gameState)
        {
            case GameState.MAP:
                m_PlayerCanDash = false;
                break;
            case GameState.GAME:
                m_PlayerCanDash = true;
                break;
        }
    }*/

    private void Update()
    {
        if (!m_DashOnCooldown && m_PlayerCanDash)
        {
            if (m_PlayerMovement.m_InputSystem.Gameplay.Dash.triggered && m_PlayerMovement.m_CharacterController.isGrounded)
            {
                Dash();
            }
        }

        m_DashCooldown -= Time.deltaTime;

        if(m_DashCooldown <= (m_DashMaxCooldown - m_MaxDashEvasionTime))
        {
            m_DashEvadeAttacks = false;
        }
        if(m_DashCooldown <= 0f)
        {
            m_DashCooldown = 0f;
            ResetDash();
        }

    }

    private void Dash()
    {
        if (m_PlayerHiddenPrayer.m_IsPlayerVisibleToEnemy)
        {
            GM.PlayerNoise(m_DashNoise);
            Debug.Log("Hago ruido al Dashear");
        }
        else
        {
            Debug.Log("NO HAGO RUIDO al Dashear =D");
        }
            
            

        m_DashOnCooldown = true;
        m_DashCooldown = m_DashMaxCooldown;
        m_DashEvadeAttacks = true;
        //Invoke("ResetDash", m_DashMaxCooldown);
        m_DashDirection = m_PlayerMovement.m_InputSystem.Gameplay.Move.ReadValue<Vector2>();
        Vector3 l_DirectionCorrected = new Vector3(m_DashDirection.x, 0f, m_DashDirection.y);
        l_DirectionCorrected = transform.TransformVector(l_DirectionCorrected);
        m_PlayerMovement.AddForceX(l_DirectionCorrected, m_DashForce);
    }

    private void ResetDash()
    {
        m_DashDirection = Vector3.zero;
        m_DashOnCooldown = false;
    }
}
