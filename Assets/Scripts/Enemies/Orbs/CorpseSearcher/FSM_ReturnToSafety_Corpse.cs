using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_ReturnToSafety_Corpse : MonoBehaviour
{
    public GameObject target;
    FSM_CorpseSearcher corpseSearch;
    Orb_Blackboard blackboard;
    ScoreManager m_ScoreManager;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY };
    public State currentState;

    void Start()
    {
        blackboard = GetComponent<Orb_Blackboard>();
        //blackboard.navMesh = GetComponent<NavMeshAgent>();
        
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        m_ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        corpseSearch = GetComponent<FSM_CorpseSearcher>();


    }

    public void Exit()
    {
        blackboard.navMesh.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;

    }

    
    void Update()
    {

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.NORMALBEHAVIOUR);
                break;

            case State.NORMALBEHAVIOUR:
                if (blackboard.GetOrbHealth() <= 0)
                {
                    ChangeState(State.RETURNINGTOENEMY);
                }
                break;

            case State.RETURNINGTOENEMY:
                ReEnter();      
                break;


        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.NORMALBEHAVIOUR:
                corpseSearch.enabled = false;          
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.NORMALBEHAVIOUR:
                corpseSearch.enabled = true;
                break;

            case State.RETURNINGTOENEMY:
                
                Debug.Log("RESPAWN");
                if (blackboard.orbCorpseStored != null)
                {
                    GameManager.Instance.GetGameObjectSpawner().SpawnBodys(1, gameObject);
                    
                }     
                Spawn();
                gameObject.SetActive(false);

                break;

        }

        currentState = newState;

    }

    void Spawn()
    {
        OrbEvents.current.StartCoroutine(OrbEvents.current.RespawnOrbs(gameObject));
        blackboard.navMesh.isStopped = false;
        corpseSearch.m_Laser.enabled = false;
        
        corpseSearch.enabled = true;
    }

    

   
}