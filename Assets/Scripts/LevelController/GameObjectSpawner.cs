using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject deadBody;
   // public GameObject player;
    //public GameObject enemy;
    public GameObject deadBodyContainer;
    public RoomsController roomsController;
    public int maxDeadBodysMap = 12; 
    public int maxDeadBodyRoom = 2;
    public List<GameObject> deadBodys = new List<GameObject>();
    int playerSpawnRoom = 5;
    int enemySpawnRoom = 5;
    int currentBodysSpawned = 0;
    List <string> roomTags = new List<string>();

    List<int> spawnersUsed = new List<int>();

    private ScoreManager m_ScoreManager;

    private GameManager GM;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        foreach (GameObject room in roomsController.rooms)
        {
            roomTags.Add(room.tag);
        }
        int spawnPosition = -1;
        maxDeadBodysMap = (int)m_ScoreManager.GetRemainingCorpses();
        
        /*if(GM.m_GamesPlayed == 0)
        {
            spawnPosition = 11;
            playerSpawnRoom = 2;
        }
        else
        {*/
            spawnPosition = Random.Range(0, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count);
            playerSpawnRoom = SpawnPlayer();
        //}

        
        
        

        SpawnEnemy();

        //Esto recoge todos los spawns posibles y crea un cadáver en unos de esos spawns. Los spawns los coge de una lista en RoomSpawner

        

        //Esto comprueba en qué sala está el tag, para añadir 1 a la cantidad de spawners usados en la sala que haya tocado

        for (int i = 0; i < deadBodys.Count; i++)
        {
            deadBodys[i].GetComponent<CorpseControl>().spawnPosition = i;
        }

        for (int i = 0; i < roomTags.Count; i++)
        {
            if(deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnPosition].tag == roomTags[i])
            {
                roomsController.currentSpawnersUsed[i]++;
            }
        }

        spawnersUsed.Add(spawnPosition);
        //Esto crea una cantidad de cadáveres pasada desde el editor en el objeto GameObjectSpawner

        SpawnBodys(maxDeadBodysMap, gameObject);
        m_ScoreManager.SetRemainingCorpses(maxDeadBodysMap);


    }

    void Update()
    {
        Debug.Log(spawnersUsed.Count + "Más te vale que funciones");
        // Test Spawn New Bodys
        if(Input.GetMouseButtonDown(0))
        {
            /*currentBodysSpawned = 10;
            spawnersUsed.Clear();
            for (int j = 0; j < roomTags.Count; j++)
            {
                roomsController.currentSpawnersUsed[j] = 0;
            }
            SpawnBodys(maxDeadBodysMap - currentBodysSpawned);*/
            /*if(spawnersUsed.Count > 0)
            {
                ClearBodys(spawnersUsed[0]);
            }*/
            
        }

    }

    public void ClearBodys(int spawnPosition)
    {
        if(spawnPosition >= 0)
        {
            if(deadBodys[spawnPosition].activeSelf)
            {
                deadBodys[spawnPosition].SetActive(false);
               // Debug.Log("Clear body " + spawnPosition);
            }
            
            spawnersUsed.Remove(spawnPosition);
            for (int i = 0; i < roomTags.Count; i++)
            {
                if(deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnPosition].tag == roomTags[i])
                {
                    roomsController.currentSpawnersUsed[i]--;
                }
            }
            currentBodysSpawned--;
        }
        
    }

    public int SpawnPlayer()
    {
        // No queremos jugador que el jugador spawnee en la sala central. Por tanto mientras la sala random sea 5 (sala central)
        // seguirá generando números. 

        playerSpawnRoom = Random.Range(1, 5);

        //Esto es para que spawnee el jugador

        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == playerSpawnRoom - 1)
            {
                // Instantiate(player, roomsController.rooms[i].transform.position, Quaternion.identity);
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().m_CharacterController.enabled = false;
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().transform.position = roomsController.rooms[i].transform.position;
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().transform.rotation = roomsController.rooms[i].transform.rotation;
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().m_CharacterController.enabled = true;
                
            }
        }
        //Debug.Log(playerSpawnRoom);
        return playerSpawnRoom;
    }


    public void SpawnEnemy()
    {
        //La suma de salas diamentralmente opuestas siempre suma 10, así que le restamos a 10 la sala del jugador para obtener
        //la sala del enemigo

        enemySpawnRoom = 5 - playerSpawnRoom;

        //Esto es para que spawnee el enemigo

        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == enemySpawnRoom - 1)
            {
                // Instantiate(enemy, roomsController.rooms[i].transform.position, Quaternion.identity);
                
                GameManager.Instance.GetEnemy().GetComponent<NavMeshAgent>().Warp(roomsController.rooms[i].transform.position);
               
            }
        }
        //Debug.Log(enemySpawnRoom + " enemy");
        //GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy_BLACKBOARD>().remainingCorpses = maxDeadBodysMap;
    }

    //numberBodys = maxDeadBodysMap - currentBodysSpawned;
    // First time --> currentBodysSpawned = 0, so --> numberBodys = maxDeadBodysMap
    public void SpawnBodys(int numberBodies, GameObject type)
    {
        int spawnPosition = -1;
        if(numberBodies > 0)
        {
            for (int i = 0; i < numberBodies; i++)
            {
                bool spawnable = false;
                while (spawnable == false)
                {
                    bool canSpawn = true;

                    //Hace lo mismo de arriba
                    spawnPosition = Random.Range(0, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count);

                    //Comprueba que no se haya usado esa posición de spawn.
                    for (int j = 0; j < spawnersUsed.Count; j++)
                    {
                        if(spawnPosition == spawnersUsed[j])
                            canSpawn = false;
                    }

                    //Comprueba si se ha alcanzado el límite de spawns por sala. El límite se asigna al valor maxDeadBodyRoom.

                    for (int j = 0; j < roomTags.Count; j++)
                    {
                        if(canSpawn && deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnPosition].tag == roomTags[j])
                        {
                            if(roomsController.currentSpawnersUsed[j] < maxDeadBodyRoom)
                            {
                                roomsController.currentSpawnersUsed[j]++;
                            }
                            else
                            {
                                canSpawn = false;
                            }
                        }
                    }

                    if(canSpawn)
                        spawnable = true;

                }
                
                spawnersUsed.Add(spawnPosition);
                deadBodys[spawnPosition].SetActive(true);
                
                currentBodysSpawned++;
            }
        }
        

    }
}
