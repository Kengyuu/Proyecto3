using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AttackerOrb : MonoBehaviour
{
    public List<Transform> rayPoints;
    public Transform castPosition;
    public LineRenderer m_Laser;
    public Animator anim;

    public Orb_Blackboard blackboard;

    bool attacking = false;
    private void Start()
    {

        
    }

    private void Update()
    {
       if (DetectionFunctions.DistanceToTarget(gameObject, GameManager.Instance.GetPlayer()) <= blackboard.maxAttackDistance)
        {
            anim.SetBool("AttackOrb", true);
            if (!attacking) Rotate();
            

            TriggerAttack();
           
            
        }

        

    }

    void TriggerAttack()
    {
        if (attacking)
        {
            foreach (Transform raycastPoint in rayPoints)
            {
                Vector3 Direction = raycastPoint.position - castPosition.position;
                Direction.Normalize();
                Ray Ray = new Ray(castPosition.position, Direction);
                Debug.DrawRay(castPosition.position, Direction * blackboard.maxAttackDistance, Color.red);
                RaycastHit l_RaycastHit;

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance))
                {
                    Debug.Log(l_RaycastHit.collider.tag);
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        Debug.Log("Hit by orb");
                        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(1, gameObject, blackboard.XForceImpulseDamage, blackboard.YForceImpulseDamage);
                        attacking = false;
                        
                    }

                }
            }
            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, blackboard.maxAttackDistance));
        }
        
    }

    void Rotate()
    {
        Vector3 direction = GameManager.Instance.GetPlayer().transform.position - transform.position;

        if (direction == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,10 * Time.deltaTime);
    }



    void setAttackTrue()
    {
        attacking = true;
        m_Laser.enabled = true;
    }

    void setAttackFalse()
    {
        attacking = false;
        anim.SetBool("AttackOrb", false);
        m_Laser.enabled = false;
    }

    



}
