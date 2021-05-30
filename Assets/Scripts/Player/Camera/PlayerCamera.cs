using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    //private PlayerInputSystem m_InputSystem;
    //[SerializeField] private float m_MouseSensitivity = 25f;
    //private Vector2 m_InputLook;
    //private float m_RotationX;
    //private Transform m_Player;

    //Only in Unity Editor:
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    private bool m_AngleLocked = false;
    private bool m_AimLocked = true;


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

    private void Awake()
    {
        //m_InputSystem = new PlayerInputSystem();
        //Cursor.lockState = CursorLockMode.Locked;

        if (m_Camera == null)
            m_Camera = Camera.main;

        //m_Player = transform.parent;
    }

    private void Start()
    {
        //Assing Yaw and Pitch vars
        m_Yaw = transform.rotation.eulerAngles.y;
        m_Pitch = m_PitchController.localRotation.eulerAngles.x;

        //Lock the cursor @ start
        Cursor.lockState = CursorLockMode.Locked;
    }

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

        /*if (!m_AngleLocked)
        {
            m_InputLook = m_InputSystem.Gameplay.Look.ReadValue<Vector2>();
            float MouseX = m_InputLook.x * m_MouseSensitivity * Time.deltaTime;
            float MouseY = m_InputLook.y * m_MouseSensitivity * Time.deltaTime;
            m_RotationX -= MouseY;
            m_RotationX = Mathf.Clamp(m_RotationX, -80f, 80f);

            transform.localRotation = Quaternion.Euler(m_RotationX, 0f, 0f);

            m_Player.Rotate(Vector3.up * MouseX);

            float l_MouseAxisX = Input.GetAxis("Mouse X");
            float l_MouseAxisY = Input.GetAxis("Mouse Y");
        }*/

        //Update yaw & pitch (Camera)
        //if(GameManager.Instance.gameState != GameState.PAUSE)
        if(GetComponent<PlayerMovement>().m_InputSystem.Gameplay.enabled)
        {
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

    }

/*    private void OnEnable()
    {
        m_InputSystem.Enable();
    }

    private void OnDisable()
    {
        m_InputSystem.Disable();
    }*/
}
