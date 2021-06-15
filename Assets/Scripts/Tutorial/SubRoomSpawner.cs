using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoomSpawner : MonoBehaviour
{
    public GameObject subRoom;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
            subRoom.SetActive(true);
    }
}
