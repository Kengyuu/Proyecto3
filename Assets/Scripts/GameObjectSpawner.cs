using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    public GameObject deadBody;
    public GameObject totem;
    public GameObject player;
    public GameObject enemy;

    public GameObject deadBodyContainer;
    public RoomsController roomsController;
    public int maxDeadBodysMap = 20; 
    int currentBodysPlaced = 0;

    int playerSpawnRoom = 5;
    int enemySpawnRoom = 5;
    int totemSpawnRoom;

    List<int> spawnersUsed = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        while (playerSpawnRoom == 5)
        {
            playerSpawnRoom = Random.Range(1, 10);
        }
        enemySpawnRoom = 10 - playerSpawnRoom;
        totemSpawnRoom = playerSpawnRoom;
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

        while(totemSpawnRoom == playerSpawnRoom || totemSpawnRoom == enemySpawnRoom)
        {
            totemSpawnRoom = Random.Range(1, 10);
        }

        Instantiate(totem, roomsController.rooms[totemSpawnRoom-1].transform.position, Quaternion.identity);
        int maxDeadBodysRoom = Random.Range(1, 4);

        for (int i = 0; i < maxDeadBodysMap; i++)
        {
            spawnersUsed.Add(Random.Range(1, deadBodyContainer.GetComponent<RoomSpawner>().spawners.Count + 1 ));
        }
        for (int i = 0; i < spawnersUsed.Count; i++)
        {
            Instantiate(deadBody, deadBodyContainer.GetComponent<RoomSpawner>().spawners[spawnersUsed[i]].transform.position, Quaternion.identity);
        }

        /*for (int i = 0; i < roomsController.rooms.Count; i++)
        {
            


        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
