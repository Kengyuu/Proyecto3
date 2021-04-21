using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent enemy;
    public GameObject target;

    float distanceToTarget;
    GameObject waypointsList;
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        waypointsList = GameObject.FindGameObjectWithTag("SpawnersContainer");
        int spawnPosition = Random.Range(0, waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        target = waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
        enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        //enemy.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z).magnitude;
        if(distanceToTarget > 0.5f)
        {
            enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            Debug.Log(distanceToTarget);
            //enemy.SetDestination(target.transform.position);
        }
        else
        {
            int spawnPosition = Random.Range(0, waypointsList.GetComponent<RoomSpawner>().spawners.Count);
            target = waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
            //enemy.SetDestination(target.transform.position);
            enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            Debug.Log("Holi");
        }
        
    }
}
