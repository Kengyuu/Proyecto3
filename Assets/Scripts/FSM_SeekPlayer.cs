using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_SeekPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent enemy;
    private Enemy_BLACKBOARD blackboard;
    GameObject Player;
    public Vector3 lastPlayerPosition;
    Transform child;
    GameObject Arm;
    public enum State { INITIAL, SEEKINGPLAYER, GOTOLASTPLAYERPOSITION, ATTACKING};
    public State currentState;



    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        blackboard = GetComponent<Enemy_BLACKBOARD>();
        child = gameObject.transform.GetChild(2);
        Arm = child.gameObject;
    }

    public void Exit()
    {



    }

    public void ReEnter()
    {
        currentState = State.INITIAL;

    }

    // Update is called once per frame
    void Update()
    {
        

        switch (currentState)
        {
            case State.INITIAL:
                ChangeState(State.SEEKINGPLAYER);
                break;

            case State.SEEKINGPLAYER:
                enemy.SetDestination(Player.transform.position);

                if (DetectionFunctions.DistanceToTarget(gameObject,Player) <= blackboard.distanceToAttack)
                {
                    ChangeState(State.ATTACKING);
                    break;
                }

                if (DetectionFunctions.DistanceToTarget(gameObject, Player)>= blackboard.playerDetectionRadius)
                {
                    ChangeState(State.GOTOLASTPLAYERPOSITION);
                }
                break;
            case State.GOTOLASTPLAYERPOSITION:

                if (enemy.remainingDistance < 0.5f)
                {
                    GetComponent<FSM_CorpseWander>().enabled = true;
                    GetComponent<FSM_CorpseWander>().ReEnter();
                    this.enabled = false;
                }

                break;
            case State.ATTACKING:

                transform.LookAt(Player.transform,transform.up);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                
                if (DetectionFunctions.DistanceToTarget(gameObject, Player) > blackboard.distanceToAttack)
                {
                    ChangeState(State.SEEKINGPLAYER);
                    break;
                }

                break;


        }


    }

    void ChangeState(State newState)
    {

        //EXIT LOGIC
        switch (currentState)
        {
            case State.SEEKINGPLAYER:


                break;
            case State.GOTOLASTPLAYERPOSITION:


                break;
            case State.ATTACKING:
                enemy.isStopped = false;
                Arm.SetActive(false);
                break;
        }

        // Enter logic
        switch (newState)
        {

            case State.SEEKINGPLAYER:
                enemy.SetDestination(Player.transform.position);

                break;
            case State.GOTOLASTPLAYERPOSITION:
                lastPlayerPosition = Player.transform.position;
                enemy.SetDestination(lastPlayerPosition);
                break;
            case State.ATTACKING:
                enemy.isStopped = true;
                Arm.SetActive(true);
                break;

        }

        currentState = newState;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            //other.gameObject.GetComponent<PlayerController>().GetDamage(0);
        }
    }
}
