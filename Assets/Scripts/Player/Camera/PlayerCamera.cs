using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private PlayerInputSystem m_InputSystem;
    [SerializeField] private float m_MouseSensitivity = 25f;
    private Vector2 m_InputLook;
    private float m_RotationX;
    private Transform m_Player;

    //Only in Unity Editor:
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    private bool m_AngleLocked = false;
    private bool m_AimLocked = true;

    private void Awake()
    {
        m_InputSystem = new PlayerInputSystem();
        Cursor.lockState = CursorLockMode.Locked;

        m_Player = transform.parent;
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

        if (!m_AngleLocked)
        { 
            m_InputLook = m_InputSystem.Gameplay.Look.ReadValue<Vector2>();

            float MouseX = m_InputLook.x * m_MouseSensitivity * Time.deltaTime;
            float MouseY = m_InputLook.y * m_MouseSensitivity * Time.deltaTime;

            m_RotationX -= MouseY;
            m_RotationX = Mathf.Clamp(m_RotationX, -80f, 80f);

            transform.localRotation = Quaternion.Euler(m_RotationX, 0f, 0f);

            m_Player.Rotate(Vector3.up * MouseX);
        }
    }

    private void OnEnable()
    {
        m_InputSystem.Enable();
    }

    private void OnDisable()
    {
        m_InputSystem.Disable();
    }
}
