using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy_BLACKBOARD : MonoBehaviour
{

    [Header("WanderCorpse")]
    public float corpsePickUpRadius = 10f;
    public float corpseDetectionRadius = 10f;

    public float closeEnoughCorpseRadius = 2f;
    public float playerDetectionRadius = 10f;
    public float cooldownToGrabCorpse = 10f;

    public float angleDetectionPlayer = 30f;

    public GameObject waypointsList;
    public GameObject lastCorpseSeen;

    public GameObject corpse;
    
    public GameObject absorbObjective;
    

    [Header("SeekPlayer")]
    public GameObject Player;
    public GameObject eyesPosition;
    public float distanceToAttack = 0.5f;
    public float waypointsNearPlayerRadius = 20f;
    public P_AnimatorController animatorController;

    public MultiAimConstraint head;

    [Header("GameState")]
    public int enemyCorpses;
    public int playerCorpses;
    public int remainingCorpses;

    public float invokeTime = 4f; 
    public float provokeTime = 4f;
    public float stunTime = 2.5f;

    private GameManager GM;

    void Start()
    {
        GM = GameManager.Instance;
        Player = GM.GetPlayer();
        waypointsList = GM.GetWaypointsList().gameObject;

    }
}
