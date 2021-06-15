using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayerInRoomTutorial : MonoBehaviour
{
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
