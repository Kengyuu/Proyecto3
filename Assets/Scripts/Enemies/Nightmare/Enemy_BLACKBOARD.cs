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
    public GameObject waypointsList;
    public GameObject lastCorpseSeen;

    public GameObject orbCorpseStored;
    public GameObject corpse;
    

    [Header("SeekPlayer")]
    public GameObject Player;
    public float distanceToAttack = 1.5f;

    public float senseRadius = 10f;

    [Header("TrapDeactivate")]

    public float cooldownToDeactivateTrap = 3f;
    public GameObject trap;

    public float trapDetectionRadius = 10f;

    public float closeEnoughTrapRadius = 2f;

    [Header("CorpseHide")]

    public float areaOfEffectInvisible = 10f;

    [Header("GameState")]
    public int enemyCorpses;
    public int playerCorpses;
    public int remainingCorpses;

    public float stunTime = 3f;

    private GameManager GM;


    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        Player = GM.GetPlayer();
        waypointsList = GM.GetWaypointsList().gameObject;

    }
}
