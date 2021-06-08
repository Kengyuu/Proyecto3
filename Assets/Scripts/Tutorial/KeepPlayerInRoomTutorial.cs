using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerInRoomTutorial : MonoBehaviour
{
    // Start is called before the first frame update
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
        {
            col.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }
}
