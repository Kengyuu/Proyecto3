using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject deadBody;
    public GameObject deadBodyContainer;
    public RoomsController roomsController;
    public int maxDeadBodysMap = 12; 
    public int maxDeadBodyRoom = 2;
    public List<GameObject> deadBodys = new List<GameObject>();
    int playerSpawnRoom = 5;
    int enemySpawnRoom = 5;
    int currentBodysSpawned = 0;
    bool firstTime = true;
    List <string> roomTags = new List<string>();

    List<int> spawnersUsed = new List<int>();

    private ScoreManager m_ScoreManager;

    private GameManager GM;

    [Header("FMOD Events")]

    public string corpseApeearEvent;

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
        
        if(GM.m_GamesPlayed == 1)
        {
            spawnPosition = 7;  
        }
        else
        {
            spawnPosition = Random.Range(0, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count);
            
        }

        
        playerSpawnRoom = SpawnPlayer();
        
        if(GameManager.Instance.GetEnemy() != null)
            SpawnEnemy();

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
        deadBodys[spawnPosition].SetActive(true);
        currentBodysSpawned++;

        SpawnBodys(maxDeadBodysMap - 1, gameObject);
        m_ScoreManager.SetRemainingCorpses(maxDeadBodysMap);


    }

    public void ClearBodys(int spawnPosition)
    {
        if(spawnPosition >= 0)
        {
            if(deadBodys[spawnPosition].activeSelf)
            {
                deadBodys[spawnPosition].SetActive(false);
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
        if(GM.m_GamesPlayed == 1)
        {
            playerSpawnRoom = 2;
        }
        else
        {
            playerSpawnRoom = Random.Range(1, 5);
        }

        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == playerSpawnRoom - 1)
            {
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().m_CharacterController.enabled = false;
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().transform.position = roomsController.rooms[i].transform.position;
                GameManager.Instance.GetPlayer().transform.rotation = roomsController.rooms[i].transform.localRotation;
                GameManager.Instance.GetPlayer().GetComponent<PlayerMovement>().m_CharacterController.enabled = true;
            }
        }  
        return playerSpawnRoom;
    }


    public void SpawnEnemy()
    {

        enemySpawnRoom = 5 - playerSpawnRoom;

        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == enemySpawnRoom - 1)
            {
                
                GameManager.Instance.GetEnemy().GetComponent<NavMeshAgent>().Warp(roomsController.rooms[i].transform.position);
               
            }
        }
    }

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

                    spawnPosition = Random.Range(0, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count);

                    for (int j = 0; j < spawnersUsed.Count; j++)
                    {
                        if(spawnPosition == spawnersUsed[j])
                            canSpawn = false;
                    }

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
                if (!firstTime)
                {
                    SoundManager.Instance.PlayEvent(corpseApeearEvent, deadBodys[spawnPosition].transform);
                }
                Invoke("FirstTimeFalse", 2);
                
                currentBodysSpawned++;
            }
        }

        
        

    }
    public void FirstTimeFalse()
    {
        firstTime = false;
    }
}
