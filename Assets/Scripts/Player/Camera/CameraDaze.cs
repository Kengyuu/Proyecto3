using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDaze : MonoBehaviour
{
    private Transform m_Player;

    void Start()
    {
        m_Player = transform.parent;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(5f * Mathf.Sin(Time.time * 5f), 0f, 0f);

        m_Player.Rotate(Vector3.up * 20f *Mathf.Sin(Time.time * 5f) * Time.deltaTime);
    }

    private void OnEnable()
    {
       // Debug.Log("Daze activado");
    }

    private void OnDisable()
    {
      //  Debug.Log("Daze desactivado");
    }
}
