using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseOrbTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void TriggerAttack()
    {
        if (attacking)
        {
            foreach (Transform raycastPoint in rayPoints)
            {
                Vector3 Direction = raycastPoint.position - castPosition.position;
                Direction.Normalize();
                Ray Ray = new Ray(castPosition.position, Direction);
                //Debug.DrawRay(castPosition.position, Direction * blackboard.maxAttackDistance, Color.red);
                RaycastHit l_RaycastHit;

                if (Physics.Raycast(Ray, out l_RaycastHit, blackboard.maxAttackDistance,mask))
                {
                    if (l_RaycastHit.collider.tag == "Player")
                    {
                        GameManager.Instance.GetPlayer().GetComponent<PlayerController>().TakeDamage(1, gameObject, blackboard.XForceImpulseDamage,
                                                                                                     blackboard.YForceImpulseDamage);
                        
                        attacking = false;

                    }

                }
            }

            m_Laser.SetPosition(1, new Vector3(0.0f, 0.0f, blackboard.maxAttackDistance));


        }
        ChangeState(State.ATTACKINGPLAYER);

    }*/

    public void ArsorbCorpse()
    {
        target.GetComponent<CorpseAbsorbTutorial>().AbsorbOrbParticles(gameObject);
    }
}
