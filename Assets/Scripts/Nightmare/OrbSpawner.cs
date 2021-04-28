using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrbSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    public event ScoreChanged onScoreChangedEvent;

    void Start()
    {
        /*onScoreChangedEvent += SpawnOrbs;
        scoreChanged.scoreChangedDelegate.AddListener()*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnOrbs()
    {
        
    }
}
