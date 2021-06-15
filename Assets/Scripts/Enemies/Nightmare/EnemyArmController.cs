using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyArmController : MonoBehaviour
{
    public NavMeshAgent enemyNavMesh;

    public Enemy_BLACKBOARD blackboard;
    public float XForceImpulseDamage = 5f;
    public float YForceImpulseDamage = 5f;
    public bool canDoDamage = true; 

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1, gameObject, XForceImpulseDamage, YForceImpulseDamage);
            if (other.GetComponent<PlayerController>().m_PlayerStunned)
            {
                 blackboard.animatorController.PlayerStunned();
 
            }
        }
    }

    IEnumerator WaitToGetStunned()
    {
        enemyNavMesh.isStopped = true;
        yield return new WaitForSeconds(6);
        enemyNavMesh.isStopped = false;
        
    }
}
