using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    //private PlayerController m_PlayerController;
    private PlayerSpecialAbilities m_PlayerHiddenPrayer;
    private GameManager GM;
    private SoundManager SM;
    [Header("Dash")]
    public Vector3 m_DashDirection;
    public float m_DashForce = 15f;
    public float m_DashMaxCooldown = 2f;
    public float m_DashCooldown = 0f;
    public bool m_PlayerCanDash = true;
    private HudController hud;

    [Header("Dash Evasion")]
    public float m_MaxDashEvasionTime = 0.5f;
    public bool m_DashEvadeAttacks = false;

    [Header("DashNoise")]
    public float m_DashNoise = 10f;

    [Header("FMOD Events")]
    public string dashEvent;

    [Header("Debug")]
    [SerializeField] bool m_DashOnCooldown = false;
    

    private void Start()
    {
        m_PlayerMovement = GetComponent<PlayerMovement>();
        //m_PlayerController = GetComponent<PlayerController>();
        m_PlayerHiddenPrayer = GetComponent<PlayerSpecialAbilities>();
        if (hud == null) hud = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (GM == null) GM = GameManager.Instance;
        if (SM == null) SM = SoundManager.Instance;
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
            if(GM.GetEnemy() != null)
            {
                Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer, false);
            }
            
        }
        if(m_DashCooldown <= 0f)
        {
            m_DashCooldown = 0f;
            ResetDash();
        }

    }

    private void Dash()
    {
        SM.PlaySound(dashEvent, transform.position);
        hud.hasDashed = true;
        if (m_PlayerHiddenPrayer.m_IsPlayerVisibleToEnemy && m_PlayerMovement.m_CharacterController.velocity.magnitude > 0.2f)
        {
            GM.PlayerNoise(m_DashNoise);
        }
            
            

        m_DashOnCooldown = true;
        m_DashCooldown = m_DashMaxCooldown;
        m_DashEvadeAttacks = true;
        //Invoke("ResetDash", m_DashMaxCooldown);
        m_DashDirection = m_PlayerMovement.m_InputSystem.Gameplay.Move.ReadValue<Vector2>();
        if(m_DashDirection.magnitude == 0)
        {
            m_DashDirection.y = 1;
        }
        Vector3 l_DirectionCorrected = new Vector3(m_DashDirection.x, 0f, m_DashDirection.y);
        l_DirectionCorrected = transform.TransformVector(l_DirectionCorrected);
        m_PlayerMovement.AddForceX(l_DirectionCorrected, m_DashForce);
        if(GM.GetEnemy() != null)
        {
            Physics.IgnoreLayerCollision(this.gameObject.layer, GM.GetEnemy().layer);
        }
        
    }

    private void ResetDash()
    {
        m_DashDirection = Vector3.zero;
        m_DashOnCooldown = false;
    }
}
