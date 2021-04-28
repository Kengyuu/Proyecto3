using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviours : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EnemyType {MAIN, CORPSESEARCHER, TRAPDEACTIVATOR, CORPSEHIDER}

    public EnemyType myType;
    
    Enemy_BLACKBOARD blackboard;
    NavMeshAgent navMesh;
    void Start()
    {
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        navMesh = GetComponent<NavMeshAgent>();
        switch(transform.tag)
        {
            case "Enemy":
                myType = EnemyType.MAIN;
                break;
            case "CorpseOrb":
                myType = EnemyType.CORPSESEARCHER;
                break;
            case "TrapOrb":
                myType = EnemyType.TRAPDEACTIVATOR;
                break;
            case "HideOrb":
                myType = EnemyType.CORPSEHIDER;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SearchObject(string objectType)
    {
        /*switch(objectType)
        {
            case "Corpse":
                GameObject corpse = DetectionFunctions.FindObjectInArea(gameObject, objectType, blackboard.corpseDetectionRadius);
                return corpse;
            case "PasiveTrap":
                blackboard.trap = DetectionFunctions.FindObjectInArea(gameObject, objectType, blackboard.trapDetectionRadius);
                return blackboard.trap;
            default:
                return null;
        }*/

        return DetectionFunctions.FindObjectInArea(gameObject, objectType, blackboard.corpseDetectionRadius);
        
    }
    public void AddCorpseToScore()
    {
        GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().enemyCorpses++;
        GameManager.Instance.m_ScoreManager.SetEnemyCorpses(GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().enemyCorpses);
        GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().remainingCorpses--;
        GameManager.Instance.m_ScoreManager.SetRemainingCorpses(GameManager.Instance.GetEnemy().GetComponent<Enemy_BLACKBOARD>().remainingCorpses);
    }

    public void SearchPlayer()
    {
        if (DetectionFunctions.FindObjectInArea(gameObject,"Player", blackboard.playerDetectionRadius))
        {
            gameObject.GetComponent<FSM_EnemyPriority>().playerSeen = true;
        }
    }

    public GameObject PickRandomWaypoint()
    {
        int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        GameObject target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        return target;
    }

    public GameObject PickRandomWaypointOrb()
    {
        int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        GameObject target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));

        while(blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition].transform.position == 
            GameManager.Instance.GetEnemy().GetComponent<FSM_EnemyPriority>().enemy.destination)
        {
            spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
            target = blackboard.waypointsList.GetComponent<RoomSpawner>().spawners[spawnPosition];
            navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            Debug.Log("Holi");
        }
        
        return target;
    }

    public void GrabCorpse(GameObject target)
    {
        GameManager.Instance.m_gameObjectSpawner.ClearBodys(target.GetComponent<CorpseControl>().spawnPosition);
        blackboard.cooldownToGrabCorpse = 3f;
    }

    public GameObject ReturnToEnemy()
    {
        GameObject target = GameManager.Instance.GetEnemy();
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        return target;
    }

    public void DeactivateTrap(GameObject trap)
    {
        //FALTA HACER
        trap.SetActive(false);
    }

    public void ConvertTrap(GameObject trap)
    {
        //FALTA HACER
    }

    public void CreateAreaInvisibility(GameObject corpse)
    {
        //FALTA HACER
        /*List<GameObject> targets = DetectionFunctions.FindObjectsInArea(gameObject, "Corpse", blackboard.areaOfEffectInvisible);
        if(targets != null)
        {
            if(targets.Count > 0)
            {
                foreach(GameObject t in targets)
                {
                    t.GetComponent<CorpseControl>().changeVisibility = true;
                }
            }
        }*/
        //corpse.GetComponent<MeshRenderer>().material = corpse.GetComponent<CorpseControl>().transparentMaterial;
        //Debug.Log(corpse.GetComponent<MeshRenderer>().material.name);
        //Debug.Log(corpse.name);
        corpse.GetComponent<MeshRenderer>().enabled = false;
    }

    public void ReturnCorpseToNormal(GameObject corpse)
    {
        corpse.GetComponent<MeshRenderer>().material = corpse.GetComponent<CorpseControl>().originalMaterial;
        corpse.GetComponent<MeshRenderer>().enabled = true;
    }
}
