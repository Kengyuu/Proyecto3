using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    private SoundManager SM;

    [Header("Player movement")]
    public float m_PlayerWalkSpeed = 8f;
    public float m_TimeSinceLastFootstep = 0.5f;
    public float m_PlayerRunSpeed = 16f;
    public float m_TimeSinceLastBreath = 42f;
    private float m_TimeSinceLastRushedBreath = 0f;
    public float m_TimeSinceLastFootstepRun = 0.25f;
    private bool m_RunPressed = false;
    public float m_RunNoise = 10f;
    public float m_PlayerJumpForce = 8f;
    public float m_Gravity = 25f;
    public float m_Mass = 1f;
    
    private float m_VerticalVelocity;
    
    [HideInInspector] public CharacterController m_CharacterController;
    private HudController hud;
    public PlayerInputSystem m_InputSystem;
    private Vector2 m_InputMove;
    private Vector3 m_Movement;
    private Vector3 m_LastMovement;
    private Vector3 m_currentImpact;

    [Header("FMOD Events")]
    
    public string walkEvent;
    public string runEvent;
    public string breathEvent;
    private string rushedBreathEvent = "event:/1 HUNTER/013_Hunter_RushedBreath";
    //EventInstance rushedBreath;
    EventInstance breath;
    EventInstance rushedBreath;



    //DEBUG:
    [Header("Debug")]
    [SerializeField] private bool m_IsGrounded;

    private void Awake()
    {
        m_InputSystem = new PlayerInputSystem();
        m_CharacterController = GetComponent<CharacterController>();
        if (hud == null) hud = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
        if (SM == null) SM = SoundManager.Instance;
        //Check run button actions
        m_InputSystem.Gameplay.Run.started += ctx => RunAction();
        m_InputSystem.Gameplay.Run.canceled += ctx => RunAction();
    }

    private void Update()
    {
        //Ground debug
        m_IsGrounded = m_CharacterController.isGrounded;
        

        Move();
       
    }//End Update()

    private void Move()
    {
        float l_PlayerCurrentSpeed = m_PlayerWalkSpeed;
        m_Movement = Vector3.zero;
        m_TimeSinceLastBreath -= Time.deltaTime;
        if (m_TimeSinceLastBreath <= 0)
        {
            breath = SM.PlayEvent(breathEvent);
            m_TimeSinceLastBreath = 42f;
        }
      

        if (GetComponent<PlayerController>().m_PlayerStunned)
        {
            breath.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            UnityEngine.Debug.Log("STOP");
            m_TimeSinceLastBreath = 0;

        }
        if (m_InputSystem.Gameplay.Move.ReadValue<Vector2>().magnitude != 0)
        {
            
            if (!m_RunPressed)
            {
                m_TimeSinceLastFootstep -= Time.deltaTime;
                if (m_TimeSinceLastFootstep <= 0)
                {
                    SM.PlaySound(walkEvent, transform.position);
                    m_TimeSinceLastFootstep = 0.5f;
                }
            }
           
            hud.hasMoved = true;

        }
        
        m_InputMove = m_InputSystem.Gameplay.Move.ReadValue<Vector2>();
        m_Movement = (m_InputMove.y * transform.forward) + (m_InputMove.x * transform.right);


        if (m_CharacterController.isGrounded)
        {
            m_VerticalVelocity = -1;

            //Player Jump
            if (m_InputSystem.Gameplay.Jump.triggered)
            {
                m_VerticalVelocity = m_PlayerJumpForce;
            }

            //Player Run
            if (m_RunPressed)
            {
                breath.setPaused(true);
                m_TimeSinceLastRushedBreath -= Time.deltaTime;
                if (m_TimeSinceLastRushedBreath <= 0)
                {
                     //stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    rushedBreath = SM.PlayEvent(rushedBreathEvent);
                    m_TimeSinceLastRushedBreath = 3.7f;
                }

                m_TimeSinceLastFootstepRun -= Time.deltaTime;
                if (m_TimeSinceLastFootstepRun <= 0)
                {
                    SM.PlaySound(runEvent, transform.position);
                    m_TimeSinceLastFootstepRun = 0.25f;
                }
                GameManager.Instance.PlayerNoise(m_RunNoise);

                l_PlayerCurrentSpeed = m_PlayerRunSpeed;

                hud.hasRun = true;
            }
            else
            {
                rushedBreath.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                breath.setPaused(false);
            }
        }
        else
        {
            m_VerticalVelocity -= m_Gravity * Time.deltaTime;
            
            m_Movement = m_LastMovement; //This makes player move while jumping to the last vector known (locks player movement input)
        }

        m_Movement.y = 0;
        m_Movement.Normalize();
        m_Movement *= l_PlayerCurrentSpeed;
        m_Movement.y = m_VerticalVelocity;


        //If we have any force, add it to the current movement vector
        if(m_currentImpact.magnitude > 0.2f)
        {
            m_Movement += m_currentImpact;
        }

        //Move the player
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(m_Movement * Time.deltaTime);

        //Reduce force speed gradually
        m_currentImpact = Vector3.Lerp(m_currentImpact, Vector3.zero, 5f * Time.deltaTime);

        m_LastMovement = m_Movement;

        //If we hit any roof...
        if ((l_CollisionFlags & CollisionFlags.CollidedAbove) != 0) m_VerticalVelocity = 0.0f;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!m_CharacterController.isGrounded && hit.normal.y < 0.1f)
        {
            //Wall Jump - doesn't work on FPS
            if (m_InputSystem.Gameplay.Jump.triggered)
            {
                m_VerticalVelocity = m_PlayerJumpForce;
                m_Movement = Vector3.Reflect(m_Movement, hit.normal) * m_PlayerWalkSpeed;
            }
        }
    }

    //Add "fake forces" to character controller
    public void AddForceX(Vector3 direction, float magnitude)
    {
        m_currentImpact += direction.normalized * magnitude / m_Mass;
    }

    public void AddForceY(float magnitude)
    {
        m_currentImpact += Vector3.up * magnitude / m_Mass;
    }

    public void ResetImpact()
    {
        m_currentImpact = Vector3.zero;
    }

    // <- INPUT SYSTEM HELPERS ->
    void RunAction()
    {
        m_RunPressed = !m_RunPressed;

        
    }
    // <- END INPUT SYSTEM HELPERS ->

    private void OnEnable()
    {
        m_InputSystem.Enable();
    }

    private void OnDisable()
    {
        m_InputSystem.Disable();
    }
}
