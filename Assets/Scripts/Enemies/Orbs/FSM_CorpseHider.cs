using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseHider : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    // private Enemy_BLACKBOARD blackboard;

    EnemyBehaviours behaviours;

    GameObject trap;


    float closeEnoughTarget;

    public Orb_Blackboard blackboard;

    public enum State { INITIAL, WANDERING, RETURNINGTOENEMY };
    public State currentState;



    void OnEnable()
    {
        enemy = GetComponent<NavMeshAgent>();

         behaviours = GetComponent<EnemyBehaviours>();
        blackboard = GetComponent<Orb_Blackboard>();
        blackboard.SetOrbHealth(3);
        ReEnter();
      
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

               // behaviours.CreateAreaInvisibility();
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
                target = behaviours.PickRandomWaypointOrb();
                
                break;

        }

        currentState = newState;

    }

   /* void OnTriggerEnter(Collider col)
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
    }*/


}
