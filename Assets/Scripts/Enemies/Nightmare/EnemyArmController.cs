using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArmController : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent enemyNavMesh;
    public float XForceImpulseDamage = 5f;
    public float YForceImpulseDamage = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("hit");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1, gameObject, XForceImpulseDamage, YForceImpulseDamage);

            StartCoroutine(WaitToGetStunned());
        }
    }

    IEnumerator WaitToGetStunned()
    {
        enemyNavMesh.isStopped = true;
        yield return new WaitForSeconds(4);
        enemyNavMesh.isStopped = false;
        
    }
}
