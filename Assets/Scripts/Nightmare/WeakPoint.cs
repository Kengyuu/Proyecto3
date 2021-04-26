using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]public int spawnPosition;
    public EnemyWeakPointsController enemyWeakPointsController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        enemyWeakPointsController.TakeDamage(gameObject);
        gameObject.SetActive(false);
    }
}
