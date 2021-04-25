using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BLACKBOARD : MonoBehaviour
{

    [Header("WanderCorpse")]
    public float corpsePickUpRadius = 10f;
    public float corpseDetectionRadius = 10f;
    public float playerDetectionRadius = 10f;
    public float cooldownToGrabCorpse = 3f;
    public GameObject waypointsList;
    public GameObject lastCorpseSeen;
    

    [Header("SeekPlayer")]
    public GameObject Player;
    public float distanceToAttack = 5f;

    public float senseRadius = 10f;
    public int enemyCorpses;
    public int playerCorpses;
    public int remainingCorpses;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        waypointsList = GameObject.FindGameObjectWithTag("SpawnersContainer");
    }
}
