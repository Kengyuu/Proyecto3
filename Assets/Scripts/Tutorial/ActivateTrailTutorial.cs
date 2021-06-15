using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrailTutorial : MonoBehaviour
{
    public GameObject hiderTrail;
    public GameObject attackTrail;
    public GameObject corpseTrail;
    public GameObject trapTrail;

    public HudController hudController;

    public GameObject promptStartRoomTwo;
    public GameObject enemy;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            hiderTrail.SetActive(true);
            attackTrail.SetActive(true);
            corpseTrail.SetActive(true);
            trapTrail.SetActive(true);
            for(int i = 0; i < 6; i++)
            {
                enemy.GetComponent<EnemyBehaviours>().AddCorpseToScore();
                hudController.UpdateAddCorpses(enemy);
            }
            
        }
        promptStartRoomTwo.SetActive(false);
        gameObject.SetActive(false);
    }
}
