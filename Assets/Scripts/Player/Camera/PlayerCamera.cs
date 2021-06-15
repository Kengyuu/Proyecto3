using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
   
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

    private PlayerInputSystem m_PlayerInput;

    private void Awake()
    {
        if (m_Camera == null)
            m_Camera = Camera.main;
    }

    private void Start()
    {
        m_Yaw = transform.rotation.eulerAngles.y;
        m_Pitch = m_PitchController.localRotation.eulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;

        m_PlayerInput = GetComponent<PlayerMovement>().m_InputSystem;
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

        if(m_PlayerInput.Gameplay.enabled || m_PlayerInput.Minimap.enabled)
        {
            float l_MouseAxisX = Input.GetAxis("Mouse X");
            float l_MouseAxisY = Input.GetAxis("Mouse Y");

            if (m_InvertHorizontalAxis) l_MouseAxisX = -l_MouseAxisX;
            if (m_InvertVerticalAxis) l_MouseAxisY = -l_MouseAxisY;

            if (!m_AngleLocked)
            {
                m_Yaw = m_Yaw + l_MouseAxisX * m_YawRotationalSpeed;
                m_Pitch = m_Pitch + l_MouseAxisY * m_PitchRotationalSpeed;
                m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);
            }

            transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
            m_PitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
        }

    }

}
