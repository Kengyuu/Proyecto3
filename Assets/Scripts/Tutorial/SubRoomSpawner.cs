using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoomSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject subRoom;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
            subRoom.SetActive(true);
    }
}
