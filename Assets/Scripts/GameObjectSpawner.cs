using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject deadBody;
    public GameObject player;
    public GameObject enemy;
    public GameObject deadBodyContainer;
    public RoomsController roomsController;
    public int maxDeadBodysMap = 20; 
    public int maxDeadBodyRoom = 2;

    int playerSpawnRoom = 5;
    int enemySpawnRoom = 5;
    List <string> roomTags = new List<string>();

    List<int> spawnersUsed = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject room in roomsController.rooms)
        {
            roomTags.Add(room.tag);
        }
        while (playerSpawnRoom == 5)
        {
            playerSpawnRoom = Random.Range(1, 10);
        }
        enemySpawnRoom = 10 - playerSpawnRoom;
        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == playerSpawnRoom - 1)
            {
                Instantiate(player, roomsController.rooms[i].transform.position, Quaternion.identity);
            }
        }
        for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            if(i == enemySpawnRoom - 1)
            {
                Instantiate(enemy, roomsController.rooms[i].transform.position, Quaternion.identity);
            }
        }

        int spawnPosition = Random.Range(0, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count);
        for (int i = 0; i < roomTags.Count; i++)
        {
            if(deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnPosition].tag == roomTags[i])
            {
                roomsController.currentSpawnersUsed[i]++;
            }
        }
        spawnersUsed.Add(spawnPosition);

        for (int i = 0; i < maxDeadBodysMap; i++)
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

                
                /*if(canSpawn && roomsController.currentSpawnersUsed[i] < maxDeadBodyRoom/*&& deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnPosition].tag == roomTag)
                {
                    roomsController.currentSpawnersUsed[i]++;
                }*/

                /*switch(deadBodyContainer.GetComponent<RoomSpawner>().spawners[i].tag)
                {
                    case "Room0":
                        if(roomsController.currentSpawnersUsed[i] >= maxDeadBodyRoom)
                        {
                            canSpawn = false;
                        }
                        else
                        {
                            roomsController.currentSpawnersUsed[i]++;
                        }
                        break;
                    case "Room1":
                        break;
                    case "Room2":
                        break;
                    case "Room3":
                        break;
                    case "Room4":
                        break;
                    case "Room5":
                        break;
                    case "Room6":
                        break;
                    case "Room7":
                        break;
                    case "Room8":
                        break;

                }*/

                if(canSpawn)
                    spawnable = true;

            }
            
            spawnersUsed.Add(spawnPosition);
            Instantiate(deadBody, deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnersUsed[i]].transform.position, Quaternion.identity, deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnersUsed[i]].transform);
        }

        foreach (int bodys in roomsController.currentSpawnersUsed)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
