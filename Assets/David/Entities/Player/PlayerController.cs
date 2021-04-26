using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Inputs")]
    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;
    public KeyCode m_JumpKeyCode = KeyCode.Space;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;
    public KeyCode m_DashKeyCode = KeyCode.Space;
    public KeyCode m_ShootKeyCode = KeyCode.Mouse0;
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    
    [Header("Player Data")]
    [Range(0, 20f)] public float m_Speed = 12f;
    [Range(0, 5f)] public float m_RunSpeedMultiplier = 1.5f;
    [SerializeField] private bool m_OnGround = false;
    private CharacterController m_CharacterController = null;
    [SerializeField] bool m_PlayerStunned = false;
    private PlayerBlackboard blackboard;

    

    [Header("Player Dash")]
    public Vector3 m_dashDirection;
    public const float m_maxDashTime = 1.0f;
    public float m_dashDistance = 10;
    public float m_dashStoppingSpeed = 0.1f;
    public bool m_PlayerDashing = false;
    float currentDashTime = m_maxDashTime;
    float dashSpeed = 6;
    int m_DashDirection = 0;
    float m_DashCooldown = 0f;
    public float m_DashMaxCooldown = 3f;

    [Header("Player Shoot")]
    public LayerMask m_ShootLayers;
    public float m_MaxShootDistance = 50f;
    public float m_ShootCastingTime = 2f;
    [SerializeField] bool m_IsPlayerShooting = false;


    [Header("Camera Controller")]
    public Camera m_Camera = null;
    public Transform m_PitchController = null;
    //Camera settings
    public float m_MinPitch = -45;
    public float m_MaxPitch = 50f;
    public bool m_InvertHorizontalAxis = false;
    public bool m_InvertVerticalAxis = true;
    public float m_YawRotationalSpeed = 2f;
    public float m_PitchRotationalSpeed = 2f;
    private float m_Yaw = 0.0f;
    private float m_Pitch = 0.0f;
    //Only in Unity Editor:
    private bool m_AngleLocked = false;
    private bool m_AimLocked = true;

    [Header("Player Jump data")]
    [Range(0, 1f)] public float m_AirSpeedFactor = 0.75f; //Reduce movement while in air
    [Range(0, 10f)] public float m_FallSpeedMultiplier = 5f;
    [Range(0, 30f)] public float m_JumpForce = 15f;
    [Range(0, 0.5f)] public float m_JumpThresholdSinceLastGround = 0.1f;
    private float m_TimeSinceLastGround = 0.0f;
    private float m_VerticalSpeed = 0.0f;


    private void Awake()
    {
        //Setup everything in case is hasn't been set on Editor
        if (m_CharacterController == null)
            m_CharacterController = GetComponent<CharacterController>();
        if (m_Camera == null) 
            m_Camera = Camera.main;
        if (m_PitchController == null) 
            m_PitchController = GameObject.FindGameObjectWithTag("PitchController").transform; //Need tag
        if (blackboard == null) blackboard = GetComponent<PlayerBlackboard>();
    }//End Awake

    void Start()
    {
        //Assing Yaw and Pitch vars
        m_Yaw = transform.rotation.eulerAngles.y;
        m_Pitch = m_PitchController.localRotation.eulerAngles.x;

        //Set V_Speed to initial 0f
        m_VerticalSpeed = 0.0f;

        //Lock the cursor @ start
        Cursor.lockState = CursorLockMode.Locked;
    }//End Start

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode))
            m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
