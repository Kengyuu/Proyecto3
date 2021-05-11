using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BLACKBOARD : MonoBehaviour
{

    [Header("WanderCorpse")]
    public float corpsePickUpRadius = 10f;
    public float corpseDetectionRadius = 10f;

    public float closeEnoughCorpseRadius = 2f;
    public float playerDetectionRadius = 10f;
    public float cooldownToGrabCorpse = 3f;

    public float angleDetectionPlayer = 30f;

    //public float detectionSensingRadius = 10f;
    public GameObject waypointsList;
    public GameObject lastCorpseSeen;

    public GameObject corpse;
    

    [Header("SeekPlayer")]
    public GameObject Player;
    public float distanceToAttack = 0.5f;
    public float waypointsNearPlayerRadius = 20f;
    //public float senseRadius = 10f;

    [Header("GameState")]
    public int enemyCorpses;
    public int playerCorpses;
    public int remainingCorpses;

    public float stunTime = 2.5f;

    private GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        Player = GM.GetPlayer();
        waypointsList = GM.GetWaypointsList().gameObject;

    }
}
