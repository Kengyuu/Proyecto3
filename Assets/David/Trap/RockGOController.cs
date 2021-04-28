using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGOController : MonoBehaviour
{
    public float m_FallSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * m_FallSpeed * Vector3.down);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("bola impacta con enemigo");
            FSM_EnemyPriority target = col.GetComponent<FSM_EnemyPriority>();
            if (target != null)
            {
                Debug.Log("Enemigo estuneado por BOLA DESDE EL CIELO");
                target.GetStunned();
                Destroy(gameObject);
            }
        }
        else if(col.CompareTag("Player"))
        {
            Debug.Log("Collide con player");
            Destroy(gameObject);
        }
    }
}
