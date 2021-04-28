using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseHider : EnemyOrbController
{
    // Start is called before the first frame update

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    private Enemy_BLACKBOARD blackboard;

    EnemyBehaviours behaviours;
    string enemyType;
    GameObject trap;

    
    //float closeEnoughTarget;

   
 
    public enum State {INITIAL, WANDERING, RETURNINGTOENEMY};
    public State currentState;

    

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        behaviours = GetComponent<EnemyBehaviours>();
        enemyType = transform.tag;
        SetOrbHealth(3);
        //target = behaviours.PickRandomWaypoint();
        //enemy.SetDestination(target.transform.position);
    }

    public void Exit()
    {
        enemy.isStopped = false;
        this.enabled = false;
    }

    public void ReEnter()
    {
        this.enabled = true;
        currentState = State.INITIAL;
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.WANDERING);
                break;

            case State.WANDERING:

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance)
                {
                    ChangeState(State.WANDERING); 
                }

                //behaviours.CreateAreaInvisibility();
                break;
        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.WANDERING:
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                enemy.isStopped = false;
                target = behaviours.PickRandomWaypoint();
                break;

        }

        currentState = newState;
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Corpse")
        {
            behaviours.CreateAreaInvisibility(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Corpse")
        {
            behaviours.ReturnCorpseToNormal(col.gameObject);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(GetOrbHealth() <= 0)
        {
            enemy.Warp(GameManager.Instance.GetEnemy().transform.position);
            ChangeState(State.INITIAL);
        }
    }
}
