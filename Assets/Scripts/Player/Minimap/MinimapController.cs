using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    private Transform m_Player;
    //private PlayerMovement m_PlayerMovement;

    /*[Header("Camera Control")]
    public Camera m_MinMapCamera;
    public float m_ZoomIncrease = 10f;
    public float m_MinZoom = 10f;
    public float m_MaxZoom = 60f;*/


    //public float m_CorpseShowRadius = 50f;
    //public LayerMask m_CorpseMask;
    //public GameObjectSpawner m_CorpsesSpawned;

    void Start()
    {
        if(m_Player == null)
            m_Player = GameManager.Instance.GetPlayer().transform;

        //m_PlayerMovement = m_Player.GetComponent<PlayerMovement>();

        //Debug.Log($"Cuerpos actuales: {GameManager.Instance.GetGameObjectSpawner().deadBodys.Count}");

/*        foreach (GameObject corpse in GameManager.Instance.GetGameObjectSpawner().deadBodys)
        {
            float l_Distance = Vector3.Distance(corpse.transform.position, m_Player.position);
            if (l_Distance <= m_CorpseShowRadius)
            {
                Debug.Log($"Cuerpo en el radio con distancia: {l_Distance} y está {corpse.activeSelf}");
            }
        }*/
    }

    

    /*public void UpdateMapCorpses()
    {
        Debug.Log("He sido llamado");
        foreach (GameObject corpse in GameManager.Instance.GetGameObjectSpawner().deadBodys)
        {

            if (corpse.activeSelf)
            {
                //Debug.Log($"Cuerpo en el radio con distancia: {l_Distance} y está {corpse.activeSelf}");
                foreach (Transform t in corpse.transform)
                {
                    float l_Distance = Vector3.Distance(corpse.transform.position, m_Player.position);
                    if (t.gameObject.CompareTag("Map") && l_Distance <= m_CorpseShowRadius)
                    {
                        t.gameObject.layer = LayerMask.NameToLayer("UI");
                    }
                    else
                    {
                        t.gameObject.layer = LayerMask.NameToLayer("Void");
                    }
                }
            }
        }
    }*/

    private void LateUpdate()
    {

        //GameManager.Instance.GetGameObjectSpawner().deadBodys




        










        /*Vector3 newPosition = m_Player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;*/

        //Vector2 value = m_PlayerMovement.m_InputSystem.Gameplay.MouseScroll.ReadValue<Vector2>();

        //Debug.Log($"Scroll {value.y}");
        /*if(value.y != 0)
        {
            //float newValue = value.y / 12;
            float newZoom = 0;
            if (value.y > 0) newZoom = m_MinMapCamera.orthographicSize - m_ZoomIncrease;
            else newZoom = m_MinMapCamera.orthographicSize + m_ZoomIncrease;

            m_MinMapCamera.orthographicSize = Mathf.Clamp(newZoom, m_MinZoom, m_MaxZoom);
        }*/


        //Si queremos que el minimapa rote con el player:
        //transform.rotation = Quaternion.Euler(0f, m_Player.eulerAngles.y, 0f);
    }
}
