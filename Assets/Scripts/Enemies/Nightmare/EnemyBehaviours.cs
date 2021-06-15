using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviours : MonoBehaviour
{
    
    public enum EnemyType {MAIN, CORPSESEARCHER, TRAPDEACTIVATOR, CORPSEHIDER}

    public EnemyType myType;
    
    Enemy_BLACKBOARD blackboard;
    NavMeshAgent navMesh;
    Orb_Blackboard blackboardOrb;
    public ScoreManager m_ScoreManager;

    GameManager GM;

    public LayerMask mask;
    void Start()
    {
        GM = GameManager.Instance;
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        blackboardOrb = GetComponent<Orb_Blackboard>();
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
    void Update()
    {
        
    }

    public GameObject SearchObject(string objectType, float detectionRadius)
    {
        return DetectionFunctions.FindObjectInArea(gameObject, objectType, detectionRadius);
    }

   
    public void AddCorpseToScore()
    {
        m_ScoreManager.AddEnemyCorpse();
    }

    public void SearchPlayer(float detectionRadius, float angleDetectionPlayer)
    {
        if(DetectionFunctions.PlayerInCone(blackboard.eyesPosition, GM.GetPlayer(), angleDetectionPlayer, detectionRadius, mask))
        {
            gameObject.GetComponent<EnemyPriorities>().playerSeen = true;
            gameObject.GetComponent<EnemyPriorities>().ChangePriority();
        }
    }

    public bool PlayerFound(float detectionRadius, float angleDetectionPlayer)
    {
        
      return DetectionFunctions.PlayerInCone(gameObject, GM.GetPlayer(), angleDetectionPlayer, detectionRadius, mask);
  
    }



    public GameObject PickRandomWaypoint()
    {
        int spawnPosition = Random.Range(0, blackboard.waypointsList.GetComponent<RoomSpawner>().spawners.Count);
        GameObject target = GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners[spawnPosition];
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        return target;
    }

    public GameObject PickRandomWaypointOrb()
    {
        int spawnPosition = Random.Range(0, GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners.Count);
        GameObject target = GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners[spawnPosition];
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));

        while(GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners[spawnPosition].transform.position == 
            GameManager.Instance.GetEnemy().GetComponent<NavMeshAgent>().destination)
        {
            spawnPosition = Random.Range(0, GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners.Count);
            target = GM.GetWaypointsList().GetComponent<RoomSpawner>().spawners[spawnPosition];
            navMesh.SetDestination(target.transform.position);
        }
        
        return target;
    }

    public void GrabCorpse(GameObject target, float cooldown)
    {
        GM.GetGameObjectSpawner().ClearBodys(target.GetComponent<CorpseControl>().spawnPosition);
        cooldown = 3f;
    }

   

    public GameObject ReturnToEnemy()
    {
        GameObject target = GameManager.Instance.GetEnemy();
        navMesh.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        return target;
    }

    public void DeactivateTrap(GameObject trap)
    {
        trap.GetComponent<PassiveTrap>().DisableTrap();
        trap.tag = "TrapDeactivated";
    }
    public void CreateAreaInvisibility(GameObject corpse)
    {
        corpse.GetComponent<MeshRenderer>().enabled = false;
    }

    public void ReturnCorpseToNormal(GameObject corpse)
    {
        corpse.GetComponent<MeshRenderer>().material = corpse.GetComponent<CorpseControl>().originalMaterial_Body;
        corpse.GetComponent<MeshRenderer>().enabled = true;
    }

 


}
