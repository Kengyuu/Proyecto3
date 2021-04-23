using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BLACKBOARD : MonoBehaviour
{

    [Header("WanderCorpse")]
    public float corpsePickUpRadius = 2f;
    public float corpseDetectionRadius = 10f;
    public float playerDetectionRadius = 10f;

    [Header("SeekPlayer")]
    public GameObject Player;
    public float distanceToAttack = 5f;
   

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
