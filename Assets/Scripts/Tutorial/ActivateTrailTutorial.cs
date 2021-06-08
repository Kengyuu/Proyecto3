using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrailTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hiderTrail;
    public GameObject attackTrail;
    public GameObject corpseTrail;
    public GameObject trapTrail;

    public HudController hudController;
    public GameObject enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
