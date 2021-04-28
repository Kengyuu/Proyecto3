using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_CorpseSearcher : EnemyOrbController
{
    // Start is called before the first frame update

    [Header("AI")]
    NavMeshAgent enemy;
    public GameObject target;
    private Enemy_BLACKBOARD blackboard;

    EnemyBehaviours behaviours;
    string enemyType;
    GameObject corpse;

    
    //float closeEnoughTarget;

   
 
    public enum State {INITIAL, WANDERING, GOINGTOCORPSE, RETURNINGTOENEMY, GRABBINGCORPSE};
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
                corpse = behaviours.SearchObject("Corpse");
                //Debug.Log(corpse.name);
                if(corpse != null)
                {
                    ChangeState(State.GOINGTOCORPSE);
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance)
                {
                    ChangeState(State.WANDERING); 
                }
                break;
            case State.GOINGTOCORPSE:
                if(target.tag != "Corpse")
                {
                    ChangeState(State.WANDERING);
                }
                else
                {
                    if (DetectionFunctions.DistanceToTarget(gameObject, target) <= blackboard.closeEnoughCorpseRadius)
                    {
                        ChangeState(State.GRABBINGCORPSE);
                    }
                }
                
                break;

            case State.GRABBINGCORPSE:
                blackboard.cooldownToGrabCorpse -= Time.deltaTime;
                if (blackboard.cooldownToGrabCorpse <= 0)
                {
                    behaviours.GrabCorpse(target);
                    ChangeState(State.RETURNINGTOENEMY);
                    break;
                }
                break;
            case State.RETURNINGTOENEMY:
                target = behaviours.ReturnToEnemy();
                if(DetectionFunctions.DistanceToTarget(gameObject, target) <= enemy.stoppingDistance + 1)
                {
                    ChangeState(State.WANDERING);
                }
                break;
        }
    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.WANDERING:
                blackboard.lastCorpseSeen = null;
                break;
            case State.GRABBINGCORPSE:
                target.tag = "Corpse";
                blackboard.orbCorpseStored = corpse;
                enemy.isStopped = false;
                break;
            case State.RETURNINGTOENEMY:
                if(corpse != null)
                {
                    behaviours.AddCorpseToScore();
                    corpse = null;
                }
                
                blackboard.orbCorpseStored = null;
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.WANDERING:
                enemy.isStopped = false;
                target = behaviours.PickRandomWaypointOrb();
                break;
            case State.GOINGTOCORPSE:
                target = corpse;
                enemy.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
                break;

            case State.GRABBINGCORPSE:
                enemy.isStopped = true;
                blackboard.cooldownToGrabCorpse = 3f;
                target.tag = "PickedCorpse";
                break;

            case State.RETURNINGTOENEMY:
                blackboard.lastCorpseSeen = corpse;
                break;

        }

        currentState = newState;
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(GetOrbHealth() <= 0)
        {
            if(blackboard.orbCorpseStored != null)
            {
                GameManager.Instance.m_gameObjectSpawner.SpawnBodys(1);
            }
            enemy.Warp(GameManager.Instance.GetEnemy().transform.position);
            SetOrbHealth(maxOrbHealth);
            corpse = null;
            ChangeState(State.INITIAL);
        }
        
    }
    void OnDrawGizmos()
    {
        if(!Application.isPlaying)
            return ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blackboard.corpseDetectionRadius);
        if(corpse != null)
            Gizmos.DrawLine(transform.position, corpse.transform.position);
    }
}
