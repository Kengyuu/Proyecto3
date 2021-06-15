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
    private HudController M_HudController;

    public bool killed = false;

    public enum State { INITIAL, NORMALBEHAVIOUR, RETURNINGTOENEMY,DEAD };
    public State currentState;

    void Start()
    {
        blackboard = GetComponent<Orb_Blackboard>();
        if (M_HudController == null) M_HudController = GameObject.FindGameObjectWithTag("HUDManager").GetComponent<HudController>();
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
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER)
                {
                    ChangeState(State.DEAD);
                }
                break;

            case State.RETURNINGTOENEMY:
                killed = false;
                ReEnter();
                if (GameManager.Instance.gameState == GameState.WIN || GameManager.Instance.gameState == GameState.GAME_OVER)
                {
                    ChangeState(State.DEAD);
                }

                break;


        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.NORMALBEHAVIOUR:
                corpseSearch.Exit();    
                killed = true;    
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.NORMALBEHAVIOUR:
                corpseSearch.ReEnter();
                killed = false;
                break;

            case State.RETURNINGTOENEMY:
                if (blackboard.orbCorpseStored != null)
                {
                    blackboard.orbCorpseStored = null;
                    GameManager.Instance.GetGameObjectSpawner().SpawnBodys(1, gameObject);
                }
                Spawn();

                break;
            case State.DEAD:
                blackboard.navMesh.isStopped = true;
                corpseSearch.Exit();
                break;

        }

        currentState = newState;

    }

    void Spawn()
    {
        corpseSearch.m_Laser.enabled = false;
        blackboard.SetOrbHealth(blackboard.m_maxLife);
        OrbEvents.current.RespawnOrbs(gameObject);
        blackboard.navMesh.isStopped = false;
        corpseSearch.enabled = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" && currentState == State.NORMALBEHAVIOUR)
        {
            corpseSearch.target = blackboard.behaviours.PickRandomWaypointOrb();
        }

    }

}