#endif
        //Player FPS
        if (!GameManager.Instance.GetIsCameraLocked()) CameraMovement(); //Allow Camera movement with mouse
        if (GameManager.Instance.GetPlayerCanMove()) //Allow Player movement
        {
            PlayerMovement();
            PlayerDash();
            PlayerShoot();
        }

        //TEST CODE:
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("RECIBO DAÑO JEJE");
            GetDamage(-1);
        }

        //END TEST CODE

    }//End Update

    private void PlayerDash()
    {
        if (m_DashCooldown > 0f) m_DashCooldown -= Time.deltaTime;
        else m_DashCooldown = 0f;

        if (Input.GetKeyDown(m_DashKeyCode) && !m_PlayerDashing && m_DashCooldown == 0f)
        {
            currentDashTime = 0;
            m_DashCooldown = m_DashMaxCooldown;

            if (Input.GetKey(m_UpKeyCode))
            {
                m_DashDirection = 1;
            }
            if (Input.GetKey(m_RightKeyCode))
            {
                m_DashDirection = 2;
            }
            if (Input.GetKey(m_LeftKeyCode))
            {
                m_DashDirection = 3;
            }
            if (Input.GetKey(m_DownKeyCode))
            {
                m_DashDirection = 0;
            }

            m_PlayerDashing = true;
        }

        if (currentDashTime < m_maxDashTime)
        {
            if (m_DashDirection == 1)
            {
                m_dashDirection = transform.forward * m_dashDistance;
            }
            else if (m_DashDirection == 2)
            {
                m_dashDirection = transform.right * m_dashDistance;
            }
            else if (m_DashDirection == 3)
            {
                m_dashDirection = -transform.right * m_dashDistance;
            }
            else if (m_DashDirection == 0)
            {
                m_dashDirection = -transform.forward * m_dashDistance;
            }

            currentDashTime += m_dashStoppingSpeed;
        }
        else
        {
            m_dashDirection = Vector3.zero;
            m_PlayerDashing = false;
            m_DashDirection = 0;
        }
        m_CharacterController.Move(m_dashDirection * Time.deltaTime * dashSpeed);
    }//End PlayerDash()



    private void PlayerShoot()
    {
        if (Input.GetKeyDown(m_ShootKeyCode) && !m_IsPlayerShooting)
        {
            Debug.Log("INICIO CASTEO DEL DISPARO");
            //GameManager.Instance.SetPlayerCanMove(false);
            //GameManager.Instance.SetIsCameraLocked(true);
            m_IsPlayerShooting = true;
            Invoke("Shoot", m_ShootCastingTime);

        }
    }

    private void Shoot()
    {
        Debug.Log("REALIZO EL RAYCAST DE DISPARO");
        RaycastHit hit;

        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, m_MaxShootDistance, m_ShootLayers))
        {
            string tag = hit.collider.transform.tag;

            switch (tag)
            {
                case "Corpse":
                    Debug.Log("Corpse hitted, changing tag + disabling GameObject");
                    //hit.transform.tag = "CorpseDisabled";
                    //hit.transform.gameObject.SetActive(false);
                    GameManager.Instance.m_gameObjectSpawner.ClearBodys(hit.collider.GetComponent<CorpseControl>().spawnPosition);
                    blackboard.m_PlayerCorpses++;
                    GameManager.Instance.m_ScoreManager.SetPlayerCorpses(blackboard.m_PlayerCorpses);

                    //ESTO ES SOLO PARA TESTEO - ESTA DUPLICADO ACTUALMENTE!!!!!!!!!!!!!!!!!
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_BLACKBOARD>().remainingCorpses--;
                    GameManager.Instance.m_ScoreManager.SetRemainingCorpses(GameManager.Instance.m_ScoreManager.GetRemainingCorpses() - 1);
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_BLACKBOARD>().playerCorpses++;

                    break;

                case "WeakPoint":
                    Debug.Log("Enemy WeakPoint hitted, calling Enemy TakeDamage()");
                    hit.collider.transform.GetComponent<WeakPoint>().TakeDamage();
                    break;
            }
        }
        //GameManager.Instance.SetPlayerCanMove(true);
        //GameManager.Instance.SetIsCameraLocked(false);
        m_IsPlayerShooting = false;
    }


    private void CameraMovement()
    {
        //Update yaw & pitch (Camera)
        float l_MouseAxisX = Input.GetAxis("Mouse X");
        float l_MouseAxisY = Input.GetAxis("Mouse Y");

        //Invert camera movement from Editor
        if (m_InvertHorizontalAxis) l_MouseAxisX = -l_MouseAxisX;
        if (m_InvertVerticalAxis) l_MouseAxisY = -l_MouseAxisY;

        //If the Angle movement isn't locked, allow moving the camera around
        if (!m_AngleLocked)
        {
            //set the new vars to move the camera...
            m_Yaw = m_Yaw + l_MouseAxisX * m_YawRotationalSpeed;
            m_Pitch = m_Pitch + l_MouseAxisY * m_PitchRotationalSpeed;
            m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);
        }

        //Move the camera
        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
    }

    private void PlayerMovement()
    {
        //Player movement...
        Vector3 l_Movement = Vector3.zero;

        Vector3 l_Right = transform.right;
        Vector3 l_Forward = transform.forward;


        l_Right.y = 0.0f;
        l_Right.Normalize();
        l_Forward.y = 0.0f;
        l_Forward.Normalize();

        //Player inputs for move around
        if (Input.GetKey(m_RightKeyCode))
            l_Movement = l_Right;
        if (Input.GetKey(m_LeftKeyCode))
            l_Movement = -l_Right;
        if (Input.GetKey(m_UpKeyCode))
            l_Movement += l_Forward;
        if (Input.GetKey(m_DownKeyCode))
            l_Movement += -l_Forward;

        //Jumping
        if (Input.GetKeyDown(m_JumpKeyCode) &&/* m_OnGround*/ m_TimeSinceLastGround < m_JumpThresholdSinceLastGround)
            m_VerticalSpeed = m_JumpForce;

        l_Movement.Normalize();
        m_VerticalSpeed = m_VerticalSpeed + Physics.gravity.y * m_FallSpeedMultiplier * Time.deltaTime;
        float l_Speed = m_Speed;

        //Reduce movement speed while on Air (jumping or falling)
        if (!m_OnGround)
            l_Speed = l_Speed * m_AirSpeedFactor;
        else
            l_Speed = m_Speed;

        //Sprint input
        if (Input.GetKey(m_RunKeyCode))
            l_Speed *= m_RunSpeedMultiplier;

        //Final assignment to player movement
        l_Movement = l_Movement * l_Speed * Time.deltaTime;
        l_Movement.y = m_VerticalSpeed * Time.deltaTime;

        //Check collision while moving and DO the Movement
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_Movement);

        m_TimeSinceLastGround += Time.deltaTime; //Time to check last time on ground

        //Check if player collides from below with anything (no matter sides) and set vertical speed to 0 to start falling
        //m_OnGround = (l_CollisionFlags & CollisionFlags.CollidedBelow) != 0;
        m_OnGround = m_CharacterController.isGrounded;

        if (m_OnGround) m_TimeSinceLastGround = 0.0f; //Player IS on ground
        if (m_OnGround || ((l_CollisionFlags & CollisionFlags.CollidedAbove) != 0 && m_VerticalSpeed >= 0f)) m_VerticalSpeed = 0.0f;
    }


    private void RestartGame()
    {
        //TBD
    }

    public void GetDamage(int dmg)
    {
        if(blackboard.m_Life > 0) blackboard.m_Life += dmg;

        if (blackboard.m_Life == 0)
        {
            //GAME OVER HERE
            Debug.Log("GAME OVER CORPSES: " + GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().enemyCorpses);
            if(GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().enemyCorpses >= 10f)
            {
                Debug.Log("GAME OVER ----------- CAGASTE");
                m_PlayerStunned = true;
                GameManager.Instance.SetPlayerCanMove(false);
                GameManager.Instance.SetIsCameraLocked(true);
            }
            else 
            {
                if (!m_PlayerStunned)
                {

                    m_PlayerStunned = true;
                    GameManager.Instance.SetPlayerCanMove(false);
                    GameManager.Instance.SetIsCameraLocked(true);
                    Invoke("RestoreLife", 1.5f);
                    if (blackboard.m_PlayerCorpses > 0)
                    {
                        int lostPlayerCorpses = Mathf.Max(1, Mathf.RoundToInt(blackboard.m_PlayerCorpses / 3));
                        blackboard.m_PlayerCorpses -= lostPlayerCorpses;
                        GameManager.Instance.m_ScoreManager.SetPlayerCorpses(blackboard.m_PlayerCorpses);
                        GameManager.Instance.m_ScoreManager.SetRemainingCorpses(GameManager.Instance.m_ScoreManager.GetRemainingCorpses() + lostPlayerCorpses);
                        GameManager.Instance.m_gameObjectSpawner.SpawnBodys(lostPlayerCorpses);
                    }
                    
                }
            }
        }
    }

    private void RestoreLife()
    {
        blackboard.m_Life = blackboard.m_MaxLife;
        GameManager.Instance.SetPlayerCanMove(true);
        GameManager.Instance.SetIsCameraLocked(false);
        m_PlayerStunned = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        /*
        //For collecting items on floor
        if (col.CompareTag("Item"))
        {
            col.GetComponent<Item>().Pick();
        }
        */
    }
}
